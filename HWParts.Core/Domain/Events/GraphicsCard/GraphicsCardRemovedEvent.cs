using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class GraphicsCardRemovedEvent : Event
    {
        public Guid Id { get; set; }

        public GraphicsCardRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
