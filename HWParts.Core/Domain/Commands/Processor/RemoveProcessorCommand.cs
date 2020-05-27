using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveProcessorCommand : ProcessorCommand
    {
        public RemoveProcessorCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveProcessorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
