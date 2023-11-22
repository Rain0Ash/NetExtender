// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers;
using NetExtender.CQRS.Requests.Dispatchers.Interfaces;

namespace NetExtender.CQRS.Requests.Dispatchers
{
    public abstract class RequestCQRSDispatcher : EntityCQRSDispatcher, IRequestCQRSDispatcher
    {
        Task IRequestCQRSDispatcher.DispatchAsync<TRequest>(TRequest request)
        {
            return DispatchAsync(request);
        }
        
        Task IRequestCQRSDispatcher.DispatchAsync<TRequest>(TRequest request, CancellationToken token)
        {
            return DispatchAsync(request, token);
        }

        Task<TResult> IRequestCQRSDispatcher.DispatchAsync<TRequest, TResult>(TRequest request)
        {
            return DispatchAsync<TRequest, TResult>(request);
        }

        Task<TResult> IRequestCQRSDispatcher.DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken token)
        {
            return DispatchAsync<TRequest, TResult>(request, token);
        }
    }
}