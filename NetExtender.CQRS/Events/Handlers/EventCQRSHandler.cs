// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Threading;
using NetExtender.CQRS.Events.Handlers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Events.Handlers
{
    public abstract class EventCQRSHandler<TContext, TEvent> : EntityCQRSHandler<TContext, TEvent>, IEventCQRSHandler<TContext, TEvent> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
    {
        protected IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? Notifier { get; }

        protected EventCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
        {
            Notifier = notifier;
        }

        public override BusinessAsync Async(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in @event, transaction, token);
        }

        public override BusinessAsync Async(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, @event, transaction, token);
        }
    }
}