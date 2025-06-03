// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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