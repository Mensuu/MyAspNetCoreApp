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
            //Singleton olsayd� veri taban�yla ba�lant� koparsa bir daha ba�lanmaz fakat Scope tekrardan gider ba�n�r.
            {
                //Hangi veritaban�na ba�lan�ca�� belirtildi.
                //DbContext.products
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
            });

            //builder.Services.AddSingleton<IHelper,Helper>(); 
            //Interface ile somut nesne �retilmesi gerekti�i s�ylendi.
            //Dependency Injection(DI)/Singleton = Uyguluma ya�ad��� s�re boyunca ayakta kal�r.Response'a u�ramaz
            //Sadece tek bir nesne �retir yani �retilen b�t�n nesneler e�ittir.

            //builder.Services.AddScoped<IHelper,Helper>();
            //Dependency Injection(DI)/Scoped = Veri taban� gibi i�lemlerde kullan�l�r.
            //Request response'a d�n���nceye kadar ayakta kal�r ve sonra memory'den gider.

            //builder.Services.AddScoped<Helper>();
            //�ok tercih edilmese de direk somut nesne olarakta tan�mlanabilir.HomeController'a yaz�ld�.
            builder.Services.AddTransient<IHelper, Helper>();
            //builder.Services.AddTransient<IHelper, Helper>(sp =>
            //{
            //    return new Helper(true);
            //}); 
            //Delegeler metotlar� in�a eder.
            //Metot i�ine yaz�lan Helper sayfas�ndaki bool kaynakl� hata i�in program ne d�nmesi gerekti�ini karar veremiyor.
            //Dependency Injection(DI)/Transient = Her durumda s�n�fa gider ve yeni bir nesne �retir.
            //B�t�n nesneleri e�it de�ildir.

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