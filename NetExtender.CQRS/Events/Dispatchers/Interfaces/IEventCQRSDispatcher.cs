// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Events.Dispatchers.Interfaces
{
    public interface IEventCQRSDispatcher<TContext> : IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        public new BusinessAsync Async<TEvent>(TContext context, TEvent @event) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, TEvent @event, CancellationToken token) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, in TEvent @event) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, in TEvent @event, CancellationToken token) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS;
        public new BusinessAsync Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, TEvent @event) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, TEvent @event, CancellationToken token) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, in TEvent @event) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, in TEvent @event, CancellationToken token) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction) where TEvent : IEventCQRS;
        public new Async SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token) where TEvent : IEventCQRS;
    }
}