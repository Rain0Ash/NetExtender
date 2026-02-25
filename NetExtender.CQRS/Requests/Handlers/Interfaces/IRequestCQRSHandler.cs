// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.CQRS.Requests.Handlers.Interfaces
{
    public interface IRequestCQRSHandler<TContext, TRequest, TResult> : IRequestCQRSHandler<TContext, TRequest>, IEntityCQRSHandler<TContext, TRequest, TResult> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS<TResult>
    {
        public new BusinessAsync<TResult> Async(TContext context, TRequest request);
        public new BusinessAsync<TResult> Async(TContext context, TRequest request, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TRequest request);
        public new BusinessAsync<TResult> Async(TContext context, in TRequest request, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, TRequest request, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync<TResult> Async(TContext context, in TRequest request, ICQRS.Transaction transaction);
        public new BusinessAsync<TResult> Async(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TRequest request);
        public new Async<TResult> SafeAsync(TContext context, TRequest request, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TRequest request);
        public new Async<TResult> SafeAsync(TContext context, in TRequest request, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, TRequest request, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new Async<TResult> SafeAsync(TContext context, in TRequest request, ICQRS.Transaction transaction);
        public new Async<TResult> SafeAsync(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token);
    }

    public interface IRequestCQRSHandler<TContext, TRequest> : IEntityCQRSHandler<TContext, TRequest> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS
    {
        public new BusinessAsync Async(TContext context, TRequest request);
        public new BusinessAsync Async(TContext context, TRequest request, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TRequest request);
        public new BusinessAsync Async(TContext context, in TRequest request, CancellationToken token);
        public new BusinessAsync Async(TContext context, TRequest request, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new BusinessAsync Async(TContext context, in TRequest request, ICQRS.Transaction transaction);
        public new BusinessAsync Async(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, TRequest request);
        public new Async SafeAsync(TContext context, TRequest request, CancellationToken token);
        public new Async SafeAsync(TContext context, in TRequest request);
        public new Async SafeAsync(TContext context, in TRequest request, CancellationToken token);
        public new Async SafeAsync(TContext context, TRequest request, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, TRequest request, ICQRS.Transaction transaction, CancellationToken token);
        public new Async SafeAsync(TContext context, in TRequest request, ICQRS.Transaction transaction);
        public new Async SafeAsync(TContext context, in TRequest request, ICQRS.Transaction transaction, CancellationToken token);
    }
}