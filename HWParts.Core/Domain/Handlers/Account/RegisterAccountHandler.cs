﻿using Flunt.Notifications;
using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RegisterAccountHandler : IRequestHandler<RegisterAccount, CommandResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public RegisterAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResponse> Handle(RegisterAccount command, CancellationToken cancellationToken)
        {
            var user = new Account(
                command.UserName,
                command.Email);

            var result = await _accountRepository.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var response = new ErrorCommandResponse("Erro ao cadastrar o usuário.");
                foreach (var error in result.Errors)
                {
                    response.AddNotification(null, error.Description);
                }

                return response;
            }

            var addToRoleResult = await _accountRepository.AddToRoleAsync(user, "Common");
            if (!addToRoleResult.Succeeded)
            {
                var response = new ErrorCommandResponse("Erro ao adicionar o cargo ao usuário.");
                foreach (var error in addToRoleResult.Errors)
                {
                    response.AddNotification(null, error.Description);
                }

                return response;
            }

            return new SuccessCommandResponse(
                "Usuário cadastrado com sucesso.",
                new
                {
                    user.Id
                });
        }
    }
}