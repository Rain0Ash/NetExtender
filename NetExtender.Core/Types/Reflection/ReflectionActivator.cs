// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public static class ReflectionActivator
    {
        public static IReflectionActivator Create(Type type, params Type[] arguments)
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
                0 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<>).MakeGenericType(type)),
                1 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                2 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                3 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                4 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                5 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                6 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                7 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                8 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                9 => (IReflectionActivator?) Activator.CreateInstance(typeof(ReflectionActivator<,,,,,,,,,>).MakeGenericType(arguments.Prepend(type).ToArray())),
                _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments.Length, null)
            } ?? throw new InvalidOperationException($"Can't create {nameof(IReflectionActivator)} instance");
        }
    }

    public readonly struct ReflectionActivator<TSource> : IReflectionActivator<TSource>
    {
        private static Func<TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return Type.EmptyTypes;
            }
        }

        public TSource Activate()
        {
            return Activator.Invoke();
        }
    }

    public readonly struct ReflectionActivator<TSource, T> : IReflectionActivator<TSource, T>
    {
        private static Func<T, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T) };
            }
        }

        public TSource Activate(T argument)
        {
            return Activator.Invoke(argument);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2> : IReflectionActivator<TSource, T1, T2>
    {
        private static Func<T1, T2, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2) };
            }
        }

        public TSource Activate(T1 first, T2 second)
        {
            return Activator.Invoke(first, second);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3> : IReflectionActivator<TSource, T1, T2, T3>
    {
        private static Func<T1, T2, T3, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third)
        {
            return Activator.Invoke(first, second, third);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4> : IReflectionActivator<TSource, T1, T2, T3, T4>
    {
        private static Func<T1, T2, T3, T4, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth)
        {
            return Activator.Invoke(first, second, third, fourth);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4, T5> : IReflectionActivator<TSource, T1, T2, T3, T4, T5>
    {
        private static Func<T1, T2, T3, T4, T5, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            return Activator.Invoke(first, second, third, fourth, fifth);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6> : IReflectionActivator<TSource, T1, T2, T3, T4, T5, T6>
    {
        private static Func<T1, T2, T3, T4, T5, T6, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            return Activator.Invoke(first, second, third, fourth, fifth, sixth);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7> : IReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7>
    {
        private static Func<T1, T2, T3, T4, T5, T6, T7, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            return Activator.Invoke(first, second, third, fourth, fifth, sixth, seventh);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8> : IReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private static Func<T1, T2, T3, T4, T5, T6, T7, T8, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            return Activator.Invoke(first, second, third, fourth, fifth, sixth, seventh, eighth);
        }
    }

    public readonly struct ReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IReflectionActivator<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSource> Activator { get; }

        static ReflectionActivator()
        {
            Activator = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>().Compile();
        }

        public Type Type
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type[] Arguments
        {
            get
            {
                return new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9) };
            }
        }

        public TSource Activate(T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            return Activator.Invoke(first, second, third, fourth, fifth, sixth, seventh, eighth, ninth);
        }
    }
}