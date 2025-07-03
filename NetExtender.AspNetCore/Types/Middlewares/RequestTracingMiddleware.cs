using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    //TODO:
    public class RequestTracingMiddleware : AsyncMiddleware
    {
        public RequestTracingMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public override Task InvokeAsync(HttpContext context)
        {
            return base.InvokeAsync(context);
        }
    }
}