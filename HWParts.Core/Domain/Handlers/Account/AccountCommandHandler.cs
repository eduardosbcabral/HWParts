//using HWParts.Core.Domain.Commands;
//using HWParts.Core.Domain.Core.Notifications;
//using HWParts.Core.Domain.Entities;
//using HWParts.Core.Domain.Interfaces;
//using HWParts.Core.Infrastructure;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Routing;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace HWParts.Core.Domain.Handlers
//{
//    public class AccountCommandHandler : CommandHandler,
//        IRequestHandler<RegisterAccountCommand, string>
//        //IRequestHandler<LoginAccountCommand, bool>,
//        //IRequestHandler<ConfirmEmailAccountCommand, bool>,
//        //IRequestHandler<ForgotPasswordAccountCommand, bool>,
//        //IRequestHandler<ResetPasswordAccountCommand, bool>
//    {
//        private readonly SignInManager<Account> _signInManager;
//        private readonly UserManager<Account> _userManager;

//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly LinkGenerator _linkGenerator;
//        private readonly IEmailSender _emailSender;

//        private readonly HWPartsDbContext _context;

//        public AccountCommandHandler(
//            IUnitOfWork uow,
//            INotificationContext notificationContext,
//            UserManager<Account> userManager,
//            SignInManager<Account> signInManager,
//            LinkGenerator linkGenerator,
//            IHttpContextAccessor httpContextAccessor,
//            IEmailSender emailSender,
//            HWPartsDbContext context) 
//            : base(uow, notificationContext)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _linkGenerator = linkGenerator;
//            _httpContextAccessor = httpContextAccessor;
//            _emailSender = emailSender;
//            _context = context;
//        }

//        // Register
//        public async Task<string> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
//        {
//            if (!request.IsValid())
//            {
//                NotifyValidationErrors(request);
//                return string.Empty;
//            }

//            using var transaction = _context.Database.BeginTransaction();
//            try
//            {
//                var user = new Account(
//                   request.Username,
//                   request.Email
//                );

//                //var result = await _userManager.CreateAsync(user, request.Password);

//                //if (!result.Succeeded)
//                //{
//                //    foreach (var error in result.Errors)
//                //    {
//                //        await Bus.RaiseEvent(new DomainNotification("CreateUser", error.Description));
//                //    }

//                //    return false;
//                //}

//                //await _userManager.AddToRoleAsync(user, "Common");

//                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
//                //{
//                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

//                //    var link = _linkGenerator.GetUriByAction(
//                //        _httpContextAccessor.HttpContext,
//                //        "ConfirmEmail",
//                //        "Account",
//                //        values: new ConfirmEmailAccountViewModel(user.Id, code),
//                //        _httpContextAccessor.HttpContext.Request.Scheme,
//                //        _httpContextAccessor.HttpContext.Request.Host);

//                //    await _emailSender.SendEmailAsync(user.Email, "Confirme seu email",
//                //        $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(link)}'>clicando aqui</a>.");

//                //    await Bus.RaiseEvent(new DomainNotification("RequireConfirmedAccount", string.Empty));
//                //    return false;
//                //}

//                //await _signInManager.SignInAsync(user, isPersistent: false);

//                transaction.Commit();

//                return string.Empty;
//            }
//            catch(Exception)
//            {
//                Notify(new Notification("CreateUser", string.Empty));
//                transaction.Rollback();
//                return string.Empty;
//            }
//        }

//        //// Login
//        //public async Task<bool> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
//        //{
//        //    if (!request.IsValid())
//        //    {
//        //        NotifyValidationErrors(request);
//        //        return false;
//        //    }

//        //    var result = await _signInManager.PasswordSignInAsync(
//        //        request.Username,
//        //        request.Password,
//        //        request.RememberMe,
//        //        lockoutOnFailure: false);

//        //    if (!result.Succeeded)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("Login", "Tentativa de login inválida."));
//        //        return false;
//        //    }

//        //    if (result.RequiresTwoFactor)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("RequiresTwoFactor", string.Empty));
//        //        return true;
//        //    }

//        //    if (result.IsLockedOut)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("IsLockedOut", string.Empty));
//        //        return true;
//        //    }

//        //    return true;
//        //}

//        //public async Task<bool> Handle(ConfirmEmailAccountCommand request, CancellationToken cancellationToken)
//        //{
//        //    if (!request.IsValid())
//        //    {
//        //        NotifyValidationErrors(request);
//        //        return false;
//        //    }

//        //    var user = await _userManager.FindByIdAsync(request.Id);

//        //    if(user == null)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("AccountNotFound", string.Empty));
//        //        return false;
//        //    }

//        //    var decodedCode = WebEncoders.Base64UrlDecode(request.Code);
//        //    var code = Encoding.Default.GetString(decodedCode);
//        //    var result = await _userManager.ConfirmEmailAsync(user, code);
            
//        //    if (!result.Succeeded)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("ConfirmEmail", "Ocorreu um erro ao confirmar o e-mail."));
//        //        return false;
//        //    }

//        //    return true;
//        //}

//        //public async Task<bool> Handle(ForgotPasswordAccountCommand request, CancellationToken cancellationToken)
//        //{
//        //    if (!request.IsValid())
//        //    {
//        //        NotifyValidationErrors(request);
//        //        return false;
//        //    }

//        //    var user = await _userManager.FindByEmailAsync(request.Email);

//        //    if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("ErrorForgotPassword", string.Empty));
//        //        return false;
//        //    }

//        //    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
//        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

//        //    var link = _linkGenerator.GetUriByAction(
//        //            _httpContextAccessor.HttpContext,
//        //            "ResetPassword",
//        //            "Account",
//        //            values: new ResetPasswordAccountViewModel(user.Id, code),
//        //            _httpContextAccessor.HttpContext.Request.Scheme,
//        //            _httpContextAccessor.HttpContext.Request.Host);

//        //    await _emailSender.SendEmailAsync(request.Email, "Resetar Senha",
//        //       $"Por favor, troque sua senha clicando aqui: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");

//        //    return true;
//        //}

//        //public async Task<bool> Handle(ResetPasswordAccountCommand request, CancellationToken cancellationToken)
//        //{
//        //    if (!request.IsValid())
//        //    {
//        //        NotifyValidationErrors(request);
//        //        return false;
//        //    }

//        //    var user = await _userManager.FindByEmailAsync(request.Email);
//        //    if (user == null)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("AccountNotFound", string.Empty));
//        //        return false;
//        //    }

//        //    var decodedCode = WebEncoders.Base64UrlDecode(request.Code);
//        //    var code = Encoding.Default.GetString(decodedCode);

//        //    var result = await _userManager.ResetPasswordAsync(user, code, request.Password);
//        //    if (!result.Succeeded)
//        //    {
//        //        await Bus.RaiseEvent(new DomainNotification("ErrorResetPasswordAccount", string.Empty));
//        //        return false;
//        //    }

//        //    return true;
//        //}
//    }
//}
