// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

namespace NetExtender.Types.Intercept.Interfaces
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