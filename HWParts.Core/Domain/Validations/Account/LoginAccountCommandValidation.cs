using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class LoginAccountCommandValidation : AbstractValidator<LoginAccountCommand>
    {
        public LoginAccountCommandValidation()
        {
            ValidateUsername();
            ValidatePassword();
        }

        protected void ValidateUsername()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório.");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
