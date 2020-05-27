using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveProcessorCommandValidation : ProcessorValidation<ProcessorCommand>
    {
        public RemoveProcessorCommandValidation()
        {
            ValidateId();
        }
    }
}
