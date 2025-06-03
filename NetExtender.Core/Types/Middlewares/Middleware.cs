// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Middlewares.Exceptions;
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

    public enum MiddlewareIdempotencyMode : Byte
    {
        None,
        Single,
        Sender,
        Argument,
        SenderArgument
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
            
            public override async ValueTask InvokeValueAsync(T argument)
            {
                if (!Memorize(argument))
                {
                    return;
                }

                try
                {
                    await Delegate.Invoke(argument);
                }
                catch (Exception)
                {
                    Forget(argument);
                    throw;
                }
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
            
            public override async ValueTask InvokeValueAsync(Object? sender, T argument)
            {
                if (!Memorize(sender, argument))
                {
                    return;
                }

                try
                {
                    await Delegate.Invoke(sender, argument);
                }
                catch (Exception)
                {
                    Forget(sender, argument);
                    throw;
                }
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
    
    public abstract class AsyncMiddleware<T> : Middleware, IAsyncMiddleware<T>, IMiddlewareConverter<T, T>, IMiddlewareAsyncConverter<T, T>
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
        
        protected ConcurrentHashSet<(Object? Sender, T Argument)> Memory { get; } = new ConcurrentHashSet<(Object? Sender, T Argument)>();
        
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

        protected Boolean Memorize(T argument)
        {
            return Memorize(Memory, null, argument);
        }

        protected Boolean Memorize(Object? sender, T argument)
        {
            return Memorize(Memory, sender, argument);
        }

        protected Boolean Forget(T argument)
        {
            return Forget(Memory, null, argument);
        }

        protected Boolean Forget(Object? sender, T argument)
        {
            return Forget(Memory, sender, argument);
        }
        
        //TODO: в DomainBuilder везде расставить Manager.Invoke, добавить рефлексивную проверку, что Domain устанавливает правила патчей ранее, чем происходит ModuleInitializer
        public virtual Task InvokeAsync(T argument)
        {
            return InvokeAsync(null, argument);
        }
        
        public abstract Task InvokeAsync(Object? sender, T argument);
        
        public async Task<Boolean> InvokeAsync<TArgument>(TArgument argument)
        {
            try
            {
                switch (this)
                {
                    case IAsyncMiddleware<TArgument> middleware:
                        await middleware.InvokeAsync(argument);
                        return true;
                    case IMiddlewareConverter<TArgument, T> converter:
                        await InvokeAsync(converter.Convert(argument));
                        return true;
                    default:
                        return false;
                }
            }
            catch (MiddlewareConvertNoInvokeException)
            {
                return true;
            }
        }
        
        public async Task<Boolean> InvokeAsync<TArgument>(Object? sender, TArgument argument)
        {
            try
            {
                switch (this)
                {
                    case IAsyncMiddleware<TArgument> middleware:
                        await middleware.InvokeAsync(sender, argument);
                        return true;
                    case IMiddlewareConverter<TArgument, T> converter:
                        await InvokeAsync(sender, converter.Convert(sender, argument));
                        return true;
                    default:
                        return false;
                }
            }
            catch (MiddlewareConvertNoInvokeException)
            {
                return true;
            }
        }
        
        public virtual async ValueTask InvokeValueAsync(T argument)
        {
            await InvokeAsync(argument);
        }
        
        public virtual async ValueTask InvokeValueAsync(Object? sender, T argument)
        {
            await InvokeAsync(sender, argument);
        }
        
        public async ValueTask<Boolean> InvokeValueAsync<TArgument>(TArgument argument)
        {
            try
            {
                switch (this)
                {
                    case IAsyncMiddleware<TArgument> middleware:
                        await middleware.InvokeValueAsync(argument);
                        return true;
                    case IMiddlewareConverter<TArgument, T> converter:
                        await InvokeValueAsync(converter.Convert(argument));
                        return true;
                    default:
                        return false;
                }
            }
            catch (MiddlewareConvertNoInvokeException)
            {
                return true;
            }
        }
        
        public async ValueTask<Boolean> InvokeValueAsync<TArgument>(Object? sender, TArgument argument)
        {
            try
            {
                switch (this)
                {
                    case IAsyncMiddleware<TArgument> middleware:
                        await middleware.InvokeValueAsync(sender, argument);
                        return true;
                    case IMiddlewareAsyncConverter<TArgument, T> converter:
                        await InvokeValueAsync(sender, await converter.ConvertAsync(sender, argument));
                        return true;
                    case IMiddlewareConverter<TArgument, T> converter:
                        await InvokeValueAsync(sender, converter.Convert(sender, argument));
                        return true;
                    default:
                        return false;
                }
            }
            catch (MiddlewareConvertNoInvokeException)
            {
                return true;
            }
        }
        
        T IMiddlewareConverter<T, T>.Convert(Object? sender, T argument)
        {
            return argument;
        }
        
        ValueTask<T> IMiddlewareAsyncConverter<T, T>.ConvertAsync(Object? sender, T argument)
        {
            return ValueTask.FromResult(argument);
        }
        
        internal sealed class AsyncHandler : AsyncMiddleware<T>
        {
            private MiddlewareDelegateAsync<T> Delegate { get; }
            
            public AsyncHandler(MiddlewareDelegateAsync<T> @delegate)
            {
                Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
            }
            
            public override async Task InvokeAsync(T argument)
            {
                if (!Memorize(argument))
                {
                    return;
                }

                try
                {
                    await Delegate.Invoke(argument);
                }
                catch (Exception)
                {
                    Forget(argument);
                    throw;
                }
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
            
            public override async Task InvokeAsync(Object? sender, T argument)
            {
                if (!Memorize(sender, argument))
                {
                    return;
                }

                try
                {
                    await Delegate.Invoke(sender, argument);
                }
                catch (Exception)
                {
                    Forget(sender, argument);
                    throw;
                }
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
    
    public abstract class Middleware<T> : Middleware, IMiddleware<T>, IMiddlewareConverter<T, T>
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
        
        protected ConcurrentHashSet<(Object? Sender, T Argument)> Memory { get; } = new ConcurrentHashSet<(Object? Sender, T Argument)>();
        
        public Boolean IsAsync
        {
            get
            {
                return false;
            }
        }

        protected Boolean Memorize(T argument)
        {
            return Memorize(Memory, null, argument);
        }

        protected Boolean Memorize(Object? sender, T argument)
        {
            return Memorize(Memory, sender, argument);
        }

        protected Boolean Forget(T argument)
        {
            return Forget(Memory, null, argument);
        }

        protected Boolean Forget(Object? sender, T argument)
        {
            return Forget(Memory, sender, argument);
        }
        
        public virtual void Invoke(T argument)
        {
            Invoke(null, argument);
        }
        
        public abstract void Invoke(Object? sender, T argument);
        
        public Boolean Invoke<TArgument>(TArgument argument)
        {
            switch (this)
            {
                case IMiddleware<TArgument> middleware:
                    middleware.Invoke(argument);
                    return true;
                case IMiddlewareConverter<TArgument, T> converter:
                    Invoke(converter.Convert(argument));
                    return true;
                default:
                    return false;
            }
        }
        
        public Boolean Invoke<TArgument>(Object? sender, TArgument argument)
        {
            switch (this)
            {
                case IMiddleware<TArgument> middleware:
                    middleware.Invoke(sender, argument);
                    return true;
                case IMiddlewareConverter<TArgument, T> converter:
                    Invoke(sender, converter.Convert(sender, argument));
                    return true;
                default:
                    return false;
            }
        }
        
        T IMiddlewareConverter<T, T>.Convert(Object? sender, T argument)
        {
            return argument;
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
                if (!Memorize(argument))
                {
                    return;
                }

                try
                {
                    Delegate.Invoke(argument);
                }
                catch (Exception)
                {
                    Forget(argument);
                    throw;
                }
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
                if (!Memorize(sender, argument))
                {
                    return;
                }

                try
                {
                    Delegate.Invoke(sender, argument);
                }
                catch (Exception)
                {
                    Forget(sender, argument);
                    throw;
                }
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
        internal MiddlewareExecutionOptions Options
        {
            init
            {
                Context = value.Context;
                Idempotency = value.Idempotency;
            }
        }

        public MiddlewareExecutionContext Context { get; init; }
        public MiddlewareIdempotencyMode Idempotency { get; init; }

        private Boolean _single;
        protected virtual Boolean Memorize<T>(ConcurrentHashSet<(Object?, T)> set, Object? sender, T argument)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Idempotency switch
            {
                MiddlewareIdempotencyMode.None => true,
                MiddlewareIdempotencyMode.Single => _single = true,
                MiddlewareIdempotencyMode.Sender when sender is not null => set.Add((sender, default!)),
                MiddlewareIdempotencyMode.Sender => true,
                MiddlewareIdempotencyMode.Argument => set.Add((null, argument)),
                MiddlewareIdempotencyMode.SenderArgument => set.Add((sender, argument)),
                _ => throw new EnumUndefinedOrNotSupportedException<MiddlewareIdempotencyMode>(Idempotency, nameof(Idempotency), null)
            };
        }

        protected virtual Boolean Forget<T>(ConcurrentHashSet<(Object?, T)> set, Object? sender, T argument)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Idempotency switch
            {
                MiddlewareIdempotencyMode.None => false,
                MiddlewareIdempotencyMode.Single => _single && !(_single = false),
                MiddlewareIdempotencyMode.Sender when sender is not null => set.TryRemove((sender, default!)),
                MiddlewareIdempotencyMode.Sender => false,
                MiddlewareIdempotencyMode.Argument => set.TryRemove((null, argument)),
                MiddlewareIdempotencyMode.SenderArgument => set.TryRemove((sender, argument)),
                _ => throw new EnumUndefinedOrNotSupportedException<MiddlewareIdempotencyMode>(Idempotency, nameof(Idempotency), null)
            };
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
        public static IMiddleware<T> Create<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new Middleware<T>.Handler(@delegate) { Options = options };
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
        public static IMiddleware<T> Create<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new Middleware<T>.SenderHandler(@delegate) { Options = options };
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
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncMiddleware<T>.AsyncHandler(@delegate) { Options = options };
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
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return new AsyncMiddleware<T>.AsyncSenderHandler(@delegate) { Options = options };
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
        public static IAsyncMiddleware<T> Create<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueHandler(@delegate) { Options = options };
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
        public static IAsyncMiddleware<T> Create<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionOptions options)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return new AsyncValueMiddleware<T>.AsyncValueSenderHandler(@delegate) { Options = options };
        }
    }
}