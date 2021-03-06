﻿using FluentValidation;
using HWParts.Core.Domain.Commands;
using System;

namespace HWParts.Core.Domain.Validations
{
    public class MemoryValidation<T> : AbstractValidator<T> where T : MemoryCommand
    {
        protected void ValidateBrand()
        {
            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("A marca é obrigatória.")
                .Length(2, 150).WithMessage("A marca deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidateModel()
        {
            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .Length(2, 150).WithMessage("O modelo deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidatePlatformId()
        {
            RuleFor(c => c.PlatformId)
                .NotEmpty().WithMessage("O ID da plataforma é obrigatório.")
                .Length(2, 150).WithMessage("O ID da plataforma deve conter entre 2 and 10 caracteres.");
        }

        protected void ValidateImageUrl()
        {
            RuleFor(c => c.ImageUrl)
                .NotEmpty().WithMessage("A URL da imagem é obrigatória.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.ImageUrl));
        }

        protected void ValidateUrl()
        {
            RuleFor(c => c.Url)
                .NotEmpty().WithMessage("A URL do componente é obrigatória.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.ImageUrl));
        }

        protected void ValidatePlatform()
        {
            RuleFor(c => c.Platform)
                .IsInEnum().WithMessage("A plataforma é obrigatória.");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
