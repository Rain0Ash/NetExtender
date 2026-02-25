// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Queries.Handlers.Interfaces
{
    public interface IQueryCQRSHandler<TContext, TQuery, TResult> : IEntityCQRSHandler<TContext, TQuery, TResult> where TContext : CQRS<TContext>.IContext where TQuery : IQueryCQRS<TResult>
    {
        public new BusinessAsync<TResult> Async(TContext context, TQuery query);
        public new BusinessAsync<TResult> Async(TContext context, TQuery query, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TQuery query);
        public new BusinessAsync<TResult> Async(TContext context, in TQuery query, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, TQuery query, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TQuery query, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TQuery query);
        public new Async<TResult> SafeAsync(TContext context, TQuery query, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TQuery query);
        public new Async<TResult> SafeAsync(TContext context, in TQuery query, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TQuery query, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TQuery query, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token);
    }
}