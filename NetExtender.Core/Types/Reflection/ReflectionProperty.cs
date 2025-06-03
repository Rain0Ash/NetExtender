// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    [Flags]
    public enum ReflectionPropertyType : Byte
    {
        None = 0,
        Get = 1,
        Set = 2,
        All = Get | Set
    }

    public readonly struct ReflectionProperty : IReflectionProperty
    {
        public static implicit operator ReflectionProperty(PropertyInfo value)
        {
            return value is not null ? new ReflectionProperty(value) : throw new ArgumentNullException(nameof(value));
        }
        
        private IReflectionProperty Internal { get; }
        
        public String Name
        {
            get
            {
                return Internal.Name;
            }
        }

        public ReflectionPropertyType Type
        {
            get
            {
                return Internal.Type;
            }
        }

        public Type Source
        {
            get
            {
                return Internal.Source;
            }
        }

        public Type Property
        {
            get
            {
                return Internal.Property;
            }
        }
        
        public ReflectionProperty(PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            if (info.DeclaringType is null)
            {
                throw new ArgumentNullException(nameof(info) + "." + nameof(info.DeclaringType));
            }

            Internal = (IReflectionProperty?) Activator.CreateInstance(typeof(ReflectionProperty<,>).MakeGenericType(info.DeclaringType, info.PropertyType), info.Name) ?? throw new InvalidOperationException();
        }

        public ReflectionProperty(String name, Type source, Type property)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Internal = (IReflectionProperty?) Activator.CreateInstance(typeof(ReflectionProperty<,>).MakeGenericType(source, property), name) ?? throw new InvalidOperationException();
        }

        public Object? GetValue(in Object source)
        {
            return Internal.GetValue(in source);
        }

        public void SetValue(in Object source, Object? value)
        {
            Internal.SetValue(source, value);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }
    }
    
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
        
        public static implicit operator ReflectionProperty<TSource, TProperty>(PropertyInfo value)
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

        public String Name
        {
            get
            {
                return Internal.Name;
            }
        }

        private Handler Internal { get; }

        public ReflectionPropertyType Type
        {
            get
            {
                ReflectionPropertyType result = ReflectionPropertyType.None;

                if (Internal.Getter is not null)
                {
                    result |= ReflectionPropertyType.Get;
                }

                if (Internal.Setter is not null)
                {
                    result |= ReflectionPropertyType.Set;
                }
                
                return result;
            }
        }

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

        public ReflectionProperty(PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Internal = Cache.GetOrAdd(info.Name, static name => new Handler(name));
        }

        public ReflectionProperty(Expression<Func<TSource, TProperty>> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.GetPropertyInfo() is not { } info)
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

        // ReSharper disable once ReturnTypeCanBeNotNullable
        public override String? ToString()
        {
            return Name;
        }

        private readonly struct Handler
        {
            public String Name { get; }
            public Func<TSource, TProperty>? Getter { get; }
            public Action<TSource, TProperty>? Setter { get; }

            public Handler(String property)
            {
                Name = property ?? throw new ArgumentNullException(nameof(property));
                Getter = ExpressionUtilities.TryCreateGetExpression(property, out Expression<Func<TSource, TProperty>>? getter) ? getter.Compile() : null;
                Setter = ExpressionUtilities.TryCreateSetExpression(property, out Expression<Action<TSource, TProperty>>? setter) ? setter.Compile() : null;
            }

            public Handler(Expression<Func<TSource, TProperty>> property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                Name = property.Name ?? throw new ArgumentNullException(nameof(property)+ "." + nameof(property.Name));
                Getter = property.Compile();
                Setter = property.TryCreateSetExpression(out Expression<Action<TSource, TProperty>>? setter) ? setter.Compile() : null;
            }

            public Handler(String property, Func<TSource, TProperty> getter, Action<TSource, TProperty> setter)
            {
                Name = property ?? throw new ArgumentNullException(nameof(property));
                Getter = getter ?? throw new ArgumentNullException(nameof(getter));
                Setter = setter ?? throw new ArgumentNullException(nameof(setter));
            }
        }
    }
}