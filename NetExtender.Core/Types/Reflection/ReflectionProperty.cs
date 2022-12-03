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

        private static ConcurrentDictionary<String, Handler> Cache { get; } = new ConcurrentDictionary<String, Handler>();

        private Handler Internal { get; }

        public Type Source
        {
            get
            {
                return typeof(TSource);
            }
        }

        public Type Property
        {
            get
            {
                return typeof(TProperty);
            }
        }

        public ReflectionProperty(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Internal = Cache.GetOrAdd(name, static name => new Handler(name));
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

            Internal = Cache.GetOrAdd(info.Name, static (_, expression) => new Handler(expression), property);
        }

        public TProperty GetValue(in TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Func<TSource, TProperty> getter = Internal.Getter ?? throw new InvalidOperationException();
            return getter.Invoke(source);
        }

        Object? IReflectionProperty<TSource>.GetValue(in TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return GetValue(in source);
        }

        Object? IReflectionProperty.GetValue(in Object source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is not TSource @object)
            {
                throw new ArgumentException($"Source is not {typeof(TSource).Name}", nameof(source));
            }

            return GetValue(in @object);
        }

        public void SetValue(in TSource source, TProperty value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Action<TSource, TProperty> setter = Internal.Setter ?? throw new InvalidOperationException();
            setter.Invoke(source, value);
        }

        void IReflectionProperty<TSource>.SetValue(in TSource source, Object? value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TProperty property = value switch
            {
                null => default!,
                TProperty result => result,
                _ => throw new ArgumentException($"Value is not {typeof(TProperty).Name}", nameof(value))
            };
            
            SetValue(in source, property);
        }

        void IReflectionProperty.SetValue(in Object source, Object? value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (source is not TSource @object)
            {
                throw new ArgumentException($"Source is not {typeof(TSource).Name}", nameof(source));
            }
            
            TProperty property = value switch
            {
                null => default!,
                TProperty result => result,
                _ => throw new ArgumentException($"Value is not {typeof(TProperty).Name}", nameof(value))
            };
            
            SetValue(in @object, property);
        }

        private readonly struct Handler
        {
            public Func<TSource, TProperty>? Getter { get; }
            public Action<TSource, TProperty>? Setter { get; }

            public Handler(String property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                Getter = ExpressionUtilities.TryCreateGetExpression(property, out Expression<Func<TSource, TProperty>>? getter) ? getter.Compile() : null;
                Setter = ExpressionUtilities.TryCreateSetExpression(property, out Expression<Action<TSource, TProperty>>? setter) ? setter.Compile() : null;
            }

            public Handler(Expression<Func<TSource, TProperty>> property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                Getter = property.Compile();
                Setter = property.TryCreateSetExpression(out Expression<Action<TSource, TProperty>>? setter) ? setter.Compile() : null;
            }

            public Handler(Func<TSource, TProperty> getter, Action<TSource, TProperty> setter)
            {
                Getter = getter ?? throw new ArgumentNullException(nameof(getter));
                Setter = setter ?? throw new ArgumentNullException(nameof(setter));
            }
        }
    }
}