// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Queries.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Queries.Handlers
{
    public abstract class QueryTransientCQRSHandler<TContext, TQuery, TResult> : QueryCQRSHandler<TContext, TQuery, TResult>, ITransient<IQueryCQRSHandler<TContext, TQuery, TResult>> where TContext : CQRS<TContext>.IContext where TQuery : IQueryCQRS<TResult>
    {
    }

    public abstract class QueryScopedCQRSHandler<TContext, TQuery, TResult> : QueryCQRSHandler<TContext, TQuery, TResult>, IScoped<IQueryCQRSHandler<TContext, TQuery, TResult>> where TContext : CQRS<TContext>.IContext where TQuery : IQueryCQRS<TResult>
    {
    }

    public abstract class QuerySingletonCQRSHandler<TContext, TQuery, TResult> : QueryCQRSHandler<TContext, TQuery, TResult>, ISingleton<IQueryCQRSHandler<TContext, TQuery, TResult>> where TContext : CQRS<TContext>.IContext where TQuery : IQueryCQRS<TResult>
    {
    }
}