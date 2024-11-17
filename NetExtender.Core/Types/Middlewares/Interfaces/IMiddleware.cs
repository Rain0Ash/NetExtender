// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

namespace NetExtender.Types.Middlewares.Interfaces
{
    public interface IMiddlewareAsyncConverter<in T, TArgument>
    {
        public ValueTask<TArgument> ConvertAsync(T argument)
        {
            return ConvertAsync(null, argument);
        }

        public ValueTask<TArgument> ConvertAsync(Object? sender, T argument);
    }
    
    public interface IMiddlewareConverter<in T, out TArgument>
    {
        public TArgument Convert(T argument)
        {
            return Convert(null, argument);
        }

        public TArgument Convert(Object? sender, T argument);
    }

    public interface IAsyncMiddleware<in T> : IAsyncMiddleware
    {
        public Task InvokeAsync(T argument);
        public Task InvokeAsync(Object? sender, T argument);
        public ValueTask InvokeValueAsync(T argument);
        public ValueTask InvokeValueAsync(Object? sender, T argument);
    }
    
    public interface IAsyncMiddleware : IMiddlewareInfo
    {
        public new Boolean IsAsync
        {
            get
            {
                return true;
            }
        }
        
        public Boolean IsValue
        {
            get
            {
                return false;
            }
        }
        
        public Task<Boolean> InvokeAsync<TArgument>(TArgument argument);
        public Task<Boolean> InvokeAsync<TArgument>(Object? sender, TArgument argument);
        public ValueTask<Boolean> InvokeValueAsync<TArgument>(TArgument argument);
        public ValueTask<Boolean> InvokeValueAsync<TArgument>(Object? sender, TArgument argument);
    }
    
    public interface IMiddleware<in T> : IMiddleware
    {
        public void Invoke(T argument);
        public void Invoke(Object? sender, T argument);
    }
    
    public interface IMiddleware : IMiddlewareInfo
    {
        public new Boolean IsAsync
        {
            get
            {
                return false;
            }
        }
        
        public Boolean Invoke<TArgument>(TArgument argument);
        public Boolean Invoke<TArgument>(Object? sender, TArgument argument);
    }
    
    public interface IMiddlewareInfo
    {
        public MiddlewareExecutionContext Context { get; }
        public MiddlewareIdempotencyMode Idempotency { get; }
        
        public Boolean IsAsync
        {
            get
            {
                return this switch
                {
                    IAsyncMiddleware middleware => middleware.IsAsync,
                    IMiddleware middleware => middleware.IsAsync,
                    _ => throw new InvalidOperationException()
                };
            }
        }
    }
}