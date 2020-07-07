using HWParts.Core.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HWParts.Web.Controllers
{
    [Authorize(Roles = ApplicationRoles.StaffRoles)]
    public class AdminController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
