// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Middlewares.Interfaces;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public abstract class MiddlewareAbstraction
    {
        protected RequestDelegate Next { get; }

        protected MiddlewareAbstraction(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }
    }
    
    public abstract class InvokeMiddlewareAbstraction
    {
    }
    
    public abstract class Middleware : MiddlewareAbstraction, Interfaces.IMiddleware
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument> : MiddlewareAbstraction, IMiddleware<TArgument>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument argument)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument1, TArgument2> : MiddlewareAbstraction, IMiddleware<TArgument1, TArgument2>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument1 first, TArgument2 second)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3> : MiddlewareAbstraction, IMiddleware<TArgument1, TArgument2, TArgument3>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4> : MiddlewareAbstraction, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : MiddlewareAbstraction, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6> : MiddlewareAbstraction, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>
    {
        protected Middleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual void Invoke(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth, TArgument6 sixth)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware : InvokeMiddlewareAbstraction, IInvokeMiddleware
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware<TArgument> : InvokeMiddlewareAbstraction, IInvokeMiddleware<TArgument>
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next, TArgument argument)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2> : InvokeMiddlewareAbstraction, IInvokeMiddleware<TArgument1, TArgument2>
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3> : InvokeMiddlewareAbstraction, IInvokeMiddleware<TArgument1, TArgument2, TArgument3>
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : InvokeMiddlewareAbstraction, IInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : InvokeMiddlewareAbstraction, IInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        public virtual void Invoke(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware : MiddlewareAbstraction, IAsyncMiddleware
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument> : MiddlewareAbstraction, IAsyncMiddleware<TArgument>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument argument)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument1, TArgument2> : MiddlewareAbstraction, IAsyncMiddleware<TArgument1, TArgument2>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3> : MiddlewareAbstraction, IAsyncMiddleware<TArgument1, TArgument2, TArgument3>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : MiddlewareAbstraction, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : MiddlewareAbstraction, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6> : MiddlewareAbstraction, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>
    {
        protected AsyncMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public virtual async Task InvokeAsync(HttpContext context, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth, TArgument6 sixth)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware<TArgument> : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware<TArgument>
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument argument)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2> : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware<TArgument1, TArgument2>
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3> : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3>
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : InvokeMiddlewareAbstraction, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
    {
        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next, TArgument1 first, TArgument2 second, TArgument3 third, TArgument4 fourth, TArgument5 fifth)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}