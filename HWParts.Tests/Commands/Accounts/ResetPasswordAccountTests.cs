using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HWParts.Tests.Commands.Accounts
{
    public class ResetPasswordAccountTests
    {
        private readonly ResetPasswordAccount command;
        private readonly ResetPasswordAccountValidation validator;

        public ResetPasswordAccountTests()
        {
            validator = new ResetPasswordAccountValidation();
            command = new ResetPasswordAccount(
                "teste@teste.com.br",
                "password",
                "code"
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
        public void Should_have_error_when_code_is_empty()
        {
            command.Code = string.Empty;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("'Code' é obrigatório.");
        }

        [Fact]
        public void Should_have_error_when_code_is_null()
        {
            command.Code = null;
            var result = validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("'Code' é obrigatório.");
        }

        [Fact]
        public void Command_is_valid()
        {
            var result = validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
