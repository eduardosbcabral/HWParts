using HWParts.Core.Domain.Validations;
using MediatR;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterAccountCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterAccountCommand(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        //public override bool IsValid()
        //{
        //    ValidationResult = new RegisterAccountCommandValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}
    }
}
