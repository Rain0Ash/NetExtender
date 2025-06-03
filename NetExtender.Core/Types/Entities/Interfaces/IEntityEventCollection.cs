// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.CQRS.Events.Interfaces;

namespace NetExtender.Types.Entities.Interfaces
{
    public interface IEntityEventCollection : IReadOnlyCollection<IEventCQRS>
    {
        public Boolean IsReadOnly { get; }
        public Boolean IsEmpty { get; }

        public void Add(IEventCQRS @event);
        public Boolean AddUnique<T>(T @event) where T : IEventCQRS;
        public Boolean Remove(IEventCQRS @event);
        public Boolean Remove<T>() where T : IEventCQRS;
        public void ResolveAll();
    }
}