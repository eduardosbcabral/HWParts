using System;

namespace HWParts.Core.Domain.Events
{
    public class MotherboardRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public MotherboardRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
