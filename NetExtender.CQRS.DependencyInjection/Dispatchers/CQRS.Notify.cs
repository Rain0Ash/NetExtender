using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.CQRS.Notify.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public partial class ServiceCQRS<TContext>
    {
        public abstract class NotifyTransientCQRSHandler<TEvent, TNotify> : NotifyCQRSHandler<TEvent, TNotify>, IMultiTransient<INotifyEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
        {
        }

        public abstract class NotifyScopedCQRSHandler<TEvent, TNotify> : NotifyCQRSHandler<TEvent, TNotify>, IMultiScoped<INotifyEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
        {
        }

        public abstract class NotifySingletonCQRSHandler<TEvent, TNotify> : NotifyCQRSHandler<TEvent, TNotify>, IMultiSingleton<INotifyEventCQRSHandler<TContext, TEvent>> where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
        {
        }
    }
}