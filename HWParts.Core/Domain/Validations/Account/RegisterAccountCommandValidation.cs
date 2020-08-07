using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterAccountCommandValidation : AccountValidation<RegisterAccountCommand>
    {
        public RegisterAccountCommandValidation()
        {
            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
        }
    }
}
