using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.ComponentPrice;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace HWParts.Web.Controllers
{
    [Authorize]
    [Route("admin/{(case|graphics-card|memory|motherboard|power-supply|processor|storage)}/edit/{id:guid}/price")]
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

        [HttpGet("list")]
        public IActionResult Index(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var prices = _appService.GetAllPricesByComponentId(id);

            return View(prices);
        }

        [HttpGet("register")]
        public IActionResult Create(Guid id)
        {
            var viewModel = new ComponentPriceViewModel(id);
            return View(viewModel);
        }

        [HttpPost("register")]
        public IActionResult Create(ComponentPriceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _appService.Register(viewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Preço registrado.";
            }

            return View(viewModel);
        }

        [HttpGet("edit/{componentPriceId:guid}")]
        public IActionResult Edit(Guid? componentPriceId)
        {
            if (componentPriceId is null)
            {
                return NotFound();
            }

            var viewModel = _appService.GetById(componentPriceId.Value);

            if (viewModel is null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost("edit/{componentPriceId:guid}")]
        public IActionResult Edit(ComponentPriceViewModel viewModel, Guid componentPriceId)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.Id = componentPriceId;

            _appService.Update(viewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Preço atualizado.";
            }

            return View(viewModel);
        }

        [HttpGet("remove/{componentPriceId:guid}")]
        public IActionResult Delete(Guid? componentPriceId)
        {
            if (componentPriceId is null)
            {
                return NotFound();
            }

            var memoryViewModel = _appService.GetById(componentPriceId.Value);

            if (memoryViewModel is null)
            {
                return NotFound();
            }

            return View(memoryViewModel);
        }

        [HttpPost("remove/{componentPriceId:guid}")]
        public IActionResult DeleteConfirmed(Guid componentPriceId, Guid id)
        {
            _appService.Remove(componentPriceId);

            if (!IsValidOperation())
            {
                return Delete(componentPriceId);
            }

            TempData["Success"] = "Preço removido.";

            var url = Request.Path.Value;
            var splitted = url.Split("/remove");

            return Redirect($"{splitted[0]}/list");
        }
    }
}
