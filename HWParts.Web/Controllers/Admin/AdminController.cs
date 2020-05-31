using Microsoft.AspNetCore.Mvc;

namespace HWParts.Web.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
