using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context) //contractor'da içi dolduruldu.
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
            //    _context.Products.Add(new Product () { Id = 1, Name = "Kalem 1", Price = 100, Stock = 100 , Barcode = "Kalem" , Width = 1 ,Height = 1});
            //    _context.Products.Add(new Product () { Id = 2, Name = "Kalem 2", Price = 200, Stock = 200 , Barcode = "Kalem", Width = 1, Height = 1 });
            //    _context.Products.Add(new Product () { Id = 3, Name = "Kalem 3", Price = 300, Stock = 300 , Barcode = "Kalem", Width = 1, Height = 1 });
            //    /*_context.SaveChanges();*/ //SaveChanges ile verileri veri tabanına ekler.
            //}


        }
        public IActionResult Index()
        {
            //var products = _productRepository.GetAll(); //GetAll metoduyla beraber _productRepository'dan tüm dataları products'a alındı.

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
        public IActionResult Add(string Name , decimal Price , int Stock , string Barcode , int Widht , int Height) //Model Binding : Kullanıcı buttona bastığında çalışır.
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
            Product newProduct = new Product() { Name=Name , Price = Price , Stock= Stock , Barcode= Barcode , Width = Widht , Height = Height}; //Product nesnesi oluşturuldu.
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToActionPermanent("Index");
        }
         
        public IActionResult Update(int id) //Gizli Http tipi Get isteğidir.Formlar ile çalışıldığında host istekleri olacaktır.
        {
            return View();
        }
    }
}
