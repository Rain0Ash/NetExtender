// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;

namespace NetExtender.CQRS.Requests.Handlers.Interfaces
{
    public interface IRequestCQRSHandler<in TRequest, TResult> : IRequestCQRSHandler<TRequest>, IEntityCQRSHandler<TRequest, TResult> where TRequest : IRequestCQRS<TResult>
    {
        public new Task<TResult> HandleAsync(TRequest request);
        public new Task<TResult> HandleAsync(TRequest request, CancellationToken token);
    }
    
    public interface IRequestCQRSHandler<in TRequest> : IEntityCQRSHandler<TRequest> where TRequest : IRequestCQRS
    {
        public new Task HandleAsync(TRequest request);
        public new Task HandleAsync(TRequest request, CancellationToken token);
    }
}