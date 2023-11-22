// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Events.Dispatchers.Interfaces;

namespace NetExtender.CQRS.Events.Dispatchers
{
    public abstract class EventCQRSDispatcher : EntityCQRSDispatcher, IEventCQRSDispatcher
    {
        Task IEventCQRSDispatcher.DispatchAsync<TEvent>(TEvent @event)
        {
            return DispatchAsync(@event);
        }
        
        Task IEventCQRSDispatcher.DispatchAsync<TEvent>(TEvent @event, CancellationToken token)
        {
            return DispatchAsync(@event, token);
        }

        public override Task<TResult> DispatchAsync<TEntity, TResult>(TEntity entity, CancellationToken token)
        {
            throw new NotSupportedException();
        }
    }
}