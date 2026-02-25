using Microsoft.Extensions.DependencyInjection;
using NetExtender.CQRS.Commands.Dispatchers.Interfaces;
using NetExtender.CQRS.Events.Dispatchers.Interfaces;
using NetExtender.CQRS.Querys.Dispatchers.Interfaces;
using NetExtender.CQRS.Requests.Dispatchers.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public abstract class TransientCQRS<TContext> : ServiceCQRS<TContext>, ITransient<IQueryCQRSDispatcher<TContext>>, ITransient<IRequestCQRSDispatcher<TContext>>, ITransient<ICommandCQRSDispatcher<TContext>>, ITransient<IEventCQRSDispatcher<TContext>> where TContext : CQRS<TContext>.IContext
    {
        public sealed override ServiceLifetime Lifetime
        {
            get
            {
                return ServiceLifetime.Transient;
            }
        }
    }

    public abstract class ScopedCQRS<TContext> : ServiceCQRS<TContext>, IScoped<IQueryCQRSDispatcher<TContext>>, IScoped<IRequestCQRSDispatcher<TContext>>, IScoped<ICommandCQRSDispatcher<TContext>>, IScoped<IEventCQRSDispatcher<TContext>> where TContext : CQRS<TContext>.IContext
    {
        public sealed override ServiceLifetime Lifetime
        {
            get
            {
                return ServiceLifetime.Scoped;
            }
        }
    }

    public abstract class SingletonCQRS<TContext> : ServiceCQRS<TContext>, ISingleton<IQueryCQRSDispatcher<TContext>>, ISingleton<IRequestCQRSDispatcher<TContext>>, ISingleton<ICommandCQRSDispatcher<TContext>>, ISingleton<IEventCQRSDispatcher<TContext>> where TContext : CQRS<TContext>.IContext
    {
        public sealed override ServiceLifetime Lifetime
        {
            get
            {
                return ServiceLifetime.Singleton;
            }
        }
    }

    public abstract partial class ServiceCQRS<TContext> : CQRS<TContext> where TContext : CQRS<TContext>.IContext
    {
        public abstract ServiceLifetime Lifetime { get; }
    }
}