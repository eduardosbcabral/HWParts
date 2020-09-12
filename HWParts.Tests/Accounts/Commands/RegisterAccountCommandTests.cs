using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;
using Xunit;

namespace HWParts.Tests.Accounts.Commands
{
    public class RegisterAccountCommandTests
    {
        private readonly RegisterAccountCommand command;
        private readonly RegisterAccountCommandValidation validator;

        public RegisterAccountCommandTests()
        {
            validator = new RegisterAccountCommandValidation();
            command = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");
        }

        [Fact]
        public void Should_have_error_when_username_is_empty()
        {
            command.UserName = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserName)
                .WithErrorMessage("Nome de usuário é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_username_is_null()
        {
            command.UserName = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserName)
                .WithErrorMessage("Nome de usuário é obrigatório.");
        }

        [Fact]
        public void Should_not_have_error_when_username_is_specified()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Should_have_error_when_email_is_empty()
        {
            command.Email = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-Mail é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_email_is_null()
        {
            command.Email = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-Mail é obrigatório.");
        }

        [Fact]
        public void Should_not_have_error_when_email_is_specified()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_have_error_when_password_is_empty()
        {
            command.Password = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Senha é obrigatória.");
        }

        [Fact]
        public void Should_have_error_when_password_is_null()
        {
            command.Password = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Senha é obrigatória.");
        }

        [Fact]
        public void Should_not_have_error_when_password_is_specified()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Command_is_valid()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
