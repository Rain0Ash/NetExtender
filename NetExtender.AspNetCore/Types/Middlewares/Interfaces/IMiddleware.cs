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
    
    public interface IMiddleware<in TArgument>
    {
        public void Invoke(HttpContext context, TArgument argument);
    }
    
    public interface IMiddleware<in TArgument1, in TArgument2>
    {
        public void Invoke(HttpContext context, TArgument1 first, TArgument2 second);
    }
    
    public interface IMiddleware<in TArgument1, in TArgument2, in TArgument3>
    {
        public void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third);
    }
    
    public interface IMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4>
    {
        public void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth);
    }
    
    public interface IMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5>
    {
        public void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth);
    }
    
    public interface IMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5, in TArgument6>
    {
        public void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth, TArgument6 sixth);
    }

    public interface IInvokeMiddleware : IMiddleware<RequestDelegate>
    {
        public new void Invoke(HttpContext context, RequestDelegate next);
    }

    public interface IInvokeMiddleware<in TArgument> : IMiddleware<RequestDelegate, TArgument>
    {
        public new void Invoke(HttpContext context, RequestDelegate next, TArgument argument);
    }

    public interface IInvokeMiddleware<in TArgument1, in TArgument2> : IMiddleware<RequestDelegate, TArgument1, TArgument2>
    {
        public new void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second);
    }

    public interface IInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3> : IMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3>
    {
        public new void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third);
    }

    public interface IInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4> : IMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3, TArgument4>
    {
        public new void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth);
    }

    public interface IInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5> : IMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        public new void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth);
    }

    public interface IAsyncMiddleware
    {
        public Task InvokeAsync(HttpContext context);
    }

    public interface IAsyncMiddleware<in TArgument>
    {
        public Task InvokeAsync(HttpContext context, TArgument argument);
    }

    public interface IAsyncMiddleware<in TArgument1, in TArgument2>
    {
        public Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second);
    }

    public interface IAsyncMiddleware<in TArgument1, in TArgument2, in TArgument3>
    {
        public Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third);
    }

    public interface IAsyncMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4>
    {
        public Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth);
    }

    public interface IAsyncMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5>
    {
        public Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth);
    }

    public interface IAsyncMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5, in TArgument6>
    {
        public Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth, TArgument6 sixth);
    }

    public interface IAsyncInvokeMiddleware : IAsyncMiddleware<RequestDelegate>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next);
    }

    public interface IAsyncInvokeMiddleware<in TArgument> : IAsyncMiddleware<RequestDelegate, TArgument>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument argument);
    }

    public interface IAsyncInvokeMiddleware<in TArgument1, in TArgument2> : IAsyncMiddleware<RequestDelegate, TArgument1, TArgument2>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second);
    }

    public interface IAsyncInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3> : IAsyncMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third);
    }

    public interface IAsyncInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4> : IAsyncMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3, TArgument4>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth);
    }

    public interface IAsyncInvokeMiddleware<in TArgument1, in TArgument2, in TArgument3, in TArgument4, in TArgument5> : IAsyncMiddleware<RequestDelegate, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        public new Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth);
    }
}