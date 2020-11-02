using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HWParts.Tests.Commands.Accounts
{
    public class ForgotPasswordAccountTests
    {
        private readonly ForgotPasswordAccount command;
        private readonly ForgotPasswordAccountValidation validator;

        public ForgotPasswordAccountTests()
        {
            validator = new ForgotPasswordAccountValidation();
            command = new ForgotPasswordAccount(
                "teste@teste.com.br"
            );
        }

        [Fact]
        public void Should_have_error_when_email_is_empty()
        {
            command.Email = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-mail é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_email_is_null()
        {
            command.Email = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-mail é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_email_is_invalid()
        {
            command.Email = "teste.com";
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("O e-mail está em um formato inválido.");
        }

        [Fact]
        public void Command_is_valid()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
