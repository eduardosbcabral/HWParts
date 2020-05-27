using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Memory;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    [Route("admin/memory")]
    public class AdminMemoryController : BaseController
    {
        private readonly IMemoryAppService _memoryAppService;

        public AdminMemoryController(
            INotificationHandler<DomainNotification> notifications,
            IMemoryAppService memoryAppService)
            : base(notifications)
        {
            _memoryAppService = memoryAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _memoryAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Create(MemoryViewModel memoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(memoryViewModel);
            }

            _memoryAppService.Register(memoryViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Memória RAM registrada.";
            }

            return View(memoryViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var memoryViewModel = _memoryAppService.GetById(id.Value);

            if (memoryViewModel is null)
            {
                return NotFound();
            }

            return View(memoryViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(MemoryViewModel memoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(memoryViewModel);
            }

            _memoryAppService.Update(memoryViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Memória RAM atualizada.";
            }

            return View(memoryViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var memoryViewModel = _memoryAppService.GetById(id.Value);

            if (memoryViewModel is null)
            {
                return NotFound();
            }

            return View(memoryViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _memoryAppService.Remove(id);

            if (!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Memória RAM removida.";

            return RedirectToAction(nameof(Index));
        }
    }
}
