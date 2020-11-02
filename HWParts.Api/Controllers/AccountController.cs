using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace HWParts.Api.Controllers
{
    [AllowAnonymous]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService) =>
            _accountAppService = accountAppService;

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterAccount command)
        {
            var result = await _accountAppService.Register(command);

            if (result.Invalid)
            {
                return BadRequest(result.Result);
            }

            return Ok(result.Result);
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailAccount command)
        {
            var result = await _accountAppService.ConfirmEmail(command);

            if (result.Invalid)
            {
                return BadRequest(result.Result);
            }

            return Ok(result.Result);
        }

        //#region Login/Related Endpoints
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAccount command)
        {
            var response = await _accountAppService.Login(command);

            if (response.Invalid)
            {
                return BadRequest(response.Result);
            }

            return Ok(response.Result);
        }

        //[HttpPost("logout")]
        //[Authorize(Roles = ApplicationRoles.AllRoles)]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}
        //#endregion

        //#region ResetPassword/Related Endpoints
        //[HttpGet("reset-password")]
        //public ActionResult ResetPassword(string code)
        //{
        //    if(code is null)
        //    {
        //        return View("Error");
        //    }

        //    return View(new ResetPasswordAccountViewModel(code));
        //}

        //[HttpPost("reset-password")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordAccountViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    await _accountAppService.ResetPassword(model);

        //    if (HasNotification("AccountNotFound"))
        //    {
        //        return RedirectToAction(nameof(ResetPasswordConfirmation));
        //    }

        //    if (HasNotification("ErrorResetPasswordAccount"))
        //    {
        //        return View();
        //    }

        //    return RedirectToAction(nameof(ResetPasswordConfirmation));
        //}

        //[HttpGet("reset-password-confirmation")]
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        //[HttpGet("forgot-password")]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost("forgot-password")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordAccountViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    await _accountAppService.ForgotPassword(model);

        //    // Don't reveal that user does not exists or is not confirmed
        //    return View("ForgotPasswordConfirmation");
        //}

        //[HttpGet("forgot-password-confirmation")]
        //public IActionResult ForgotPasswordConfirmation()
        //{
        //    return View();
        //}
        //#endregion
    }
}