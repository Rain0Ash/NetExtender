// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

namespace NetExtender.Types.Middlewares.Interfaces
{
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
        
        public IAsyncMiddleware<T>? AsyncInvoker<T>();
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
        
        public IMiddleware<T>? Invoker<T>();
    }
    
    public interface IMiddlewareInfo
    {
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
        
        public MiddlewareExecutionContext Context { get; }
    }
}