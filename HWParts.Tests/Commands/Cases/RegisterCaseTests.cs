using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Validations;
using Xunit;

namespace HWParts.Tests.Commands.Cases
{
    public class RegisterCaseTests
    {
        private readonly RegisterCaseCommand command;
        private readonly RegisterCaseValidation validator;

        public RegisterCaseTests()
        {
            validator = new RegisterCaseValidation();
            command = new RegisterCaseCommand(
                "12345678",
                "http://urlimagem.com",
                "http://url.com",
                EPlatform.NewEgg,
                "Brand test",
                "Model test");
        }

        [Fact]
        public void Should_have_error_when_platform_id_is_empty()
        {
            command.PlatformId = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PlatformId)
                .WithErrorMessage("O ID da plataforma é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_platform_id_is_null()
        {
            command.PlatformId = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.PlatformId)
                .WithErrorMessage("O ID da plataforma é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_image_url_is_empty()
        {
            command.ImageUrl = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ImageUrl)
                .WithErrorMessage("A URL da imagem é obrigatória.");
        }

        [Fact]
        public void Should_have_error_when_image_url_is_null()
        {
            command.ImageUrl = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ImageUrl)
                .WithErrorMessage("A URL da imagem é obrigatória.");
        }

        [Fact]
        public void Should_have_error_when_url_is_empty()
        {
            command.Url = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Url)
                .WithErrorMessage("A URL do componente é obrigatória.");
        }

        [Fact]
        public void Should_have_error_when_url_is_null()
        {
            command.Url = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Url)
                .WithErrorMessage("A URL do componente é obrigatória.");
        }

        [Fact]
        public void Should_have_error_when_url_is_invalid()
        {
            command.Url = "isnoturl";
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Url)
                .WithErrorMessage("URL do componente informada não é válida.");
        }

        [Fact]
        public void Command_is_valid()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
