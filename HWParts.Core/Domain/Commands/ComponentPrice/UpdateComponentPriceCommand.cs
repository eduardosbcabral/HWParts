using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class UpdateComponentPriceCommand : ComponentPriceCommand
    {
        public UpdateComponentPriceCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateComponentPriceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}