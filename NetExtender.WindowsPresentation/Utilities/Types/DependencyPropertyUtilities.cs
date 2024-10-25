// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using Expression = System.Linq.Expressions.Expression;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyPropertyUtilities
    {
        [ReflectionNaming]
        private static Action<FrameworkPropertyMetadata, FrameworkPropertyMetadataOptions> TranslateFlags { get; }
        
        static DependencyPropertyUtilities()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo method = typeof(FrameworkPropertyMetadata).GetMethod(nameof(TranslateFlags), binding) ?? throw new MissingMethodException(nameof(FrameworkPropertyMetadata), nameof(TranslateFlags));

            ParameterExpression instance = Expression.Parameter(typeof(FrameworkPropertyMetadata), nameof(instance));
            ParameterExpression options = Expression.Parameter(typeof(FrameworkPropertyMetadataOptions), nameof(options));
            
            MethodCallExpression call = Expression.Call(instance, method, options);
            TranslateFlags = Expression.Lambda<Action<FrameworkPropertyMetadata, FrameworkPropertyMetadataOptions>>(call, instance, options).Compile();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("metadata")]
        public static TMetadata? Seal<TMetadata>(TMetadata? metadata) where TMetadata : PropertyMetadata
        {
            OneWayBugHandler.Seal(metadata);
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("metadata")]
        public static TMetadata? Unseal<TMetadata>(TMetadata? metadata) where TMetadata : PropertyMetadata
        {
            OneWayBugHandler.Unseal(metadata);
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetConverter(this DependencyObject target, DependencyProperty property, IValueConverter? converter)
        {
            SetConverter(property, target, converter);
        }

        public static void SetConverter(this DependencyProperty property, DependencyObject target, IValueConverter? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (property.GetBinding(target) is not { } binding)
            {
                return;
            }

            binding = binding.MemberwiseClone();
            binding.Converter = converter;
            
            BindingOperations.SetBinding(target, property, binding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetConverter(this DependencyObject target, DependencyProperty property, Func<IValueConverter?, IValueConverter?> converter)
        {
            SetConverter(property, target, converter);
        }
        
        public static void SetConverter(this DependencyProperty property, DependencyObject target, Func<IValueConverter?, IValueConverter?> converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (property.GetBinding(target) is not { } binding)
            {
                return;
            }

            binding = binding.MemberwiseClone();
            binding.Converter = converter(binding.Converter);

            BindingOperations.SetBinding(target, property, binding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Binding? GetBinding(this DependencyObject target, DependencyProperty property)
        {
            return GetBinding(property, target);
        }

        public static Binding? GetBinding(this DependencyProperty property, DependencyObject target)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            
            return BindingOperations.GetBinding(target, property);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBinding(this DependencyObject target, DependencyProperty property, Binding? binding)
        {
            SetBinding(property, target, binding);
        }
        
        public static void SetBinding(this DependencyProperty property, DependencyObject target, Binding? binding)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (binding is null)
            {
                BindingOperations.ClearBinding(target, property);
                return;
            }
            
            BindingOperations.SetBinding(target, property, binding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetBinding(this DependencyObject target, DependencyProperty property, Func<Binding?, Binding?> factory)
        {
            SetBinding(property, target, factory);
        }
        
        public static void SetBinding(this DependencyProperty property, DependencyObject target, Func<Binding?, Binding?> factory)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            if (factory(property.GetBinding(target)) is not { } binding)
            {
                BindingOperations.ClearBinding(target, property);
                return;
            }
            
            BindingOperations.SetBinding(target, property, binding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ModifyBinding(this DependencyObject target, DependencyProperty property, Action<Binding> action)
        {
            ModifyBinding(property, target, action);
        }

        public static void ModifyBinding(this DependencyProperty property, DependencyObject target, Action<Binding> action)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (property.GetBinding(target) is not { } binding)
            {
                return;
            }

            action(binding);
            property.SetBinding(target, binding);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearBinding(this DependencyObject target, DependencyProperty property)
        {
            ClearBinding(property, target);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearBinding(this DependencyProperty property, DependencyObject target)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            
            BindingOperations.ClearBinding(target, property);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearBindings(this DependencyObject target)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            
            BindingOperations.ClearAllBindings(target);
        }
        
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

        public static DependencyProperty Register<T, TType>(String name, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new PropertyMetadata(callback));
        }

        public static DependencyProperty Register<T, TType>(String name, Boolean twoway)
        {
            return twoway ? Register<T, TType>(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register<T, TType>(name);
        }

        public static DependencyProperty Register<T, TType>(String name, FrameworkPropertyMetadataOptions options)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new FrameworkPropertyMetadata(default(T), options));
        }

        public static DependencyProperty Register<T, TType>(String name, Boolean twoway, PropertyChangedCallback? callback)
        {
            return twoway ? Register<T, TType>(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register<T, TType>(name, callback);
        }

        public static DependencyProperty Register<T, TType>(String name, FrameworkPropertyMetadataOptions options, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new FrameworkPropertyMetadata(default(T), options, callback));
        }

        public static DependencyProperty Register<T, TType>(String name, T value)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new PropertyMetadata(value));
        }

        public static DependencyProperty Register<T, TType>(String name, T value, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new PropertyMetadata(value, callback));
        }

        public static DependencyProperty Register<T, TType>(String name, T value, Boolean twoway)
        {
            return twoway ? Register<T, TType>(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register<T, TType>(name, value);
        }

        public static DependencyProperty Register<T, TType>(String name, T value, FrameworkPropertyMetadataOptions options)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register<T, TType>(name, new FrameworkPropertyMetadata(value, options));
        }

        public static DependencyProperty Register<T, TType>(String name, T value, Boolean twoway, PropertyChangedCallback? callback)
        {
            return twoway ? Register<T, TType>(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register<T, TType>(name, value, callback);
        }

        public static DependencyProperty Register<T, TType>(String name, T value, FrameworkPropertyMetadataOptions options, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
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

        public static DependencyProperty Register(String name, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
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
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(default, options));
        }

        public static DependencyProperty Register(String name, Boolean twoway, PropertyChangedCallback? callback)
        {
            return twoway ? Register(name, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register(name, callback);
        }

        public static DependencyProperty Register(String name, Boolean value, Boolean twoway, PropertyChangedCallback? callback)
        {
            return Register<Boolean>(name, value, twoway, callback);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register(String name, FrameworkPropertyMetadataOptions options, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(default, options, callback));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, new PropertyMetadata(value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, new PropertyMetadata(value, callback));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value, Boolean twoway)
        {
            return twoway ? Register(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) : Register(name, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value, FrameworkPropertyMetadataOptions options)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(value, options));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value, Boolean twoway, PropertyChangedCallback? callback)
        {
            return twoway ? Register(name, value, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback) : Register(name, value, callback);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DependencyProperty Register<T>(String name, T value, FrameworkPropertyMetadataOptions options, PropertyChangedCallback? callback)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return Register(name, (PropertyMetadata) new FrameworkPropertyMetadata(value, options, callback));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TMetadata? GetMetadata<T, TMetadata>(this DependencyProperty property) where T : DependencyObject where TMetadata : PropertyMetadata
        {
            return GetMetadata<TMetadata>(property, typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TMetadata? GetMetadata<TMetadata>(this DependencyProperty property, Type type) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return property.GetMetadata(type) as TMetadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TMetadata? GetMetadata<TMetadata>(this DependencyProperty property, DependencyObjectType type) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return property.GetMetadata(type) as TMetadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TMetadata? GetMetadata<TMetadata>(this DependencyProperty property, DependencyObject @object) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            return property.GetMetadata(@object) as TMetadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetMetadata<T, TMetadata>(this DependencyProperty property, [MaybeNullWhen(false)] out TMetadata metadata) where T : DependencyObject where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            metadata = GetMetadata<T, TMetadata>(property);
            return metadata is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetMetadata<TMetadata>(this DependencyProperty property, Type? type, [MaybeNullWhen(false)] out TMetadata metadata) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            metadata = type is not null ? GetMetadata<TMetadata>(property, type) : null;
            return metadata is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetMetadata<TMetadata>(this DependencyProperty property, DependencyObjectType? type, [MaybeNullWhen(false)] out TMetadata metadata) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            metadata = type is not null ? GetMetadata<TMetadata>(property, type) : null;
            return metadata is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetMetadata<TMetadata>(this DependencyProperty property, DependencyObject? @object, [MaybeNullWhen(false)] out TMetadata metadata) where TMetadata : PropertyMetadata
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            metadata = @object is not null ? GetMetadata<TMetadata>(property, @object) : null;
            return metadata is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("metadata")]
        public static TMetadata? Clone<TMetadata>(this TMetadata? metadata) where TMetadata : PropertyMetadata
        {
            return Clone(metadata, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [return: NotNullIfNotNull("metadata")]
        public static TMetadata? Clone<TMetadata>(this TMetadata? metadata, Boolean flags) where TMetadata : PropertyMetadata
        {
            if (metadata is null)
            {
                return metadata;
            }
            
            Type type = metadata.GetType();
            switch (metadata)
            {
                case FrameworkPropertyMetadata framework when type == typeof(FrameworkPropertyMetadata):
                {
                    FrameworkPropertyMetadata result = new FrameworkPropertyMetadata(framework.DefaultValue, FrameworkPropertyMetadataOptions.None, framework.PropertyChangedCallback, framework.CoerceValueCallback, framework.IsAnimationProhibited, framework.DefaultUpdateSourceTrigger);
                    
                    if (!flags)
                    {
                        return Unsafe.As<FrameworkPropertyMetadata, TMetadata>(ref result);
                    }
                    
                    result.AffectsMeasure = framework.AffectsMeasure;
                    result.AffectsArrange = framework.AffectsArrange;
                    result.AffectsParentMeasure = framework.AffectsParentMeasure;
                    result.AffectsParentArrange = framework.AffectsParentArrange;
                    result.AffectsRender = framework.AffectsRender;
                    result.Inherits = framework.Inherits;
                    result.OverridesInheritanceBehavior = framework.OverridesInheritanceBehavior;
                    result.IsNotDataBindable = framework.IsNotDataBindable;
                    result.BindsTwoWayByDefault = framework.BindsTwoWayByDefault;
                    result.Journal = framework.Journal;
                    result.SubPropertiesDoNotAffectRender = framework.SubPropertiesDoNotAffectRender;
                    return Unsafe.As<FrameworkPropertyMetadata, TMetadata>(ref result);
                }
                case UIPropertyMetadata ui when type == typeof(UIPropertyMetadata):
                {
                    UIPropertyMetadata result = new UIPropertyMetadata(ui.DefaultValue, ui.PropertyChangedCallback, ui.CoerceValueCallback, ui.IsAnimationProhibited);
                    return Unsafe.As<UIPropertyMetadata, TMetadata>(ref result);
                }
                case not null when type == typeof(PropertyMetadata):
                {
                    PropertyMetadata result = new PropertyMetadata(metadata.DefaultValue, metadata.PropertyChangedCallback, metadata.CoerceValueCallback);
                    return Unsafe.As<PropertyMetadata, TMetadata>(ref result);
                }
                default:
                    throw new NotSupportedException($"Metadata '{type}' not supported for clone.");
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadata<T>(this DependencyProperty property, PropertyMetadata metadata) where T : DependencyObject
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            property.OverrideMetadata(typeof(T), metadata);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadata<T>(this DependencyProperty property, PropertyMetadata metadata, DependencyPropertyKey key) where T : DependencyObject
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            property.OverrideMetadata(typeof(T), metadata, key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TMetadata CreateMetadataWithOptions<TMetadata>(this TMetadata metadata, FrameworkPropertyMetadataOptions options) where TMetadata : PropertyMetadata
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            metadata = metadata.Clone(false);
            
            if (metadata is FrameworkPropertyMetadata framework)
            {
                TranslateFlags(framework, options);
            }
            
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOptions(this DependencyProperty property, Type type, FrameworkPropertyMetadataOptions options)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyMetadata? metadata = property.GetMetadata(type);
            
            if (metadata is not FrameworkPropertyMetadata { BindsTwoWayByDefault: true })
            {
                property.OverrideMetadata(type, metadata.CreateMetadataWithOptions(options));
                return;
            }
            
            metadata = metadata.CreateMetadataWithOptions(options);
            
            if (metadata is not FrameworkPropertyMetadata { BindsTwoWayByDefault: false } framework)
            {
                property.OverrideMetadata(type, metadata);
                return;
            }
            
            framework.DefaultUpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            property.OverrideMetadata(type, framework);
            OneWayBugHandler.Handle(framework);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOptions(this DependencyProperty property, Type type, DependencyPropertyKey key, FrameworkPropertyMetadataOptions options)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyMetadata? metadata = property.GetMetadata(type);
            
            if (metadata is not FrameworkPropertyMetadata { BindsTwoWayByDefault: true })
            {
                property.OverrideMetadata(type, metadata.CreateMetadataWithOptions(options), key);
                return;
            }
            
            metadata = metadata.CreateMetadataWithOptions(options);
            
            if (metadata is not FrameworkPropertyMetadata { BindsTwoWayByDefault: false } framework)
            {
                property.OverrideMetadata(type, metadata, key);
                return;
            }
            
            framework.DefaultUpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            property.OverrideMetadata(type, framework, key);
            OneWayBugHandler.Handle(framework);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOptions<T>(this DependencyProperty property, FrameworkPropertyMetadataOptions options) where T : DependencyObject
        {
            OverrideMetadataOptions(property, typeof(T), options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOptions<T>(this DependencyProperty property, DependencyPropertyKey key, FrameworkPropertyMetadataOptions options) where T : DependencyObject
        {
            OverrideMetadataOptions(property, typeof(T), key, options);
        }
        
        private static class OneWayBugHandler
        {
            [ReflectionNaming]
            private static Action<PropertyMetadata, Boolean> Sealed { get; }
            
            static OneWayBugHandler()
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                MethodInfo method = typeof(PropertyMetadata).GetProperty(nameof(Sealed), binding)?.SetMethod ?? throw new MissingMethodException(nameof(FrameworkPropertyMetadata), nameof(Sealed));
                
                ParameterExpression instance = Expression.Parameter(typeof(PropertyMetadata), nameof(instance));
                ParameterExpression seal = Expression.Parameter(typeof(Boolean), nameof(seal));
                
                MethodCallExpression call = Expression.Call(instance, method, seal);
                Sealed = Expression.Lambda<Action<PropertyMetadata, Boolean>>(call, instance, seal).Compile();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Seal(PropertyMetadata? metadata)
            {
                if (metadata is not null)
                {
                    Sealed(metadata, true);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Unseal(PropertyMetadata? metadata)
            {
                if (metadata is not null)
                {
                    Sealed(metadata, false);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Handle(FrameworkPropertyMetadata? metadata)
            {
                if (metadata is not { BindsTwoWayByDefault: true })
                {
                    return;
                }

                Unseal(metadata);
                metadata.BindsTwoWayByDefault = false;
                Seal(metadata);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay(this DependencyProperty property, Type type)
        {
            OverrideMetadataOneWay(property, type, default(FrameworkPropertyMetadataOptions?));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void OverrideMetadataOneWay(this DependencyProperty property, Type type, FrameworkPropertyMetadataOptions? options)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyMetadata metadata = property.GetMetadata(type);
            metadata = options.HasValue ? CreateMetadataWithOptions(metadata, options.Value) : metadata.Clone();
            
            if (metadata is not FrameworkPropertyMetadata framework)
            {
                property.OverrideMetadata(type, metadata);
                return;
            }
            
            framework.BindsTwoWayByDefault = false;
            framework.DefaultUpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            property.OverrideMetadata(type, framework);
            OneWayBugHandler.Handle(framework);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay(this DependencyProperty property, Type type, DependencyPropertyKey key)
        {
            OverrideMetadataOneWay(property, type, key, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void OverrideMetadataOneWay(this DependencyProperty property, Type type, DependencyPropertyKey key, FrameworkPropertyMetadataOptions? options)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyMetadata metadata = property.GetMetadata(type);
            metadata = options.HasValue ? CreateMetadataWithOptions(metadata, options.Value) : metadata.Clone();
            
            if (metadata is not FrameworkPropertyMetadata framework)
            {
                property.OverrideMetadata(type, metadata, key);
                return;
            }
            
            framework.BindsTwoWayByDefault = false;
            framework.DefaultUpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            property.OverrideMetadata(type, metadata, key);
            OneWayBugHandler.Handle(framework);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay<T>(this DependencyProperty property) where T : DependencyObject
        {
            OverrideMetadataOneWay<T>(property, default(FrameworkPropertyMetadataOptions?));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay<T>(this DependencyProperty property, FrameworkPropertyMetadataOptions? options) where T : DependencyObject
        {
            OverrideMetadataOneWay(property, typeof(T), options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay<T>(this DependencyProperty property, DependencyPropertyKey key) where T : DependencyObject
        {
            OverrideMetadataOneWay<T>(property, key, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataOneWay<T>(this DependencyProperty property, DependencyPropertyKey key, FrameworkPropertyMetadataOptions? options) where T : DependencyObject
        {
            OverrideMetadataOneWay(property, typeof(T), key, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TMetadata CreateMetadataWithDefaultValue<TMetadata>(this TMetadata metadata, Object? value) where TMetadata : PropertyMetadata
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            metadata = metadata.Clone();
            metadata.DefaultValue = value;
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TMetadata CreateMetadataWithDefaultValue<TMetadata>(this TMetadata metadata, FrameworkPropertyMetadataOptions? options, Object? value) where TMetadata : PropertyMetadata
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            
            metadata = options.HasValue ? metadata.CreateMetadataWithOptions(options.Value) : metadata.Clone();
            metadata.DefaultValue = value;
            return metadata;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue(this DependencyProperty property, Type type, Object? value)
        {
            OverrideMetadataDefaultValue(property, type, default(FrameworkPropertyMetadataOptions?), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue(this DependencyProperty property, Type type, FrameworkPropertyMetadataOptions? options, Object? value)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            property.OverrideMetadata(type, property.GetMetadata(type).CreateMetadataWithDefaultValue(options, value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue(this DependencyProperty property, Type type, DependencyPropertyKey key, Object? value)
        {
            OverrideMetadataDefaultValue(property, type, key, null, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue(this DependencyProperty property, Type type, DependencyPropertyKey key, FrameworkPropertyMetadataOptions? options, Object? value)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            property.OverrideMetadata(type, property.GetMetadata(type).CreateMetadataWithDefaultValue(options, value), key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue<T>(this DependencyProperty property, Object? value) where T : DependencyObject
        {
            OverrideMetadataDefaultValue<T>(property, default(FrameworkPropertyMetadataOptions?), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue<T>(this DependencyProperty property, FrameworkPropertyMetadataOptions? options, Object? value) where T : DependencyObject
        {
            OverrideMetadataDefaultValue(property, typeof(T), options, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue<T>(this DependencyProperty property, DependencyPropertyKey key, Object? value) where T : DependencyObject
        {
            OverrideMetadataDefaultValue<T>(property, key, null, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OverrideMetadataDefaultValue<T>(this DependencyProperty property, DependencyPropertyKey key, FrameworkPropertyMetadataOptions? options, Object? value) where T : DependencyObject
        {
            OverrideMetadataDefaultValue(property, typeof(T), key, options, value);
        }
    }
}