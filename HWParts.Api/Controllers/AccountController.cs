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

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

        [HttpPost("reset-password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordAccount command)
        {
            var response = await _accountAppService.ResetPassword(command);

            if (response.Invalid)
            {
                return BadRequest(response.Result);
            }

            return Ok(response.Result);
        }

        [HttpPost("forgot-password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordAccount command)
        {
            var response = await _accountAppService.ForgotPassword(command);

            if (response.Invalid)
            {
                return BadRequest(response.Result);
            }

            return Ok(response.Result);
        }
    }
}