using NetExtender.CQRS.Queries.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public partial class ServiceCQRS<TContext>
    {
        public abstract class QueryTransientCQRSHandler<TQuery, TResult> : QueryCQRSHandler<TQuery, TResult>, ISingleTransient<IQueryCQRSHandler<TContext, TQuery, TResult>> where TQuery : IQueryCQRS<TResult>
        {
        }

        public abstract class QueryScopedCQRSHandler<TQuery, TResult> : QueryCQRSHandler<TQuery, TResult>, ISingleScoped<IQueryCQRSHandler<TContext, TQuery, TResult>> where TQuery : IQueryCQRS<TResult>
        {
        }

        public abstract class QuerySingletonCQRSHandler<TQuery, TResult> : QueryCQRSHandler<TQuery, TResult>, ISingleSingleton<IQueryCQRSHandler<TContext, TQuery, TResult>> where TQuery : IQueryCQRS<TResult>
        {
        }
    }
}