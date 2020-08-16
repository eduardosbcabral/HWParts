using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemovePowerSupplyCommand : PowerSupplyCommand
    {
        public RemovePowerSupplyCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePowerSupplyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
