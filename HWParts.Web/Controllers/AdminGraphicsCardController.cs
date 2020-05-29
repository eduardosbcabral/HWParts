﻿using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.GraphicsCard;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    [Route("admin/graphics-card")]
    public class AdminGraphicsCardController : BaseController
    {
        private readonly IGraphicsCardAppService _graphicsCardAppService;

        public AdminGraphicsCardController(
            INotificationHandler<DomainNotification> notifications,
            IGraphicsCardAppService graphicsCardAppService) 
            : base(notifications)
        {
            _graphicsCardAppService = graphicsCardAppService;
        }

        [HttpGet("list")]
        public IActionResult Index(int? page)
        {
            var paginationObject = _graphicsCardAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("register")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Create(GraphicsCardViewModel graphicsCardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(graphicsCardViewModel);
            }

            _graphicsCardAppService.Register(graphicsCardViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Placa de Vídeo registrada.";
            }

            return View(graphicsCardViewModel);
        }

        [HttpGet("edit/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var graphicsCardViewModel = _graphicsCardAppService.GetById(id.Value);

            if (graphicsCardViewModel is null)
            {
                return NotFound();
            }

            return View(graphicsCardViewModel);
        }

        [HttpPost("edit/{id:guid}")]
        public IActionResult Edit(GraphicsCardViewModel graphicsCardViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(graphicsCardViewModel);
            }

            _graphicsCardAppService.Update(graphicsCardViewModel);

            if (IsValidOperation())
            {
                ViewBag.Success = "Placa de Vídeo atualizada.";
            }

            return View(graphicsCardViewModel);
        }

        [HttpGet("remove/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var graphicsCardViewModel = _graphicsCardAppService.GetById(id.Value);

            if (graphicsCardViewModel is null)
            {
                return NotFound();
            }

            return View(graphicsCardViewModel);
        }

        [HttpPost("remove/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _graphicsCardAppService.Remove(id);

            if (!IsValidOperation())
            {
                return Delete(id);
            }

            ViewBag.Success = "Placa de Vídeo removida.";

            return RedirectToAction(nameof(Index));
        }
    }
}