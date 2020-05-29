using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class PowerSupplyRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public PowerSupplyRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
