// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Querys.Dispatchers.Interfaces
{
    public interface IQueryCQRSDispatcher<TContext> : IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>;
        public new Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>;
    }
}