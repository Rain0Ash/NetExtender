// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Entities
{
    public class EntityEventCollection : IEntityEventCollection
    {
        public static IEntityEventCollection Empty { get; } = new None();

        private List<IEventCQRS> Events { get; } = new List<IEventCQRS>(4);
        
        public Int32 Count
        {
            get
            {
                return Events.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Count <= 0;
            }
        }

        public void Add(IEventCQRS @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            Events.Add(@event);
        }
    
        public Boolean AddUnique<T>(T @event) where T : IEventCQRS
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if(Events.OfType<T>().Any())
            {
                return false;
            }

            Add(@event);
            return true;
        }

        public Boolean Remove(IEventCQRS @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return Events.Remove(@event);
        }
    
        public Boolean Remove<T>() where T : IEventCQRS
        {
            Boolean successful = false;
            foreach (T @event in Events.OfType<T>().ToArray())
            {
                Events.Remove(@event);
                successful = true;
            }
            
            return successful;
        }

        public void ResolveAll()
        {
            foreach (IEventCQRS @event in Events)
            {
                @event.Resolved = true;
            }
        }

        public List<IEventCQRS>.Enumerator GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        IEnumerator<IEventCQRS> IEnumerable<IEventCQRS>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private sealed class None : IEntityEventCollection
        {
            public Int32 Count
            {
                get
                {
                    return 0;
                }
            }

            public Boolean IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public Boolean IsEmpty
            {
                get
                {
                    return true;
                }
            }

            public void Add(IEventCQRS @event)
            {
                throw new ReadOnlyException();
            }

            public Boolean AddUnique<T>(T @event) where T : IEventCQRS
            {
                throw new ReadOnlyException();
            }

            public Boolean Remove(IEventCQRS @event)
            {
                return false;
            }

            public Boolean Remove<T>() where T : IEventCQRS
            {
                return false;
            }

            public void ResolveAll()
            {
            }

            public IEnumerator<IEventCQRS> GetEnumerator()
            {
                yield break;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}