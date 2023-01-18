using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Controllers.Helpers;
using MyAspNetCoreApp.Web.Models;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //private Helper _helper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger /*, Helper helper*/)
        {
        //    _helper = helper;
            _logger = logger;
        }
         
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //var text = "asp.net";
            //var upperText = _helper.Upper(text);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}