// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.Builder
{
    public abstract class ApplicationBuilder<T> : IApplicationBuilder<T> where T : class
    {
        public virtual T Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public abstract T Build(String[] arguments);

        public virtual T Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }
}