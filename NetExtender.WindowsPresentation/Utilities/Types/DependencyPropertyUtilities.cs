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
        private sealed record AnalysisInformation
        {
            public static Type[] CallbackMethodTypes { get; } = { typeof(DependencyObject), typeof(DependencyPropertyChangedEventArgs) };
            public static Type[] SemiCallbackMethodTypes { get; } = { typeof(DependencyObject) };

            public Type Type { get; }
            public FieldInfo Dependency { get; }
            public PropertyInfo Property { get; }
            public MethodInfo? Method { get; }
            public PropertyChangedCallback? Delegate { get; }

            public AnalysisInformation(Type type, FieldInfo dependency, PropertyInfo property, MethodInfo? method)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
                Property = property ?? throw new ArgumentNullException(nameof(property));
                Method = method;
                Delegate = CreateCallback(method);
            }
        }

        private delegate void PropertyChangedSemiCallback(DependencyObject sender);

        [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
        private static PropertyChangedCallback? CreateCallback(MethodInfo? method)
        {
            if (method is null)
            {
                return null;
            }

            if (method.TryCreateDelegate<PropertyChangedCallback>(out PropertyChangedCallback? callback))
            {
                return callback;
            }

            if (method.TryCreateDelegate<PropertyChangedSemiCallback>(out PropertyChangedSemiCallback? semi))
            {
                return (sender, _) => semi(sender);
            }

            return null;
        }

        private static Boolean Analyze(Type type, String name, [MaybeNullWhen(false)] out MethodInfo result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            result = type.GetMethod($"On{name}Changed", binding, AnalysisInformation.CallbackMethodTypes) ??
                type.GetMethod($"{name}Changed", binding, AnalysisInformation.CallbackMethodTypes) ??
                type.GetMethod($"Change{name}", binding, AnalysisInformation.CallbackMethodTypes) ??
                type.GetMethod($"On{name}Changed", binding, AnalysisInformation.SemiCallbackMethodTypes) ??
                type.GetMethod($"{name}Changed", binding, AnalysisInformation.SemiCallbackMethodTypes) ??
                type.GetMethod($"Change{name}", binding, AnalysisInformation.SemiCallbackMethodTypes);

            return result is not null;
        }

        private static Boolean Analyze(Type type, String name, [MaybeNullWhen(false)] out AnalysisInformation result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo? property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            FieldInfo? field = type.GetField($"{name}Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            Analyze(type, name, out MethodInfo? method);
            result = field is not null && property is not null ? new AnalysisInformation(type, field, property, method) : null;
            return result is not null;
        }

        private static Boolean Verify(AnalysisInformation information)
        {
            if (information is null)
            {
                throw new ArgumentNullException(nameof(information));
            }

            if (!information.Dependency.FieldType.IsSameAsOrSubclassOf<DependencyProperty>())
            {
                return false;
            }

            if (!information.Dependency.IsStatic || !information.Dependency.IsInitOnly)
            {
                throw new InvalidOperationException($"Dependency property field {information.Dependency.Name} must be static and readonly.");
            }

            if (information.Method is not null && !information.Method.IsStatic)
            {
                throw new InvalidOperationException($"Dependency property method {information.Method.Name} must be static.");
            }

            return true;
        }

        private static AnalysisInformation Analyze(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StackTrace stack = new StackTrace(1);

            foreach (Type type in stack.GetStackTypesUnique())
            {
                if (!Analyze(type, name, out AnalysisInformation? information) || !Verify(information))
                {
                    continue;
                }

                return information;
            }

            throw new MissingMemberException($"Could not find {name} {nameof(DependencyProperty)} field and access property.");
        }

        public static DependencyProperty Register<T, TType>(String name)
        {
            return Register<T, TType>(name, (PropertyMetadata?) null);
        }

        public static DependencyProperty Register<T, TType>(String name, PropertyMetadata? metadata)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            metadata ??= new PropertyMetadata();
            metadata.DefaultValue ??= default(T);
            metadata.PropertyChangedCallback ??= Analyze(typeof(TType), name, out MethodInfo? method) ? CreateCallback(method) : null;
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
            return Register(name, (PropertyMetadata?) null);
        }

        public static DependencyProperty Register(String name, PropertyMetadata? metadata)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            AnalysisInformation information = Analyze(name);

            Object? value = metadata?.DefaultValue;

            if (value is PropertyMetadata inner)
            {
                value = inner;
            }

            value ??= ReflectionUtilities.Default(information.Property.PropertyType);

            if (value is not null && !value.GetType().IsAssignableTo(information.Property.PropertyType))
            {
                throw new InvalidOperationException($"Default value type '{value.GetType()}' does not match property type '{information.Property.PropertyType}'.");
            }

            metadata ??= new PropertyMetadata();
            metadata.DefaultValue ??= value;
            metadata.PropertyChangedCallback ??= information.Delegate;
            return DependencyProperty.Register(name, information.Property.PropertyType, information.Type, metadata);
        }

        public static DependencyProperty Register(String name, PropertyChangedCallback callback)
        {
            return Register(name, new PropertyMetadata(callback));
        }

        public static DependencyProperty Register(String name, Boolean twoway)
        {
            return twoway ? Register(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register(name);
        }

        public static DependencyProperty Register(String name, Boolean value, Boolean twoway)
        {
            return Register<Boolean>(name, value, twoway);
        }

        public static DependencyProperty Register(String name, FrameworkPropertyMetadataOptions options)
        {
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(default, options));
        }

        public static DependencyProperty Register(String name, Boolean twoway, PropertyChangedCallback callback)
        {
            return twoway ? Register(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register(name, callback);
        }

        public static DependencyProperty Register(String name, Boolean value, Boolean twoway, PropertyChangedCallback callback)
        {
            return Register<Boolean>(name, value, twoway, callback);
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