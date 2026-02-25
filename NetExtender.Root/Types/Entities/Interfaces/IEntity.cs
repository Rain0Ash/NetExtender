// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Entities.Interfaces
{
    public interface IEventsEntity<out TEvent, out TEvents> : IEntity where TEvents : class, IReadOnlyCollection<TEvent>
    {
        Boolean IEntity.HasEvents
        {
            get
            {
                return Events is { Count: > 0 };
            }
        }

        public new TEvents? Events
        {
            get
            {
                return null;
            }
        }
    }

    public interface IEntityId<out T> : IEntity<T>
    {
        public T Id { get; }

        T IEntity<T>.Get()
        {
            return Id;
        }
    }

    public interface IEntityValue<out T> : IEntity<T>
    {
        public T Value { get; }

        T IEntity<T>.Get()
        {
            return Value;
        }
    }

    public interface IEntity<out T> : IEntity
    {
        public T Get();
    }

    public interface IEntity
    {
        public Type Self
        {
            get
            {
                return GetType();
            }
        }

        public Boolean HasEvents
        {
            get
            {
                return Events is { Count: > 0 };
            }
        }

        public IReadOnlyCollection<IEntity>? Events
        {
            get
            {
                return null;
            }
        }

        public Boolean CanDelete
        {
            get
            {
                return true;
            }
        }

        public void Delete()
        {
            if (!CanDelete)
            {
                //TODO: ToBusinessException
                throw new InvalidOperationException("Cannot delete entity.");
            }
        }
    }
}