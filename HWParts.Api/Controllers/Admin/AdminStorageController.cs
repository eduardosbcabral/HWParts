//using HWParts.Core.Application.Interfaces;
//using HWParts.Core.Application.ViewModels.Storage;
//using HWParts.Core.Domain.Core.Notifications;
//using HWParts.Core.Infrastructure.Identity.Models;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using HWParts.Web.Controllers;

//namespace HWParts.Api.Controllers
//{
//    [Area("Admin")]
//    [Authorize(Roles = ApplicationRoles.StaffRoles)]
//    [Route("admin/storage")]
//    public class AdminStorageController : BaseController
//    {
//        private readonly IStorageAppService _storageAppService;

//        public AdminStorageController(
//            INotificationHandler<DomainNotification> notifications,
//            IStorageAppService storageAppService) 
//            : base(notifications)
//        {
//            _storageAppService = storageAppService;
//        }

//        [HttpGet("list")]
//        public IActionResult Index(int? page)
//        {
//            var paginationObject = _storageAppService.ListPaginated(page);
//            return View(paginationObject);
//        }

//        [HttpGet("register")]
//        public IActionResult Create()
//        {
//            return View(new StorageViewModel());
//        }

//        [HttpPost("register")]
//        public IActionResult Create(StorageViewModel storageViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(storageViewModel);
//            }

//            _storageAppService.Register(storageViewModel);

//            if (IsValidOperation())
//            {
//                ViewBag.Success = "Storage registrado.";
//            }

//            return View(storageViewModel);
//        }

//        [HttpGet("edit/{id:guid}")]
//        public IActionResult Edit(Guid? id)
//        {
//            if (id is null)
//            {
//                return NotFound();
//            }

//            var storageViewModel = _storageAppService.GetById(id.Value);

//            if (storageViewModel is null)
//            {
//                return NotFound();
//            }

//            return View(storageViewModel);
//        }

//        [HttpPost("edit/{id:guid}")]
//        public IActionResult Edit(StorageViewModel storageViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(storageViewModel);
//            }

//            _storageAppService.Update(storageViewModel);

//            if (IsValidOperation())
//            {
//                ViewBag.Success = "Storage atualizado.";
//            }

//            return View(storageViewModel);
//        }

//        [HttpGet("remove/{id:guid}")]
//        public IActionResult Delete(Guid? id)
//        {
//            if (id is null)
//            {
//                return NotFound();
//            }

//            var storageViewModel = _storageAppService.GetById(id.Value);

//            if (storageViewModel is null)
//            {
//                return NotFound();
//            }

//            return View(storageViewModel);
//        }

//        [HttpPost("remove/{id:guid}")]
//        public IActionResult DeleteConfirmed(Guid id)
//        {
//            _storageAppService.Remove(id);

//            if (!IsValidOperation())
//            {
//                return Delete(id);
//            }

//            ViewBag.Success = "Storage removido.";

//            return RedirectToAction(nameof(Index));
//        }

//        [HttpGet("import")]
//        public IActionResult Import()
//        {
//            return View();
//        }

//        [HttpPost("import")]
//        public async Task<IActionResult> Import(ImportStoragesViewModel viewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(viewModel);
//            }

//            await _storageAppService.Import(viewModel);


//            if (!IsValidOperation())
//            {
//                return View(viewModel);
//            }

//            ViewBag.Success = "Storages Importados.";

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
