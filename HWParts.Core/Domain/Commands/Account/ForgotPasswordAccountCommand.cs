using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class ForgotPasswordAccountCommand : AccountCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new ForgotPasswordAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
