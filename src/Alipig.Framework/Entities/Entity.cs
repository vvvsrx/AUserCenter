using System;
using System.Runtime.Serialization;

namespace Alipig.Framework.Entities
{
    public abstract class Entity : Entity<Guid>
    {
    }

    [DataContract]
    [Serializable]
    public abstract class Entity<TId>
    {

        [DataMember]
        public virtual TId ID { get; protected set; }
        public virtual int Version { get; set; }
        [DataMember]
        public virtual int AutoId { get; protected set; }
        [DataMember]
        public virtual int IsDel { get; set; }
        [DataMember]
        public virtual DateTime CreateTime { get; set; }
        [DataMember]
        public virtual DateTime UpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null &&
                   Equals(obj.ID, default(TId));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(ID, other.ID))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(ID, default(TId)))
                return base.GetHashCode();
            return ID.GetHashCode();
        }
    }
}
