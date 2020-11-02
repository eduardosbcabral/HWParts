using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ResetPasswordAccountValidation : AbstractValidator<ResetPasswordAccount>
    {
        public ResetPasswordAccountValidation()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateCode();
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail está em um formato inválido.");
        }

        protected void ValidatePassword()
        {
            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }

        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("'{PropertyName}' é obrigatório.");
        }
    }
}
