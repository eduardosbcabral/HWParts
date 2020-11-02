using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;
using Xunit;

namespace HWParts.Tests.Commands.Accounts
{
    public class ConfirmEmailAccountTests
    {
        private readonly ConfirmEmailAccount command;
        private readonly ConfirmEmailAccountValidation validator;

        public ConfirmEmailAccountTests()
        {
            validator = new ConfirmEmailAccountValidation();
            command = new ConfirmEmailAccount(
                "id_123",
                "code_123");
        }

        [Fact]
        public void Should_have_error_when_id_is_empty()
        {
            command.Id = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("'Id' é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_id_is_null()
        {
            command.Id = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("'Id' é obrigatório.");
        }

        [Fact]
        public void Should_not_have_error_when_id_is_specified()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_have_error_when_code_is_empty()
        {
            command.Code = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("O Token é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_code_is_null()
        {
            command.Code = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("O Token é obrigatório.");
        }

        [Fact]
        public void Should_not_have_error_when_code_is_specified()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Command_is_valid()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
