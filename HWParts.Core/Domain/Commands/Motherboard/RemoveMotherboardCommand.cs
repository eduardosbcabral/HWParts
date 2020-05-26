using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveMotherboardCommand : MotherboardCommand
    {
        public RemoveMotherboardCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveMotherboardCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
