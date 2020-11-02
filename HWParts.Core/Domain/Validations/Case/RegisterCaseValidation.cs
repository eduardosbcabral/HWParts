using FluentValidation;
using HWParts.Core.Domain.Commands;
using System;

namespace HWParts.Core.Domain.Validations
{
    public class RegisterCaseValidation : AbstractValidator<RegisterCaseCommand>
    {
        public RegisterCaseValidation()
        {
            ValidateBrand();
            ValidateModel();
            ValidatePlatformId();
            ValidateImageUrl();
            ValidateUrl();
            ValidatePlatform();
        }

        protected void ValidateBrand()
        {
            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("A marca � obrigat�ria.")
                .Length(2, 150).WithMessage("A marca deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidateModel()
        {
            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("O modelo � obrigat�rio.")
                .Length(2, 150).WithMessage("O modelo deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidatePlatformId()
        {
            RuleFor(c => c.PlatformId)
                .NotEmpty().WithMessage("O ID da plataforma � obrigat�rio.")
                .Length(2, 150).WithMessage("O ID da plataforma deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidateImageUrl()
        {
            RuleFor(c => c.ImageUrl)
                .NotEmpty().WithMessage("A URL da imagem � obrigat�ria.");
        }

        protected void ValidateUrl()
        {
            RuleFor(c => c.Url)
                .NotEmpty().WithMessage("A URL do componente � obrigat�ria.")
                .Must(BeAValidUrl)
                .WithMessage("URL do componente informada n�o � v�lida.");
        }

        protected void ValidatePlatform()
        {
            RuleFor(c => c.Platform)
                .IsInEnum().WithMessage("A plataforma � obrigat�ria.");
        }

        private static bool BeAValidUrl(string arg)
        {
            return Uri.TryCreate(arg, UriKind.Absolute, out _);
        }
    }
}
