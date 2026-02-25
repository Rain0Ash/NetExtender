// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.CQRS.Events.Handlers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Events.Handlers
{
    public abstract class EventTransientCQRSHandler<TContext, TEvent> : EventCQRSHandler<TContext, TEvent>, ITransient<IEventCQRSHandler<TContext, TEvent>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
    {
        protected EventTransientCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
            : base(notifier)
        {
        }
    }

    public abstract class EventScopedCQRSHandler<TContext, TEvent> : EventCQRSHandler<TContext, TEvent>, IScoped<IEventCQRSHandler<TContext, TEvent>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
    {
        protected EventScopedCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
            : base(notifier)
        {
        }
    }

    public abstract class EventSingletonCQRSHandler<TContext, TEvent> : EventCQRSHandler<TContext, TEvent>, ISingleton<IEventCQRSHandler<TContext, TEvent>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS
    {
        protected EventSingletonCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
            : base(notifier)
        {
        }
    }
}