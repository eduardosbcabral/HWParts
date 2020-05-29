using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemovePowerSupplyCommandValidation : PowerSupplyValidation<PowerSupplyCommand>
    {
        public RemovePowerSupplyCommandValidation()
        {
            ValidateId();
        }
    }
}
