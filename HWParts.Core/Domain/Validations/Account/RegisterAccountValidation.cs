using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterAccountValidation : AbstractValidator<RegisterAccount>
    {
        public RegisterAccountValidation()
        {
            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
        }

        protected void ValidateUsername()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório.");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-Mail é obrigatório.");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
