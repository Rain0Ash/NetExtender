using System;
using NetExtender.Types.Middlewares.Attributes;

namespace NetExtender.Domains.Builder
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ApplicationBuilderMiddlewareAttribute : MiddlewareRegisterAttribute
    {
    }
}