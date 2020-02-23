using System;
using System.Collections.Generic;
using System.Text;

namespace HWParts.Core.Domain.Entities
{
    public abstract class EntityBase
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
