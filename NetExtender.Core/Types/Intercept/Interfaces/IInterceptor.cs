// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IAnyInterceptor<in TSender, in TArgument> : IInterceptor<TSender, TArgument>, IAsyncInterceptor<TSender, TArgument> where TSender : IInterceptTarget<TArgument>, IAsyncInterceptTarget<TArgument>
    {
    }
    
    public interface IAnyInterceptor<T, in TSender, in TArgument> : IInterceptor<T, TSender, TArgument>, IAsyncInterceptor<T, TSender, TArgument> where TSender : IInterceptTarget<T, TArgument>, IAsyncInterceptTarget<T, TArgument>
    {
    }

    public interface IAsyncInterceptor<in TSender, in TArgument> where TSender : IAsyncInterceptTarget<TArgument>
    {
        public ValueTask InterceptAsync(TSender sender, TArgument args);
    }
    
    public interface IAsyncInterceptor<T, in TSender, in TArgument> where TSender : IAsyncInterceptTarget<T, TArgument>
    {
        public ValueTask<T> InterceptAsync(TSender sender, TArgument args);
    }
    
    public interface IInterceptor<in TSender, in TArgument> where TSender : IInterceptTarget<TArgument>
    {
        public void Intercept(TSender sender, TArgument args);
    }
    
    public interface IInterceptor<out T, in TSender, in TArgument> where TSender : IInterceptTarget<T, TArgument>
    {
        public T Intercept(TSender sender, TArgument args);
    }
}