using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterGraphicsCardCommandValidation : GraphicsCardValidation<GraphicsCardCommand>
    {
        public RegisterGraphicsCardCommandValidation()
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
