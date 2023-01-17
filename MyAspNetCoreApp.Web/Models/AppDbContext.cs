using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp.Web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)    
        {
            //Contractor oluşturuldu ve DdContextOptions isiminde bir sınıf aldı.
            //Hangi databease kullanılacağını belirtmek için generic olarak AppDbContex olarak verildi.
            //Bir option belirtildi ve bu options base(miras aldığı DbContext)'deki optionca gidicektir.
            //Bu kullanılan option program.cs dosyasında doldurulacaktır.
            //Bir Db oluşturulduktan sonra entityler verilmek zorundadır yoksa şuan boş haldedir.
        }
        public DbSet<Product> Products { get; set; } 
        //Product sınıfı generic olarak enitity olarak oluşturuldu.Eğer isimini veritabanınkinden farklı verilirse attribute verelirmesi gerekir.
        //Products.cs içerisine [Table("Products")] yazılmalıdır. 
    }
   
}
