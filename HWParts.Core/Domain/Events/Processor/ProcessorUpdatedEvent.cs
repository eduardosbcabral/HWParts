using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class ProcessorUpdatedEvent : Event
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public ProcessorUpdatedEvent(Guid id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
        }
    }
}
