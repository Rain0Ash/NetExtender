// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Middlewares.Interfaces;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public abstract class MiddlewareBase
    {
        protected RequestDelegate Next { get; }

        protected MiddlewareBase(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }
    }
    
    public abstract class InvokeMiddlewareBase
    {
    }
    
    public abstract class Middleware : MiddlewareBase, Interfaces.IMiddleware
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
    
    public abstract class Middleware<TArgument> : MiddlewareBase, IMiddleware<TArgument>
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
    
    public abstract class Middleware<TArgument1, TArgument2> : MiddlewareBase, IMiddleware<TArgument1, TArgument2>
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
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3> : MiddlewareBase, IMiddleware<TArgument1, TArgument2, TArgument3>
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
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4> : MiddlewareBase, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
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
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : MiddlewareBase, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
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
    
    public abstract class Middleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6> : MiddlewareBase, IMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>
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
    
    public abstract class InvokeMiddleware : InvokeMiddlewareBase, IInvokeMiddleware
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
    
    public abstract class InvokeMiddleware<TArgument> : InvokeMiddlewareBase, IInvokeMiddleware<TArgument>
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
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2> : InvokeMiddlewareBase, IInvokeMiddleware<TArgument1, TArgument2>
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
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3> : InvokeMiddlewareBase, IInvokeMiddleware<TArgument1, TArgument2, TArgument3>
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
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : InvokeMiddlewareBase, IInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
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
    
    public abstract class InvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : InvokeMiddlewareBase, IInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
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

    public abstract class AsyncMiddleware : MiddlewareBase, IAsyncMiddleware
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

    public abstract class AsyncMiddleware<TArgument> : MiddlewareBase, IAsyncMiddleware<TArgument>
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

    public abstract class AsyncMiddleware<TArgument1, TArgument2> : MiddlewareBase, IAsyncMiddleware<TArgument1, TArgument2>
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

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3> : MiddlewareBase, IAsyncMiddleware<TArgument1, TArgument2, TArgument3>
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

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : MiddlewareBase, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
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

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : MiddlewareBase, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
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

    public abstract class AsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6> : MiddlewareBase, IAsyncMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>
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
    
    public abstract class AsyncInvokeMiddleware : InvokeMiddlewareBase, IAsyncInvokeMiddleware
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
    
    public abstract class AsyncInvokeMiddleware<TArgument> : InvokeMiddlewareBase, IAsyncInvokeMiddleware<TArgument>
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
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2> : InvokeMiddlewareBase, IAsyncInvokeMiddleware<TArgument1, TArgument2>
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
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3> : InvokeMiddlewareBase, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3>
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
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4> : InvokeMiddlewareBase, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4>
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
    
    public abstract class AsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5> : InvokeMiddlewareBase, IAsyncInvokeMiddleware<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>
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