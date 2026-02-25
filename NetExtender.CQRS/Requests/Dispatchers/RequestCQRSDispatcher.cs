// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Requests.Dispatchers.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests.Dispatchers
{
    public abstract class RequestCQRSDispatcher<TContext> : EntityCQRSDispatcher<TContext>, IRequestCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request)
        {
            return Async(context, request);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, CancellationToken token)
        {
            return Async(context, request, token);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request)
        {
            return Async(context, in request);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, CancellationToken token)
        {
            return Async(context, in request, token);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Async(context, request, transaction);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, request, transaction, token);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Async(context, in request, transaction);
        }

        BusinessAsync IRequestCQRSDispatcher<TContext>.Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async(context, in request, transaction, token);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request)
        {
            return SafeAsync(context, request);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, CancellationToken token)
        {
            return SafeAsync(context, request, token);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request)
        {
            return SafeAsync(context, in request);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, CancellationToken token)
        {
            return SafeAsync(context, in request, token);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, request, transaction);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, request, transaction, token);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return SafeAsync(context, in request, transaction);
        }

        Async IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync(context, in request, transaction, token);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request)
        {
            return Async<TRequest, TResult>(context, request);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, CancellationToken token)
        {
            return Async<TRequest, TResult>(context, request, token);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request)
        {
            return Async<TRequest, TResult>(context, in request);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token)
        {
            return Async<TRequest, TResult>(context, in request, token);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return Async<TRequest, TResult>(context, request, transaction);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TRequest, TResult>(context, request, transaction, token);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return Async<TRequest, TResult>(context, in request, transaction);
        }

        BusinessAsync<TResult> IRequestCQRSDispatcher<TContext>.Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return Async<TRequest, TResult>(context, in request, transaction, token);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request)
        {
            return SafeAsync<TRequest, TResult>(context, request);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, CancellationToken token)
        {
            return SafeAsync<TRequest, TResult>(context, request, token);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request)
        {
            return SafeAsync<TRequest, TResult>(context, in request);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token)
        {
            return SafeAsync<TRequest, TResult>(context, in request, token);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction)
        {
            return SafeAsync<TRequest, TResult>(context, request, transaction);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TRequest, TResult>(context, request, transaction, token);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction)
        {
            return SafeAsync<TRequest, TResult>(context, in request, transaction);
        }

        Async<TResult> IRequestCQRSDispatcher<TContext>.SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token)
        {
            return SafeAsync<TRequest, TResult>(context, in request, transaction, token);
        }
    }
}