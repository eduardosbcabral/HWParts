using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class StorageRegisteredEvent : Event
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public StorageRegisteredEvent(Guid id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
        }
    }
}
