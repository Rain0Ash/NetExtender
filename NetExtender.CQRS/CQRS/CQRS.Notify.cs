using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers;
using NetExtender.CQRS.Notify.Interfaces;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class NotifyCQRSHandler<TEvent, TNotify> : NotifyCQRSHandler<TContext, TEvent, TNotify> where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
        {
        }
    }
}