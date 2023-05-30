// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.Builder
{
    public abstract class ApplicationBuilder<T> : IApplicationBuilder<T> where T : class
    {
        protected virtual T New()
        {
            return New<T>();
        }

        protected virtual TType New<TType>() where TType : class
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

        public abstract T Build(String[] arguments);

        public virtual T Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }
}