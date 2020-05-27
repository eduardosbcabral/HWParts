using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    [Route("admin/motherboards")]
    public class AdminMotherboardController : BaseController
    {
        private readonly IMotherboardAppService _motherboardAppService;

        public AdminMotherboardController(
            INotificationHandler<DomainNotification> notifications,
            IMotherboardAppService motherboardAppService)
            : base(notifications)
        {
            _motherboardAppService = motherboardAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _motherboardAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Create(MotherboardViewModel motherboardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(motherboardViewModel);
            }

            _motherboardAppService.Register(motherboardViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Placa-Mãe registrada.";
            }

            return View(motherboardViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            var motherboardViewModel = _motherboardAppService.GetById(id.Value);

            if(motherboardViewModel is null)
            {
                return NotFound();
            }

            return View(motherboardViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(MotherboardViewModel motherboardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(motherboardViewModel);
            }

            _motherboardAppService.Update(motherboardViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Placa-Mãe atualizada.";
            }

            return View(motherboardViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if(id is null)
            {
                return NotFound();
            }

            var motherboardViewModel = _motherboardAppService.GetById(id.Value);

            if(motherboardViewModel is null)
            {
                return NotFound();
            }

            return View(motherboardViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _motherboardAppService.Remove(id);

            if(!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Placa-Mãe removida.";

            return RedirectToAction(nameof(Index));
        }
    }
}
