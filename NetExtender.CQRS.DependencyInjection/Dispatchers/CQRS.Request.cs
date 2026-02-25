using NetExtender.CQRS.Requests.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Dispatchers
{
    public partial class ServiceCQRS<TContext>
    {
        public abstract class RequestTransientCQRSHandler<TRequest, TResult> : RequestCQRSHandler<TRequest, TResult>, ISingleTransient<IRequestCQRSHandler<TContext, TRequest, TResult>> where TRequest : IRequestCQRS<TResult>
        {
        }

        public abstract class RequestScopedCQRSHandler<TRequest, TResult> : RequestCQRSHandler<TRequest, TResult>, ISingleScoped<IRequestCQRSHandler<TContext, TRequest, TResult>> where TRequest : IRequestCQRS<TResult>
        {
        }

        public abstract class RequestSingletonCQRSHandler<TRequest, TResult> : RequestCQRSHandler<TRequest, TResult>, ISingleSingleton<IRequestCQRSHandler<TContext, TRequest, TResult>> where TRequest : IRequestCQRS<TResult>
        {
        }

        public abstract class RequestTransientCQRSHandler<TRequest> : RequestCQRSHandler<TRequest>, ISingleTransient<IRequestCQRSHandler<TContext, TRequest>> where TRequest : IRequestCQRS
        {
        }

        public abstract class RequestScopedCQRSHandler<TRequest> : RequestCQRSHandler<TRequest>, ISingleScoped<IRequestCQRSHandler<TContext, TRequest>> where TRequest : IRequestCQRS
        {
        }

        public abstract class RequestSingletonCQRSHandler<TRequest> : RequestCQRSHandler<TRequest>, ISingleSingleton<IRequestCQRSHandler<TContext, TRequest>> where TRequest : IRequestCQRS
        {
        }
    }
}