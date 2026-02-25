// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Handlers.Interfaces
{
    public interface IEntityCQRSHandler<TContext, TEntity, TResult> : IEntityCQRSHandler<TContext, TEntity> where TContext : CQRS<TContext>.IContext where TEntity : IEntityCQRS<TResult>
    {
        public new BusinessAsync<TResult> Async(TContext context, TEntity entity);
        public new BusinessAsync<TResult> Async(TContext context, TEntity entity, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity);
        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, TEntity entity, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TEntity entity);
        public new Async<TResult> SafeAsync(TContext context, TEntity entity, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TEntity entity);
        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
    }

    public interface IEntityCQRSHandler<TContext, TEntity> where TContext : CQRS<TContext>.IContext where TEntity : IEntityCQRS
    {
        public ICQRS.Transaction.Mode TransactionMode { get; }

        public BusinessAsync Async(TContext context, TEntity entity);
        public BusinessAsync Async(TContext context, TEntity entity, CancellationToken token);
        public BusinessAsync Async(TContext context, in TEntity entity);
        public BusinessAsync Async(TContext context, in TEntity entity, CancellationToken token);
        public BusinessAsync Async(TContext context, TEntity entity, ICQRS.Transaction transaction);
        public BusinessAsync Async(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public BusinessAsync Async(TContext context, in TEntity entity, ICQRS.Transaction transaction);
        public BusinessAsync Async(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public Async SafeAsync(TContext context, TEntity entity);
        public Async SafeAsync(TContext context, TEntity entity, CancellationToken token);
        public Async SafeAsync(TContext context, in TEntity entity);
        public Async SafeAsync(TContext context, in TEntity entity, CancellationToken token);
        public Async SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction);
        public Async SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        public Async SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction);
        public Async SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
    }
}