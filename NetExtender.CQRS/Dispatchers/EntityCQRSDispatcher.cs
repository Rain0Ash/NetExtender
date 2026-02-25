// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Dispatchers
{
    public abstract class EntityCQRSDispatcher<TContext> : IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        protected virtual CancellationToken Token
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CancellationToken.None;
            }
        }

        public BusinessAsync Async<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS
        {
            return Async(context, entity, Token);
        }

        public BusinessAsync Async<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
        {
            return Async(context, entity, default, token);
        }

        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS
        {
            return Async(context, in entity, Token);
        }

        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
        {
            return Async(context, in entity, default, token);
        }

        public BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
        {
            return Async(context, entity, transaction, Token);
        }

        public abstract BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;

        public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
        {
            return Async(context, in entity, transaction, Token);
        }

        public abstract BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS;

        public Async SafeAsync<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, entity, Token);
        }

        public Async SafeAsync<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, entity, default, token);
        }

        public Async SafeAsync<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, in entity, Token);
        }

        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, in entity, default, token);
        }

        public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, entity, transaction, Token);
        }

        public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
        {
            return Async(context, entity, transaction, token);
        }

        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
        {
            return SafeAsync(context, in entity, transaction, Token);
        }

        public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
        {
            return Async(context, in entity, transaction, token);
        }

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, entity, Token);
        }

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, entity, default, token);
        }

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, in entity, Token);
        }

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, in entity, default, token);
        }

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, entity, transaction, Token);
        }

        public abstract BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;

        public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, in entity, transaction, Token);
        }

        public abstract BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>;

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, entity, Token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, entity, default, token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, in entity, Token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, in entity, default, token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, entity, transaction, Token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, entity, transaction, token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
        {
            return SafeAsync<TEntity, TResult>(context, in entity, transaction, Token);
        }

        public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
        {
            return Async<TEntity, TResult>(context, in entity, transaction, token);
        }
    }
}