// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Querys.Dispatchers.Interfaces;
using NetExtender.Initializer.CQRS.Exceptions;

namespace NetExtender.CQRS.Queries.Dispatchers
{
    public abstract class QueryCQRSDispatcher : EntityCQRSDispatcher, IQueryCQRSDispatcher
    {
        Task<TResult> IQueryCQRSDispatcher.DispatchAsync<TQuery, TResult>(TQuery query)
        {
            return DispatchAsync<TQuery, TResult>(query);
        }

        Task<TResult> IQueryCQRSDispatcher.DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token)
        {
            return DispatchAsync<TQuery, TResult>(query, token);
        }

        public override Task DispatchAsync<TEntity>(TEntity entity, CancellationToken token)
        {
            throw new DispatchNotSupportedException<TEntity>(GetType());
        }
    }
}