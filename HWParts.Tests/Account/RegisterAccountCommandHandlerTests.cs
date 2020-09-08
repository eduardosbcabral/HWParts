using HWParts.Core.Domain.CommandHandlers;
using HWParts.Core.Domain.Commands;
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

namespace HWParts.Tests
{
    public class RegisterAccountCommandHandlerTests
    {
        [Fact]
        public async Task Handler_return_success()
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
        }

        [Fact]
        public async Task Handler_return_false_when_command_is_invalid()
        {
            var fakeAccountRepository = new Mock<IAccountRepository>();
            var handler = new RegisterAccountCommandHandler(fakeAccountRepository.Object);

            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "");

            var result = await handler.Handle(fakeCommand, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal(1, handler.Notifications.Count);
        }

        [Fact]
        public async Task Handler_return_false_when_create_user_returns_error()
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

            Assert.True(result.Valid);
            Assert.Contains(handler.Notifications, x => x.Property == "CreateUser");
        }

        [Fact]
        public async Task Handler_return_false_when_add_to_role_returns_error()
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

            Assert.True(result.Valid);
            Assert.Contains(handler.Notifications, x => x.Property == "AddUserToRole");
        }
    }
}
