using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ImportProcessorsCommandValidation : AbstractValidator<ImportProcessorsCommand>
    {
        public ImportProcessorsCommandValidation()
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