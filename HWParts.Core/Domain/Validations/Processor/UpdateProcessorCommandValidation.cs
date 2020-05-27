using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateProcessorCommandValidation : ProcessorValidation<ProcessorCommand>
    {
        public UpdateProcessorCommandValidation()
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
