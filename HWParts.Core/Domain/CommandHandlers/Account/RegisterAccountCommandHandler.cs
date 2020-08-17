using Flunt.Notifications;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class RegisterAccountCommandHandler : Notifiable, IRequestHandler<RegisterAccountCommand, bool>
    {
        private readonly UserManager<Account> _userManager;
        private readonly HWPartsDbContext _context;

        public RegisterAccountCommandHandler(
            UserManager<Account> userManager,
            HWPartsDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = new Account(
                    command.Username,
                    command.Email);

                var result = await _userManager.CreateAsync(user, command.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        AddNotification("CreateUser", error.Description);
                    }

                    return false;
                }

                await _userManager.AddToRoleAsync(user, "Common");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        AddNotification("AddUserToRole", error.Description);
                    }

                    return false;
                }

                return true;
                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //    var link = _linkGenerator.GetUriByAction(
                //        _httpContextAccessor.HttpContext,
                //        "ConfirmEmail",
                //        "Account",
                //        values: new ConfirmEmailAccountViewModel(user.Id, code),
                //        _httpContextAccessor.HttpContext.Request.Scheme,
                //        _httpContextAccessor.HttpContext.Request.Host);

                //    await _emailSender.SendEmailAsync(user.Email, "Confirme seu email",
                //        $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(link)}'>clicando aqui</a>.");

                //    AddNotification("RequireConfirmedAccount", string.Empty);
                //    return false;
                //}
            }
            catch (Exception)
            {
                AddNotification("CreateUser", string.Empty);
                transaction.Rollback();
                return false;
            }
        }
    }
}
