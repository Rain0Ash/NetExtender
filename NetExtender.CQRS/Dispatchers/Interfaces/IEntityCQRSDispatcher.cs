// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Dispatchers.Interfaces
{
    public interface IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        public BusinessAsync Async<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS;
        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS;
        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>;
        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>;
        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;
    }
}