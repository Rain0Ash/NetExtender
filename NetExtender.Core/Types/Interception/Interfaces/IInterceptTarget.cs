using System;
using System.Threading.Tasks;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IAnyInterceptTarget<in TArgument> : IInterceptTarget<TArgument>, IAsyncInterceptTarget<TArgument>
    {
    }
    
    public interface IAnyInterceptTarget<T, in TArgument> : IInterceptTarget<T, TArgument>, IAsyncInterceptTarget<T, TArgument>
    {
    }
    
    public interface IAsyncInterceptTarget<in TArgument> : IInterceptTargetRaise<TArgument>
    {
        public Task InvokeAsync(TArgument argument);
    }
    
    public interface IAsyncInterceptTarget<T, in TArgument> : IInterceptTargetResult<T, TArgument>
    {
        public ValueTask<T> InvokeAsync(TArgument argument);
    }
    
    public interface IInterceptTarget<in TArgument> : IInterceptTargetRaise<TArgument>
    {
        public void Invoke(TArgument argument);
    }
    
    public interface IInterceptTarget<out T, in TArgument> : IInterceptTargetResult<T, TArgument>
    {
        public T Invoke(TArgument argument);
    }

    public interface IInterceptTargetResult<out T, in TArgument> : IInterceptTargetRaise<TArgument>
    {
        public Boolean HasResult(TArgument argument);
        public T Result(TArgument argument);
    }

    public interface IInterceptTargetRaise<in TArgument>
    {
        public void RaiseIntercepting(TArgument args);
        public void RaiseIntercepted(TArgument args);
    }
}