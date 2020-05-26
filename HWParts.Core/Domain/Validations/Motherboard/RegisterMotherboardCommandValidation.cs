using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterMotherboardCommandValidation : MotherboardValidation<RegisterMotherboardCommand>
    {
        public RegisterMotherboardCommandValidation()
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
