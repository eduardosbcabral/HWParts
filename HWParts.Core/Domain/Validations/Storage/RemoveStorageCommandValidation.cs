using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveStorageCommandValidation : StorageValidation<StorageCommand>
    {
        public RemoveStorageCommandValidation()
        {
            ValidateId();
        }
    }
}
