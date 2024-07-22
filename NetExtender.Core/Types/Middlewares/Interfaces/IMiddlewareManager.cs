using System;
using System.Collections.Generic;

namespace NetExtender.Types.Middlewares.Interfaces
{
    public interface IMiddlewareManager : IMiddlewareInvoker, ICollection<IMiddlewareInfo>
    {
        public MiddlewareManagerContext Context { get; }
        
        public Boolean Contains(Type type);
        public Boolean Contains<T>() where T : IMiddlewareInfo;
        public Boolean Contains<T>(MiddlewareDelegate<T> @delegate);
        public Boolean Contains<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public Boolean Contains<T>(MiddlewareSenderDelegate<T> @delegate);
        public Boolean Contains<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public Boolean Contains<T>(MiddlewareDelegateAsync<T> @delegate);
        public Boolean Contains<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public Boolean Contains<T>(MiddlewareSenderDelegateAsync<T> @delegate);
        public Boolean Contains<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public Boolean Contains<T>(MiddlewareDelegateValueAsync<T> @delegate);
        public Boolean Contains<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public Boolean Contains<T>(MiddlewareSenderDelegateValueAsync<T> @delegate);
        public Boolean Contains<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IMiddlewareInfo Add(Type type);
        public T Add<T>() where T : IMiddlewareInfo;
        public IMiddleware<T> Add<T>(MiddlewareDelegate<T> @delegate);
        public IMiddleware<T> Add<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public IMiddleware<T> Add<T>(MiddlewareSenderDelegate<T> @delegate);
        public IMiddleware<T> Add<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateAsync<T> @delegate);
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateAsync<T> @delegate);
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateValueAsync<T> @delegate);
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateValueAsync<T> @delegate);
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public void AddRange<TMiddleware>(IEnumerable<TMiddleware> source) where TMiddleware : IMiddlewareInfo;
        public Int32 Remove(Type type);
        public Int32 Remove<T>() where T : IMiddlewareInfo;
        public IMiddleware<T>? Remove<T>(MiddlewareDelegate<T> @delegate);
        public IMiddleware<T>? Remove<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public IMiddleware<T>? Remove<T>(MiddlewareSenderDelegate<T> @delegate);
        public IMiddleware<T>? Remove<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateAsync<T> @delegate);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateAsync<T> @delegate);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateValueAsync<T> @delegate);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateValueAsync<T> @delegate);
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution);
        public IMiddlewareInfo? RemoveAt(MiddlewareExecutionContext execution, Int32 index);
        public IMiddlewareInfo[] RemoveAll(Predicate<IMiddlewareInfo> predicate);
        public IMiddlewareInfo[] RemoveAll(Predicate<IMiddlewareInfo> predicate, MiddlewareExecutionContext execution);
        public void Clear(MiddlewareExecutionContext execution);
        public void From(IMiddlewareManager manager);
        public Int32 this[MiddlewareExecutionContext execution] { get; }
        public IMiddlewareInfo? this[MiddlewareExecutionContext execution, Int32 index] { get; set; }
    }
}