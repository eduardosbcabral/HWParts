using FluentValidation.Results;
using HWParts.Core.Domain.Core.Events;
using MediatR;
using System;

namespace HWParts.Core.Domain.Core.Commands
{
    public abstract class Command : Message, IRequest<ValidationResult>, IBaseRequest
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid();
    }
}
