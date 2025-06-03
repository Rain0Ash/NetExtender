// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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