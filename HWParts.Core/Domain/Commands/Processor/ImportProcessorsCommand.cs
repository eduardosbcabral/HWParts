using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportProcessorsCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportProcessorsCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportProcessorsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
