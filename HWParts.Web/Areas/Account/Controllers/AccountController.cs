using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Infrastructure.Identity.Models;
using HWParts.Web.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Entities = HWParts.Core.Domain.Entities;

namespace HWParts.Web.Areas.Account.Controllers
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

        #region Register/Related Endpoints
        [HttpGet("register")]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(new RegisterAccountViewModel(returnUrl, externalLogins));
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterAccountViewModel registerViewModel)
        {
            registerViewModel.ReturnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            await _accountAppService.Register(registerViewModel);

            if (HasNotification("CreateUser"))
            {
                return View(registerViewModel);
            }

            if (HasNotification("RequireConfirmedAccount"))
            {
                return RedirectToAction(nameof(RegisterConfirmation), new { email = registerViewModel.Email });
            }

            if (!IsValidOperation())
            {
                return View(registerViewModel);
            }

            return LocalRedirect(registerViewModel.ReturnUrl);
        }

        [HttpGet("register-confirmation")]
        public IActionResult RegisterConfirmation(string email)
        {
            return View("RegisterConfirmation", email);
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
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            return View(new LoginAccountViewModel(returnUrl));
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAccountViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            await _accountAppService.Login(loginViewModel);

            if (HasNotification("RequiresTwoFactor"))
            {
                return RedirectToAction("SendCode", new { loginViewModel.ReturnUrl, loginViewModel.RememberMe });
            }

            if (HasNotification("IsLockedOut"))
            {
                return View("Lockout");
            }

            if(!IsValidOperation())
            {
                return View(loginViewModel);
            }

            return RedirectToLocal(loginViewModel.ReturnUrl);
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