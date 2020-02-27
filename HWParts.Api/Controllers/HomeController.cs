using Microsoft.AspNetCore.Mvc;

namespace HWParts.Api.Controllers
{
    public class HomeController : Controller    
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
