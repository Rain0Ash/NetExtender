using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Queries.Dispatchers;
using NetExtender.CQRS.Queries.Handlers;
using NetExtender.CQRS.Queries.Handlers.Interfaces;
using NetExtender.CQRS.Queries.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class QueryCQRSDispatcher : QueryCQRSDispatcher<TContext>
        {
        }

        public abstract class QueryCQRSHandler<TQuery, TResult> : QueryCQRSHandler<TContext, TQuery, TResult> where TQuery : IQueryCQRS<TResult>
        {
        }
    }

    public partial class CQRS<TContext>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static partial class Dispatcher
        {
            private static class Query<TQuery, TResult> where TQuery : IQueryCQRS<TResult>
            {
                public static Type? Handler { get; }

                static Query()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)].Types.Search(static type => type is { IsAbstract: false }, null);
                }
            }

            public readonly struct Query
            {
                private CQRS<TContext> CQRS { get; }

                public Query(CQRS<TContext> dispatcher)
                {
                    CQRS = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>) ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, query), query) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, query), query, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, in query), in query) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, in query), in query, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, query), query, transaction) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, query), query, transaction, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, in query), in query, transaction) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.Async(Next(ref context, in query), in query, transaction, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, query), query) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, query), query, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, in query), in query) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, in query), in query, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, query), query, transaction) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, query), query, transaction, token) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, in query), in query, transaction) : throw CQRS.Throw<TQuery>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TQuery, TResult>(TContext context, in TQuery query, ICQRS.Transaction transaction, CancellationToken token) where TQuery : IQueryCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Query<TQuery, TResult>.Handler ?? typeof(IQueryCQRSHandler<TContext, TQuery, TResult>)) is IQueryCQRSHandler<TContext, TQuery, TResult> handler ? handler.SafeAsync(Next(ref context, in query), in query, transaction, token) : throw CQRS.Throw<TQuery>();
                }
            }
        }
    }
}