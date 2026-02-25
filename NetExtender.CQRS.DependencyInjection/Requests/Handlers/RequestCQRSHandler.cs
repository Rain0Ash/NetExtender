// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.CQRS.Requests.Handlers.Interfaces;
using NetExtender.CQRS.Requests.Interfaces;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.CQRS.Requests.Handlers
{
    public abstract class RequestTransientCQRSHandler<TContext, TRequest, TResult> : RequestCQRSHandler<TContext, TRequest, TResult>, ITransient<IRequestCQRSHandler<TContext, TRequest, TResult>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS<TResult>
    {
    }

    public abstract class RequestScopedCQRSHandler<TContext, TRequest, TResult> : RequestCQRSHandler<TContext, TRequest, TResult>, IScoped<IRequestCQRSHandler<TContext, TRequest, TResult>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS<TResult>
    {
    }

    public abstract class RequestSingletonCQRSHandler<TContext, TRequest, TResult> : RequestCQRSHandler<TContext, TRequest, TResult>, ISingleton<IRequestCQRSHandler<TContext, TRequest, TResult>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS<TResult>
    {
    }

    public abstract class RequestTransientCQRSHandler<TContext, TRequest> : RequestCQRSHandler<TContext, TRequest>, ITransient<IRequestCQRSHandler<TContext, TRequest>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS
    {
    }

    public abstract class RequestScopedCQRSHandler<TContext, TRequest> : RequestCQRSHandler<TContext, TRequest>, IScoped<IRequestCQRSHandler<TContext, TRequest>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS
    {
    }

    public abstract class RequestSingletonCQRSHandler<TContext, TRequest> : RequestCQRSHandler<TContext, TRequest>, ISingleton<IRequestCQRSHandler<TContext, TRequest>> where TContext : CQRS<TContext>.IContext where TRequest : IRequestCQRS
    {
    }
}