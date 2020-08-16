﻿using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register([FromBody] RegisterAccountCommand command)
        {
            var result = await _accountAppService.Register(command);
            return Ok(result);
        }

        //[HttpGet("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(ConfirmEmailAccountViewModel model)
        //{
        //    if (model == null)
        //    {
        //        return View("Error");
        //    }

        //    await _accountAppService.ConfirmEmail(model);

        //    if (HasNotification("AccountNotFound"))
        //    {
        //        return View("Error");
        //    }

        //    if (HasNotification("ConfirmEmail"))
        //    {
        //        return View("Error");
        //    }

        //    return View("ConfirmEmail", model.Email);
        //}
        //#endregion

        //#region Login/Related Endpoints
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginAccountCommand command)
        //{
        //    _accountAppService.Login(command);

        //    if (!IsValidOperation())
        //    {
        //        return BadRequest();
        //    }

        //    var domainEvent = GetNotification<AccountRegisteredEvent>();
        //    return Ok(domainEvent);
        //}

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