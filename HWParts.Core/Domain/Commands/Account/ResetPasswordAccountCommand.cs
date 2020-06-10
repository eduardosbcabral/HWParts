using HWParts.Core.Domain.Validations;

namespace HWParts.Core.Domain.Commands
{
    public class ResetPasswordAccountCommand : AccountCommand
    {
        public string Code { get; set; }

        public ResetPasswordAccountCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ResetPasswordAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}