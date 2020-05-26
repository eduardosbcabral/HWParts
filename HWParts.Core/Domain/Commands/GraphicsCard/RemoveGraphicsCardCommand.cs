using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveGraphicsCardCommand : GraphicsCardCommand
    {
        public RemoveGraphicsCardCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveGraphicsCardCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
