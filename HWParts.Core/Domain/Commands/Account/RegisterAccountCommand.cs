using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterAccountCommand : AccountCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new RegisterAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
