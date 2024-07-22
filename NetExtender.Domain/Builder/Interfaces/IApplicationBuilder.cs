// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Domains.Builder.Interfaces
{
    public interface IApplicationBuilder<out T> : IScannable where T : class
    {
        public IMiddlewareManager? Manager { get; }
        
        public T Build();
        public T Build(String[] arguments);
        public T Build(ImmutableArray<String> arguments);
    }
}