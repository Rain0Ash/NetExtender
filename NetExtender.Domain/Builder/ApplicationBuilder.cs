// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.Builder
{
    public abstract class ApplicationBuilder<T> : IApplicationBuilder<T> where T : class
    {
        private IMiddlewareManager? _manager;
        public virtual IMiddlewareManager? Manager
        {
            get
            {
                return _manager ??= New(out MiddlewareManagerOptions? options) ? Scan(options.Create()) : null;
            }
        }

        public virtual Boolean IsScan
        {
            get
            {
                return true;
            }
        }

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
        
        protected virtual Boolean New([MaybeNullWhen(false)] out MiddlewareManagerOptions options)
        {
            options = new MiddlewareManagerOptions();
            return true;
        }
        
        protected IMiddlewareManager Scan<TAttribute>(IMiddlewareManager manager) where TAttribute : ApplicationBuilderMiddlewareAttribute
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            
            return IsScan ? manager.Scan<TAttribute>() : manager;
        }
        
        protected virtual IMiddlewareManager Scan(IMiddlewareManager manager)
        {
            return Scan<ApplicationBuilderMiddlewareAttribute>(manager);
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