using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Bus;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class AccountCommandHandler : CommandHandler,
        IRequestHandler<RegisterAccountCommand, bool>,
        IRequestHandler<LoginAccountCommand, bool>
    {
        private readonly IMediatorHandler Bus;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) 
            : base(uow, bus, notifications)
        {
            Bus = bus;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register
        public async Task<bool> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = new IdentityUser
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
                request.Email, 
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
    }
}
