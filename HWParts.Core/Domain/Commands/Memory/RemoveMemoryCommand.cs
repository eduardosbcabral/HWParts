using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveMemoryCommand : MemoryCommand
    {
        public RemoveMemoryCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveMemoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
