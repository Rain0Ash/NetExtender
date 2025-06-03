// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Middlewares.Exceptions;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Middlewares
{
    [Flags]
    public enum MiddlewareManagerContext : Byte
    {
        None = 0
    }
    
    public class MiddlewareManager : IMiddlewareManager
    {
        private static Object Sender { get; } = new Object();
        protected static ConcurrentDictionary<Type, Func<IMiddlewareInfo>?> Activator { get; } = new ConcurrentDictionary<Type, Func<IMiddlewareInfo>?>(); 
        protected ConcurrentDictionary<MiddlewareExecutionContext, List<IMiddlewareInfo>> Internal { get; } = new ConcurrentDictionary<MiddlewareExecutionContext, List<IMiddlewareInfo>>();
        
        public Int32 Count
        {
            get
            {
                return Internal.Values.Sum(static context => context.Count);
            }
        }
        
        Boolean ICollection<IMiddlewareInfo>.IsReadOnly
        {
            get
            {
                return false;
            }
        }
        
        public MiddlewareManagerOptions Options { get; }
        
        MiddlewareManagerContext IMiddlewareManager.Context
        {
            get
            {
                return Options.Context;
            }
        }
        
        public MiddlewareManager()
            : this(null)
        {
        }
        
        public MiddlewareManager(MiddlewareManagerOptions? options)
        {
            Options = options ?? new MiddlewareManagerOptions();
        }
        
        protected static Func<IMiddlewareInfo>? Activate(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean Predicate(Type @interface)
            {
                if (@interface is null)
                {
                    throw new ArgumentNullException(nameof(@interface));
                }
                
                if (!@interface.IsGenericType)
                {
                    return false;
                }
                
                @interface = @interface.GetGenericTypeDefinition();
                return @interface == typeof(IMiddleware<>) || @interface == typeof(IAsyncMiddleware<>);
            }
            
            return type.GetInterfaces().Any(Predicate) ? ExpressionUtilities.CreateNewExpression<IMiddlewareInfo>(type).Compile() : null;
        }
        
        protected virtual List<IMiddlewareInfo>? Context(MiddlewareExecutionContext context, Boolean require = true)
        {
            return require ? Internal.GetOrAdd(context, static _ => new List<IMiddlewareInfo>()) : Internal.TryGetValue(context, out List<IMiddlewareInfo>? result) ? result : null;
        }
        
        protected readonly struct InvokeResult
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public Object? Sender { get; init; }
            public MiddlewareExecutionContext Execution { get; }
            public IMiddlewareInfo Middleware { get; }
            public Exception? Exception { get; init; }
            
            public InvokeResult(MiddlewareExecutionContext execution, IMiddlewareInfo middleware)
            {
                Sender = null;
                Execution = execution;
                Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
                Exception = null;
            }
            
            public override String ToString()
            {
                return $"{{ {nameof(Sender)}: {Sender}, {nameof(Execution)}: {Execution}, {nameof(Middleware)}: {Middleware} }}";
            }
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual IReadOnlyCollection<InvokeResult> Parallel<T>(Object? sender, T argument, IReadOnlyCollection<IMiddlewareInfo> context, MiddlewareExecutionContext execution, MiddlewareManagerContext manager)
        {
            ConcurrentBag<InvokeResult> result = new ConcurrentBag<InvokeResult>();
            
            if (ReferenceEquals(sender, Sender))
            {
                void Handler(IMiddlewareInfo info)
                {
                    try
                    {
                        switch (info)
                        {
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info));
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Exception = exception });
                    }
                }
                
                System.Threading.Tasks.Parallel.ForEach(context, Handler);
            }
            else
            {
                void Handler(IMiddlewareInfo info)
                {
                    try
                    {
                        switch (info)
                        {
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info) { Sender = sender });
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Sender = sender, Exception = exception });
                    }
                }
                
                System.Threading.Tasks.Parallel.ForEach(context, Handler);
            }
            
            return result;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual IReadOnlyCollection<InvokeResult> Sequential<T>(Object? sender, T argument, IReadOnlyCollection<IMiddlewareInfo> context, MiddlewareExecutionContext execution, MiddlewareManagerContext manager)
        {
            ConcurrentBag<InvokeResult> result = new ConcurrentBag<InvokeResult>();
            
            if (ReferenceEquals(sender, Sender))
            {
                foreach (IMiddlewareInfo info in context)
                {
                    try
                    {
                        switch (info)
                        {
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info));
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Exception = exception });
                    }
                }
            }
            else
            {
                foreach (IMiddlewareInfo info in context)
                {
                    try
                    {
                        switch (info)
                        {
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info) { Sender = sender });
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Sender = sender, Exception = exception });
                    }
                }
            }
            
            return result;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual async Task<IReadOnlyCollection<InvokeResult>> ParallelAsync<T>(Object? sender, T argument, IReadOnlyCollection<IMiddlewareInfo> context, MiddlewareExecutionContext execution, MiddlewareManagerContext manager)
        {
            ConcurrentBag<InvokeResult> result = new ConcurrentBag<InvokeResult>();
            
            if (ReferenceEquals(sender, Sender))
            {
                async ValueTask Handler(IMiddlewareInfo info, CancellationToken token)
                {
                    try
                    {
                        switch (info)
                        {
                            case IAsyncMiddleware<T> { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware<T> { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(argument);
                                break;
                            }
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info));
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Exception = exception });
                    }
                }
                
                await System.Threading.Tasks.Parallel.ForEachAsync(context, Handler).ConfigureAwait(false);
            }
            else
            {
                async ValueTask Handler(IMiddlewareInfo info, CancellationToken token)
                {
                    try
                    {
                        switch (info)
                        {
                            case IAsyncMiddleware<T> { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware<T> { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(sender, argument);
                                break;
                            }
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info) { Sender = sender });
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Sender = sender, Exception = exception });
                    }
                }
                
                await System.Threading.Tasks.Parallel.ForEachAsync(context, Handler).ConfigureAwait(false);
            }
            
            return result;
        }
        
        // ReSharper disable once CognitiveComplexity
        protected virtual async Task<IReadOnlyCollection<InvokeResult>> SequentialAsync<T>(Object? sender, T argument, IReadOnlyCollection<IMiddlewareInfo> context, MiddlewareExecutionContext execution, MiddlewareManagerContext manager)
        {
            ConcurrentBag<InvokeResult> result = new ConcurrentBag<InvokeResult>();
            
            if (ReferenceEquals(sender, Sender))
            {
                foreach (IMiddlewareInfo info in context)
                {
                    try
                    {
                        switch (info)
                        {
                            case IAsyncMiddleware<T> { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware<T> { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(argument);
                                break;
                            }
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info));
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Exception = exception });
                    }
                }
            }
            else
            {
                foreach (IMiddlewareInfo info in context)
                {
                    try
                    {
                        switch (info)
                        {
                            case IAsyncMiddleware<T> { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware<T> { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: true } middleware:
                            {
                                await middleware.InvokeValueAsync(sender, argument);
                                break;
                            }
                            case IAsyncMiddleware { IsValue: false } middleware:
                            {
                                await middleware.InvokeAsync(sender, argument);
                                break;
                            }
                            case IMiddleware<T> middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                            case IMiddleware middleware:
                            {
                                middleware.Invoke(sender, argument);
                                break;
                            }
                        }
                        
                        result.Add(new InvokeResult(execution, info) { Sender = sender });
                    }
                    catch (Exception exception)
                    {
                        result.Add(new InvokeResult(execution, info) { Sender = sender, Exception = exception });
                    }
                }
            }
            
            return result;
        }
        
        public void Invoke<T>(T argument)
        {
            Invoke(Sender, argument);
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual void Invoke<T>(Object? sender, T argument)
        {
            MiddlewareManagerContext manager = Options.Context;
            
            IReadOnlyCollection<InvokeResult>[] result = new IReadOnlyCollection<InvokeResult>[Internal.Count];
            
            Int32 i = 0;
            foreach ((MiddlewareExecutionContext execution, List<IMiddlewareInfo>? context) in Internal.OrderByKeys())
            {
                switch (execution)
                {
                    case MiddlewareExecutionContext.Parallel:
                        result[i++] = Parallel(sender, argument, context, execution, manager);
                        continue;
                    case MiddlewareExecutionContext.Sequential:
                        result[i++] = Sequential(sender, argument, context, execution, manager);
                        continue;
                    default:
                        goto case default;
                }
            }
            
            InvokeResult[] exception = result.SelectMany().Where(static result => result.Exception is not null).ToArray();
            
            static MiddlewareInvokeException Selector(InvokeResult exception)
            {
                return new MiddlewareInvokeException($"Middleware: '{exception}'", exception.Exception);
            }
            
            switch (exception.Length)
            {
                case <= 0:
                    return;
                case 1:
                    throw new MiddlewareInvokeException($"Exception when invoke middleware: '{exception[0]}'.", exception[0].Exception);
                default:
                    throw new MiddlewareInvokeException("Exception when invoke middlewares:", new AggregateException(exception.Select(Selector)));
            }
        }
        
        public Task InvokeAsync<T>(T argument)
        {
            return InvokeAsync(Sender, argument);
        }
        
        public virtual async Task InvokeAsync<T>(Object? sender, T argument)
        {
            MiddlewareManagerContext manager = Options.Context;
            Task<IReadOnlyCollection<InvokeResult>>[] result = new Task<IReadOnlyCollection<InvokeResult>>[Internal.Count];
            
            Int32 i = 0;
            foreach ((MiddlewareExecutionContext execution, List<IMiddlewareInfo>? context) in Internal.OrderByKeys())
            {
                switch (execution)
                {
                    case MiddlewareExecutionContext.Parallel:
                        result[i++] = ParallelAsync(sender, argument, context, execution, manager);
                        continue;
                    case MiddlewareExecutionContext.Sequential:
                        result[i++] = SequentialAsync(sender, argument, context, execution, manager);
                        continue;
                    default:
                        goto case default;
                }
            }
            
            InvokeResult[] exception = (await Task.WhenAll(result)).SelectMany().Where(static result => result.Exception is not null).ToArray();
            
            static MiddlewareInvokeException Selector(InvokeResult exception)
            {
                return new MiddlewareInvokeException($"Middleware: '{exception}'", exception.Exception);
            }
            
            switch (exception.Length)
            {
                case <= 0:
                    return;
                case 1:
                    throw new MiddlewareInvokeException($"Exception when invoke middleware: '{exception[0]}'.", exception[0].Exception);
                default:
                    throw new MiddlewareInvokeException("Exception when invoke middlewares:", new AggregateException(exception.Select(Selector)));
            }
        }
        
        public Boolean Contains(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            try
            {
                if (Activator.GetOrAdd(type, Activate) is null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.GetType() == type));
        }
        
        public Boolean Contains<T>() where T : IMiddlewareInfo
        {
            try
            {
                if (Activator.GetOrAdd(typeof(T), Activate) is null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
            }
            
            return Internal.Values.Any(static context => context.Any(static middleware => middleware.GetType() == typeof(T)));
        }
        
        public Boolean Contains(IMiddlewareInfo middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }
            
            return Context(middleware.Context, false) is { } context && context.Contains(middleware);
        }
        
        public Boolean Contains<T>(MiddlewareDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public Boolean Contains<T>(MiddlewareDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public Boolean Contains<T>(MiddlewareDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Internal.Values.Any(context => context.Any(middleware => middleware.Equals(@delegate)));
        }
        
        public Boolean Contains<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            return Context(execution, false)?.Any(middleware => middleware.Equals(@delegate)) ?? false;
        }
        
        public IMiddlewareInfo Add(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (Activator.GetOrAdd(type, Activate) is not { } activator)
            {
                throw new TypeNotSupportedException(type);
            }
            
            IMiddlewareInfo middleware = activator();
            Add(middleware);
            return middleware;
        }
        
        public T Add<T>() where T : IMiddlewareInfo
        {
            if (Activator.GetOrAdd(typeof(T), Activate) is not { } activator)
            {
                throw new TypeNotSupportedException(typeof(T));
            }
            
            T middleware = (T) activator();
            Add(middleware);
            return middleware;
        }
        
        public void Add(IMiddlewareInfo middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }
            
            if (Context(middleware.Context) is not { } context)
            {
                throw new InvalidOperationException();
            }
            
            context.Add(middleware);
        }
        
        public IMiddleware<T> Add<T>(MiddlewareDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            Middleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IMiddleware<T> Add<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public IMiddleware<T> Add<T>(MiddlewareSenderDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            Middleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IMiddleware<T> Add<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            AsyncMiddleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IAsyncMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            AsyncMiddleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IAsyncMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            AsyncValueMiddleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IAsyncMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            AsyncValueMiddleware<T> middleware = @delegate;
            Add(middleware);
            return middleware;
        }
        
        public IAsyncMiddleware<T> Add<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            IAsyncMiddleware<T> middleware = Middleware.Create(@delegate, execution);
            Add(middleware);
            return middleware;
        }
        
        public void AddRange<TMiddleware>(IEnumerable<TMiddleware> source) where TMiddleware : IMiddlewareInfo
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            void Handler(TMiddleware? middleware)
            {
                if (middleware is not null)
                {
                    Add(middleware);
                }
            }
            
            System.Threading.Tasks.Parallel.ForEach(source, Handler);
        }
        
        public Int32 Remove(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            try
            {
                if (Activator.GetOrAdd(type, Activate) is null)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
            }
            
            return Internal.Values.Sum(context => context.RemoveAll(middleware => middleware.GetType() == type));
        }
        
        public Int32 Remove<T>() where T : IMiddlewareInfo
        {
            try
            {
                if (Activator.GetOrAdd(typeof(T), Activate) is null)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
            }
            
            return Internal.Values.Sum(static context => context.RemoveAll(static middleware => middleware.GetType() == typeof(T)));
        }
        
        public Boolean Remove(IMiddlewareInfo middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }
            
            if (Context(middleware.Context) is not { } context)
            {
                throw new InvalidOperationException();
            }
            
            return context.Remove(middleware);
        }
        
        public IMiddleware<T>? Remove<T>(MiddlewareDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IMiddleware<T>? middleware = context[index] as IMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IMiddleware<T>? Remove<T>(MiddlewareDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IMiddleware<T>? middleware = context[index] as IMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IMiddleware<T>? Remove<T>(MiddlewareSenderDelegate<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IMiddleware<T>? middleware = context[index] as IMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IMiddleware<T>? Remove<T>(MiddlewareSenderDelegate<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IMiddleware<T>? middleware = context[index] as IMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateValueAsync<T> @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
                
                if (index < 0)
                {
                    continue;
                }
                
                IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
                context.RemoveAt(index);
                return middleware;
            }
            
            return null;
        }
        
        public IAsyncMiddleware<T>? Remove<T>(MiddlewareSenderDelegateValueAsync<T> @delegate, MiddlewareExecutionContext execution)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }
            
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            Int32 index = context.FindIndex(middleware => middleware.Equals(@delegate));
            IAsyncMiddleware<T>? middleware = context[index] as IAsyncMiddleware<T>;
            context.RemoveAt(index);
            return middleware;
        }
        
        public IMiddlewareInfo? RemoveAt(MiddlewareExecutionContext execution, Int32 index)
        {
            if (Context(execution, false) is not { } context)
            {
                return null;
            }
            
            IMiddlewareInfo middleware = context[index];
            context.RemoveAt(index);
            return middleware;
        }
        
        public IMiddlewareInfo[] RemoveAll(Predicate<IMiddlewareInfo> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            List<IMiddlewareInfo> result = new List<IMiddlewareInfo>();
            
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                result.AddRange(context.FindAll(predicate));
                context.RemoveAll(predicate);
            }
            
            return result.ToArray();
        }
        
        public IMiddlewareInfo[] RemoveAll(Predicate<IMiddlewareInfo> predicate, MiddlewareExecutionContext execution)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (Context(execution) is not { } context)
            {
                return Array.Empty<IMiddlewareInfo>();
            }
            
            List<IMiddlewareInfo> result = context.FindAll(predicate);
            context.RemoveAll(predicate);
            return result.ToArray();
        }
        
        public void Clear(MiddlewareExecutionContext execution)
        {
            if (Context(execution, false) is not { } context)
            {
                return;
            }
            
            context.Clear();
        }
        
        public void Clear()
        {
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                context.Clear();
            }
        }
        
        public IEnumerator<IMiddlewareInfo> GetEnumerator()
        {
            IMiddlewareInfo[] array = new IMiddlewareInfo[Count];
            
            Int32 index = 0;
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                context.CopyTo(array, index);
                index += context.Count;
            }
            
            return ((IEnumerable<IMiddlewareInfo>) array).GetEnumerator();
        }

        public void CopyTo(IMiddlewareInfo[] array, Int32 index)
        {
            if (array.Length - index < Count)
            {
                throw new ArgumentException("The destination array does not have enough space to copy all elements.");
            }
            
            Int32 current = index;
            foreach (List<IMiddlewareInfo> context in Internal.Values)
            {
                context.CopyTo(array, current);
                current += context.Count;
            }
        }
        
        public virtual void From(IMiddlewareManager manager)
        {
            AddRange(manager);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Int32 this[MiddlewareExecutionContext execution]
        {
            get
            {
                return Context(execution, false)?.Count ?? 0;
            }
        }
        
        public IMiddlewareInfo? this[MiddlewareExecutionContext execution, Int32 index]
        {
            get
            {
                return Context(execution, false)?[index];
            }
            set
            {
                if (Context(execution) is not { } context)
                {
                    throw new EnumUndefinedOrNotSupportedException<MiddlewareExecutionContext>(execution, nameof(execution), null);
                }
                
                context[index] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
    
    public class MiddlewareManagerOptions
    {
        public static MiddlewareManagerOptions Default
        {
            get
            {
                return new MiddlewareManagerOptions();
            }
        }
        
        public MiddlewareManagerContext Context { get; init; }
        
        public virtual IMiddlewareManager Create()
        {
            return new MiddlewareManager(this);
        }
    }
}