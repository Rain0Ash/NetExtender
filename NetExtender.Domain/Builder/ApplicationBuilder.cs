// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;
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
        
        protected ImmutableArray<String>? Arguments
        {
            get
            {
                return NetExtender.Initializer.Initializer.Arguments;
            }
            set
            {
                NetExtender.Initializer.Initializer.Arguments = value;
            }
        }
        
        protected virtual Boolean Confidential
        {
            get
            {
                return false;
            }
        }
        
        public virtual Boolean IsScan
        {
            get
            {
                return true;
            }
        }
        
        public virtual ReflectionPatchThrow Patch
        {
            get
            {
                return ReflectionPatchThrow.Throw;
            }
        }
        
        protected virtual void Setup(ImmutableArray<String> arguments)
        {
            Arguments = arguments;
            
            switch (Patch)
            {
                case ReflectionPatchThrow.Ignore:
                    return;
                case ReflectionPatchThrow.Log:
                    switch (ReflectionPatchUtilities.Failed.ToArray())
                    {
                        case { Length: 1 } patches:
                        {
                            (IReflectionPatchInfo patch, Exception? exception) = patches[0];
                            $"Patch '{patch}' failed{(exception?.Message is { } message ? $":{Environment.NewLine}{message}" : ".")}".ToConsole(ConsoleColor.Red);
                            return;
                        }
                        case { Length: > 1 } patches:
                        {
                            $"Patching failed:{Environment.NewLine}{String.Join($"{Environment.NewLine}    ", patches.Keys())}".ToConsole(ConsoleColor.Red);
                            return;
                        }
                        default:
                        {
                            return;
                        }
                    }
                case ReflectionPatchThrow.Throw:
                case ReflectionPatchThrow.LogThrow:
                    switch (ReflectionPatchUtilities.Failed.ToArray())
                    {
                        case { Length: 1 } patches:
                        {
                            (IReflectionPatchInfo patch, Exception? exception) = patches[0];
                            throw new ReflectionOperationException($"Patch '{patch}' failed.", exception);
                        }
                        case { Length: > 1 } patches:
                        {
                            throw new AggregateException("Patching failed.", patches.Select(static pair => new ReflectionOperationException($"Patch '{pair.Key}' failed.", pair.Value)));
                        }
                        default:
                        {
                            return;
                        }
                    }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ReflectionPatchThrow>(Patch, nameof(Patch), null);
            }
        }
        
        protected virtual void Finish()
        {
            if (Confidential)
            {
                Arguments = null;
            }
        }

        protected virtual T New(ImmutableArray<String> arguments)
        {
            return New<T>(arguments);
        }

        protected virtual TType New<TType>(ImmutableArray<String> arguments) where TType : class
        {
            Setup(arguments);
            
            try
            {
                TType? instance = Activator.CreateInstance(typeof(TType), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) as TType;
                return instance ?? throw new InvalidOperationException($"Can't create instance of {typeof(TType)} for builder");
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Can't create instance of {typeof(TType)} for builder", exception);
            }
            finally
            {
                Finish();
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