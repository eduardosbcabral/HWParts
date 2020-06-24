using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.Services;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    [Authorize]
    [Route("admin/{case|graphics-card|memory|motherboard|power-supply|processor|storage}/edit/{id:guid}/price")]
    public class ComponentPriceController : BaseController
    {
        private readonly IComponentPriceAppService _appService;

        public ComponentPriceController(
            INotificationHandler<DomainNotification> notifications,
            IComponentPriceAppService appService) 
            : base(notifications)
        {
            _appService = appService;
        }

        [HttpGet]
        public IActionResult Index(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var prices = _appService.GetAllPricesByComponentId(id);

            return View(prices);
        }
    }
}
