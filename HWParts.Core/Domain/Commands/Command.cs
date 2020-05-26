﻿using FluentValidation.Results;
using HWParts.Core.Domain.Events;
using System;

namespace HWParts.Core.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
