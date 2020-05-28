using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateStorageCommandValidation : StorageValidation<StorageCommand>
    {
        public UpdateStorageCommandValidation()
        {
            ValidateId();
            ValidateBrand();
            ValidateModel();
            ValidatePlatformId();
            ValidateImageUrl();
            ValidateUrl();
            ValidatePlatform();
        }
    }
}
