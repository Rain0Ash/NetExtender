// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests.Dispatchers.Interfaces
{
    public interface IRequestCQRSDispatcher<TContext> : IEntityCQRSDispatcher<TContext> where TContext : CQRS<TContext>.IContext
    {
        public new BusinessAsync Async<TRequest>(TContext context, TRequest request) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, in TRequest request) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS;
        public new BusinessAsync Async<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, TRequest request) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, in TRequest request) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS;
        public new Async SafeAsync<TRequest>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>;
        public new BusinessAsync<TResult> Async<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction) where TRequest : IRequestCQRS<TResult>;
        public new Async<TResult> SafeAsync<TRequest, TResult>(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
    }
}