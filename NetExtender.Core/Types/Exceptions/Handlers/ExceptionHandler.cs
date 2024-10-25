using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading.Tasks;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Exceptions.Handlers
{
    public enum ExceptionHandlerAction
    {
        Successful,
        Default,
        Ignore,
        Throw,
        Rethrow
    }
    
    public delegate ExceptionHandlerAction ExceptionHandlerDelegate(Exception? exception);
    public delegate ExceptionHandlerAction ArgumentExceptionHandlerDelegate(Object? argument, Exception? exception);
    public delegate ExceptionHandlerAction ArgumentExceptionHandlerDelegate<in T>(T? argument, Exception? exception);

    public abstract class ExceptionHandler
    {
        public Boolean Invoke(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Unit Action()
            {
                action();
                return Unit.Default;
            }

            return Invoke(Action, out _);
        }
        
        public abstract Boolean Invoke<T>(Func<T> action, [MaybeNullWhen(false)] out T result);
        
        public async ValueTask<Boolean> Invoke(Func<Task> action)
        {
            async Task<Unit> Action()
            {
                await action();
                return Unit.Default;
            }

            Maybe<Unit> result = await Invoke(Action);
            return result.HasValue;
        }
        
        public abstract ValueTask<Maybe<T>> Invoke<T>(Func<Task<T>> request);

        protected virtual ExceptionHandlerAction Handle(Exception? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Rethrow;
        }

        protected virtual void Finally()
        {
        }
    }
}