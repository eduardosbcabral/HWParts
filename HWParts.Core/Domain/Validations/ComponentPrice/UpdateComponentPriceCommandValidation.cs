using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class UpdateComponentPriceCommandValidation : ComponentPriceValidation<UpdateComponentPriceCommand>
    {
        public UpdateComponentPriceCommandValidation()
        {
            ValidatePlatform();
            ValidatePrice();
        }
    }
}
