using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers.Components
{
    [Area("Component")]
    [Route("component/motherboard")]
    [AllowAnonymous]
    public class MotherboardController : BaseController
    {
        private readonly IMotherboardAppService _appService;

        public MotherboardController(
            INotificationHandler<DomainNotification> notifications,
            IMotherboardAppService appService)
            : base(notifications)
        {
            _appService = appService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var paginationObject = _appService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("detail")]
        public IActionResult Detail(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var viewModel = _appService.GetById(id.Value);

            if (viewModel is null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
