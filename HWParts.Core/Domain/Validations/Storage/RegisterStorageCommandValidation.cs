using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterStorageCommandValidation : StorageValidation<StorageCommand>
    {
        public RegisterStorageCommandValidation()
        {
            ValidateBrand();
            ValidateModel();
            ValidatePlatformId();
            ValidateImageUrl();
            ValidateUrl();
            ValidatePlatform();
        }
    }
}
