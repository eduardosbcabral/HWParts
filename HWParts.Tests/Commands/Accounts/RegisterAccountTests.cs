﻿using FluentValidation.TestHelper;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;
using Xunit;

namespace HWParts.Tests.Commands.Accounts
{
    public class RegisterAccountTests
    {
        private readonly RegisterAccount command;
        private readonly RegisterAccountValidation validator;

        public RegisterAccountTests()
        {
            validator = new RegisterAccountValidation();
            command = new RegisterAccount(
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
