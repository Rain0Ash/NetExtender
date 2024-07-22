using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Types.Middlewares.Interfaces;

namespace NetExtender.Types.Middlewares
{
    public delegate void MiddlewareDelegate<in T>(T argument);
    public delegate void MiddlewareSenderDelegate<in T>(Object? sender, T argument);
    public delegate Task MiddlewareDelegateAsync<in T>(T argument);
    public delegate Task MiddlewareSenderDelegateAsync<in T>(Object? sender, T argument);
    public delegate ValueTask MiddlewareDelegateValueAsync<in T>(T argument);
    public delegate ValueTask MiddlewareSenderDelegateValueAsync<in T>(Object? sender, T argument);
    
    public enum MiddlewareExecutionContext : Byte
    {
        Parallel,
        Sequential
    }
    
    public abstract class AsyncValueMiddleware<T> : AsyncMiddleware<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator AsyncValueMiddleware<T>?(MiddlewareDelegateValueAsync<T>? value)
        {
            return value is not null ? new AsyncValueHandler(value) : null;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator AsyncValueMiddleware<T>?(MiddlewareSenderDelegateValueAsync<T>? value)
        {
            return value is not null ? new AsyncValueSenderHandler(value) : null;
        }
        
        public override Boolean IsValue
        {
            get
            {
                return true;
            }
        }
        
        public override Task InvokeAsync(T argument)
        {
            return InvokeValueAsync(argument).AsTask();
        }
        
        public override Task InvokeAsync(Object? sender, T argument)
        {
            return InvokeValueAsync(sender, argument).AsTask();
        }
        
        public override ValueTask InvokeValueAsync(T argument)
        {
            return InvokeValueAsync(null, argument);
        }
        
        public abstract override ValueTask InvokeValueAsync(Object? sender, T argument);
        
        internal sealed class AsyncValueHandler : AsyncValueMiddleware<T>
        {
            private MiddlewareDelegateValueAsync<T> Delegate { get; }
            
            public AsyncValueHandler(MiddlewareDelegateValueAsync<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override ValueTask InvokeValueAsync(T argument)
            {
                return Delegate.Invoke(argument);
            }
            
            public override ValueTask InvokeValueAsync(Object? sender, T argument)
            {
                return InvokeValueAsync(argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
        
        internal sealed class AsyncValueSenderHandler : AsyncValueMiddleware<T>
        {
            private MiddlewareSenderDelegateValueAsync<T> Delegate { get; }
            
            public AsyncValueSenderHandler(MiddlewareSenderDelegateValueAsync<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override ValueTask InvokeValueAsync(Object? sender, T argument)
            {
                return Delegate.Invoke(sender, argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
    }
    
    public abstract class AsyncMiddleware<T> : Middleware, IAsyncMiddleware<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator AsyncMiddleware<T>?(MiddlewareDelegateAsync<T>? value)
        {
            return value is not null ? new AsyncHandler(value) : null;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator AsyncMiddleware<T>?(MiddlewareSenderDelegateAsync<T>? value)
        {
            return value is not null ? new AsyncSenderHandler(value) : null;
        }
        
        public Boolean IsAsync
        {
            get
            {
                return true;
            }
        }
        
        public virtual Boolean IsValue
        {
            get
            {
                return false;
            }
        }
        
        public virtual Task InvokeAsync(T argument)
        {
            return InvokeAsync(null, argument);
        }
        
        public abstract Task InvokeAsync(Object? sender, T argument);
        
        public virtual async ValueTask InvokeValueAsync(T argument)
        {
            await InvokeAsync(argument);
        }
        
        public virtual async ValueTask InvokeValueAsync(Object? sender, T argument)
        {
            await InvokeAsync(sender, argument);
        }
        
        public override IAsyncMiddleware<TArgument>? AsyncInvoker<TArgument>()
        {
            throw new NotImplementedException();
        }
        
        internal sealed class AsyncHandler : AsyncMiddleware<T>
        {
            private MiddlewareDelegateAsync<T> Delegate { get; }
            
            public AsyncHandler(MiddlewareDelegateAsync<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override Task InvokeAsync(T argument)
            {
                return Delegate.Invoke(argument);
            }
            
            public override Task InvokeAsync(Object? sender, T argument)
            {
                return InvokeAsync(argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
        
        internal sealed class AsyncSenderHandler : AsyncMiddleware<T>
        {
            private MiddlewareSenderDelegateAsync<T> Delegate { get; }
            
            public AsyncSenderHandler(MiddlewareSenderDelegateAsync<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override Task InvokeAsync(Object? sender, T argument)
            {
                return Delegate.Invoke(sender, argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
    }
    
    public abstract class Middleware<T> : Middleware, IMiddleware<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator Middleware<T>?(MiddlewareDelegate<T>? value)
        {
            return value is not null ? new Handler(value) : null;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator Middleware<T>?(MiddlewareSenderDelegate<T>? value)
        {
            return value is not null ? new SenderHandler(value) : null;
        }
        
        public Boolean IsAsync
        {
            get
            {
                return false;
            }
        }
        
        public virtual void Invoke(T argument)
        {
            Invoke(null, argument);
        }
        
        public abstract void Invoke(Object? sender, T argument);
        
        public override IMiddleware<TArgument>? Invoker<TArgument>()
        {
            throw new NotImplementedException();
        }
        
        internal sealed class Handler : Middleware<T>
        {
            private MiddlewareDelegate<T> Delegate { get; }
            
            public Handler(MiddlewareDelegate<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override void Invoke(T argument)
            {
                Delegate.Invoke(argument);
            }
            
            public override void Invoke(Object? sender, T argument)
            {
                Invoke(argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
        
        internal sealed class SenderHandler : Middleware<T>
        {
            private MiddlewareSenderDelegate<T> Delegate { get; }
            
            public SenderHandler(MiddlewareSenderDelegate<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override void Invoke(Object? sender, T argument)
            {
                Delegate.Invoke(sender, argument);
            }
            
            public override Int32 GetHashCode()
            {
                return Delegate.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || Delegate.Equals(other);
            }
            
            public override String? ToString()
            {
                return Delegate.ToString();
            }
        }
    }
    
    public abstract class Middleware : IMiddlewareInfo
    {
        protected static class InvokerHandler<T, TArgument>
        {
            
        }
        
        public MiddlewareExecutionContext Context { get; init; }
        
        public virtual IMiddleware<T>? Invoker<T>()
        {
            return null;
        }
        
        public virtual IAsyncMiddleware<T>? AsyncInvoker<T>()
        {
            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddleware<T> Create<T>(MiddlewareDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new Middleware<T>.Handler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddleware<T> Create<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new Middleware<T>.Handler(@delegate) { Context = context };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddleware<T> Create<T>(MiddlewareSenderDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new Middleware<T>.SenderHandler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddleware<T> Create<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new Middleware<T>.SenderHandler(@delegate) { Context = context };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncMiddleware<T>.AsyncHandler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncMiddleware<T>.AsyncHandler(@delegate) { Context = context };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new AsyncMiddleware<T>.AsyncSenderHandler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new AsyncMiddleware<T>.AsyncSenderHandler(@delegate) { Context = context };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueHandler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueHandler(@delegate) { Context = context };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueSenderHandler(@delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext context)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueSenderHandler(@delegate) { Context = context };
        }
    }
}