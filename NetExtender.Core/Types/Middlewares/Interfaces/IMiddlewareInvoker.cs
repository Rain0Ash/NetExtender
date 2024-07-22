using System;
using System.Threading.Tasks;

namespace NetExtender.Types.Middlewares.Interfaces
{
    public interface IMiddlewareInvoker
    {
        public void Invoke<T>(T argument);
        public void Invoke<T>(Object? sender, T argument);
        public Task InvokeAsync<T>(T argument);
        public Task InvokeAsync<T>(Object? sender, T argument);
    }
}