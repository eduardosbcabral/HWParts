using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class CaseRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public CaseRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
