// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.CQRS.Notify.Interfaces;
using NetExtender.CQRS.Handlers;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Notify.Handlers
{
    public abstract class NotifyCQRSHandler<TContext, TEvent, TNotify> : EntityCQRSHandler<TContext, TNotify>, INotifyCQRSHandler<TContext, TEvent, TNotify> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
    {
        public abstract Maybe<TNotify> Notify(TEvent @event);

        public override BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in notify, transaction, token);
        }

        public override BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, notify, transaction, token);
        }

        public BusinessAsync Async(TContext context, TEvent @event)
        {
            return Async(context, @event, Token);
        }

        public BusinessAsync Async(TContext context, TEvent @event, CancellationToken token)
        {
            return Async(context, @event, default, token);
        }

        public BusinessAsync Async(TContext context, in TEvent @event)
        {
            return Async(context, in @event, Token);
        }

        public BusinessAsync Async(TContext context, in TEvent @event, CancellationToken token)
        {
            return Async(context, in @event, default, token);
        }

        public BusinessAsync Async(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return Async(context, @event, transaction, Token);
        }

        public BusinessAsync Async(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            Maybe<TNotify> maybe = Notify(@event);
            return maybe.Unwrap(out TNotify? notify) ? Async(context, notify, transaction, token) : default;
        }

        public BusinessAsync Async(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return Async(context, in @event, transaction, Token);
        }

        public BusinessAsync Async(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            Maybe<TNotify> maybe = Notify(@event);
            return maybe.Unwrap(out TNotify? notify) ? Async(context, notify, transaction, token) : default;
        }

        public Async SafeAsync(TContext context, TEvent @event)
        {
            return SafeAsync(context, @event, Token);
        }

        public Async SafeAsync(TContext context, TEvent @event, CancellationToken token)
        {
            return SafeAsync(context, @event, default, token);
        }

        public Async SafeAsync(TContext context, in TEvent @event)
        {
            return SafeAsync(context, in @event, Token);
        }

        public Async SafeAsync(TContext context, in TEvent @event, CancellationToken token)
        {
            return SafeAsync(context, in @event, default, token);
        }

        public Async SafeAsync(TContext context, TEvent @event, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, @event, transaction, Token);
        }

        public Async SafeAsync(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, @event, transaction, token);
        }

        public Async SafeAsync(TContext context, in TEvent @event, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in @event, transaction, Token);
        }

        public Async SafeAsync(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in @event, transaction, token);
        }
    }

    public abstract class NotifyCQRSHandler<TContext, TNotify> : EntityCQRSHandler<TContext, TNotify>, INotifyCQRSHandler<TContext, TNotify> where TContext : CQRS<TContext>.IContext where TNotify : INotifyCQRS
    {
        public override BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in notify, transaction, token);
        }

        public override BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, notify, transaction, token);
        }
    }
}