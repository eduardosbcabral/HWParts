using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class ProcessorRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public ProcessorRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
