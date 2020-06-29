using FluentValidation;
using HWParts.Core.Domain.Commands;
using System;

namespace HWParts.Core.Domain.Validations
{
    public class ComponentPriceValidation<T> : AbstractValidator<T> where T : ComponentPriceCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public void ValidatePrice()
        {
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("O preço é obrigatório.");
        }

        public void ValidatePlatform()
        {
            RuleFor(c => c.Platform)
                .IsInEnum().WithMessage("A plataforma é obrigatória.");
        }
    }
}
