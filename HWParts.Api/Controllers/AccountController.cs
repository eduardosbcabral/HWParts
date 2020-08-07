using HWParts.Api.Extensions;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Events;
using HWParts.Core.Infrastructure.Identity.Models;
using HWParts.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities = HWParts.Core.Domain.Entities;

namespace HWParts.Api.Controllers
{
    [Area("Account")]
    [AllowAnonymous]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly SignInManager<Entities.Account> _signInManager;
        private readonly UserManager<Entities.Account> _userManager;

        private readonly IAccountAppService _accountAppService;

        public AccountController(
            INotificationHandler<DomainNotification> notifications,
            UserManager<Entities.Account> userManager,
            SignInManager<Entities.Account> signInManager,
            IAccountAppService accountAppService)
            : base(notifications)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountAppService = accountAppService;
        }

        #region Register Endpoints
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountCommand command)
        {
            await _accountAppService.Register(command);

            if (!IsValidOperation())
            {
                return BadRequest();
            }

            var domainEvent = GetNotification<AccountRegisteredEvent>();
            return this.CreatedEntity(domainEvent);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailAccountViewModel model)
        {
            if (model == null)
            {
                return View("Error");
            }

            await _accountAppService.ConfirmEmail(model);

            if (HasNotification("AccountNotFound"))
            {
                return View("Error");
            }

            if (HasNotification("ConfirmEmail"))
            {
                return View("Error");
            }

            return View("ConfirmEmail", model.Email);
        }
        #endregion

        #region Login/Related Endpoints
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginAccountCommand command)
        {
            _accountAppService.Login(command);

            if (!IsValidOperation())
            {
                return BadRequest();
            }

            var domainEvent = GetNotification<AccountRegisteredEvent>();
            return Ok(domainEvent);
        }

        [HttpPost("logout")]
        [Authorize(Roles = ApplicationRoles.AllRoles)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ResetPassword/Related Endpoints
        [HttpGet("reset-password")]
        public ActionResult ResetPassword(string code)
        {
            if(code is null)
            {
                return View("Error");
            }
            
            return View(new ResetPasswordAccountViewModel(code));
        }

        [HttpPost("reset-password")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _accountAppService.ResetPassword(model);

            if (HasNotification("AccountNotFound"))
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            if (HasNotification("ErrorResetPasswordAccount"))
            {
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet("reset-password-confirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _accountAppService.ForgotPassword(model);

            // Don't reveal that user does not exists or is not confirmed
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet("forgot-password-confirmation")]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}