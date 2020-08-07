using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HWParts.Web.Controllers;

namespace HWParts.Api.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.StaffRoles)]
    [Route("admin/account")]
    public class AdminAccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;

        public AdminAccountController(
            INotificationHandler<DomainNotification> notifications,
            IAccountAppService accountAppService)
            : base(notifications)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _accountAppService.ListPaginated(page);
            return View(paginationObject);
        }
    }
}
