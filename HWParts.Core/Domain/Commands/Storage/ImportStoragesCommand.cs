using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportStoragesCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportStoragesCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportStoragesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
