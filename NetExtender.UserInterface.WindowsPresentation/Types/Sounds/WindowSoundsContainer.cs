// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.UserInterface.WindowsPresentation.Sounds
{
    public abstract class WindowSoundsContainer
    {
        static WindowSoundsContainer()
        {
            String _ = ReflectionUtilities.GetEntryAssemblyNamespace(out Assembly assembly);

            if (assembly.GetTypeWithoutNamespace($"{nameof(Default)}{nameof(WindowSoundsContainer)}") is not { } type || !type.IsSubclassOf(typeof(WindowSoundsContainer)))
            {
                return;
            }

            @default = Activator.CreateInstance(type) as WindowSoundsContainer;
        }
        
        public static WindowSoundsContainer Null
        {
            get
            {
                return WindowSoundsNullContainer.Instance;
            }
        }

        private static readonly WindowSoundsContainer? @default;
        public static WindowSoundsContainer Default
        {
            get
            {
                return @default ?? Null;
            }
        }

        public abstract ValueTask Play(String? value);

        public ValueTask Play<T>(T value) where T : unmanaged, Enum
        {
            return EnumHandler<T>.Play(this, value);
        }

        private sealed class WindowSoundsNullContainer : WindowSoundsContainer
        {
            public static WindowSoundsNullContainer Instance { get; } = new WindowSoundsNullContainer();
            
            public override ValueTask Play(String? value)
            {
                return ValueTask.CompletedTask;
            }
        }
        
        private static class EnumHandler<T> where T : unmanaged, Enum
        {
            private delegate ValueTask PlayDelegate(Object instance, T value);
            private static ConcurrentDictionary<Type, PlayDelegate?> Storage { get; } = new ConcurrentDictionary<Type, PlayDelegate?>();

            private static PlayDelegate? Register(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                const BindingFlags binding = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
                MethodInfo? method = type.GetMethod(nameof(WindowSoundsContainer.Play), 0, binding, new []{ typeof(T) });

                if (method is null || method.ReturnType != typeof(ValueTask))
                {
                    return null;
                }
                
                ParameterExpression instance = Expression.Parameter(typeof(Object), nameof(instance));
                ParameterExpression value = Expression.Parameter(typeof(T), nameof(value));
                UnaryExpression convert = Expression.Convert(instance, type);
                MethodCallExpression call = Expression.Call(convert, method, value);
                return Expression.Lambda<PlayDelegate>(call, instance, value).Compile();
            }

            public static ValueTask Play(WindowSoundsContainer container, T value)
            {
                if (container is null)
                {
                    throw new ArgumentNullException(nameof(container));
                }

                if (Storage.GetOrAdd(container.GetType(), Register) is { } @delegate)
                {
                    return @delegate.Invoke(container, value);
                }
                
                return container.Play(value.ToString());
            }
        }
    }

    public abstract class WindowSoundsContainerSingleton<T> : WindowSoundsContainer where T : WindowSoundsContainer, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }
}