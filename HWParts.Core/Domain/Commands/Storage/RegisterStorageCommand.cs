using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterStorageCommand : StorageCommand
    {
        public RegisterStorageCommand(
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform,
            string brand,
            string model)
        {
            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Platform = platform;
            Brand = brand;
            Model = model;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterStorageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
