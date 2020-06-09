using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class AccountCommandHandler : CommandHandler,
        IRequestHandler<RegisterAccountCommand, bool>,
        IRequestHandler<LoginAccountCommand, bool>,
        IRequestHandler<ConfirmEmailAccountCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        private readonly IEmailSender _emailSender;

        public AccountCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender) 
            : base(uow, bus, notifications)
        {
            Bus = bus;

            _userManager = userManager;
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        // Register
        public async Task<bool> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = new Account
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    await Bus.RaiseEvent(new DomainNotification("CreateUser", error.Description));
                }

                return false;
            }

            await _userManager.AddClaimAsync(
                user,
                new Claim(UserClaims.Components, UserClaimValues.Write)
            );

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var link = _linkGenerator.GetUriByAction(
                    _httpContextAccessor.HttpContext,
                    "ConfirmEmail",
                    "Account",
                    values: new ConfirmEmailAccountViewModel(user.Id, code),
                    _httpContextAccessor.HttpContext.Request.Scheme,
                    _httpContextAccessor.HttpContext.Request.Host);

                await _emailSender.SendEmailAsync(user.Email, "Confirme seu email",
                    $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(link)}'>clicando aqui</a>.");

                await Bus.RaiseEvent(new DomainNotification("RequireConfirmedAccount", string.Empty));
                return false;
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        // Login
        public async Task<bool> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(
                request.Username, 
                request.Password, 
                request.RememberMe, 
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                await Bus.RaiseEvent(new DomainNotification("Login", "Tentativa de login inválida."));
                return false;
            }

            if (result.RequiresTwoFactor)
            {
                await Bus.RaiseEvent(new DomainNotification("RequiresTwoFactor", string.Empty));
                return true;
            }

            if (result.IsLockedOut)
            {
                await Bus.RaiseEvent(new DomainNotification("IsLockedOut", string.Empty));
                return true;
            }

            return true;
        }

        public async Task<bool> Handle(ConfirmEmailAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = await _userManager.FindByIdAsync(request.Id);

            if(user == null)
            {
                await Bus.RaiseEvent(new DomainNotification("AccountNotFound", string.Empty));
                return false;
            }

            var decodedCode = WebEncoders.Base64UrlDecode(request.Code);
            var code = Encoding.Default.GetString(decodedCode);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            
            if (!result.Succeeded)
            {
                await Bus.RaiseEvent(new DomainNotification("ConfirmEmail", "Ocorreu um erro ao confirmar o e-mail."));
                return false;
            }

            return true;
        }
    }
}
