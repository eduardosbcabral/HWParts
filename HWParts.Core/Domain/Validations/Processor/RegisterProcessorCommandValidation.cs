using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterProcessorCommandValidation : ProcessorValidation<ProcessorCommand>
    {
        public RegisterProcessorCommandValidation()
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
