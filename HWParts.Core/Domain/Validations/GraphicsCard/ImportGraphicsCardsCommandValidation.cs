using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ImportGraphicsCardsCommandValidation : AbstractValidator<ImportGraphicsCardsCommand>
    {
        public ImportGraphicsCardsCommandValidation()
        {
            ValidateFile();
        }

        public void ValidateFile()
        {
            RuleFor(c => c.File)
                .NotEmpty().WithMessage("O arquivo é obrigatório.");
        }
    }
}
