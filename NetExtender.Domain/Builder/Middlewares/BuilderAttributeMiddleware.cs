using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Patch;
using NetExtender.Types.Attributes.Interfaces;
using NetExtender.Types.Middlewares;

namespace NetExtender.Domains.Builder.Middlewares
{
    [ApplicationBuilderMiddleware]
    internal sealed class BuilderAttributeMiddleware : Middleware<IApplicationBuilder>
    {
        public BuilderAttributeMiddleware()
        {
            Idempotency = MiddlewareIdempotencyMode.Argument;
        }
        
        public override void Invoke(Object? sender, IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (!Memorize(sender, builder))
            {
                return;
            }

            List<Exception> exceptions = new List<Exception>();
            foreach (Attribute attribute in builder.GetType().GetCustomAttributes())
            {
                if (attribute is PatchAttribute || attribute is not IInvokeAttribute invoke)
                {
                    continue;
                }

                try
                {
                    invoke.Invoke(sender, null);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            switch (exceptions.Count)
            {
                case 0:
                    return;
                case 1:
                    throw exceptions[0];
                default:
                    throw new AggregateException(exceptions);
            }
        }
    }
}