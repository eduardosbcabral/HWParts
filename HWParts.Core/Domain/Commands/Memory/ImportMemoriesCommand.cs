using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportMemoriesCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportMemoriesCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportMemoriesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
