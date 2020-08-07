using Microsoft.AspNetCore.Mvc;

namespace HWParts.Web.Controllers
{
    public class HomeController : Controller    
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
