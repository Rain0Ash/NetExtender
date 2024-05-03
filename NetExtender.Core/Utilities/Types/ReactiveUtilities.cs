// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class ReactiveUtilities
    {
        [ReflectionNaming]
        private static Assembly? ReactiveUI { get; }
        private static Type? ReactiveObjectInterface { get; }
        private static MethodInfo? RaiseAndSetIfChangedMethod { get; }

        static ReactiveUtilities()
        {
            try
            {
                if (!InitializeReactiveUI(out Assembly? assembly, out Type? @interface, out MethodInfo? method))
                {
                    return;
                }

                ReactiveUI = assembly;
                ReactiveObjectInterface = @interface;
                RaiseAndSetIfChangedMethod = method;
            }
            catch (Exception)
            {
                ReactiveUI = null;
                ReactiveObjectInterface = null;
                RaiseAndSetIfChangedMethod = null;
            }
        }

        private static Boolean InitializeReactiveUI([MaybeNullWhen(false)] out Assembly assembly, [MaybeNullWhen(false)] out Type reactive, [MaybeNullWhen(false)] out MethodInfo raise)
        {
            assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == nameof(ReactiveUI));
            reactive = default;
            raise = default;
            
            if (assembly?.GetType($"{nameof(ReactiveUI)}.IReactiveObject") is not { IsInterface: true } @interface)
            {
                return false;
            }

            if (!@interface.HasInterface(typeof(INotifyPropertyChanging), typeof(INotifyPropertyChanged)))
            {
                return false;
            }

            static Boolean HasInterfaces(Type @interface)
            {
                const BindingFlags binding = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
                return
                    @interface.GetMethod(nameof(RaisePropertyChanging), binding, new[] { typeof(PropertyChangingEventArgs) })?.IsVoid() == true &&
                    @interface.GetMethod(nameof(RaisePropertyChanged), binding, new[] { typeof(PropertyChangedEventArgs) })?.IsVoid() == true;
            }
            
            if (!HasInterfaces(@interface))
            {
                return false;
            }

            reactive = @interface;
            if (assembly.GetType($"{nameof(ReactiveUI)}.IReactiveObjectExtensions") is not { } extensions)
            {
                return false;
            }
            
            if (extensions.GetMethod(nameof(RaiseAndSetIfChanged), BindingFlags.Static | BindingFlags.Public) is not { IsGenericMethod: true } method)
            {
                return false;
            }

            raise = method;
            return true;
        }
        
        private record ReactiveObjectHandler
        {
            private delegate TDestination RaiseAndSetIfChangedHandler<in TSource, TDestination>(TSource source, ref TDestination field, TDestination value, String? property) where TSource : class;
            private static ConcurrentDictionary<(Type Source, Type Destination), ReactiveObjectHandler?> Cache { get; } = new ConcurrentDictionary<(Type, Type), ReactiveObjectHandler?>();

            public Type Source { get; }
            public Type Destination { get; }
            private Delegate? Handler { get; }
            
            private ReactiveObjectHandler(Type source, Type destination)
            {
                Source = source ?? throw new ArgumentNullException(nameof(source));
                Destination = destination ?? throw new ArgumentNullException(nameof(destination));
                Handler = CreateHandler(Source, Destination);
            }

            private static ReactiveObjectHandler? Create((Type Source, Type Destination) type)
            {
                return Create(type.Source, type.Destination);
            }

            private static ReactiveObjectHandler? Create(Type source, Type destination)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (destination is null)
                {
                    throw new ArgumentNullException(nameof(destination));
                }

                if (ReactiveObjectInterface is null || !source.HasInterface(ReactiveObjectInterface))
                {
                    return null;
                }

                try
                {
                    return new ReactiveObjectHandler(source, destination);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Boolean Invoke<TSource, TDestination>(TSource source, ref TDestination field, TDestination value, String? property, [MaybeNullWhen(false)] out TDestination result) where TSource : class
            {
                if (Handler is not RaiseAndSetIfChangedHandler<TSource, TDestination> handler)
                {
                    result = default;
                    return false;
                }

                result = handler.Invoke(source, ref field, value, property);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Handle<TSource, TDestination>(TSource source, ref TDestination field, TDestination value, String? property, [MaybeNullWhen(false)] out TDestination result) where TSource : class
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (Cache.GetOrAdd((source.GetType(), !typeof(TDestination).IsValueType && field is not null ? field.GetType() : typeof(TDestination)), Create) is { } handler)
                {
                    return handler.Invoke(source, ref field, value, property, out result);
                }

                result = default;
                return false;
            }
            
            private static Delegate? CreateHandler(Type type, Type destination)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (destination is null)
                {
                    throw new ArgumentNullException(nameof(destination));
                }

                if (RaiseAndSetIfChangedMethod is not { } method || type.IsValueType)
                {
                    return null;
                }
                
                ParameterExpression source = Expression.Parameter(type, nameof(source));
                ParameterExpression field = Expression.Parameter(destination.MakeByRefType(), nameof(field));
                ParameterExpression value = Expression.Parameter(destination, nameof(value));
                ParameterExpression property = Expression.Parameter(typeof(String), nameof(property));
                method = method.MakeGenericMethod(type, destination);
                
                MethodCallExpression call = Expression.Call(null, method, Expression.Convert(source, type), field, Expression.Convert(value, destination), property);
                return Expression.Lambda(typeof(RaiseAndSetIfChangedHandler<,>).MakeGenericType(type, destination), call, source, field, value, property).Compile();
            }
        }
        
        private record PropertyEventHandler
        {
            private static ConcurrentDictionary<Type, PropertyEventHandler?> Cache { get; } = new ConcurrentDictionary<Type, PropertyEventHandler?>();

            private Type Type { get; }
            private Action<Object?, String?>? Changing { get; }
            private Action<Object?, String?>? Changed { get; }

            private PropertyEventHandler(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Changing = CreateHandler<PropertyChangingEventHandler, PropertyChangingEventArgs>(Type);
                Changed = CreateHandler<PropertyChangedEventHandler, PropertyChangedEventArgs>(Type);
            }

            private static PropertyEventHandler? Create(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                try
                {
                    return new PropertyEventHandler(type);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public static PropertyEventHandler? Get(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return Cache.GetOrAdd(type, Create);
            }

            public static PropertyEventHandler? Get<TSource>(TSource? source) where TSource : class
            {
                return source is not null ? Get(source.GetType()) : null;
            }

            private static Action<Object?, String?>? CreateHandler<TDelegate, TArgs>(Type type) where TDelegate : Delegate
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (typeof(TArgs).GetConstructor(new[] { typeof(String) }) is not { } constructor)
                {
                    return null;
                }

                FieldInfo[] fields = type.GetEventFields<TDelegate>();
                FieldInfo? @event;
                switch (fields.Length)
                {
                    case <= 0:
                        return null;
                    case 1:
                        @event = fields[0];
                        break;
                    default:
                        String? name = typeof(TDelegate) == typeof(PropertyChangingEventHandler) ? "Changing" : typeof(TDelegate) == typeof(PropertyChangedEventHandler) ? "Changed" : null;
                        @event = name is not null ? fields.FirstOrDefault(field => field.Name.Contains(name)) : null;
                        break;
                }

                if (@event is null || !typeof(MulticastDelegate).IsAssignableFrom(@event.FieldType) || @event.FieldType.GetMethod("Invoke") is not { } method)
                {
                    return null;
                }
                
                ParameterExpression source = Expression.Parameter(typeof(Object), nameof(source));
                ParameterExpression property = Expression.Parameter(typeof(String), nameof(property));
                NewExpression argument = Expression.New(constructor, property);
                MemberExpression field = Expression.Field(Expression.Convert(source, type), @event);
                
                BinaryExpression notnull = Expression.NotEqual(field, Expression.Constant(null, typeof(Object)));
                MethodCallExpression call = Expression.Call(field, method, Expression.Convert(source, method.GetParameters()[0].ParameterType), argument);
                ConditionalExpression conditional = Expression.IfThen(notnull, call);
                return Expression.Lambda<Action<Object?, String?>>(conditional, source, property).Compile();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void RaiseChanging(Object? source, String? property)
            {
                Changing?.Invoke(source, property);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void RaiseChanged(Object? source, String? property)
            {
                Changed?.Invoke(source, property);
            }
        }

        public static TDestination RaiseProperty<TSource, TDestination>(this TSource source, ref TDestination field, TDestination value, [CallerMemberName] String? property = null) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            PropertyEventHandler? handler = PropertyEventHandler.Get(source);

            if (handler is null)
            {
                field = value;
                return value;
            }
            
            handler.RaiseChanging(source, property);
            field = value;
            handler.RaiseChanged(source, property);
            return value;
        }

        public static TDestination RaiseAndSetIfChanged<TSource, TDestination>(this TSource source, ref TDestination field, TDestination value, [CallerMemberName] String? property = null) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ReactiveObjectHandler.Handle(source, ref field, value, property, out TDestination? result) ? result : RaiseProperty(source, ref field, value, property);
        }
        
        public static void RaisePropertyChanging<TSource>(this TSource source, [CallerMemberName] String? property = null) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            PropertyEventHandler.Get(source)?.RaiseChanging(source, property);
        }
        
        public static void RaisePropertyChanging<TSource>(this TSource source, params String?[]? properties) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                return;
            }
            
            if (PropertyEventHandler.Get(source) is not { } handler)
            {
                return;
            }

            foreach (String? property in properties)
            {
                handler.RaiseChanging(source, property);
            }
        }

        public static void RaisePropertyChanged<TSource>(this TSource source, [CallerMemberName] String? property = null) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            PropertyEventHandler.Get(source)?.RaiseChanged(source, property);
        }

        public static void RaisePropertyChanged<TSource>(this TSource source, params String?[]? properties) where TSource : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                return;
            }
            
            if (PropertyEventHandler.Get(source) is not { } handler)
            {
                return;
            }

            foreach (String? property in properties)
            {
                handler.RaiseChanged(source, property);
            }
        }
    }
}