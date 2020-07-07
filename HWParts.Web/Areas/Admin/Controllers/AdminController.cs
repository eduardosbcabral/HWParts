using HWParts.Core.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HWParts.Web.Controllers;

namespace HWParts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.StaffRoles)]
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
