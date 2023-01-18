using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers.Helpers
{
    public class Helper : IHelper
    {
        //private bool _isConfiguration;
        private readonly AppDbContext _context;
        public Helper(AppDbContext context)
        {
            _context = context;
        }
        //public Helper(bool isConfiguration)
        //{
        //    _isConfiguration = isConfiguration;
        //}
        public string Upper(string text)
        {
            return text.ToUpper();
        }
    }
}
