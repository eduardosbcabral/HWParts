using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Case;
using HWParts.Core.Domain.Commands;
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
    [Route("admin/case")]
    public class AdminCaseController : BaseController
    {
        private readonly ICaseAppService _caseAppService;

        public AdminCaseController(
            INotificationHandler<DomainNotification> notifications,
            ICaseAppService caseAppService) 
            : base(notifications)
        {
            _caseAppService = caseAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _caseAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View(new CaseViewModel());
        }

        [HttpPost("register")]
        public IActionResult Create(CaseViewModel caseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(caseViewModel);
            }

            _caseAppService.Register(caseViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Gabinete registrado.";
            }

            return View(caseViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var caseViewModel = _caseAppService.GetById(id.Value);

            if (caseViewModel is null)
            {
                return NotFound();
            }

            return View(caseViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(CaseViewModel caseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(caseViewModel);
            }

            _caseAppService.Update(caseViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Gabinete atualizado.";
            }

            return View(caseViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var caseViewModel = _caseAppService.GetById(id.Value);

            if (caseViewModel is null)
            {
                return NotFound();
            }

            return View(caseViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _caseAppService.Remove(id);

            if (!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Gabinete removido.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(ImportCasesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _caseAppService.Import(viewModel);


            if(!IsValidOperation())
            {
                return View(viewModel);
            }

            ViewBag.Success = "Gabinete Importados.";

            return RedirectToAction(nameof(Index));
        }
    }
}
