// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Events.Interfaces;
using NetExtender.CQRS.Notify.Handlers.Interfaces;
using NetExtender.CQRS.Notify.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Notify.Handlers
{
    public abstract class NotifyTransientCQRSHandler<TContext, TEvent, TNotify> : NotifyCQRSHandler<TContext, TEvent, TNotify>, ITransient<INotifyCQRSHandler<TContext, TEvent, TNotify>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
    {
    }

    public abstract class NotifyScopedCQRSHandler<TContext, TEvent, TNotify> : NotifyCQRSHandler<TContext, TEvent, TNotify>, IScoped<INotifyCQRSHandler<TContext, TEvent, TNotify>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
    {
    }

    public abstract class NotifySingletonCQRSHandler<TContext, TEvent, TNotify> : NotifyCQRSHandler<TContext, TEvent, TNotify>, ISingleton<INotifyCQRSHandler<TContext, TEvent, TNotify>> where TContext : CQRS<TContext>.IContext where TEvent : IEventCQRS where TNotify : INotifyCQRS<TEvent>
    {
    }
}