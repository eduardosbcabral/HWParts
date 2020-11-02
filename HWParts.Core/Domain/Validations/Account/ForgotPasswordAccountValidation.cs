using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ForgotPasswordAccountValidation : AbstractValidator<ForgotPasswordAccount>
    {
        public ForgotPasswordAccountValidation()
        {
            ValidateEmail();
        }

        public void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail está em um formato inválido.");
        }
    }
}
