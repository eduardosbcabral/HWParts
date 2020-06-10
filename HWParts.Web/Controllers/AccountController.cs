using HWParts.Core.Application.Interfaces;
using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HWParts.Web.Controllers
{
    [Authorize]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;

        private readonly IAccountAppService _accountAppService;

        public AccountController(
            INotificationHandler<DomainNotification> notifications,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IAccountAppService accountAppService)
            : base(notifications)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountAppService = accountAppService;
        }

        #region Register/Related Endpoints
        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(new RegisterAccountViewModel(returnUrl, externalLogins));
        }

        [HttpPost("register")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult RegisterConfirmation(string email)
        {
            return View("RegisterConfirmation", email);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            return View(new LoginAccountViewModel(returnUrl));
        }

        [HttpPost("login")]
        [AllowAnonymous]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        [HttpGet("send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCode(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if(user is null)
            {
                return View("Error");
            }

            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors
                .Select(p => new SelectListItem { Text = p, Value = p })
                .ToList();

            return View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        [HttpPost("send-code")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            //var message = "Your security code is: " + code;
            //if (model.SelectedProvider == "Email")
            //{
            //    await .SendEmailAsync(await UserManager.GetEmailAsync(user), "Security Code", message);
            //}
            //else if (model.SelectedProvider == "Phone")
            //{
            //    await .SendSmsAsync(await UserManager.GetPhoneNumberAsync(user), message);
            //}

            return RedirectToAction("VerifyCode", new 
            { 
                Provider = model.SelectedProvider, 
                model.ReturnUrl, 
                model.RememberMe 
            });
        }

        [HttpGet("reset-password")]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if(code is null)
            {
                return View("Error");
            }
            
            return View(new ResetPasswordViewModel(code));
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [HttpGet("reset-password-confirmation")]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if(user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that user does not exists or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
            //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet("forgot-password-confirmation")]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

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