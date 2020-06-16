using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportPowerSuppliesCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportPowerSuppliesCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportPowerSuppliesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
