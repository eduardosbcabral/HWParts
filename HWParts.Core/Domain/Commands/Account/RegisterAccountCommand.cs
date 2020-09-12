﻿using FluentValidation.Results;
using HWParts.Core.Domain.Validations;
using MediatR;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterAccountCommand : AccountCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterAccountCommand()
        {

        }

        public RegisterAccountCommand(string username, string email, string password)
        {
            UserName = username;
            Email = email;
            Password = password;
        }
    }
}
