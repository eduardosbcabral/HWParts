using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class ComponentPriceRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public ComponentPriceRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
