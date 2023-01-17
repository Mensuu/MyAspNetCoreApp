using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class Product2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class OrnekController : Controller
    { 
        public IActionResult Index()
        {
            // --ViewBag ve ViewData--
            //Bu iki veri taşıma yöntemi aynı action metot içerisine taşınmasını sağlar.
            //ViewBag.name = "Asp.Net Core";
            //ViewData["Age"] = 20;
            //ViewData["Names"] = new List<string>() { "Menekşe", "Mensu", "Melike" };
            //ViewBag.person = new { Id = 1, Name = "Menekşe", Age = 24 };

            // --ViewBag ve TempData--
            //TempData ile farklı action metot içerisine taşınmasını sağlar.
            //ViewBag.name = "Menekşe";
            //TempData["surname"] = "Çorum";

            // --ViewModel--
            //Hacimli dataların taşınmasını sağlar.
            var productList = new List<Product2>() 
            { 
                new() {Id = 1 , Name = "Menekşe"},
                new() {Id = 2 , Name = "Melike"}
            };


            return View(productList);
        }
        public IActionResult Index2()
        {
            var surName = TempData["surname"]; // TempData ile farklı bir metotda değer ataması olarakta çağırabiliriz.
            return View();
        }
        public IActionResult Index3()
        {
            //veritabanına kaydetme işlemi
            return RedirectToAction("Index", "Ornek");
        }
        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonParametre", "Ornek", new { id = id });
        }
        public IActionResult JsonParametre(int id)
        {
            return Json(new { Id = id });
        }
        public IActionResult ContentResult()
        {
            return Content("Content Result");
        }
        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "kalem 1", price = 100 });
        }
        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}
