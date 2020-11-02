using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Handlers;
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
    public class ResetPasswordAccountHandlerTests
    {
        private readonly Mock<IAccountRepository> fakeAccountRepository;
        private readonly Mock<Account> fakeAccount;
        private readonly ResetPasswordAccount command;

        private readonly IdentityResult identityResultSuccess;
        private readonly IdentityResult identityResultFailed;

        public ResetPasswordAccountHandlerTests()
        {
            fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccount = new Mock<Account>();
            command = new ResetPasswordAccount(
                "teste@teste.com.br",
                "password",
                "code"
            );
            identityResultSuccess = IdentityResult.Success;
            identityResultFailed = IdentityResult.Failed();
        }

        [Fact]
        public async Task Should_not_return_error()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);
            fakeAccountRepository.Setup(x => x.ResetPasswordAsync(It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultSuccess);

            var handler = new ResetPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Equal("Sua senha foi trocada com sucesso!", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_find_by_email_returns_null()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Account);

            var handler = new ResetPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Ocorreu um erro ao trocar a sua senha.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_reset_password_returns_error()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);
            fakeAccountRepository.Setup(x => x.ResetPasswordAsync(It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultFailed);

            var handler = new ResetPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Não foi possível trocar a sua senha.", result.Message);
        }
    }
}
