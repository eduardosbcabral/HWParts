using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class AccountValidation<T> : AbstractValidator<T> where T : AccountCommand
    {
        
    }
}
