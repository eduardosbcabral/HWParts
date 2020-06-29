using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveComponentPriceCommand : ComponentPriceCommand
    {
        public RemoveComponentPriceCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveComponentPriceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
