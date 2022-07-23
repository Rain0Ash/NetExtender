// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using NetExtender.Utilities.Core;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyPropertyUtilities
    {
        private static Boolean Analyze(IReflect type, String name, [MaybeNullWhen(false)] out FieldInfo field, [MaybeNullWhen(false)] out PropertyInfo property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            field = type.GetField($"{name}Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return property is not null && field is not null;
        }
        
        // ReSharper disable once UnusedTupleComponentInReturnValue
        private static (Type Type, FieldInfo Dependency, PropertyInfo Property) Analyze(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StackTrace stack = new StackTrace(1);

            foreach (Type type in stack.GetStackTypesUnique())
            {
                if (!Analyze(type, name, out FieldInfo? field, out PropertyInfo? property))
                {
                    continue;
                }

                if (!field.FieldType.IsSameAsOrSubclassOf<DependencyProperty>())
                {
                    continue;
                }

                if (!field.IsStatic || !field.IsInitOnly)
                {
                    throw new InvalidOperationException($"Dependency property field {field.Name} must be static and readonly.");
                }
                
                return (type, field, property);
            }

            throw new MissingMemberException($"Could not find {name} {nameof(DependencyProperty)} field and access property.");
        }
        
        public static DependencyProperty Register<T, TType>(String name)
        {
            return DependencyProperty.Register(name, typeof(T), typeof(TType));
        }
        
        public static DependencyProperty Register<T, TType>(String name, PropertyMetadata metadata)
        {
            return DependencyProperty.Register(name, typeof(T), typeof(TType), metadata);
        }

        public static DependencyProperty Register<T, TType>(String name, PropertyChangedCallback callback)
        {
            return Register<T, TType>(name, new PropertyMetadata(callback));
        }

        public static DependencyProperty Register<T, TType>(String name, Boolean twoway)
        {
            return twoway ? Register<T, TType>(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register<T, TType>(name);
        }

        public static DependencyProperty Register<T, TType>(String name, FrameworkPropertyMetadataOptions options)
        {
            return Register<T, TType>(name, new FrameworkPropertyMetadata(default(T), options));
        }
        
        public static DependencyProperty Register<T, TType>(String name, Boolean twoway, PropertyChangedCallback callback)
        {
            return twoway ? Register<T, TType>(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register<T, TType>(name, callback);
        }

        public static DependencyProperty Register<T, TType>(String name, FrameworkPropertyMetadataOptions options, PropertyChangedCallback callback)
        {
            return Register<T, TType>(name, new FrameworkPropertyMetadata(default(T), options, callback));
        }
        
        public static DependencyProperty Register<T, TType>(String name, T value)
        {
            return Register<T, TType>(name, new PropertyMetadata(value));
        }
        
        public static DependencyProperty Register<T, TType>(String name, T value, PropertyChangedCallback callback)
        {
            return Register<T, TType>(name, new PropertyMetadata(value, callback));
        }

        public static DependencyProperty Register<T, TType>(String name, T value, Boolean twoway)
        {
            return twoway ? Register<T, TType>(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register<T, TType>(name, value);
        }

        public static DependencyProperty Register<T, TType>(String name, T value, FrameworkPropertyMetadataOptions options)
        {
            return Register<T, TType>(name, new FrameworkPropertyMetadata(value, options));
        }

        public static DependencyProperty Register<T, TType>(String name, T value, Boolean twoway, PropertyChangedCallback callback)
        {
            return twoway ? Register<T, TType>(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register<T, TType>(name, value, callback);
        }

        public static DependencyProperty Register<T, TType>(String name, T value, FrameworkPropertyMetadataOptions options, PropertyChangedCallback callback)
        {
            return Register<T, TType>(name, new FrameworkPropertyMetadata(value, options, callback));
        }

        public static DependencyProperty Register(String name)
        {
            (Type type, _, PropertyInfo property) = Analyze(name);
            return DependencyProperty.Register(name, property.PropertyType, type);
        }

        public static DependencyProperty Register(String name, PropertyMetadata metadata)
        {
            (Type type, _, PropertyInfo property) = Analyze(name);

            Object? value = metadata.DefaultValue;

            if (value is PropertyMetadata inner)
            {
                value = inner;
            }

            value ??= ReflectionUtilities.Default(property.PropertyType);
            
            if (value is not null && !value.GetType().IsSameAsOrSubclassOf(property.PropertyType))
            {
                throw new InvalidOperationException($"Default value type '{value.GetType()}' does not match property type '{property.PropertyType}'.");
            }
            
            return DependencyProperty.Register(name, property.PropertyType, type, metadata);
        }

        public static DependencyProperty Register(String name, PropertyChangedCallback callback)
        {
            return Register(name, new PropertyMetadata(callback));
        }

        public static DependencyProperty Register(String name, Boolean twoway)
        {
            return twoway ? Register(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register(name);
        }

        public static DependencyProperty Register(String name, FrameworkPropertyMetadataOptions options)
        {
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(default, options));
        }
        
        public static DependencyProperty Register(String name, Boolean twoway, PropertyChangedCallback callback)
        {
            return twoway ? Register(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register(name, callback);
        }

        public static DependencyProperty Register(String name, FrameworkPropertyMetadataOptions options, PropertyChangedCallback callback)
        {
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(default, options, callback));
        }
        
        public static DependencyProperty Register<T>(String name, T value)
        {
            return Register(name, new PropertyMetadata(value));
        }
        
        public static DependencyProperty Register<T>(String name, T value, PropertyChangedCallback callback)
        {
            return Register(name, new PropertyMetadata(value, callback));
        }

        public static DependencyProperty Register<T>(String name, T value, Boolean twoway)
        {
            return twoway ? Register(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register(name, value);
        }

        public static DependencyProperty Register<T>(String name, T value, FrameworkPropertyMetadataOptions options)
        {
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(value, options));
        }

        public static DependencyProperty Register<T>(String name, T value, Boolean twoway, PropertyChangedCallback callback)
        {
            return twoway ? Register(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register(name, value, callback);
        }

        public static DependencyProperty Register<T>(String name, T value, FrameworkPropertyMetadataOptions options, PropertyChangedCallback callback)
        {
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(value, options, callback));
        }
    }
}