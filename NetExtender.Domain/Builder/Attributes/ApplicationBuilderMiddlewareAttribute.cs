// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Middlewares.Attributes;

namespace NetExtender.Domains.Builder
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ApplicationBuilderMiddlewareAttribute : MiddlewareRegisterAttribute
    {
    }
}