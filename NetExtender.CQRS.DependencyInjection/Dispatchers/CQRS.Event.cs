using System.Collections.Generic;
using NetExtender.CQRS.Events.Handlers.Interfaces;
using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public partial class ServiceCQRS<TContext>
    {
        public abstract class EventTransientCQRSHandler<TEvent> : EventCQRSHandler<TEvent>, ISingleTransient<IEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS
        {
            protected EventTransientCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
                : base(notifier)
            {
            }
        }

        public abstract class EventScopedCQRSHandler<TEvent> : EventCQRSHandler<TEvent>, ISingleScoped<IEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS
        {
            protected EventScopedCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
                : base(notifier)
            {
            }
        }

        public abstract class EventSingletonCQRSHandler<TEvent> : EventCQRSHandler<TEvent>, ISingleSingleton<IEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS
        {
            protected EventSingletonCQRSHandler(IEnumerable<INotifyEventCQRSHandler<TContext, TEvent>>? notifier)
                : base(notifier)
            {
            }
        }
    }
}