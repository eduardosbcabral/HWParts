using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportCasesCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportCasesCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportCaseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
