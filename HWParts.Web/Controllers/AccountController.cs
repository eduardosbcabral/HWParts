using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HWParts.Web.Controllers
{
    [Authorize]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var registerViewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                    .ToList()
            };

            return View(registerViewModel);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            registerViewModel.ReturnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(registerViewModel);
            }

            await _userManager.AddClaimAsync(
                user,
                new Claim(UserClaims.Components, UserClaimValues.Write)
            );

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return RedirectToPage("RegisterConfirmation", new { email = registerViewModel.Email });
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(registerViewModel.ReturnUrl);
            }
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(loginViewModel);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(loginViewModel.ReturnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendCode", new { loginViewModel.ReturnUrl, loginViewModel.RememberMe });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return View(loginViewModel);
            }
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("reset-password")]
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

        [HttpPost("reset-password")]
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

            // TODO: SEND USER CODE EMAIL
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

        //[HttpGet("account/confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
        //    {
        //        return View("Error");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user is null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);

        //    if (!result.Succeeded)
        //    {
        //        return View("Error");
        //    }

        //    return View("ConfirmEmail");
        //}

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
    }
}