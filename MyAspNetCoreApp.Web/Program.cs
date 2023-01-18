using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyAspNetCoreApp.Web.Controllers.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => //DbContext Default olarak Scoped'tur.
            //Singleton olsaydý veri tabanýyla baðlantý koparsa bir daha baðlanmaz fakat Scope tekrardan gider baðnýr.
            {
                //Hangi veritabanýna baðlanýcaðý belirtildi.
                //DbContext.products
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
            });

            //builder.Services.AddSingleton<IHelper,Helper>(); 
            //Interface ile somut nesne üretilmesi gerektiði söylendi.
            //Dependency Injection(DI)/Singleton = Uyguluma yaþadýðý süre boyunca ayakta kalýr.Response'a uðramaz
            //Sadece tek bir nesne üretir yani üretilen bütün nesneler eþittir.

            //builder.Services.AddScoped<IHelper,Helper>();
            //Dependency Injection(DI)/Scoped = Veri tabaný gibi iþlemlerde kullanýlýr.
            //Request response'a dönüþünceye kadar ayakta kalýr ve sonra memory'den gider.

            //builder.Services.AddScoped<Helper>();
            //Çok tercih edilmese de direk somut nesne olarakta tanýmlanabilir.HomeController'a yazýldý.
            builder.Services.AddTransient<IHelper, Helper>();
            //builder.Services.AddTransient<IHelper, Helper>(sp =>
            //{
            //    return new Helper(true);
            //}); 
            //Delegeler metotlarý inþa eder.
            //Metot içine yazýlan Helper sayfasýndaki bool kaynaklý hata için program ne dönmesi gerektiðini karar veremiyor.
            //Dependency Injection(DI)/Transient = Her durumda sýnýfa gider ve yeni bir nesne üretir.
            //Bütün nesneleri eþit deðildir.

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
} 