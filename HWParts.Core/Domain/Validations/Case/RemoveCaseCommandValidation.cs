using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveCaseCommandValidation : CaseValidation<CaseCommand>
    {
        public RemoveCaseCommandValidation()
        {
            ValidateId();
        }
    }
}
