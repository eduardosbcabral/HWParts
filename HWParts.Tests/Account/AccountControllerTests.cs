using HWParts.Api.Controllers;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
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

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Register(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Register(fakeCommand);
            
            Assert.IsType<OkObjectResult>(requestResult);

            var response = requestResult.GetValue<SuccessCommandResponse>();
            Assert.NotNull(response);
            Assert.IsType<SuccessCommandResponse>(response);
        }

        [Fact]
        public async Task Register_account_returns_false()
        {
            var fakeCommand = new RegisterAccountCommand("TestUser", "test_user@test.com", "123456");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Register(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Register(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);

            var response = requestResult.GetValue<ErrorCommandResponse>();
            Assert.NotNull(response);
            Assert.IsType<ErrorCommandResponse>(response);
        }
    }
}
