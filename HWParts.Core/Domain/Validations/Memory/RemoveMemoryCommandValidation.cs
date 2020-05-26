using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveMemoryCommandValidation : MemoryValidation<RemoveMemoryCommand>
    {
        public RemoveMemoryCommandValidation()
        {
            ValidateId();
        }
    }
}
