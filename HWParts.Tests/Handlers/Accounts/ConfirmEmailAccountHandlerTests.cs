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
    public class ConfirmEmailAccountHandlerTests
    {
        private readonly Mock<IAccountRepository> fakeAccountRepository;
        private readonly Mock<Account> fakeAccount;
        private readonly ConfirmEmailAccount command;
        private readonly IdentityResult identityResultSuccess;
        private readonly IdentityResult identityResultFailed;

        public ConfirmEmailAccountHandlerTests()
        {
            fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccount = new Mock<Account>();
            command = new ConfirmEmailAccount(
                "id_123",
                "code_123");
            identityResultSuccess = IdentityResult.Success;
            identityResultFailed = IdentityResult.Failed();
        }

        [Fact]
        public async Task Should_not_return_error()
        {
            fakeAccountRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);

            fakeAccountRepository.Setup(x => x.ConfirmEmailAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultSuccess));

            var handler = new ConfirmEmailAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Equal("E-mail confirmado com sucesso.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_find_by_id_returns_null()
        {
            fakeAccountRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Account);

            var handler = new ConfirmEmailAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Ocorreu um erro ao encontrar o usuário.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_confirm_email_returns_failure()
        {
            fakeAccountRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);

            fakeAccountRepository.Setup(x => x.ConfirmEmailAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultFailed));

            var handler = new ConfirmEmailAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Ocorreu um erro ao confirmar o e-mail.", result.Message);
        }
    }
}
