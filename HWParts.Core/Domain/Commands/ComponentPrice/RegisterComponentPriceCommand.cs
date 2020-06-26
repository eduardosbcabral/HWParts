using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterComponentPriceCommand : ComponentPriceCommand
    {
        public RegisterComponentPriceCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterComponentPriceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
