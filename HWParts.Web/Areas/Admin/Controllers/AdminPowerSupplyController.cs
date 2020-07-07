using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.PowerSupply;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HWParts.Web.Controllers;

namespace HWParts.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.StaffRoles)]
    [Route("admin/power-supply")]
    public class AdminPowerSupplyController : BaseController
    {
        private readonly IPowerSupplyAppService _powerSupplyAppService;

        public AdminPowerSupplyController(
            INotificationHandler<DomainNotification> notifications,
            IPowerSupplyAppService powerSupplyAppService) 
            : base(notifications)
        {
            _powerSupplyAppService = powerSupplyAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _powerSupplyAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View(new PowerSupplyViewModel());
        }

        [HttpPost("register")]
        public IActionResult Create(PowerSupplyViewModel powerSupplyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(powerSupplyViewModel);
            }

            _powerSupplyAppService.Register(powerSupplyViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Fonte registrada.";
            }

            return View(powerSupplyViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var powerSupplyViewModel = _powerSupplyAppService.GetById(id.Value);

            if (powerSupplyViewModel is null)
            {
                return NotFound();
            }

            return View(powerSupplyViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(PowerSupplyViewModel powerSupplyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(powerSupplyViewModel);
            }

            _powerSupplyAppService.Update(powerSupplyViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Fonte atualizada.";
            }

            return View(powerSupplyViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var powerSupplyViewModel = _powerSupplyAppService.GetById(id.Value);

            if (powerSupplyViewModel is null)
            {
                return NotFound();
            }

            return View(powerSupplyViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _powerSupplyAppService.Remove(id);

            if (!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Fonte removida.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(ImportPowerSuppliesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _powerSupplyAppService.Import(viewModel);


            if (!IsValidOperation())
            {
                return View(viewModel);
            }

            ViewBag.Success = "Fontes Importadas.";

            return RedirectToAction(nameof(Index));
        }
    }
}
