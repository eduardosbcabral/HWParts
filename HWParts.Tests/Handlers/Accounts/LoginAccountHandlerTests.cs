using HWParts.Core.Application.ViewModels.Account;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Handlers;
using HWParts.Core.Domain.Handlers.Responses.Accounts;
using HWParts.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HWParts.Tests.Handlers.Accounts
{
    public class LoginAccountHandlerTests
    {
        private readonly Mock<IAccountRepository> fakeAccountRepository;
        private readonly LoginAccount command;
        private readonly SignInResult signInResultSuccess;
        private readonly SignInResult signInResultTwoFactorRequired;
        private readonly SignInResult signInResultLockedOut;
        private readonly SignInResult signInResultFailure;

        public LoginAccountHandlerTests()
        {
            fakeAccountRepository = new Mock<IAccountRepository>();
            command = new LoginAccount(
                "testUser",
                "123456");
            signInResultSuccess = SignInResult.Success;
            signInResultTwoFactorRequired = SignInResult.TwoFactorRequired;
            signInResultLockedOut = SignInResult.LockedOut;
            signInResultFailure = SignInResult.Failed;
        }

        [Fact]
        public async Task Should_not_have_notifications()
        {
            fakeAccountRepository.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(signInResultSuccess));

            var safeAccountDTO = new SafeAccountDTO() { UserName = "test" };

            fakeAccountRepository.Setup(x => x.FindByNameAsyncSafe(It.IsAny<string>()))
                .Returns(Task.FromResult(safeAccountDTO));
            var handler = new LoginAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Equal("Login realizado com sucesso.", result.Message);
        }

        [Fact]
        public async Task Should_have_notification_when_sign_in_returns_failure()
        {
            fakeAccountRepository.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(signInResultFailure));

            var handler = new LoginAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Tentativa de login inválida.", result.Message);
        }

        [Fact]
        public async Task Should_have_notification_when_sign_in_two_factor_is_required()
        {
            fakeAccountRepository.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(signInResultTwoFactorRequired));

            var handler = new LoginAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Ocorreu um erro. É necessário validar a conta utilizando a verificação de dois passos.", result.Message);
        }

        [Fact]
        public async Task Should_have_notification_when_sign_in_is_locked_out()
        {
            fakeAccountRepository.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(signInResultLockedOut));

            var handler = new LoginAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Ocorreu um erro. Sua conta está bloqueada.", result.Message);
        }
    }
}
