using HWParts.Core.Domain.CommandHandlers;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure;
using HWParts.Tests.FakeObjects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
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
            var fakeUserManager = new FakeUserManager();

            var fakeContext = new Mock<HWPartsDbContext>();

            var handler = new RegisterAccountCommandHandler(fakeUserManager, fakeContext.Object);

            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");

            var result = await handler.Handle(fakeCommand, CancellationToken.None);

            Assert.True(result);
        }
    }
}
