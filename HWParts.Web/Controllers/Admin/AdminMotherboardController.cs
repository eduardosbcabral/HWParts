using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Motherboard;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HWParts.Web.Controllers
{
    [Authorize]
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
        [Authorize(Policy = "CanWriteComponentData")]
        public IActionResult Create()
        {
            return View(new MotherboardViewModel());
        }

        [HttpPost("register")]
        [Authorize(Policy = "CanWriteComponentData")]
        [ValidateAntiForgeryToken]
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
        [Authorize(Policy = "CanWriteComponentData")]
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
        [Authorize(Policy = "CanWriteComponentData")]
        [ValidateAntiForgeryToken]
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
        [Authorize(Policy = "CanRemoveComponentData")]
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
        [Authorize(Policy = "CanRemoveComponentData")]
        [ValidateAntiForgeryToken]
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

        [HttpGet("import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(ImportMotherboardsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _motherboardAppService.Import(viewModel);


            if (!IsValidOperation())
            {
                return View(viewModel);
            }

            ViewBag.Success = "Placas-Mãe Importadas.";

            return RedirectToAction(nameof(Index));
        }
    }
}
