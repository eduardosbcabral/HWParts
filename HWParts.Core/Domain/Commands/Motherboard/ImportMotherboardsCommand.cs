using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportMotherboardsCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportMotherboardsCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportMotherboardsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
