using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Middlewares.Interfaces;

namespace NetExtender.Types.Middlewares
{
    internal static class MiddlewareRegister
    {
        [DoesNotReturn]
        public static IEnumerable<IMiddlewareInfo> Register()
        {
            throw new ReflectionExampleException();
        }
    }
}