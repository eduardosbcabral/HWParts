using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class LoginAccountValidation : AbstractValidator<LoginAccount>
    {
        public LoginAccountValidation()
        {
            ValidateUsername();
            ValidatePassword();
        }

        protected void ValidateUsername()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório.");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
