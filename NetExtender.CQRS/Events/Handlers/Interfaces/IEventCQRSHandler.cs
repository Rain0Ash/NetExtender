// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Events.Handlers.Interfaces
{
    public interface IEventCQRSHandler<TContext, TEvent> : IEntityCQRSHandler<TContext, TEvent> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
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
}