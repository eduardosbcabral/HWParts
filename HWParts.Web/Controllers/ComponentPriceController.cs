using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.ComponentPrice;
using HWParts.Core.Domain.Core.Notifications;
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

            ViewData["ReturnUrl"] = PreviousUrl();

            return View(prices);
        }

        [HttpGet("create")]
        public IActionResult Create(Guid id)
        {
            ViewData["ReturnUrl"] = PreviousUrl();
            var viewModel = new ComponentPriceViewModel(id);
            return View(viewModel);
        }

        [HttpPost("create")]
        public IActionResult Create(ComponentPriceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _appService.Register(viewModel);

            return RedirectToAction(nameof(Index));
        }

        private string PreviousUrl()
        {
            var currentUrl = Request.Path.Value;
            var splittedUrl = currentUrl.Split("/");

            var previousUrl = string.Empty;

            for(var i = 1; i < splittedUrl.Length-1; i++)
            {
                previousUrl += "/" + splittedUrl[i];
            }

            return previousUrl;
        }
    }
}
