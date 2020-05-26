using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class RemoveMotherboardCommandValidation : MotherboardValidation<RemoveMotherboardCommand>
    {
        public RemoveMotherboardCommandValidation()
        {
            ValidateId();
        }
    }
}
