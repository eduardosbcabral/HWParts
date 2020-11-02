using HWParts.Core.Domain.Handlers;
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
        private readonly Mock<IAccountRepository> fakeAccountRepository;
        private readonly RegisterAccount command;
        private readonly IdentityResult identityResultSuccess;
        private readonly IdentityResult identityResultFailure;

        public RegisterAccountHandlerTests()
        {
            fakeAccountRepository = new Mock<IAccountRepository>();
            command = new RegisterAccount(
                "testUser",
                "test_user@test.com",
                "123456");
            identityResultSuccess = IdentityResult.Success;
            identityResultFailure = IdentityResult.Failed(new IdentityError());
        }

        [Fact]
        public async Task Should_not_have_notifications()
        {
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultSuccess));
            fakeAccountRepository.Setup(x => x.AddToRoleAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultSuccess));

            var handler = new RegisterAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Empty(result.Notifications);
            Assert.Equal("Usuário cadastrado com sucesso.", result.Message);
            Assert.IsType<SuccessCommandResponse>(result);
        }

        [Fact]
        public async Task Should_have_notification_when_create_user_returns_failure()
        {
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultFailure));

            var handler = new RegisterAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Erro ao cadastrar o usuário.", result.Message);
            Assert.NotEmpty(result.Notifications);
            Assert.IsType<ErrorCommandResponse>(result);
        }

        [Fact]
        public async Task Should_have_notification_when_add_to_role_returns_failure()
        {
            fakeAccountRepository.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            fakeAccountRepository.Setup(x => x.AddToRoleAsync(It.IsAny<Account>(), It.IsAny<string>()))
                .Returns(Task.FromResult(identityResultFailure));

            var handler = new RegisterAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Erro ao adicionar o cargo ao usuário.", result.Message);
            Assert.NotEmpty(result.Notifications);
            Assert.IsType<ErrorCommandResponse>(result);
        }
    }
}
