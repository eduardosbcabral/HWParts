using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterMemoryCommandValidation : MemoryValidation<RegisterMemoryCommand>
    {
        public RegisterMemoryCommandValidation()
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
