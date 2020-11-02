using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Handlers;
using HWParts.Core.Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HWParts.Tests.Handlers.Accounts
{
    public class ForgotPasswordAccountHandlerTests
    {
        private readonly Mock<IAccountRepository> fakeAccountRepository;
        private readonly Mock<Account> fakeAccount;
        private readonly ForgotPasswordAccount command;

        public ForgotPasswordAccountHandlerTests()
        {
            fakeAccountRepository = new Mock<IAccountRepository>();
            fakeAccount = new Mock<Account>();
            command = new ForgotPasswordAccount(
                "teste@teste.com");
        }

        [Fact]
        public async Task Should_not_return_error()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);
            fakeAccountRepository.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<Account>()))
                .ReturnsAsync(true);
            fakeAccountRepository.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<Account>()))
                .ReturnsAsync("code");

            var handler = new ForgotPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Valid);
            Assert.Equal("Para trocar a sua senha acesse o link que foi enviado para o seu e-mail.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_find_by_email_returns_null()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Account);

            var handler = new ForgotPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Não foi possível resetar a senha.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_is_email_confirmed_returns_false()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);
            fakeAccountRepository.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<Account>()))
                .ReturnsAsync(true);

            var handler = new ForgotPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Não foi possível resetar a senha.", result.Message);
        }

        [Fact]
        public async Task Should_return_error_when_generate_password_reset_token_returns_false()
        {
            fakeAccountRepository.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeAccount.Object);
            fakeAccountRepository.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<Account>()))
                .ReturnsAsync(true);
            fakeAccountRepository.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<Account>()))
                .ReturnsAsync(string.Empty);

            var handler = new ForgotPasswordAccountHandler(fakeAccountRepository.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Invalid);
            Assert.Equal("Não foi possível resetar a senha.", result.Message);
        }
    }
}
