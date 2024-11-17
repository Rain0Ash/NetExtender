using System;
using System.Threading.Tasks;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Interception
{
    public abstract class AnyInterceptor<T, TSender, TArgument> : Interceptor<TSender, TArgument>, IAnyInterceptor<T, TSender, TArgument> where TSender : IInterceptTarget<T, TArgument>, IAsyncInterceptTarget<T, TArgument> where TArgument : class, ISimpleInterceptEventArgs<T>
    {
        public static AnyInterceptor<T, TSender, TArgument> Default { get; } = new Any();
        
        public abstract T Intercept(TSender sender, TArgument args);
        public abstract ValueTask<T> InterceptAsync(TSender sender, TArgument args);

        private sealed class Any : AnyInterceptor<T, TSender, TArgument>
        {
            public override T Intercept(TSender sender, TArgument args)
            {
                return Interceptor<T, TSender, TArgument>.Default.Intercept(sender, args);
            }

            public override ValueTask<T> InterceptAsync(TSender sender, TArgument args)
            {
                return AsyncInterceptor<T, TSender, TArgument>.Default.InterceptAsync(sender, args);
            }
        }
    }
    
    public class AsyncInterceptor<T, TSender, TArgument> : Interceptor<TSender, TArgument>, IAsyncInterceptor<T, TSender, TArgument> where TSender : IAsyncInterceptTarget<T, TArgument> where TArgument : class, ISimpleInterceptEventArgs<T>
    {
        public static AsyncInterceptor<T, TSender, TArgument> Default { get; } = new AsyncInterceptor<T, TSender, TArgument>();
        
        // ReSharper disable once CognitiveComplexity
        public virtual async ValueTask<T> InterceptAsync(TSender sender, TArgument args)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            await Wait(args);
            
            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (sender.HasResult(args))
            {
                return sender.Result(args);
            }

            Maybe<T> result = default;

            try
            {
                result = await sender.InvokeAsync(args);
            }
            catch (Exception exception)
            {
                args.Intercept(exception);
                sender.RaiseIntercepted(args);
                await Wait(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (result.HasValue)
            {
                args.Intercept(result.Value);
                sender.RaiseIntercepted(args);
                await Wait(args);
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            return sender.Result(args);
        }
    }
    
    public class Interceptor<T, TSender, TArgument> : Interceptor<TSender, TArgument>, IInterceptor<T, TSender, TArgument> where TSender : IInterceptTarget<T, TArgument> where TArgument : ISimpleInterceptEventArgs<T>
    {
        public static Interceptor<T, TSender, TArgument> Default { get; } = new Interceptor<T, TSender, TArgument>();
        
        public virtual T Intercept(TSender sender, TArgument args)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            
            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (sender.HasResult(args))
            {
                return sender.Result(args);
            }

            Maybe<T> result = default;

            try
            {
                result = sender.Invoke(args);
            }
            catch (Exception exception)
            {
                args.Intercept(exception);
                sender.RaiseIntercepted(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (result.HasValue)
            {
                args.Intercept(result.Value);
                sender.RaiseIntercepted(args);
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            return sender.Result(args);
        }
    }

    public abstract class Interceptor<TSender, TArgument> where TSender : IInterceptTargetRaise<TArgument> where TArgument : ISimpleInterceptEventArgs
    {
        protected virtual Task Wait(TArgument argument)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(nameof(argument));
            }

            if (!argument.IsCancel)
            {
                return Task.CompletedTask;
            }

            TimeSpan wait = argument.Wait;
            return wait > default(TimeSpan) ? Task.Delay(wait, argument.Token) : Task.CompletedTask;
        }
    }
}