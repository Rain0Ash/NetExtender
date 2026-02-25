// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Events.Dispatchers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Events.Dispatchers
{
    public abstract class EventCQRSDispatcher<TContext> : EntityCQRSDispatcher<TContext>, IEventCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event)
        {
            return Async(context, @event);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, CancellationToken token)
        {
            return Async(context, @event, token);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return Async(context, @event, transaction);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, @event, transaction, token);
        }

        public override BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TEntity, TResult>(context, in entity, transaction, token);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event)
        {
            return Async(context, in @event);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, CancellationToken token)
        {
            return Async(context, in @event, token);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return Async(context, in @event, transaction);
        }

        BusinessAsync IEventCQRSDispatcher<TContext>.Async<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in @event, transaction, token);
        }

        public override BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TEntity, TResult>(context, entity, transaction, token);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event)
        {
            return SafeAsync(context, @event);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, CancellationToken token)
        {
            return SafeAsync(context, @event, token);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event)
        {
            return SafeAsync(context, in @event);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, CancellationToken token)
        {
            return SafeAsync(context, in @event, token);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, @event, transaction);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, @event, transaction, token);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in @event, transaction);
        }

        Async IEventCQRSDispatcher<TContext>.SafeAsync<TEvent>(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, in @event, transaction, token);
        }
    }
}