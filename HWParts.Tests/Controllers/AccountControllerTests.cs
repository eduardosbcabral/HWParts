using HWParts.Api.Controllers;
using HWParts.Core.Application.Interfaces;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace HWParts.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Register_account_returns_success()
        {
            var fakeCommand = new RegisterAccount("TestUser", "test_user@test.com", "123456");

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Register(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Register(fakeCommand);
            
            Assert.IsType<OkObjectResult>(requestResult);
        }

        [Fact]
        public async Task Register_account_returns_bad_request()
        {
            var fakeCommand = new RegisterAccount("TestUser", "test_user@test.com", "123456");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Register(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Register(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);
        }

        [Fact]
        public async Task Login_account_returns_success()
        {
            var fakeCommand = new LoginAccount("username", "password");

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Login(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Login(fakeCommand);

            Assert.IsType<OkObjectResult>(requestResult);
        }

        [Fact]
        public async Task Login_account_returns_bad_request()
        {
            var fakeCommand = new LoginAccount("username", "password");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.Login(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.Login(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);
        }

        [Fact]
        public async Task Confirm_email_account_returns_success()
        {
            var fakeCommand = new ConfirmEmailAccount("id_123", "code_123");

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ConfirmEmail(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ConfirmEmail(fakeCommand);

            Assert.IsType<OkObjectResult>(requestResult);
        }

        [Fact]
        public async Task Confirm_email_account_returns_bad_request()
        {
            var fakeCommand = new ConfirmEmailAccount("id_123", "code_123");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ConfirmEmail(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ConfirmEmail(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);
        }

        [Fact]
        public async Task Forgot_password_account_returns_success()
        {
            var fakeCommand = new ForgotPasswordAccount("test@test.com");

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ForgotPassword(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ForgotPassword(fakeCommand);

            Assert.IsType<OkObjectResult>(requestResult);
        }

        [Fact]
        public async Task Forgot_password_account_returns_bad_request()
        {
            var fakeCommand = new ForgotPasswordAccount("test@test.com");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ForgotPassword(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ForgotPassword(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);
        }

        [Fact]
        public async Task Reset_password_account_returns_success()
        {
            var fakeCommand = new ResetPasswordAccount("test@test.com", "password", "code");

            CommandResponse commandResponse = new SuccessCommandResponse();

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ResetPassword(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ResetPassword(fakeCommand);

            Assert.IsType<OkObjectResult>(requestResult);
        }

        [Fact]
        public async Task Reset_password_account_returns_bad_request()
        {
            var fakeCommand = new ResetPasswordAccount("test@test.com", "password", "code");

            CommandResponse commandResponse = new ErrorCommandResponse();
            commandResponse.AddNotification(null);

            var fakeAccountService = new Mock<IAccountAppService>();
            fakeAccountService.Setup(x => x.ResetPassword(fakeCommand))
                .Returns(Task.FromResult(commandResponse));

            var accountController = new AccountController(fakeAccountService.Object);

            var requestResult = await accountController.ResetPassword(fakeCommand);

            Assert.IsType<BadRequestObjectResult>(requestResult);
        }
    }
}
