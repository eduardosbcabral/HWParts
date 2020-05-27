using System;

namespace HWParts.Core.Domain.Core.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public EntityBase(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EntityBase compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
