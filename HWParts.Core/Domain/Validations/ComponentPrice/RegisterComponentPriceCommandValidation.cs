using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterComponentPriceCommandValidation : ComponentPriceValidation<RegisterComponentPriceCommand>
    {
        public RegisterComponentPriceCommandValidation()
        {
            ValidatePrice();
            ValidatePlatform();
        }
    }
}
