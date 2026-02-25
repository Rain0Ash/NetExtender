// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Requests.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests.Handlers
{
    public abstract class RequestCQRSHandler<TContext, TRequest, TResult> : EntityCQRSHandler<TContext, TRequest, TResult>, IRequestCQRSHandler<TContext, TRequest, TResult> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS<TResult>
    {
        public override BusinessAsync<TResult> Async(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in request, transaction, token);
        }

        public override BusinessAsync<TResult> Async(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, request, transaction, token);
        }
    }

    public abstract class RequestCQRSHandler<TContext, TRequest> : EntityCQRSHandler<TContext, TRequest>, IRequestCQRSHandler<TContext, TRequest> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS
    {
        public override BusinessAsync Async(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in request, transaction, token);
        }

        public override BusinessAsync Async(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, request, transaction, token);
        }
    }
}