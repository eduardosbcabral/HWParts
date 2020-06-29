using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveComponentPriceCommandValidation : ComponentPriceValidation<RemoveComponentPriceCommand>
    {
        public RemoveComponentPriceCommandValidation()
        {
            ValidateId();
        }
    }
}
