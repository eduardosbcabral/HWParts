using HWParts.Core.Domain.Enums;
using HWParts.Core.Domain.Validations;
using System;

namespace HWParts.Core.Domain.Commands
{
    public class UpdateStorageCommand : StorageCommand
    {
        public UpdateStorageCommand(
            Guid id,
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform,
            string brand,
            string model)
        {
            Id = id;
            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Platform = platform;
            Brand = brand;
            Model = model;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateStorageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
