using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class MemoryRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public MemoryRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}