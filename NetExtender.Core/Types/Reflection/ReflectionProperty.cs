// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Reflection
{
    public readonly struct ReflectionProperty<TSource, TProperty> : IReflectionProperty<TSource, TProperty> where TSource : notnull
    {
        public static implicit operator ReflectionProperty<TSource, TProperty>(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new ReflectionProperty<TSource, TProperty>(value);
        }

        public static implicit operator ReflectionProperty<TSource, TProperty>(Expression<Func<TSource, TProperty>> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new ReflectionProperty<TSource, TProperty>(value);
        }

        private static ConcurrentDictionary<String, Property> Cache { get; } = new ConcurrentDictionary<String, Property>();

        private Property Internal { get; }

        public ReflectionProperty(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Internal = Cache.GetOrAdd(name, static name => new Property(name));
        }

        public ReflectionProperty(Expression<Func<TSource, TProperty>> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.GetPropertyInfo() is not PropertyInfo info)
            {
                throw new InvalidOperationException();
            }

            Internal = Cache.GetOrAdd(info.Name, static (_, expression) => new Property(expression), property);
        }

        public TProperty GetValue(in TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Internal.Getter.Invoke(source);
        }

        public void SetValue(in TSource source, TProperty value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Internal.Setter.Invoke(source, value);
        }

        private readonly struct Property
        {
            public Func<TSource, TProperty> Getter { get; }
            public Action<TSource, TProperty> Setter { get; }

            public Property(String property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                Getter = ExpressionUtilities.CreateGetExpression<TSource, TProperty>(property).Compile();
                Setter = ExpressionUtilities.CreateSetExpression<TSource, TProperty>(property).Compile();
            }

            public Property(Expression<Func<TSource, TProperty>> property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                Getter = property.Compile();
                Setter = property.CreateSetExpression().Compile();
            }

            public Property(Func<TSource, TProperty> getter, Action<TSource, TProperty> setter)
            {
                Getter = getter ?? throw new ArgumentNullException(nameof(getter));
                Setter = setter ?? throw new ArgumentNullException(nameof(setter));
            }
        }
    }
}