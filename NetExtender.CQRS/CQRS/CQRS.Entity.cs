using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class EntityCQRSDispatcher : EntityCQRSDispatcher<TContext>
        {
        }

        public abstract class EntityCQRSHandler<TEntity> : NetExtender.CQRS.Handlers.EntityCQRSHandler<TContext, TEntity> where TEntity : IEntityCQRS
        {
        }

        public abstract class EntityCQRSHandler<TEntity, TResult> : EntityCQRSHandler<TContext, TEntity, TResult> where TEntity : IEntityCQRS<TResult>
        {
        }
    }

    public partial class CQRS<TContext>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static partial class Dispatcher
        {
            private static class Entity<TEntity> where TEntity : IEntityCQRS
            {
                public static Type Handler { get; }

                static Entity()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IEntityCQRSHandler<TContext, TEntity>)].Types.Require(static type => type is { IsAbstract: false }, true);
                }
            }

            private static class Entity<TEntity, TResult> where TEntity : IEntityCQRS<TResult>
            {
                public static Type Handler { get; }

                static Entity()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IEntityCQRSHandler<TContext, TEntity, TResult>)].Types.Require(static type => type is { IsAbstract: false }, true);
                }
            }

            public readonly struct Entity
            {
                private CQRS<TContext> CQRS { get; }

                public Entity(CQRS<TContext> cqrs)
                {
                    CQRS = cqrs ?? throw new ArgumentNullException(nameof(cqrs));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, entity), entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, entity), entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, in entity), in entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, in entity), in entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, entity), entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, entity), entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, in entity), in entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.Async(Next(ref context, in entity), in entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, TEntity entity) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, entity), entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, entity), entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, in TEntity entity) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, in entity), in entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, entity), entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, entity), entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TEntity>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS
                {
                    return CQRS.Provider.GetService(Entity<TEntity>.Handler) is IEntityCQRSHandler<TContext, TEntity> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, entity), entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, entity), entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, in entity), in entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, in entity), in entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, entity), entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, entity), entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, in entity), in entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.Async(Next(ref context, in entity), in entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, entity), entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, entity), entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, in entity), in entity) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, entity), entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, entity), entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, transaction) : throw CQRS.Throw<TEntity>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TEntity, TResult>(TContext context, in TEntity entity, ICQRS.Transaction transaction, CancellationToken token) where TEntity : IEntityCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Entity<TEntity, TResult>.Handler) is IEntityCQRSHandler<TContext, TEntity, TResult> handler ? handler.SafeAsync(Next(ref context, in entity), in entity, transaction, token) : throw CQRS.Throw<TEntity>();
                }
            }
        }
    }
}