using HWParts.Core.Domain.CommandHandlers;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using HWParts.Core.Infrastructure;
using HWParts.Tests.FakeObjects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HWParts.Tests.Handlers.Accounts
{
    public class RegisterAccountHandlerTests
    {
        [Fact]
        public async Task Should_not_have_notifications()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();

            var fakeIdentityResultSuccess = IdentityResult.Success;
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(fakeIdentityResultSuccess));
            fakeAccountRepository.Setup(x => x.AddToRoleAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(fakeIdentityResultSuccess));

            var handler = new RegisterAccountCommandHandler(fakeAccountRepository.Object);

            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");

            var result = await handler.Handle(fakeCommand, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Empty(result.Notifications);
            Assert.Equal("Usuário cadastrado com sucesso.", result.Message);
            Assert.IsType<SuccessCommandResponse>(result);
        }

        [Fact]
        public async Task Should_have_notification_when_create_user_returns_failure()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();

            var identityResultFailed = IdentityResult.Failed(new IdentityError());
            var handler = new RegisterAccountCommandHandler(fakeAccountRepository.Object);
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultFailed));

            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");

            var result = await handler.Handle(fakeCommand, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Erro ao cadastrar o usuário.", result.Message);
            Assert.Single(result.Notifications);
            Assert.IsType<ErrorCommandResponse>(result);
        }

        [Fact]
        public async Task Should_have_notification_when_add_to_role_returns_failure()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();

            var identityResultFailed = IdentityResult.Failed(new IdentityError());
            var handler = new RegisterAccountCommandHandler(fakeAccountRepository.Object);
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            fakeAccountRepository.Setup(x => x.AddToRoleAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultFailed));

            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");

            var result = await handler.Handle(fakeCommand, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Erro ao adicionar o cargo ao usuário.", result.Message);
            Assert.Single(result.Notifications);
            Assert.IsType<ErrorCommandResponse>(result);
        }
    }
}
