using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.CQRS.Requests.Dispatchers;
using NetExtender.CQRS.Requests.Handlers;
using NetExtender.CQRS.Requests.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.CQRS
{
    public partial class CQRS<TContext>
    {
        public abstract class RequestCQRSDispatcher : RequestCQRSDispatcher<TContext>
        {
        }

        public abstract class RequestCQRSHandler<TRequest> : NetExtender.CQRS.Requests.Handlers.RequestCQRSHandler<TContext, TRequest> where TRequest : IRequestCQRS
        {
        }

        public abstract class RequestCQRSHandler<TRequest, TResult> : RequestCQRSHandler<TContext, TRequest, TResult> where TRequest : IRequestCQRS<TResult>
        {
        }
    }

    public partial class CQRS<TContext>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static partial class Dispatcher
        {
            private static class Request<TRequest> where TRequest : IRequestCQRS
            {
                public static Type? Handler { get; }

                static Request()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IRequestCQRSHandler<TContext, TRequest>)].Types.Search(static type => type is { IsAbstract: false }, null);
                }
            }

            private static class Request<TRequest, TResult> where TRequest : IRequestCQRS<TResult>
            {
                public static Type? Handler { get; }

                static Request()
                {
                    Handler = ReflectionUtilities.Inherit[typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)].Types.Search(static type => type is { IsAbstract: false }, null);
                }
            }

            public readonly struct Request
            {
                private CQRS<TContext> CQRS { get; }

                public Request(CQRS<TContext> dispatcher)
                {
                    CQRS = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, TRequest request) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, request), request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, request), request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, in TRequest request) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, in request), in request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, in request), in request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, request), request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, request), request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, in request), in request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.Async(Next(ref context, in request), in request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, TRequest request) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, request), request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, request), request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, in TRequest request) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, in request), in request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, in request), in request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, request), request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, request), request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, in request), in request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS
                {
                    return CQRS.Provider.GetService(Request<TRequest>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest>)) is IRequestCQRSHandler<TContext, TRequest> handler ? handler.SafeAsync(Next(ref context, in request), in request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, request), request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, request), request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, in request), in request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, in request), in request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, request), request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, request), request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, in request), in request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.Async(Next(ref context, in request), in request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, request), request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, request), request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, in request), in request) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, in request), in request, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, request), request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, request), request, transaction, token) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, in request), in request, transaction) : throw CQRS.Throw<TRequest>();
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>
                {
                    return CQRS.Provider.GetService(Request<TRequest, TResult>.Handler ?? typeof(IRequestCQRSHandler<TContext, TRequest, TResult>)) is IRequestCQRSHandler<TContext, TRequest, TResult> handler ? handler.SafeAsync(Next(ref context, in request), in request, transaction, token) : throw CQRS.Throw<TRequest>();
                }
            }
        }
    }
}