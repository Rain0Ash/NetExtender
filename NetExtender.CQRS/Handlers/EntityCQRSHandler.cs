// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Interfaces;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Handlers
{
    public abstract class EntityCQRSHandler<TContext, TEntity, TResult> : EntityCQRSHandlerWrapper<TContext, TEntity, TResult>, IEntityCQRSHandler<TContext, TEntity, TResult> where TContext : CQRS<TContext>.IContext where TEntity : IEntityCQRS<TResult>
    {
        public new BusinessAsync<TResult> Async(TContext context, TEntity entity)
        {
            return Async(context, entity, Token);
        }

        public new BusinessAsync<TResult> Async(TContext context, TEntity entity, CancellationToken token)
        {
            return Async(context, entity, default, token);
        }

        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity)
        {
            return Async(context, in entity, Token);
        }

        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity, CancellationToken token)
        {
            return Async(context, in entity, default, token);
        }

        public new BusinessAsync<TResult> Async(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Async(context, entity, transaction, Token);
        }

        public new abstract BusinessAsync<TResult> Async(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);

        public new BusinessAsync<TResult> Async(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Async(context, in entity, transaction, Token);
        }

        public new abstract BusinessAsync<TResult> Async(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);

        public new Async<TResult> SafeAsync(TContext context, TEntity entity)
        {
            return SafeAsync(context, entity, Token);
        }

        public new Async<TResult> SafeAsync(TContext context, TEntity entity, CancellationToken token)
        {
            return SafeAsync(context, entity, default, token);
        }

        public new Async<TResult> SafeAsync(TContext context, in TEntity entity)
        {
            return SafeAsync(context, in entity, Token);
        }

        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, CancellationToken token)
        {
            return SafeAsync(context, in entity, default, token);
        }

        public new Async<TResult> SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, entity, transaction, Token);
        }

        public new Async<TResult> SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, entity, transaction, token);
        }

        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in entity, transaction, Token);
        }

        public new Async<TResult> SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected sealed override BusinessAsync<TResult> HandleResultAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected sealed override BusinessAsync<TResult> HandleResultAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in entity, transaction, token);
        }
    }

    public abstract class EntityCQRSHandlerWrapper<TContext, TEntity, TResult> : EntityCQRSHandler<TContext, TEntity> where TContext : CQRS<TContext>.IContext where TEntity : IEntityCQRS
    {
        private protected abstract BusinessAsync<TResult> HandleResultAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);
        private protected abstract BusinessAsync<TResult> HandleResultAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override BusinessAsync Async(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return HandleResultAsync(context, entity, transaction, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override BusinessAsync Async(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return HandleResultAsync(context, in entity, transaction, token);
        }
    }

    public abstract class EntityCQRSHandler<TContext, TEntity> : IEntityCQRSHandler<TContext, TEntity> where TContext : CQRS<TContext>.IContext where TEntity : IEntityCQRS
    {
        public virtual ICQRS.Transaction.Mode TransactionMode
        {
            get
            {
                return ICQRS.Transaction.Mode.NotRequired;
            }
        }

        protected virtual CancellationToken Token
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CancellationToken.None;
            }
        }

        public BusinessAsync Async(TContext context, TEntity entity)
        {
            return Async(context, entity, Token);
        }

        public BusinessAsync Async(TContext context, TEntity entity, CancellationToken token)
        {
            return Async(context, entity, default, token);
        }

        public BusinessAsync Async(TContext context, in TEntity entity)
        {
            return Async(context, in entity, Token);
        }

        public BusinessAsync Async(TContext context, in TEntity entity, CancellationToken token)
        {
            return Async(context, in entity, default, token);
        }

        public BusinessAsync Async(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return Async(context, entity, transaction, Token);
        }

        public abstract BusinessAsync Async(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token);

        public BusinessAsync Async(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return Async(context, in entity, transaction, Token);
        }

        public abstract BusinessAsync Async(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token);

        public Async SafeAsync(TContext context, TEntity entity)
        {
            return SafeAsync(context, entity, Token);
        }

        public Async SafeAsync(TContext context, TEntity entity, CancellationToken token)
        {
            return SafeAsync(context, entity, default, token);
        }

        public Async SafeAsync(TContext context, in TEntity entity)
        {
            return SafeAsync(context, in entity, Token);
        }

        public Async SafeAsync(TContext context, in TEntity entity, CancellationToken token)
        {
            return SafeAsync(context, in entity, default, token);
        }

        public Async SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, entity, transaction, Token);
        }

        public Async SafeAsync(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, entity, transaction, token);
        }

        public Async SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in entity, transaction, Token);
        }

        public Async SafeAsync(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in entity, transaction, token);
        }
    }
}