using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Controllers.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private IHelper _helper;
        private readonly ProductRepository _productRepository; 
        public ProductsController(AppDbContext context, IHelper helper) //contractor'da içi dolduruldu. 
            //IHelper helper yazılması Constractor Injection olarak adlandırılır.
        { 
            //DI Container : Classların alınabilmesine imkan veririr

            //Dependency Injection Pattern :
            //Bağımlıkların yönetilmesini sağlayan ve çözüm üreten bir dizayn pattern'dır.
            //Constactor ve metotda bir ihtiyaç duyulan dataları veye classları alınabilir.


            _productRepository = new ProductRepository(); //Nesne örneği oluşturuldu.


            //Aşağısı silme işlemi için kaldırıldı çünkü if döngüsü tüm hiçbir data yoksa aşağıdaki dataları ekliyor.ProductRepository'e eklendi.
            //if (!_productRepository.GetAll().Any())
            ////Eğer hiçbir veri yoksa eklemek için if döngüsü kullanıldı.
            ////Any ifadesi data varsa true döner ! işareti ise olumsuz demektir yani ikisi beraber kullanılırsa hiçbir veri yoksa anlamına gelir.
            ////Eğer varsa eklemeyecektir bu şekilde.
            //{
            //    _productRepository.Add(new Product { Id = 1, Name = "Kalem 1", Price = 100, Stock = 100 }); //Örnek data eklendi.Bu datalar ram(memory)'de duruyor herhangi bir veritabanına kaydedilmedi.
            //    _productRepository.Add(new Product { Id = 2, Name = "Kalem 2", Price = 200, Stock = 200 });
            //    _productRepository.Add(new Product { Id = 3, Name = "Kalem 3", Price = 300, Stock = 300 });
            //}


            _context= context; //Nesne örneği üretilmiş bir DbContext sınıfı.

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 100, Barcode = "Kalem", Width = 1, Height = 1 });
            //    _context.Products.Add(new Product() { Name = "Kalem 2", Price = 200, Stock = 200, Barcode = "Kalem", Width = 1, Height = 1 });
            //    _context.Products.Add(new Product() { Name = "Kalem 3", Price = 300, Stock = 300, Barcode = "Kalem", Width = 1, Height = 1 });
            //    _context.SaveChanges();
            //    //SaveChanges ile verileri veri tabanına ekler.Id identity olduğundan kaynaklı id yazıldığında hata alacaktır.
            //}

            _helper= helper;

        }
        public IActionResult Index([FromServices]IHelper helper2)
            //IHelper helper2 burada Method Injection olarak kullanılır.
            //FromServices attribute'ü IHelper helper2'nin DI Container'den geliceğini belirtir.Kullanıcıdan alınmaz.
        {
            //var products = _productRepository.GetAll(); //GetAll metoduyla beraber _productRepository'dan tüm dataları products'a alındı.

            var text = "Asp.Net";
            //IHelper helper = new Helper();
            //Bu kullanım doğru değil çünkü Interface kullanılmış olsada somut nesne verildi ve new kullanıldı.
            //Amaç DI ile bunlardan kurtulmaktır.
            //Uygulamanın genelini ilgilendiren tüm sınıfların dışarından birer constractor olarak gelmesini sağlamaktır. 
            //Şuan uygulama herhangi bir class'ın constractor'ında bu interface ile karşılaştığında hangi sınıftan nesne örneğini üreteceğini
            //ve ayni zamanda da yaşam döngüsünün nasıl olacağını bilmiyor.Bu yüzden program.cs tarafına nesne sınıfı üretilmesi gerektiği söylenmelidir.
            var upperText = _helper.Upper(text);
            var status = _helper.Equals(helper2);


            var products = _context.Products.ToList();
            return View(products); //View tarafına aktarmak için ViewModel yardımıyla products aktarıldı.
        }

        public IActionResult Remove(int id) //products id'si alındı.
        {
            //_productRepository.Remove(id);
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet] //Sayftada gösterilmek istenildiğinde kullanılır.
        public IActionResult Add() //Gizli Http tipi Get isteğidir.Formlar ile çalışıldığında host istekleri olacaktır. 
        {
           return View();
        }
        [HttpPost] //Kaydetmek için kullanılır.Genellikle bir sayfası olmaz.
        public IActionResult Add(Product newProduct) //Model Binding : Kullanıcı buttona bastığında çalışır.
        {
            //Request Header - Body
            //Get metofunda veriler Url'de taşınır fakat bu çok sağlıklı bir yöntem değildir.Post tercih edilir.


            // 1.Yöntem:
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var barcode = HttpContext.Request.Form["Barcode"].ToString();
            //var widht = int.Parse(HttpContext.Request.Form["Widht"].ToString());
            //var height = int.Parse(HttpContext.Request.Form["Height"].ToString());

            //2.Yöntem
            //Product newProduct = new() { Name = Name, Price = Price, Stock = Stock, Barcode = Barcode, Width = Widht, Height = Height }; //Product nesnesi oluşturuldu.
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla eklendi.";
            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public IActionResult Update(int id) //Gizli Http tipi Get isteğidir.Formlar ile çalışıldığında host istekleri olacaktır.
        {
            var product = _context.Products.Find(id);
            return View();
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct ,/* int productId ,*/ string type)
        //Product updateProduct bir class olduğu için buna complex type denilir.Bu direk olarak request'in body kısmında bekler.
        //int product basit bir tiptir.Default olarak request'inn query string kısmında bekler.(Url)
        //Bir kısmını query string bir kısmı body yapılacaktır.

        {
            //updateProduct.Id= productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToActionPermanent("Index");
        }

    }
}

