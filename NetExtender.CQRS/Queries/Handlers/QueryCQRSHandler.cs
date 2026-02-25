// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Queries.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Queries.Handlers
{
    public abstract class QueryCQRSHandler<TContext, TQuery, TResult> : EntityCQRSHandler<TContext, TQuery, TResult>, IQueryCQRSHandler<TContext, TQuery, TResult> where TContext : CQRS<TContext>.IContext where TQuery : IQueryCQRS<TResult>
    {
        public override BusinessAsync<TResult> Async(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in query, transaction, token);
        }

        public override BusinessAsync<TResult> Async(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, query, transaction, token);
        }
    }
}