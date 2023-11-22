// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.Builder
{
    public abstract class ApplicationBuilder<T> : IApplicationBuilder<T> where T : class
    {
        protected virtual T New(ImmutableArray<String> arguments)
        {
            return New<T>(arguments);
        }

        protected virtual TType New<TType>(ImmutableArray<String> arguments) where TType : class
        {
            try
            {
                TType? instance = Activator.CreateInstance(typeof(TType), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) as TType;
                return instance ?? throw new InvalidOperationException($"Can't create instance of {typeof(TType)} for builder");
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Can't create instance of {typeof(TType)} for builder", exception);
            }
        }
        
        public virtual T Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public virtual T Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public abstract T Build(ImmutableArray<String> arguments);
    }
}