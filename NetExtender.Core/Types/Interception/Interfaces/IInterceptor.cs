using System.Threading.Tasks;

namespace NetExtender.Types.Interception.Interfaces
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