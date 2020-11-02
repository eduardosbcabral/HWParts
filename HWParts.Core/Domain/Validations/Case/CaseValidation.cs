using FluentValidation;
using HWParts.Core.Domain.Commands;
using System;

namespace HWParts.Core.Domain.Validations
{
    public class CaseValidation<T> : AbstractValidator<T> where T : CaseCommand
    {
    }
}
