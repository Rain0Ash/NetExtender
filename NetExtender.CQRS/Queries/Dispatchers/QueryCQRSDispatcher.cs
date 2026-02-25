// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Querys.Dispatchers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Queries.Dispatchers
{
    public abstract class QueryCQRSDispatcher<TContext> : EntityCQRSDispatcher<TContext>, IQueryCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query)
        {
            return Async<TQuery, TResult>(context, query);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, CancellationToken token)
        {
            return Async<TQuery, TResult>(context, query, token);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction)
        {
            return Async<TQuery, TResult>(context, query, transaction);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TQuery, TResult>(context, query, transaction, token);
        }

        public override BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in entity, transaction, token);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query)
        {
            return Async<TQuery, TResult>(context, in query);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token)
        {
            return Async<TQuery, TResult>(context, in query, token);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction)
        {
            return Async<TQuery, TResult>(context, in query, transaction);
        }

        BusinessAsync<TResult> IQueryCQRSDispatcher<TContext>.Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TQuery, TResult>(context, in query, transaction, token);
        }

        public override BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, entity, transaction, token);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query)
        {
            return SafeAsync<TQuery, TResult>(context, query);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, CancellationToken token)
        {
            return SafeAsync<TQuery, TResult>(context, query, token);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query)
        {
            return SafeAsync<TQuery, TResult>(context, in query);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token)
        {
            return SafeAsync<TQuery, TResult>(context, in query, token);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction)
        {
            return SafeAsync<TQuery, TResult>(context, query, transaction);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TQuery, TResult>(context, query, transaction, token);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction)
        {
            return SafeAsync<TQuery, TResult>(context, in query, transaction);
        }

        Async<TResult> IQueryCQRSDispatcher<TContext>.SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TQuery, TResult>(context, in query, transaction, token);
        }
    }
}