using System;
using NetExtender.CQRS.Events.Interfaces;

namespace NetExtender.CQRS.Events
{
    public abstract record BeforeSaveIdEventCQRS<T> : BeforeIdEventCQRS<T>, IBeforeSaveIdEventCQRS<T>
    {
        protected BeforeSaveIdEventCQRS()
        {
        }

        protected BeforeSaveIdEventCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BeforeSaveValueEventCQRS<T> : BeforeValueEventCQRS<T>, IBeforeSaveValueEventCQRS<T>
    {
        protected BeforeSaveValueEventCQRS()
        {
        }

        protected BeforeSaveValueEventCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BeforeSaveEventCQRS : BeforeEventCQRS, IBeforeSaveEventCQRS
    {
    }

    public abstract record BeforeIdEventCQRS<T> : IdEventCQRS<T>, IBeforeIdEventCQRS<T>
    {
        protected BeforeIdEventCQRS()
        {
        }

        protected BeforeIdEventCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record BeforeValueEventCQRS<T> : ValueEventCQRS<T>, IBeforeValueEventCQRS<T>
    {
        protected BeforeValueEventCQRS()
        {
        }

        protected BeforeValueEventCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record BeforeEventCQRS : EventCQRS, IBeforeEventCQRS
    {
    }

    public abstract record AfterSaveIdEventCQRS<T> : AfterIdEventCQRS<T>, IAfterSaveIdEventCQRS<T>
    {
        protected AfterSaveIdEventCQRS()
        {
        }

        protected AfterSaveIdEventCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record AfterSaveValueEventCQRS<T> : AfterValueEventCQRS<T>, IAfterSaveValueEventCQRS<T>
    {
        protected AfterSaveValueEventCQRS()
        {
        }

        protected AfterSaveValueEventCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record AfterSaveEventCQRS : AfterEventCQRS, IAfterSaveEventCQRS
    {
    }

    public abstract record AfterIdEventCQRS<T> : IdEventCQRS<T>, IAfterIdEventCQRS<T>
    {
        protected AfterIdEventCQRS()
        {
        }

        protected AfterIdEventCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record AfterValueEventCQRS<T> : ValueEventCQRS<T>, IAfterValueEventCQRS<T>
    {
        protected AfterValueEventCQRS()
        {
        }

        protected AfterValueEventCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record AfterEventCQRS : EventCQRS, IAfterEventCQRS
    {
    }

    public abstract record IdEventCQRS<T> : IdEntityCQRS<T>, IIdEventCQRS<T>
    {
        public Boolean Resolved { get; set; }

        protected IdEventCQRS()
        {
        }

        protected IdEventCQRS(T id)
            : base(id)
        {
        }
    }

    public abstract record ValueEventCQRS<T> : ValueEntityCQRS<T>, IValueEventCQRS<T>
    {
        public Boolean Resolved { get; set; }

        protected ValueEventCQRS()
        {
        }

        protected ValueEventCQRS(T value)
            : base(value)
        {
        }
    }

    public abstract record EventCQRS : EntityCQRS, IEventCQRS
    {
        public Boolean Resolved { get; set; }
    }
}