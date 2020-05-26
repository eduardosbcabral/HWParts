using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateMemoryCommandValidation : MemoryValidation<UpdateMemoryCommand>
    {
        public UpdateMemoryCommandValidation()
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
