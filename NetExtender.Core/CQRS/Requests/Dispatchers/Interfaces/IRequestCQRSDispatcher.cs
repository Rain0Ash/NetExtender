// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.CQRS.Dispatchers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;

namespace NetExtender.CQRS.Requests.Dispatchers.Interfaces
{
    public interface IRequestCQRSDispatcher : IEntityCQRSDispatcher
    {
        public new Task DispatchAsync<TRequest>(TRequest request) where TRequest : IRequestCQRS;
        public new Task DispatchAsync<TRequest>(TRequest request, CancellationToken token) where TRequest : IRequestCQRS;
        public new Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request) where TRequest : IRequestCQRS<TResult>;
        public new Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken token) where TRequest : IRequestCQRS<TResult>;
    }
}