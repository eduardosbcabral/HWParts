using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Validations;
using Microsoft.AspNetCore.Http;

namespace HWParts.Core.Domain.Commands
{
    public class ImportGraphicsCardsCommand : Command
    {
        public IFormFile File { get; set; }

        public ImportGraphicsCardsCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new ImportGraphicsCardsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
