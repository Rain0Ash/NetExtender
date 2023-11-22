// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Handlers;
using NetExtender.CQRS.Requests.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;

namespace NetExtender.CQRS.Requests.Handlers
{
    public abstract class RequestCQRSHandler<TRequest, TResult> : EntityCQRSHandler<TRequest, TResult>, IRequestCQRSHandler<TRequest, TResult> where TRequest : IRequestCQRS<TResult>
    {
        public override Task<TResult> HandleAsync(TRequest request)
        {
            return HandleAsync(request, CancellationToken.None);
        }
        
        public abstract override Task<TResult> HandleAsync(TRequest request, CancellationToken token);
    }
    
    public abstract class RequestCQRSHandler<TRequest> : EntityCQRSHandler<TRequest>, IRequestCQRSHandler<TRequest> where TRequest : IRequestCQRS
    {
        public override Task HandleAsync(TRequest request)
        {
            return HandleAsync(request, CancellationToken.None);
        }
        
        public abstract override Task HandleAsync(TRequest request, CancellationToken token);
    }
}