using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateMotherboardCommandValidation : MotherboardValidation<UpdateMotherboardCommand>
    {
        public UpdateMotherboardCommandValidation()
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
