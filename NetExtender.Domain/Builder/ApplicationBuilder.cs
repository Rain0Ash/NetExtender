// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Patch;
using NetExtender.Types.Attributes.Interfaces;
using NetExtender.Types.Console.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.Builder
{
    //TODO: ConsoleApplicationBuilder
    public abstract class ApplicationBuilder<T> : IApplicationBuilder<T> where T : class
    {
        public virtual IConsole Console
        {
            get
            {
                return IConsole.Default;
            }
        }
        
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

        protected virtual Boolean IsDesignMode
        {
            get
            {
                return Assembly.GetEntryAssembly()?.GetName().Name == "ef";
            }
        }

        protected virtual void Setup(ImmutableArray<String> arguments)
        {
            Arguments = arguments;
            
            foreach (IInvokeAttribute attribute in ReflectionUtilities.GetCustomAttributes<PatchAttribute>(GetType()).OfType<IInvokeAttribute>())
            {
                attribute.Invoke(this, null);
            }

            NetExtender.Initializer.Initializer.IsDomain = true;
            
            switch (Patch)
            {
                case ReflectionPatchThrow.Ignore:
                    goto successful;
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
                            goto successful;
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
                            goto successful;
                        }
                    }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ReflectionPatchThrow>(Patch, nameof(Patch), null);
            }

            successful:
            Initialize(arguments);
        }

        protected virtual void Finish()
        {
            if (Confidential)
            {
                Arguments = null;
            }
        }

        protected virtual void Initialize(ImmutableArray<String> arguments)
        {
            Initialize(arguments, Console);
        }

        protected virtual void Initialize(ImmutableArray<String> arguments, IConsole console)
        {
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
                return instance ?? throw new InvalidOperationException($"Can't create instance of '{typeof(TType).Name}' for builder.");
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Can't create instance of '{typeof(TType).Name}' for builder.", exception);
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