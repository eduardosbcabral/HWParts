using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    [Route("component/case")]
    [AllowAnonymous]
    public class CaseController : BaseController
    {
        private readonly ICaseAppService _caseAppService;

        public CaseController(
            INotificationHandler<DomainNotification> notifications,
            ICaseAppService caseAppService) 
            : base(notifications)
        {
            _caseAppService = caseAppService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var paginationObject = _caseAppService.ListPaginated(page);
            return View(paginationObject);
        }

        [HttpGet("detail")]
        public IActionResult Detail(Guid? id)
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
    }
}
