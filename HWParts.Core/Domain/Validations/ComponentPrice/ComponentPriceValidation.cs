using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ComponentPriceValidation<T> : AbstractValidator<T> where T : ComponentPriceCommand
    {
        public void ValidatePrice()
        {
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("O preço é obrigatório.");
        }

        public void ValidatePlatform()
        {
            RuleFor(c => c.Platform)
                .NotEmpty().WithMessage("A plataforma é obrigatória.");
        }
    }
}
