using HWParts.Api.Controllers;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace HWParts.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Register_account_returns_success()
        {
            var fakeCommand = new RegisterAccountCommand("TestUser", "test_user@test.com", "123456");

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Register(fakeCommand)).Returns(Task.FromResult(true));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Register(fakeCommand);
            
            Assert.IsType<OkObjectResult>(requestResult);

            var resultValue = requestResult.GetValue<bool>();
            Assert.True(resultValue);
        }
    }
}
