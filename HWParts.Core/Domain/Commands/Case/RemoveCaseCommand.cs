using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveCaseCommand : CaseCommand
    {
        public RemoveCaseCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCaseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
