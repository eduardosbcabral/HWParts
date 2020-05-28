using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class StorageRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public StorageRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
