using System;
using NetExtender.CQRS.Interfaces;

namespace NetExtender.CQRS
{
    public abstract record IdEntityCQRS<T> : EntityCQRS, IIdEntityCQRS<T>
    {
        public T Id { get; init; }

        protected IdEntityCQRS()
        {
            Id = default!;
        }

        protected IdEntityCQRS(T id)
        {
            Id = id;
        }
    }

    public abstract record ValueEntityCQRS<T> : EntityCQRS, IValueEntityCQRS<T>
    {
        public T Value { get; init; }

        protected ValueEntityCQRS()
        {
            Value = default!;
        }

        protected ValueEntityCQRS(T value)
        {
            Value = value;
        }
    }

    public abstract record EntityCQRS : IEntityCQRS
    {
        public virtual Boolean IsEmpty
        {
            get
            {
                return false;
            }
        }
    }
}