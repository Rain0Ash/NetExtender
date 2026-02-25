// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Cecil;
using NetExtender.Types.Comparers;
using NetExtender.Exceptions;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Attributes;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public static partial class MiddlewareUtilities
    {
        private readonly struct Options
        {
            public TypeSet Source { get; init; }
            public Inherit.Result Inherit { get; init; }
        }

        private readonly struct MiddlewareResult : IComparableStruct<MiddlewareResult>
        {
            public MonoCecilType Type { get; }
            public IMiddlewareInfo? Middleware { get; }
            public Exception? Exception { get; init; }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type is null;
                }
            }

            public MiddlewareResult(MonoCecilType type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Middleware = null;
                Exception = null;
            }

            public MiddlewareResult(MonoCecilType type, IMiddlewareInfo middleware)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
                Exception = null;
            }

            public Int32 CompareTo(MiddlewareResult other)
            {
                return TypeComparer.NameOrdinalIgnoreCase.Compare(Type, other.Type);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static List<MiddlewareResult>? ScanStaticType(MonoCecilType type, Options _, out MiddlewareResult result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo? method = type.Type?.GetMethod(nameof(MiddlewareRegister.Register), binding, Type.EmptyTypes);

            static Boolean IsMiddleware(Type @interface)
            {
                if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition() != typeof(IEnumerable<>))
                {
                    return false;
                }

                return @interface.GetGenericArguments()[0].IsAssignableTo(typeof(IMiddlewareInfo));
            }

            if (method is null || !method.ReturnType.GetSafeInterfacesUnsafe().Prepend(method.ReturnType).Any(IsMiddleware))
            {
                result = new MiddlewareResult(type)
                {
                    Exception = new MissingMethodException($"Type '{type.FullName}' must contains method 'static T:{nameof(IEnumerable)}<T{nameof(Middleware)}:{nameof(IMiddlewareInfo)}> {nameof(MiddlewareRegister.Register)}()'.")
                };

                return null;
            }

            static IEnumerable? Source(MonoCecilType type, MethodInfo method, out MiddlewareResult result)
            {
                try
                {
                    IEnumerable? source = method.Invoke(null, Array.Empty<Object>()) as IEnumerable;

                    result = source is not null ? default : new MiddlewareResult(type)
                    {
                        Exception = new TypeNotSupportedException(type, $"Method '{method.Name}' of type '{type}' must return {nameof(IEnumerable)}.")
                    };

                    return source;
                }
                catch (Exception exception)
                {
                    result = new MiddlewareResult(type)
                    {
                        Exception = new TypeNotSupportedException(type, $"Exception when invoke {method.Name} of type '{type}'.", exception)
                    };

                    return null;
                }
            }

            if (Source(type, method, out result) is not { } source)
            {
                return null;
            }

            static List<MiddlewareResult> Handle(IEnumerable source, MonoCecilType type)
            {
                List<MiddlewareResult> result = new List<MiddlewareResult>();

                foreach (IMiddlewareInfo middleware in source.OfType<IMiddlewareInfo>().ToArray())
                {
                    if (middleware.Context == MiddlewareExecutionContext.Sequential)
                    {
                        result.Add(new MiddlewareResult(type)
                        {
                            Exception = new TypeNotSupportedException(type, $"Middleware '{type}' must not be '{nameof(MiddlewareExecutionContext.Sequential)}'.")
                        });

                        continue;
                    }

                    result.Add(new MiddlewareResult(type, middleware));
                }

                return result;
            }

            return Handle(source, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static MiddlewareResult ScanType(MonoCecilType type, Options options)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!options.Inherit[typeof(IMiddleware<>)].Types.Contains(type) && !options.Inherit[typeof(IAsyncMiddleware<>)].Types.Contains(type))
            {
                return new MiddlewareResult(type)
                {
                    Exception = new TypeNotSupportedException(type, $"Type '{type}' must implements '{typeof(IMiddleware<>).Name}' or '{typeof(IAsyncMiddleware<>).Name}'.")
                };
            }

            try
            {
                if (type.Type is not { } instance || Activator.CreateInstance(instance) is not IMiddlewareInfo middleware)
                {
                    return new MiddlewareResult(type)
                    {
                        Exception = new TypeNotSupportedException(type, $"Can't create middleware of type '{type}'.")
                    };
                }

                if (middleware.Context == MiddlewareExecutionContext.Sequential)
                {
                    return new MiddlewareResult(type)
                    {
                        Exception = new TypeNotSupportedException(type, $"Middleware '{type}' must not be '{nameof(MiddlewareExecutionContext.Sequential)}'.")
                    };
                }

                return new MiddlewareResult(type, middleware);

            }
            catch (Exception exception)
            {
                return new MiddlewareResult(type)
                {
                    Exception = new TypeNotSupportedException(type, $"Can't create middleware of type '{type}'.", exception)
                };
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IMiddlewareManager Scan(this IMiddlewareManager manager, Type attribute, Options options)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            ConcurrentBag<MiddlewareResult> bag = new ConcurrentBag<MiddlewareResult>();

            void Handler(MonoCecilType? type)
            {
                if (type is null || type.IsInterface || !options.Inherit.Attributes[attribute].Types.Contains(type))
                {
                    return;
                }

                try
                {
                    if (!type.IsAbstract)
                    {
                        bag.Add(ScanType(type, options));
                        return;
                    }

                    if (type is not { IsAbstract: true, IsSealed: true })
                    {
                        return;
                    }

                    if (ScanStaticType(type, options, out MiddlewareResult result) is not { } results)
                    {
                        bag.Add(result);
                        return;
                    }

                    bag.AddRange(results);
                }
                catch (Exception exception)
                {
                    bag.Add(new MiddlewareResult(type) { Exception = exception });
                }
            }

            Parallel.ForEach<MonoCecilType>(options.Source, Handler);
            return Verify(manager, bag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IMiddlewareManager Scan<TAttribute>(this IMiddlewareManager manager, Options options) where TAttribute : MiddlewareRegisterAttribute
        {
            return Scan(manager, typeof(TAttribute), options);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IMiddlewareManager Verify(IMiddlewareManager manager, ConcurrentBag<MiddlewareResult> bag)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (bag is null)
            {
                throw new ArgumentNullException(nameof(bag));
            }

            MiddlewareResult[] result = bag.ToArray();

            Array.Sort(result);
            if (result.Select(static result => result.Exception).WhereNotNull().ToArray() is { Length: > 0 } exceptions)
            {
                return exceptions.Length switch
                {
                    0 => manager,
                    1 => throw new ScanOperationException(null, exceptions[0]),
                    _ => throw new ScanOperationException(null, new AggregateException(exceptions))
                };
            }

            manager.AddRange(result.Select(static result => result.Middleware).WhereNotNull().ToArray());
            return manager;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan(this IMiddlewareManager manager)
        {
            return Scan<MiddlewareRequiredAttribute>(manager);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan<TAttribute>(this IMiddlewareManager manager) where TAttribute : MiddlewareRegisterAttribute
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            Inherit.Result inherit = ReflectionUtilities.Inherit;
            Options options = new Options
            {
                Source = inherit.Attributes[typeof(TAttribute)].Types,
                Inherit = inherit
            };

            return Scan<TAttribute>(manager, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan(this IMiddlewareManager manager, IEnumerable<MonoCecilType?> source)
        {
            return Scan<MiddlewareRequiredAttribute>(manager, source);
        }

        public static IMiddlewareManager Scan<TAttribute>(this IMiddlewareManager manager, IEnumerable<MonoCecilType?> source) where TAttribute : MiddlewareRegisterAttribute
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Inherit.Result inherit = ReflectionUtilities.Inherit;
            Options options = new Options
            {
                Source = inherit.Attributes[typeof(Attribute)].Types.Intersect(source!),
                Inherit = inherit
            };

            return Scan<TAttribute>(manager, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan(this IMiddlewareManager manager, Assembly assembly)
        {
            return Scan<MiddlewareRequiredAttribute>(manager, assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan<TAttribute>(this IMiddlewareManager manager, Assembly assembly) where TAttribute : MiddlewareRegisterAttribute
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Scan<TAttribute>(manager, assembly.GetCecilTypes());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan(this IMiddlewareManager manager, IEnumerable<Assembly?> assemblies)
        {
            return Scan<MiddlewareRequiredAttribute>(manager, assemblies);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMiddlewareManager Scan<TAttribute>(this IMiddlewareManager manager, IEnumerable<Assembly?> assemblies) where TAttribute : MiddlewareRegisterAttribute
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            return Scan<TAttribute>(manager, assemblies.GetCecilTypes());
        }
    }
}