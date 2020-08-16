using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class RemoveStorageCommand : StorageCommand
    {
        public RemoveStorageCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveStorageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
