using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ConfirmEmailAccountValidation : AbstractValidator<ConfirmEmailAccount>
    {
        public ConfirmEmailAccountValidation()
        {
            ValidateId();
            ValidateCode();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("'{PropertyName}' é obrigatório.");
        }

        protected void ValidateCode()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage("O Token é obrigatório.");
        }
    }
}
