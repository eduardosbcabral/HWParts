using FluentValidation;
using HWParts.Core.Domain.Commands;

namespace HWParts.Core.Domain.Validations
{
    public class ImportStoragesCommandValidation : AbstractValidator<ImportStoragesCommand>
    {
        public ImportStoragesCommandValidation()
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
