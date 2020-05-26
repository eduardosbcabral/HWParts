using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveGraphicsCardCommandValidation : GraphicsCardValidation<RemoveGraphicsCardCommand>
    {
        public RemoveGraphicsCardCommandValidation()
        {
            ValidateId();
        }
    }
}
