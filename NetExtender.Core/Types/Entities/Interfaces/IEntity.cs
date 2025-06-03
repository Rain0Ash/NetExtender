// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Entities.Interfaces
{
    public interface IEntityId<out T> : IEntity<T>
    {
        public T Id { get; }
    }
    
    public interface IEntityValue<out T> : IEntity<T>
    {
        public T Value { get; }
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
                return !Events.IsEmpty;
            }
        }

        public IEntityEventCollection Events
        {
            get
            {
                return EntityEventCollection.Empty;
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
                throw new InvalidOperationException("Cannot delete entity.");
            }
        }
    }
}