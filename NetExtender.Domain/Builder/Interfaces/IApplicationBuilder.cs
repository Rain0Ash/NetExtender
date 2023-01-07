// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;

namespace NetExtender.Domains.Builder.Interfaces
{
    public interface IApplicationBuilder<out T> where T : class
    {
        public T Build();
        public T Build(String[] arguments);
        public T Build(ImmutableArray<String> arguments);
    }
}