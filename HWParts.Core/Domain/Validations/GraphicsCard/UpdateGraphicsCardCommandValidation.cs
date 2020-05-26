using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateGraphicsCardCommandValidation : GraphicsCardValidation<UpdateGraphicsCardCommand>
    {
        public UpdateGraphicsCardCommandValidation()
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
