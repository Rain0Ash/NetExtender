// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Notify.Handlers.Interfaces
{
    public interface INotifyCQRSHandler<TContext, TEvent, TNotify> : INotifyEventCQRSHandler<TContext, TEvent> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
    {
        public Maybe<TNotify> Notify(TEvent @event);
        public BusinessAsync Async(TContext context, TNotify notify);
        public BusinessAsync Async(TContext context, TNotify notify, CancellationToken token);
        public BusinessAsync Async(TContext context, in TNotify notify);
        public BusinessAsync Async(TContext context, in TNotify notify, CancellationToken token);
        public BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction);
        public BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction);
        public BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public Async SafeAsync(TContext context, TNotify notify);
        public Async SafeAsync(TContext context, TNotify notify, CancellationToken token);
        public Async SafeAsync(TContext context, in TNotify notify);
        public Async SafeAsync(TContext context, in TNotify notify, CancellationToken token);
        public Async SafeAsync(TContext context, TNotify notify, ICQRS.Transaction transaction);
        public Async SafeAsync(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public Async SafeAsync(TContext context, in TNotify notify, ICQRS.Transaction transaction);
        public Async SafeAsync(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
    }

    public interface INotifyEventCQRSHandler<TContext, TEvent> : IEntityCQRSHandler<TContext, TEvent> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
    {
        public new BusinessAsync Async(TContext context, TEvent @event);
        public new BusinessAsync Async(TContext context, TEvent @event, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TEvent @event);
        public new BusinessAsync Async(TContext context, in TEvent @event, CancellationToken token);
        public new BusinessAsync Async(TContext context, TEvent @event, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TEvent @event, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, TEvent @event);
        public new Async SafeAsync(TContext context, TEvent @event, CancellationToken token);
        public new Async SafeAsync(TContext context, in TEvent @event);
        public new Async SafeAsync(TContext context, in TEvent @event, CancellationToken token);
        public new Async SafeAsync(TContext context, TEvent @event, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, TEvent @event, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, in TEvent @event, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, in TEvent @event, ICQRS.Transaction transaction, CancellationToken token);
    }

    public interface INotifyCQRSHandler<TContext, TNotify> : IEntityCQRSHandler<TContext, TNotify> where TContext : CQRS<TContext>.IContext where TNotify : INotifyCQRS
    {
        public new BusinessAsync Async(TContext context, TNotify notify);
        public new BusinessAsync Async(TContext context, TNotify notify, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TNotify notify);
        public new BusinessAsync Async(TContext context, in TNotify notify, CancellationToken token);
        public new BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, TNotify notify);
        public new Async SafeAsync(TContext context, TNotify notify, CancellationToken token);
        public new Async SafeAsync(TContext context, in TNotify notify);
        public new Async SafeAsync(TContext context, in TNotify notify, CancellationToken token);
        public new Async SafeAsync(TContext context, TNotify notify, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, in TNotify notify, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, in TNotify notify, ICQRS.Transaction transaction, CancellationToken token);
    }
}