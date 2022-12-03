// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Types.Reflection.Interfaces;

namespace NetExtender.Types.Anonymous
{
    public static class AnonymousActivator
    {
        public static IAnonymousActivatorInfo Create(Type type, params Type[] arguments)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return arguments.Length switch
            {
                0 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<>).MakeGenericType(type)),
                1 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                2 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                3 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                4 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                5 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                6 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                7 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                8 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                9 => (IAnonymousActivatorInfo?) Activator.CreateInstance(typeof(AnonymousActivator<,,,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments.Length, null)
            } ?? throw new InvalidOperationException($"Can't create {nameof(IAnonymousActivatorInfo)} instance");
        }
    }

    public readonly struct AnonymousActivator<TSource> : IAnonymousActivator where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate()
        {
            return Internal.Activate();
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject>.Activate()
        {
            return Activate();
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T> : IAnonymousActivator<T> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T argument)
        {
            return Internal.Activate(argument);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T>.Activate(T argument)
        {
            return Activate(argument);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2> : IAnonymousActivator<T1, T2> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second)
        {
            return Internal.Activate(first, second);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2>.Activate(T1 first, T2 second)
        {
            return Activate(first, second);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3> : IAnonymousActivator<T1, T2, T3> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third)
        {
            return Internal.Activate(first, second, third);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3>.Activate(T1 first, T2 second, T3 third)
        {
            return Activate(first, second, third);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4> : IAnonymousActivator<T1, T2, T3, T4> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth)
        {
            return Internal.Activate(first, second, third, fourth);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4>.Activate(T1 first, T2 second, T3 third, T4 fourth)
        {
            return Activate(first, second, third, fourth);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4, T5> : IAnonymousActivator<T1, T2, T3, T4, T5> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4, T5> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4, T5>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Internal.Activate(first, second, third, fourth, fifth);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5>.Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Activate(first, second, third, fourth, fifth);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4, T5, T6> : IAnonymousActivator<T1, T2, T3, T4, T5, T6> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Internal.Activate(first, second, third, fourth, fifth, sixth);
        }

        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6>.Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Activate(first, second, third, fourth, fifth, sixth);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4, T5, T6, T7> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Internal.Activate(first, second, third, fourth, fifth, sixth, seventh);
        }
        
        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7>.Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Activate(first, second, third, fourth, fifth, sixth, seventh);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7, T8> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Internal.Activate(first, second, third, fourth, fifth, sixth, seventh, eighth);
        }
        
        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7, T8>.Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Activate(first, second, third, fourth, fifth, sixth, seventh, eighth);
        }
    }
    
    public readonly struct AnonymousActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IAnonymousActivator<T1, T2, T3, T4, T5, T6, T7, T8, T9> where TSource : IAnonymousObject
    {
        private static ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9> Internal { get; }

        static AnonymousActivator()
        {
            Internal = new ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        }

        public Type Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Internal.Arguments;
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Internal.Activate(first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }
        
        IAnonymousObject IReflectionActivator<IAnonymousObject, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Activate(first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }
    }
}