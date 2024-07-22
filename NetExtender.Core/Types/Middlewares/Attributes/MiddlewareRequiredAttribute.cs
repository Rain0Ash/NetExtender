using System;

namespace NetExtender.Types.Middlewares.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MiddlewareRequiredAttribute : MiddlewareRegisterAttribute
    {
    }
}