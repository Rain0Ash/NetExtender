// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares.Interfaces
{
    public interface IMiddleware
    {
        public void Invoke(HttpContext context);
    }

    public interface IAsyncMiddleware
    {
        public Task InvokeAsync(HttpContext context);
    }
}