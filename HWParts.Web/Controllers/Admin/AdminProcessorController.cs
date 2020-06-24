using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Processor;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HWParts.Web.Controllers
{
    [Authorize]
    [Route("admin/processor")]
    public class AdminProcessorController : BaseController
    {
        private readonly IProcessorAppService _processorAppService;

        public AdminProcessorController(
            INotificationHandler<DomainNotification> notifications,
            IProcessorAppService processorAppService) 
            : base(notifications)
        {
            _processorAppService = processorAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _processorAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View(new ProcessorViewModel());
        }

        [HttpPost("register")]
        public IActionResult Create(ProcessorViewModel processorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(processorViewModel);
            }

            _processorAppService.Register(processorViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Processador registrado.";
            }

            return View(processorViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var processorViewModel = _processorAppService.GetById(id.Value);

            if (processorViewModel is null)
            {
                return NotFound();
            }

            return View(processorViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(ProcessorViewModel processorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(processorViewModel);
            }

            _processorAppService.Update(processorViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Processador atualizada.";
            }

            return View(processorViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var processorViewModel = _processorAppService.GetById(id.Value);

            if (processorViewModel is null)
            {
                return NotFound();
            }

            return View(processorViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _processorAppService.Remove(id);

            if (!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Processador removido.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("import")]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(ImportProcessorsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _processorAppService.Import(viewModel);


            if (!IsValidOperation())
            {
                return View(viewModel);
            }

            ViewBag.Success = "Processadores Importados.";

            return RedirectToAction(nameof(Index));
        }
    }
}
