// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Anonymous;
using NetExtender.Types.Attributes;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;
using AssembliesHashAlgorithm = System.Configuration.Assemblies.AssemblyHashAlgorithm;

namespace NetExtender.Utilities.Core
{
    [Flags]
    public enum TypeSignature : UInt64
    {
        Any = 0,
        Public = TypeAttributes.Public,
        NotPublic = TypeAttributes.NotPublic | Public << 1,
        AutoLayout = TypeAttributes.AutoLayout | SequentialLayout >> 1,
        SequentialLayout = TypeAttributes.SequentialLayout,
        ExplicitLayout = TypeAttributes.ExplicitLayout,
        Array = Value << 4,
        SZArray = Array << 1 | Array,
        VariableBoundArray = Array << 2 | Array,
        ElementType = Array << 3,
        SignatureType = ElementType << 1,
        ByRef = SignatureType << 1,
        MarshalByRef = ByRef << 1,
        ByRefLike = MarshalByRef << 1,
        Pointer = ByRefLike << 1,
        Value = Class << 1,
        Primitive = Value << 1,
        Enum = Primitive << 1,
        Class = TypeAttributes.Class | 1UL << 32,
        Interface = TypeAttributes.Interface,
        Abstract = TypeAttributes.Abstract,
        Sealed = TypeAttributes.Sealed,
        Nested = 512,
        GenericType = Class << 16,
        GenericTypeDefinition = GenericType << 1,
        ConstructedGenericType = GenericTypeDefinition << 1,
        GenericParameter = ConstructedGenericType << 2,
        GenericTypeParameter = GenericParameter << 1,
        GenericMethodParameter = GenericTypeParameter << 1,
        SpecialName = TypeAttributes.SpecialName,
        RTSpecialName = TypeAttributes.RTSpecialName,
        Import = TypeAttributes.Import,
        Serializable = TypeAttributes.Serializable,
        Contextful = Serializable << 1,
        AnsiClass = TypeAttributes.AnsiClass | UnicodeClass >> 1,
        UnicodeClass = TypeAttributes.UnicodeClass,
        AutoClass = TypeAttributes.AutoClass,
        COMObject = AutoClass << 1,
        IsSecurityCritical = 1UL << 56,
        IsSecuritySafeCritical = IsSecurityCritical << 1,
        IsSecurityTransparent = IsSecuritySafeCritical << 1
    }
    
    [Flags]
    public enum PrimitiveType : Byte
    {
        Default = 0,
        String = 1,
        Decimal = 2,
        Complex = 4,
        TimeSpan = 8,
        DateTime = 16,
        DateTimeOffset = 32,
        All = String | Decimal | Complex | TimeSpan | DateTime | DateTimeOffset
    }

    [Flags]
    public enum AssemblyNameType : Byte
    {
        Default = 0,
        Version = 1,
        Culture = 2,
        PublicKeyToken = 4,
        All = Version | Culture | PublicKeyToken
    }

    public enum MethodVisibilityType : Byte
    {
        Unavailable,
        Private,
        Public
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public static partial class ReflectionUtilities
    {
        public const String Constructor = Initializer.Initializer.ReflectionUtilities.Constructor;
        
        private static readonly IResettableLazy<InheritEvaluator> inherit = new ResettableLazy<InheritEvaluator>(InheritEvaluator.Create, LazyThreadSafetyMode.ExecutionAndPublication);
        public static Inherit.Result Inherit
        {
            get
            {
                return inherit.Value;
            }
        }
        
        private static readonly ConcurrentHashSet<Task> scanningset = new ConcurrentHashSet<Task>();
        private static readonly ConcurrentBag<Task> scanningbag = new ConcurrentBag<Task>();
        public static Int32 Scanning
        {
            get
            {
                return scanningset.Count;
            }
        }
        
        private static readonly ConcurrentHashSet<Assembly> assemblies = new ConcurrentHashSet<Assembly>();
        public static IReadOnlyCollection<Assembly> Assemblies
        {
            get
            {
                return assemblies;
            }
        }
        
        public static Type NullableAttribute { get; }
        public static Type NullableContextAttribute { get; }
        private static HashSet<Type> VarArgTypes { get; }
        private static Predicate<Type> IsByRefLikePredicate { get; }
        public static Boolean AssemblyLoadCallStaticConstructor { get; set; }
        public static event EmptyHandler? Reset;

        static ReflectionUtilities()
        {
            VarArgTypes = new[] { Type.GetType("System.ArgIterator"), Type.GetType("System.RuntimeArgumentHandle"), Type.GetType("System.TypedReference") }.OfType<Type>().ToHashSet();
            IsByRefLikePredicate = typeof(Type).GetProperty("IsByRefLike")?.GetMethod?.CreateDelegate(typeof(Predicate<Type>)) as Predicate<Type> ?? (_ => false);
            NullableAttribute = Type.GetType("System.Runtime.CompilerServices.NullableAttribute") ?? throw new InvalidInitializationException();
            NullableContextAttribute = Type.GetType("System.Runtime.CompilerServices.NullableContextAttribute") ?? throw new InvalidInitializationException();
            
            Scan().ContinueWith(static _ =>
            {
                if (scanningset.Count > 0)
                {
                    return;
                }
                
                inherit.Reset(InheritEvaluator.Create);
                Reset?.Invoke();
            });

            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        private static async void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            Task<Assembly> task = Task.Run(async () =>
            {
                Assembly assembly = args.LoadedAssembly;
                await Scan(assembly).ConfigureAwait(false);
                
                CallStaticInitializerAttribute<StaticInitializerRequiredAttribute>(assembly);
                
                if (AssemblyLoadCallStaticConstructor)
                {
                    CallStaticInitializerAttribute(assembly);
                }
                
                return assembly;
            });
            
            scanningset.Add(task);
            scanningbag.Add(task);

            _ = await task.ContinueWith(async static task =>
            {
                Assembly assembly = await task;
                scanningset.Remove(task);
                assemblies.Add(assembly);
            });

            await Task.WhenAll(scanningbag).ContinueWith(static () =>
            {
                if (scanningset.Count > 0)
                {
                    return;
                }
                
                inherit.Reset(InheritEvaluator.Create);
                Reset?.Invoke();
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TSource> New<TSource>()
        {
            return ExpressionStorage<TSource>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, TSource> New<TSource, T>()
        {
            return ExpressionStorage<TSource, T>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, TSource> New<TSource, T1, T2>()
        {
            return ExpressionStorage<TSource, T1, T2>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, TSource> New<TSource, T1, T2, T3>()
        {
            return ExpressionStorage<TSource, T1, T2, T3>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, TSource> New<TSource, T1, T2, T3, T4>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T5, TSource> New<TSource, T1, T2, T3, T4, T5>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4, T5>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T5, T6, TSource> New<TSource, T1, T2, T3, T4, T5, T6>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T5, T6, T7, TSource> New<TSource, T1, T2, T3, T4, T5, T6, T7>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TSource> New<TSource, T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSource> New<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            return ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>.New;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TFrom, TTo>? Assign<TFrom, TTo>()
        {
            return AssignStorage<TFrom, TTo>.Assign;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<TFrom, TTo>? Assign<TFrom, TTo>(Boolean simple)
        {
            return simple ? AssignStorage<TFrom, TTo>.Simple : AssignStorage<TFrom, TTo>.Assign;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [return: NotNullIfNotNull("source")]
        public static Type? BiggestCommonType(this IEnumerable? source)
        {
            if (source is null)
            {
                return null;
            }
            
            Type? result = null;
            foreach (Object? item in source)
            {
                if (item?.GetType() is not { } type)
                {
                    continue;
                }
                
                if (result is null)
                {
                    result = type;
                    continue;
                }
                
                while (result?.BaseType is not null && !result.IsAssignableFrom(type))
                {
                    result = result.BaseType;
                }
            }
            
            return result switch
            {
                null when source is IList list => list.AsQueryable().ElementType,
                null => typeof(Object),
                _ => result
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join(params String[] values)
        {
            return Join(null, values);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join(Char separator, params String[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            
            return String.Join(separator, values);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join(String? separator, params String[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            
            return separator is not null ? String.Join(separator, values) : Join('.', values);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsVarArgType(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return VarArgTypes.Contains(type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsStackOnly(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsByRef || type.IsVarArgType() || IsByRefLikePredicate.Invoke(type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsBoxable(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return !type.IsPointer && !type.IsStackOnly();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsILBoxable(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsBoxable() || type.IsByRef || type.IsPointer;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAssignableFrom<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableFrom(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAssignableFrom<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableFrom(typeof(T).GetTypeInfo());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAssignableTo<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableTo(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAssignableTo<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableTo(typeof(T).GetTypeInfo());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSameAsOrSubclassOf(this Type type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return type == other || type.IsSubclassOf(other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSameAsOrSubclassOf(this TypeInfo type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return type.AsType() == other || type.IsSubclassOf(other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSameAsOrSubclassOf<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsSameAsOrSubclassOf(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSameAsOrSubclassOf<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsSameAsOrSubclassOf(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsGenericTypeDefinedAs(this Type type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null || !type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == other;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsGenericTypeDefinedAs(this TypeInfo type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null || !type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == other;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetGenericArgumentsCount(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericArguments().Length : 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasInterface<T>(this Type type) where T : class
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.HasInterface(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasInterface<T>(this TypeInfo type) where T : class
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.HasInterface(typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasInterface(this Type type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.GetInterfaces().Contains(@interface);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasInterface(this TypeInfo type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.ImplementedInterfaces.Contains(@interface);
        }
        
        public static Boolean HasInterface(this Type type, params Type?[]? interfaces)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (interfaces is null)
            {
                return true;
            }
            
            return interfaces.Length switch
            {
                <= 0 => true,
                1 => interfaces[0] is null || type.GetInterfaces().Contains(interfaces[0]),
                _ => interfaces.WhereNotNull().All(new HashSet<Type>(type.GetInterfaces()).Contains)
            };
        }

        public static Boolean HasInterface(this TypeInfo type, params Type?[]? interfaces)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (interfaces is null)
            {
                return true;
            }

            return interfaces.Length switch
            {
                <= 0 => true,
                1 => interfaces[0] is null || type.ImplementedInterfaces.Contains(interfaces[0]),
                _ => interfaces.WhereNotNull().All(new HashSet<Type>(type.ImplementedInterfaces).Contains)
            };
        }
        
        public static Boolean HasAnyInterface(this Type type, params Type?[]? interfaces)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (interfaces is null)
            {
                return false;
            }
            
            return interfaces.Length switch
            {
                <= 0 => false,
                1 => interfaces[0] is not null && type.GetInterfaces().Contains(interfaces[0]),
                _ => interfaces.WhereNotNull().Any(new HashSet<Type>(type.GetInterfaces()).Contains)
            };
        }

        public static Boolean HasAnyInterface(this TypeInfo type, params Type?[]? interfaces)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (interfaces is null)
            {
                return false;
            }

            return interfaces.Length switch
            {
                <= 0 => false,
                1 => interfaces[0] is not null && type.ImplementedInterfaces.Contains(interfaces[0]),
                _ => interfaces.WhereNotNull().Any(new HashSet<Type>(type.ImplementedInterfaces).Contains)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("Usage", "CA2200")]
        public static Boolean HasAbstract(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            try
            {
                return Initializer.Initializer.ReflectionUtilities.HasAbstract(type);
            }
            catch (Initializer.Initializer.ReflectionUtilities.TypeSealException exception)
            {
                throw new TypeNotSupportedException(exception.Type, exception.Message);
            }
            catch (Exception exception)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type TryGetGenericTypeDefinition(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] TryGetGenericTypeDefinitionInterfaces(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type[] interfaces = type.GetInterfaces();
            interfaces.InnerChange(TryGetGenericTypeDefinition);

            return interfaces;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo TryGetGenericMethodDefinition(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? TryGetGenericArguments(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericArguments() : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? TryGetGenericArguments(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericArguments() : null;
        }
        
        public static Type[]? GetGenericArguments(this Type generic, Type @base)
        {
            if (generic is null)
            {
                throw new ArgumentNullException(nameof(generic));
            }

            if (@base is null)
            {
                throw new ArgumentNullException(nameof(@base));
            }

            Type? type = generic;
            while (type is not null && type != typeof(Object))
            {
                if (@base == type.TryGetGenericTypeDefinition())
                {
                    return type.GetGenericArguments();
                }

                type = type.BaseType;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? TryGetGenericInterface(this Type type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            type = type.TryGetGenericTypeDefinition();
            @interface = @interface.TryGetGenericTypeDefinition();
            return type.TryGetGenericTypeDefinitionInterfaces().FirstOrDefault(item => item == @interface);
        }

        public static Boolean IsInheritable(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Type? declaring = method.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            return declaring is { IsVisible: true, IsSealed: false } && !method.IsStatic && (method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOverridable(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsAbstract || method.IsInheritable() && method is {IsVirtual: true, IsFinal: false};
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAsync(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Type type = method.ReturnType;
            return type == typeof(ValueTask) || type.Closes(typeof(ValueTask<>)) || type == typeof(Task) || type.Closes(typeof(Task<>));
        }

        private static Boolean Closes(this Type? type, Type? open)
        {
            if (type is null || open is null)
            {
                return false;
            }

            TypeInfo info = type.GetTypeInfo();

            if (info.IsGenericType && type.GetGenericTypeDefinition() == open)
            {
                return true;
            }

            if (type.GetInterfaces().Any(@interface => @interface.Closes(open)))
            {
                return true;
            }

            if (info.BaseType is not { } @base)
            {
                return false;
            }

            if (@base.GetTypeInfo().IsGenericType && @base.GetGenericTypeDefinition() == open)
            {
                return true;
            }

            return info.BaseType?.Closes(open) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnonymousType(this Object? value)
        {
            return value is not null && IsAnonymousType(value.GetType());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnonymousType(this Type? type)
        {
            return type is not null && type.Namespace is null;
        }

        private static class AnonymousTypeStorage
        {
            private static ConcurrentDictionary<Type, AnonymousTypeProperties> Storage { get; } = new ConcurrentDictionary<Type, AnonymousTypeProperties>();

            public static AnonymousTypeProperties? Get(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (!type.IsAnonymousType())
                {
                    throw new NotSupportedException();
                }

                try
                {
                    return Storage.GetOrAdd(type, AnonymousTypeProperties.Create);
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousTypeProperties? GetAnonymousProperties(this Object? value)
        {
            return value is not null ? GetAnonymousProperties(value.GetType()) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousTypeProperties? GetAnonymousProperties(this Type type)
        {
            return AnonymousTypeStorage.Get(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.IsDefined(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.IsDefined(typeof(T), inherit);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this MemberInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            return Initializer.Initializer.ReflectionUtilities.IsAbstract(info);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            return Initializer.Initializer.ReflectionUtilities.IsAbstract(info);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this EventInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            
            return Initializer.Initializer.ReflectionUtilities.IsAbstract(info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute<T>() is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute<T>(inherit) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo info, Type attribute)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return info.GetCustomAttribute(attribute) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo info, Type attribute, Boolean inherit)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return info.GetCustomAttribute(attribute, inherit) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute(typeof(T)) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute(typeof(T), inherit) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttributes(typeof(T)).OfType<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }
        
        public static List<Object> FilterOnBrowsableAttribute<T>(T source) where T : IEnumerable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            List<Object> result = new List<Object>();

            foreach (Object? @object in source)
            {
                if (@object is null)
                {
                    continue;
                }
                
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField;
                if (@object.ToString() is not { Length: > 0 } name || @object.GetType().GetField(name, binding) is not { } field)
                {
                    result.Add(@object);
                    continue;
                }
                
                if (field.GetCustomAttributes<BrowsableAttribute>(true).FirstOrDefault() is not { } browsable || browsable.Browsable)
                {
                    result.Add(@object);
                }
            }

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MemberInfo member in type.GetMembers())
            {
                if (member.HasAttribute(attribute))
                {
                    yield return member;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MemberInfo member in type.GetMembers())
            {
                if (member.HasAttribute(attribute, inherit))
                {
                    yield return member;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MemberInfo member in type.GetMembers(binding))
            {
                if (member.HasAttribute(attribute))
                {
                    yield return member;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MemberInfo member in type.GetMembers(binding))
            {
                if (member.HasAttribute(attribute, inherit))
                {
                    yield return member;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MemberInfo[] members = type.GetMembers();
            return members.Where(static member => member.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MemberInfo[] members = type.GetMembers();
            return inherit ? members.Where(static member => member.HasAttribute<TAttribute>(false)) : members.Where(static member => member.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MemberInfo[] members = type.GetMembers(binding);
            return members.Where(static member => member.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MemberInfo[] members = type.GetMembers(binding);
            return inherit ? members.Where(static member => member.HasAttribute<TAttribute>(false)) : members.Where(static member => member.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (FieldInfo field in type.GetFields())
            {
                if (field.HasAttribute(attribute))
                {
                    yield return field;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (FieldInfo field in type.GetFields())
            {
                if (field.HasAttribute(attribute, inherit))
                {
                    yield return field;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (FieldInfo field in type.GetFields(binding))
            {
                if (field.HasAttribute(attribute))
                {
                    yield return field;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (FieldInfo field in type.GetFields(binding))
            {
                if (field.HasAttribute(attribute, inherit))
                {
                    yield return field;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            FieldInfo[] fields = type.GetFields();
            return fields.Where(static field => field.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            FieldInfo[] fields = type.GetFields();
            return inherit ? fields.Where(static field => field.HasAttribute<TAttribute>(false)) : fields.Where(static field => field.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            FieldInfo[] fields = type.GetFields(binding);
            return fields.Where(static field => field.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            FieldInfo[] fields = type.GetFields(binding);
            return inherit ? fields.Where(static field => field.HasAttribute<TAttribute>(false)) : fields.Where(static field => field.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.HasAttribute(attribute))
                {
                    yield return property;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.HasAttribute(attribute, inherit))
                {
                    yield return property;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (PropertyInfo property in type.GetProperties(binding))
            {
                if (property.HasAttribute(attribute))
                {
                    yield return property;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (PropertyInfo property in type.GetProperties(binding))
            {
                if (property.HasAttribute(attribute, inherit))
                {
                    yield return property;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyInfo[] properties = type.GetProperties();
            return properties.Where(static property => property.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyInfo[] properties = type.GetProperties();
            return inherit ? properties.Where(static property => property.HasAttribute<TAttribute>(false)) : properties.Where(static property => property.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyInfo[] properties = type.GetProperties(binding);
            return properties.Where(static property => property.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            PropertyInfo[] properties = type.GetProperties(binding);
            return inherit ? properties.Where(static property => property.HasAttribute<TAttribute>(false)) : properties.Where(static property => property.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (EventInfo @event in type.GetEvents())
            {
                if (@event.HasAttribute(attribute))
                {
                    yield return @event;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (EventInfo @event in type.GetEvents())
            {
                if (@event.HasAttribute(attribute, inherit))
                {
                    yield return @event;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (EventInfo @event in type.GetEvents(binding))
            {
                if (@event.HasAttribute(attribute))
                {
                    yield return @event;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (EventInfo @event in type.GetEvents(binding))
            {
                if (@event.HasAttribute(attribute, inherit))
                {
                    yield return @event;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            EventInfo[] events = type.GetEvents();
            return events.Where(static @event => @event.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            EventInfo[] events = type.GetEvents();
            return inherit ? events.Where(static @event => @event.HasAttribute<TAttribute>(false)) : events.Where(static @event => @event.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            EventInfo[] events = type.GetEvents(binding);
            return events.Where(static @event => @event.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetEvents<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            EventInfo[] events = type.GetEvents(binding);
            return inherit ? events.Where(static @event => @event.HasAttribute<TAttribute>(false)) : events.Where(static @event => @event.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.HasAttribute(attribute))
                {
                    yield return method;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.HasAttribute(attribute, inherit))
                {
                    yield return method;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MethodInfo method in type.GetMethods(binding))
            {
                if (method.HasAttribute(attribute))
                {
                    yield return method;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (MethodInfo method in type.GetMethods(binding))
            {
                if (method.HasAttribute(attribute, inherit))
                {
                    yield return method;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MethodInfo[] methods = type.GetMethods();
            return methods.Where(static method => method.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MethodInfo[] methods = type.GetMethods();
            return inherit ? methods.Where(static method => method.HasAttribute<TAttribute>(false)) : methods.Where(static method => method.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MethodInfo[] methods = type.GetMethods(binding);
            return methods.Where(static method => method.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            MethodInfo[] methods = type.GetMethods(binding);
            return inherit ? methods.Where(static method => method.HasAttribute<TAttribute>(false)) : methods.Where(static method => method.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (constructor.HasAttribute(attribute))
                {
                    yield return constructor;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (constructor.HasAttribute(attribute, inherit))
                {
                    yield return constructor;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (ConstructorInfo constructor in type.GetConstructors(binding))
            {
                if (constructor.HasAttribute(attribute))
                {
                    yield return constructor;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            foreach (ConstructorInfo constructor in type.GetConstructors(binding))
            {
                if (constructor.HasAttribute(attribute, inherit))
                {
                    yield return constructor;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.Where(static constructor => constructor.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            ConstructorInfo[] constructors = type.GetConstructors();
            return inherit ? constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(false)) : constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            ConstructorInfo[] constructors = type.GetConstructors(binding);
            return constructors.Where(static constructor => constructor.HasAttribute<TAttribute>());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            ConstructorInfo[] constructors = type.GetConstructors(binding);
            return inherit ? constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(false)) : constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, null, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, returnType, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type[]? indexes)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, null, indexes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, null, null, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType, Type[]? indexes)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, returnType, indexes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectProperty(binding, match, returnType, null, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBase? SelectMethod(this Binder binder, BindingFlags binding, MethodBase[] match, Type[] types)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }
            
            return binder.SelectMethod(binding, match, types, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? SelectMethod(this Binder binder, BindingFlags binding, MethodInfo[] match, Type[] types)
        {
            return SelectMethod(binder, binding, match, types, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? SelectMethod(this Binder binder, BindingFlags binding, MethodInfo[] match, Type[] types, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            // ReSharper disable once CoVariantArrayConversion
            return (MethodInfo?) binder.SelectMethod(binding, match, types, modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetFirstAttributeOrDefault<T>(this PropertyDescriptor descriptor) where T : Attribute
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            return descriptor.Attributes.OfType<T>().FirstOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? GetAttributeValue<T, TResult>(this PropertyDescriptor descriptor, Func<T, TResult> selector) where T : Attribute
        {
            return GetAttributeValue(descriptor, selector, default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? GetAttributeValue<T, TResult>(this PropertyDescriptor descriptor, Func<T, TResult> selector, TResult? @default) where T : Attribute
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            return descriptor.Attributes.OfType<T>().FirstOrDefault() is { } result ? selector(result) : @default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Attribute? GetFirstAttributeOrDefault(this PropertyDescriptor descriptor, Type attribute)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            return descriptor.Attributes.Cast<Attribute>().FirstOrDefault(item => item.GetType().IsAssignableFrom(attribute));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadOnly(this PropertyDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            
            return descriptor.GetFirstAttributeOrDefault<ReadOnlyAttribute>() is { } attribute ? attribute.IsReadOnly : descriptor.IsReadOnly;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeCategory(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }
            
            return dependency.GetFirstAttributeOrDefault<CategoryAttribute>() is { } attribute ? attribute.Category : dependency.Category;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeDescription(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }
            
            return dependency.GetFirstAttributeOrDefault<DescriptionAttribute>() is { } attribute ? attribute.Description : dependency.Description;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeDisplayName(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }
            
            return dependency.GetFirstAttributeOrDefault<DisplayNameAttribute>() is { } attribute ? attribute.DisplayName : dependency.DisplayName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("parameters")]
        public static String[]? Names(this ParameterInfo[]? parameters)
        {
            return parameters?.ConvertAll(static parameter => parameter.Name ?? String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("parameters")]
        public static Type[]? Types(this ParameterInfo[]? parameters)
        {
            return parameters?.ConvertAll(static parameter => parameter.ParameterType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParameterInfo[]? GetSafeParameters(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            try
            {
                return method.GetParameters();
            }
            catch (TypeLoadException)
            {
                return null;
            }
            catch (NotSupportedException) when (method is ConstructorBuilder builder)
            {
                return CodeGeneratorUtilities.Storage.Parameters.ConstructorBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException) when (method is MethodBuilder builder)
            {
                return CodeGeneratorUtilities.Storage.Parameters.MethodBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ParameterInfo[]? GetSafeIndexParameters(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            try
            {
                return property.GetIndexParameters();
            }
            catch (TypeLoadException)
            {
                return null;
            }
            catch (NotSupportedException) when (property is PropertyBuilder builder)
            {
                return CodeGeneratorUtilities.Storage.Parameters.PropertyBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] GetParameterNames(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetParameters().Names();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetSafeParameterNames(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetSafeParameters()?.Names();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] GetIndexParameterNames(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetIndexParameters().Names();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetSafeIndexParameterNames(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetSafeIndexParameters()?.Names();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetParameterTypes(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetParameters().Types();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? GetSafeParameterTypes(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetSafeParameters()?.Types();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetIndexParameterTypes(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetIndexParameters().Types();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? GetSafeIndexParameterTypes(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetSafeIndexParameters()?.Types();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type MemberType(this MemberInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                FieldInfo field => field.FieldType,
                PropertyInfo property => property.PropertyType,
                _ => throw new ArgumentException($"Member {info.GetType().Name} is not {nameof(FieldInfo)} or {nameof(PropertyInfo)}")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSafe(this MethodBase method)
        {
            return IsSafe(method, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSafe(this MethodBase method, [MaybeNullWhen(false)] out ParameterInfo[] parameters)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            parameters = method.GetSafeParameters();
            return parameters is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, parameters.Types());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, parameters.Types(), modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, bindingAttr, parameters.Types());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, bindingAttr, null, parameters, modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            return GetMethod(type, name, bindingAttr, null, parameters, modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Binder? binder, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, bindingAttr, binder, parameters.Types(), modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, bindingAttr, binder, callConvention, parameters.Types(), modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, genericParameterCount, parameters.Types());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return type.GetMethod(name, genericParameterCount, parameters.Types(), modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            return type.GetMethod(name, genericParameterCount, bindingAttr, null, types, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, genericParameterCount, bindingAttr, parameters.Types());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, Type[] types, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            return type.GetMethod(name, genericParameterCount, bindingAttr, null, types, modifiers);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, genericParameterCount, bindingAttr, parameters.Types(), modifiers);
        }

        private static IEnumerable<MethodInfo> Filter(this MethodInfo[] methods, String name, Type[] generics, Type[] types)
        {
            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }
            
            return methods.Where(info => info.Name == name && info.IsGenericMethod && info.GetGenericArguments().Length == generics.Length)
                .Select(info => info.MakeGenericMethod(generics))
                .Select(method => new KeyValuePair<MethodInfo, ParameterInfo[]?>(method, method.GetSafeParameters()))
                .WhereValueNotNull()
                .Where(pair => pair.Value.Length == types.Length && pair.Value.Select(static parameter => parameter.ParameterType).SequenceEqual(types))
                .Keys();
        }
        
        public static MethodInfo? GetMethod(this Type type, String name, Type[] generics, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            MethodInfo[] array = type.GetMethods().Filter(name, generics, types).ToArray();

            return array.Length switch
            {
                <= 0 => null,
                1 => array[0],
                _ => throw new AmbiguousMatchException()
            };
        }
        
        public static MethodInfo? GetMethod(this Type type, String name, Type[] generics, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return GetMethod(type, name, generics, parameters.Types());
        }
        
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] generics, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            MethodInfo[] array = type.GetMethods(bindingAttr).Filter(name, generics, types).ToArray();

            return array.Length switch
            {
                <= 0 => null,
                1 => array[0],
                _ => throw new AmbiguousMatchException()
            };
        }
        
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] generics, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }
            
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            
            return GetMethod(type, name, bindingAttr, generics, parameters.Types());
        }
        
        public static MethodInfo GetParentDefinition(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (!method.IsVirtual)
            {
                return method;
            }

            Type? parent = method.DeclaringType?.BaseType;
            MethodInfo @base = method.GetBaseDefinition();

            if (method == @base)
            {
                return method;
            }

            while (parent is not null)
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                foreach (MethodInfo info in parent.GetMethods(binding))
                {
                    if (method.Name == info.Name && @base == info.GetBaseDefinition() && info.DeclaringType == parent)
                    {
                        return info;
                    }
                }
                
                parent = parent.BaseType;
            }

            return method;
        }

        public static TDelegate GetParentMethod<TDelegate>(this MethodInfo method, Object instance) where TDelegate : Delegate
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (method.GetParentDefinition() is not { IsAbstract: false } parent)
            {
                return method.CreateDelegate<TDelegate>(instance);
            }

            IntPtr pointer = parent.MethodHandle.GetFunctionPointer();
            return New<TDelegate, Object, IntPtr>().Invoke(instance, pointer);
        }

        public static (Func<TInstance, T>? Get, Action<TInstance, T>? Set) GetParentProperty<TInstance, T>(this PropertyInfo property) where TInstance : notnull
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return (property.GetMethod is not null ? instance => property.GetParentGetMethod<T>(instance)!.Invoke() : null, property.SetMethod is not null ? (instance, value) => property.GetParentSetMethod<T>(instance)!.Invoke(value) : null);
        }

        public static (Func<T>? Get, Action<T>? Set) GetParentProperty<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return (property.GetParentGetMethod<T>(instance), property.GetParentSetMethod<T>(instance));
        }

        public static Func<T>? GetParentGetMethod<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return property.GetMethod?.GetParentMethod<Func<T>>(instance);
        }

        public static Action<T>? GetParentSetMethod<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return property.SetMethod?.GetParentMethod<Action<T>>(instance);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType()
        {
            return TryGetEntryPointType(out Type? type) ? type : throw new EntryPointNotFoundException("Entry point type not found.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return TryGetEntryPointType(assembly, out Type? type) ? type : throw new EntryPointNotFoundException("Entry point type not found.");
        }

        public static Boolean TryGetEntryPointType([MaybeNullWhen(false)] out Type result)
        {
            Assembly? assembly = Assembly.GetEntryAssembly();

            if (assembly is not null)
            {
                return TryGetEntryPointType(assembly, out result);
            }

            result = default;
            return false;
        }

        public static Boolean TryGetEntryPointType(this Assembly assembly, [MaybeNullWhen(false)] out Type result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            Type? type = assembly.EntryPoint?.DeclaringType;

            if (type is null)
            {
                result = default;
                return false;
            }

            result = type;
            return true;
        }

        public static FileInfo GetAssemblyFile(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return new FileInfo(assembly.Location);
        }

        [Obsolete]
        public static FileInfo GetAssemblyFileFromCodeBase(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            String? code = assembly.EscapedCodeBase;
            if (code is null)
            {
                throw new InvalidOperationException();
            }

            Uri uri = new Uri(code);

            if (!uri.IsFile)
            {
                throw new InvalidOperationException();
            }

            return new FileInfo(uri.LocalPath);
        }

        public static Assembly LoadAssembly(this AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.Load(assembly);
        }

        public static Assembly LoadAssembly(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.Load(assembly);
        }

        public static Boolean TryLoadAssembly(this AssemblyName assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssembly(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssembly(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssembly(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Assembly LoadAssemblyFile(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFile(assembly);
        }

        public static Boolean TryLoadAssemblyFile(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFile(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Assembly LoadAssemblyFrom(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFrom(assembly);
        }

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, AssemblyHashAlgorithm algorithm)
        {
            return LoadAssemblyFrom(assembly, hash, algorithm switch
            {
                AssemblyHashAlgorithm.None => AssembliesHashAlgorithm.None,
                AssemblyHashAlgorithm.MD5 => AssembliesHashAlgorithm.MD5,
                AssemblyHashAlgorithm.Sha1 => AssembliesHashAlgorithm.SHA1,
                AssemblyHashAlgorithm.Sha256 => AssembliesHashAlgorithm.SHA256,
                AssemblyHashAlgorithm.Sha384 => AssembliesHashAlgorithm.SHA384,
                AssemblyHashAlgorithm.Sha512 => AssembliesHashAlgorithm.SHA512,
                _ => throw new EnumUndefinedOrNotSupportedException<AssemblyHashAlgorithm>(algorithm, nameof(algorithm), null)
            });
        }

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, AssembliesHashAlgorithm algorithm)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFrom(assembly, hash, algorithm);
        }

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);

            if (token.SequenceEqual(name.GetPublicKeyToken() ?? throw new CryptographicException()) ||
                token.SequenceEqual(name.GetPublicKey() ?? throw new CryptographicException()))
            {
                return LoadAssemblyFrom(assembly);
            }

            throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);
            return token.SequenceEqual(name.GetPublicKey() ?? throw new CryptographicException()) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyTokenFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);
            return token.SequenceEqual(name.GetPublicKeyToken() ?? throw new CryptographicException()) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);

            String hextoken = name.GetPublicKeyToken()?.ToHexString() ?? throw new CryptographicException();
            if (String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase))
            {
                return LoadAssemblyFrom(assembly);
            }

            hextoken = name.GetPublicKey()?.ToHexString() ?? throw new CryptographicException();
            if (String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase))
            {
                return LoadAssemblyFrom(assembly);
            }

            throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            String hextoken = AssemblyName.GetAssemblyName(assembly).GetPublicKey()?.ToHexString() ?? throw new CryptographicException();
            return String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyTokenFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            String hextoken = AssemblyName.GetAssemblyName(assembly).GetPublicKeyToken()?.ToHexString() ?? throw new CryptographicException();
            return String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, Byte[]? hash, AssemblyHashAlgorithm algorithm, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, hash, algorithm);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, Byte[]? hash, AssembliesHashAlgorithm algorithm, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, hash, algorithm);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyTokenFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyTokenFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyTokenFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyTokenFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String? GetManifestResourceString(this Assembly assembly, String resource)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(resource))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
            }

            try
            {
                using Stream? stream = assembly.GetManifestResourceStream(resource);

                if (stream is null)
                {
                    return null;
                }

                using StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace()
        {
            return GetEntryTypeNamespace(false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace(Boolean root)
        {
            return GetEntryPointType().Namespace is { } @namespace ? root ? @namespace.Split('.')[0] : @namespace : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace(this Assembly assembly)
        {
            return GetEntryTypeNamespace(assembly, false);
        }

        public static String GetEntryTypeNamespace(this Assembly assembly, Boolean root)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            String? @namespace = GetEntryPointType(assembly).Namespace;

            if (@namespace is null)
            {
                throw new InvalidOperationException();
            }

            return root ? @namespace.Split('.')[0] : @namespace;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace()
        {
            return GetEntryAssemblyNamespace(out _, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace(out Type type)
        {
            return GetEntryAssemblyNamespace(out _, out type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace(out Assembly assembly)
        {
            return GetEntryAssemblyNamespace(out assembly, out _);
        }

        public static String GetEntryAssemblyNamespace(out Assembly assembly, out Type type)
        {
            if (!TryGetEntryPointType(out Type? entry))
            {
                throw new EntryPointNotFoundException("Entry point type not found.");
            }

            type = entry;
            assembly = type.Assembly;
            if (!assembly.TryGetEntryTypeNamespace(out String? @namespace))
            {
                throw new EntryPointNotFoundException($"Entry point type namespace not found at '{assembly.FullName}'.");
            }

            return @namespace;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEntryTypeNamespace([MaybeNullWhen(false)] out String result)
        {
            return TryGetEntryTypeNamespace(false, out result);
        }

        public static Boolean TryGetEntryTypeNamespace(Boolean root, [MaybeNullWhen(false)] out String result)
        {
            if (!TryGetEntryPointType(out Type? type))
            {
                result = null;
                return false;
            }

            result = root ? type.Namespace?.Split('.')[0] : type.Namespace;
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEntryTypeNamespace(this Assembly assembly, [MaybeNullWhen(false)] out String result)
        {
            return TryGetEntryTypeNamespace(assembly, false, out result);
        }

        public static Boolean TryGetEntryTypeNamespace(this Assembly assembly, Boolean root, [MaybeNullWhen(false)] out String result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (!TryGetEntryPointType(assembly, out Type? type))
            {
                result = null;
                return false;
            }

            result = root ? type.Namespace?.Split('.')[0] : type.Namespace;
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this IEnumerable<Type> source, String name)
        {
            return GetTypesWithoutNamespace(source, name, false);
        }

        public static Type[] GetTypesWithoutNamespace(this IEnumerable<Type> source, String name, Boolean insensitive)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return source.Where(type => String.Equals(type.Name, name, comparison)).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(String name)
        {
            return GetTypesWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypesWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this Assembly assembly, String name)
        {
            return GetTypesWithoutNamespace(assembly, name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this Assembly assembly, String name, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return GetTypesWithoutNamespace(assembly.GetSafeTypes(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name)
        {
            return GetTypeWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypeWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(this Assembly assembly, String name)
        {
            return GetTypeWithoutNamespace(assembly, name, false);
        }

        public static Type? GetTypeWithoutNamespace(this Assembly assembly, String name, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type[] type = GetTypesWithoutNamespace(assembly, name, insensitive);

            return type.Length switch
            {
                0 => null,
                1 => type[0],
                _ => throw new AmbiguousMatchException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(String name, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(name, false, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(String name, Boolean insensitive, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(this Assembly assembly, String name, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(assembly, name, false, out result);
        }

        public static Boolean TryGetTypeWithoutNamespace(this Assembly assembly, String name, Boolean insensitive, [MaybeNullWhen(false)] out Type result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type[] type = GetTypesWithoutNamespace(assembly, name, insensitive);

            if (type.Length == 1)
            {
                result = type[0];
                return true;
            }

            result = null;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypes(this AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            return domain.GetAssemblies().GetTypes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypes(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().SelectMany(static assembly => assembly.GetSafeTypes());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Type[] GetSafeTypes(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.WhereNotNull().ToArray();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }
            
            return domain.GetAssemblies().GetInterfaces();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().SelectMany(static assembly => assembly.GetInterfaces());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetSafeTypes().Where(static type => type.IsInterface);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Name(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            AssemblyName name = assembly.GetName();
            return name.Name ?? name.FullName;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.WhereNotNull().Select(Name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> FullName(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.WhereNotNull().Select(static assembly => assembly.GetName().FullName);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<Type?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.WhereNotNull().Select(static type => type.Name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> FullName(this IEnumerable<Type?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.WhereNotNull().Select(static type => type.FullName ?? type.Name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<MemberInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.Select(static member => member.Name);
        }

        public static String FullName(this ParameterInfo parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            return $"{parameter.ParameterType.Name} {parameter.Name}";
        }

        public static String DeclarationName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return $"{method.DeclaringType?.Name ?? "UnknownType"}.{method.Name}";
        }

        public static String DeclarationFullName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return $"{method.DeclaringType?.FullName ?? "UnknownType"}.{method.Name}";
        }

        public static String FullName(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ParameterInfo[]? parameters = method.GetSafeParameters();
            return $"{method.DeclarationName()}({(parameters is not null ? String.Join(", ", parameters.Select(FullName)) : "Unknown")})";
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<String> ToNameSet(this IEnumerable<MemberInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.Name().ToImmutableHashSet();
        }
        
        public static IEnumerable<MemberInfo> Where(this IEnumerable<MemberInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            foreach (MemberInfo member in source)
            {
                if (member.MemberType() == type)
                {
                    yield return member;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Where<T>(this IEnumerable<MemberInfo> source)
        {
            return Where(source, typeof(T));
        }
        
        public static IEnumerable<FieldInfo> Where(this IEnumerable<FieldInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            foreach (FieldInfo field in source)
            {
                if (field.FieldType == type)
                {
                    yield return field;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Where<T>(this IEnumerable<FieldInfo> source)
        {
            return Where(source, typeof(T));
        }
        
        public static IEnumerable<PropertyInfo> Where(this IEnumerable<PropertyInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            foreach (PropertyInfo property in source)
            {
                if (property.PropertyType == type)
                {
                    yield return property;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Where<T>(this IEnumerable<PropertyInfo> source)
        {
            return Where(source, typeof(T));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetNamespaces(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetSafeTypes().Select(static type => type.Namespace);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> OrderBy(this IEnumerable<Type> source, TypeComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            TypeComparer comparer = comparison;
            return source.OrderBy(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> OrderByDescending(this IEnumerable<Type> source, TypeComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            TypeComparer comparer = comparison;
            return source.OrderByDescending(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> OrderBy(this IEnumerable<Assembly> source, AssemblyComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            AssemblyComparer comparer = comparison;
            return source.OrderBy(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> OrderByDescending(this IEnumerable<Assembly> source, AssemblyComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            AssemblyComparer comparer = comparison;
            return source.OrderByDescending(comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this IEnumerable<Type> source, String? @namespace)
        {
            return GetTypesInNamespace(source, @namespace, false);
        }

        public static IEnumerable<Type> GetTypesInNamespace(this IEnumerable<Type> source, String? @namespace, Boolean insensitive)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return source.Where(type => String.Equals(type.Namespace, @namespace, comparison)).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace)
        {
            return GetTypesInNamespace(@namespace, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace, Boolean insensitive)
        {
            return GetTypesInNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), @namespace, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace)
        {
            return GetTypesInNamespace(assembly, @namespace, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return GetTypesInNamespace(assembly.GetSafeTypes(), @namespace, insensitive);
        }

        public static Boolean IsJITOptimized(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (Object attribute in assembly.GetCustomAttributes(typeof(DebuggableAttribute), false))
            {
                if (attribute is DebuggableAttribute debuggable)
                {
                    return !debuggable.IsJITOptimizerDisabled;
                }
            }

            return true;
        }

        private static Boolean IsSystemAssembly(String? assembly)
        {
            if (assembly is null)
            {
                return false;
            }

            return assembly.StartsWith("System.") || assembly.StartsWith("Microsoft.") || assembly.StartsWith("netstandard");
        }

        public static Boolean IsSystemAssembly(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return IsSystemAssembly(assembly.FullName);
        }

        public static Boolean IsSystemAssembly(this AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return IsSystemAssembly(assembly.FullName);
        }

        /// <inheritdoc cref="GetStackInfo(Int32)"/>
        public static String GetStackInfo()
        {
            return GetStackInfo(2);
        }

        /// <summary>
        /// Get a String containing stack information from zero or more levels up the call stack.
        /// </summary>
        /// <param name="levels">
        /// A <see cref="System.Int32"/> which indicates how many levels up the stack 
        /// the information should be retrieved from.  This value must be zero or greater.
        /// </param>
        /// <returns>
        /// A String in this format:
        /// <para>
        /// file name::namespace.[one or more type names].method name
        /// </para>
        /// </returns>
        public static String GetStackInfo(Int32 levels)
        {
            if (levels < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(levels), levels, null);
            }

            StackFrame frame = new StackFrame(levels, true);
            MethodBase method = frame.GetMethod() ?? throw new InvalidOperationException();

            return $"{Path.GetFileName(frame.GetFileName())}::{method.DeclaringType?.FullName ?? String.Empty}.{method.Name} - Line {frame.GetFileLineNumber()}";
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class Custom
        {
            public static IEnumerable<Assembly> Assemblies
            {
                get
                {
                    Assembly calling = Assembly.GetCallingAssembly();
                    Assembly? entry = Assembly.GetEntryAssembly();
                    return AppDomain.CurrentDomain.GetAssemblies().WhereNot(IsSystemAssembly).Append(entry).Append(calling).WhereNotNull().Distinct()
                        .OrderByDescending(assembly => assembly == Assembly.GetExecutingAssembly())
                        .ThenByDescending(assembly => assembly.GetName().FullName.StartsWith(nameof(NetExtender)))
                        .ThenByDescending(assembly => assembly == calling)
                        .ThenByDescending(assembly => assembly == entry)
                        .ThenBy(assembly => assembly.GetName().FullName);
                }
            }

            public static IEnumerable<Type> Types
            {
                get
                {
                    return Assemblies.GetTypes();
                }
            }
        }

        /// <summary>
        /// Calls the static constructor of this type.
        /// </summary>
        /// <param name="type">The type of which to call the static constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type CallStaticConstructor(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            CallStaticConstructor(type.TypeHandle);
            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RuntimeTypeHandle CallStaticConstructor(this RuntimeTypeHandle handle)
        {
            RuntimeHelpers.RunClassConstructor(handle);
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source)
        {
            return CallStaticConstructor(source, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source, Boolean lazy)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            static void Call(Type type)
            {
                CallStaticConstructor(type);
            }

            return source.ForEach(Call).MaterializeIfNot(lazy);
        }

        private static Assembly CallStaticInitializerAttribute<TAttribute>(this Assembly assembly) where TAttribute : StaticInitializerAttribute, new()
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            static IEnumerable<StaticInitializerAttribute> Handler(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                IEnumerable<StaticInitializerAttribute> attributes = GetCustomAttributes<TAttribute>(type);
                
                if (typeof(TAttribute) == typeof(StaticInitializerRequiredAttribute))
                {
                    attributes = GetCustomAttributes<StaticInitializerNetExtenderAttribute>(type).Concat(attributes);
                }

                foreach (StaticInitializerAttribute attribute in attributes)
                {
                    if (!attribute.Active || !attribute.Platform.IsOSPlatform())
                    {
                        continue;
                    }

                    if (attribute.Type is null)
                    {
                        yield return new TAttribute { Type = type, Priority = attribute.Priority, Active = attribute.Active, Platform = attribute.Platform };
                        continue;
                    }

                    yield return attribute;
                }
            }

            IEnumerable<Type> result = assembly.GetSafeTypes()
                .SelectMany(Handler)
                .OrderByDescending(static attribute => attribute is StaticInitializerNetExtenderAttribute)
                .ThenByDescending(static attribute => attribute.Priority)
                .ThenBy(static attribute => attribute.Type?.FullName)
                .Select(static attribute => attribute.Type)
                .WhereNotNull()
                .Distinct();

            foreach (Type type in result)
            {
                type.CallStaticConstructor();
            }

            return assembly;
        }

        /// <summary>
        /// Calls the static constructor of types with <see cref="StaticInitializerAttribute"/> in assembly.
        /// </summary>
        /// <param name="assembly">The assembly of which to call the static constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly CallStaticInitializerAttribute(this Assembly assembly)
        {
            return CallStaticInitializerAttribute<StaticInitializerAttribute>(assembly);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies)
        {
            return CallStaticInitializerAttribute(assemblies, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies, Boolean lazy)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttribute(assembly);
            }

            return assemblies.ForEach(Call).MaterializeIfNot(lazy);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute()
        {
            return CallStaticInitializerAttribute(Custom.Assemblies);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<Assembly> CallStaticInitializerRequiredAttribute()
        {
            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttribute<StaticInitializerRequiredAttribute>(assembly);
            }

            return Custom.Assemblies.ForEach(Call).Materialize();
        }

        /// <summary>
        /// Gets the method that called the current method where this method is used. This does not work when used in async methods.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase? GetCallingMethod()
        {
            return new StackFrame(2, false).GetMethod();
        }

        /// <summary>
        /// Gets the type that called the current method where this method is used.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Type? GetCallingType()
        {
            return new StackFrame(2, false).GetMethod()?.DeclaringType;
        }

        /// <summary>
        /// Returns all the public properties of this object whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="object">The object.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Object @object)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            return GetAllPropertiesOfType<T>(@object.GetType());
        }

        /// <summary>
        /// Returns all the public properties of this type whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="type">The type of which to get the properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties().Where(static info => info.PropertyType == typeof(T));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static Object? GetPropertyValue(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(@object, name, out Object? result))
            {
                return result;
            }

            throw new Exception($"'{name}' is neither a property or a field of type '{result}'.");
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Object if successful else <see cref="object"/> <see cref="Type"/></param>
        public static Boolean GetPropertyValue(this Object @object, String name, out Object? result)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (@object is not Type type)
            {
                type = @object.GetType();
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo? property = type.GetProperty(name, binding);
            if (property is not null)
            {
                result = property.GetValue(@object);
                return true;
            }

            FieldInfo? field = type.GetField(name, binding);
            if (field is not null)
            {
                result = field.GetValue(@object);
                return true;
            }

            result = type;
            return false;
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static T? GetPropertyValue<T>(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Object? value = GetPropertyValue(@object, name);
            return value switch
            {
                null => default,
                T result => result,
                _ => value.TryConvert(out T? result) ? result : throw new InvalidCastException()
            };
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        public static T GetPropertyValue<T>(this Object @object, String name, ParseHandler<Object?, T> converter)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter(GetPropertyValue(@object, name));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>(this Object @object, String name, out T? result)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(@object, name, out Object? property))
            {
                if (property is T value)
                {
                    result = value;
                    return true;
                }

                try
                {
                    return property.TryConvert(out result);
                }
                catch (Exception)
                {
                    result = default;
                    return false;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>(this Object @object, String name, TryParseHandler<Object?, T> converter, out T? result)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (GetPropertyValue(@object, name, out Object? value) && converter(value, out result))
            {
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// <para>
        /// If the type is nullable, this function gets its generic definition."/>.
        /// </para>
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        public static Type GetPropertyType(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            Type property = GetPropertyTypeReal(type, name);

            // get the generic type of nullable, not THE nullable
            if (property.IsGenericType && property.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                property = FirstGenericArgument(property);
            }

            return property;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// <para>
        /// If the type is nullable, this function gets its generic definition."/>.
        /// </para>
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static Type GetPropertyType(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            Type property = GetPropertyTypeReal(type, name, binding);

            // get the generic type of nullable, not THE nullable
            if (property.IsGenericType && property.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                property = FirstGenericArgument(property);
            }

            return property;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetPropertyTypeReal(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(type, name);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetPropertyTypeReal(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(type, name, binding);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyInfo(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return GetPropertyInfo(@object.GetType(), name);
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyInfo(this Object @object, String name, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return GetPropertyInfo(@object.GetType(), name, binding);
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        public static PropertyInfo GetPropertyInfo(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo? property = type.GetProperty(name);

            if (property is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return property;
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo? property = type.GetProperty(name, binding);

            if (property is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return property;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsIndexer(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetIndexParameters().Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsVoid(this Type? type)
        {
            return type == typeof(void);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsVoid(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.ReturnType.IsVoid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOverridden(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
        }

        public static FieldAttributes Access(this FieldAttributes attributes)
        {
            return attributes & FieldAttributes.FieldAccessMask;
        }

        public static MethodAttributes Access(this MethodAttributes attributes)
        {
            return attributes & MethodAttributes.MemberAccessMask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsRead(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.CanRead)
            {
                return MethodVisibilityType.Unavailable;
            }

            return info.GetMethod?.IsPublic switch
            {
                null => MethodVisibilityType.Unavailable,
                false => MethodVisibilityType.Private,
                true => MethodVisibilityType.Public
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsWrite(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.CanWrite)
            {
                return MethodVisibilityType.Unavailable;
            }

            return info.SetMethod?.IsPublic switch
            {
                null => MethodVisibilityType.Unavailable,
                false => MethodVisibilityType.Private,
                true => MethodVisibilityType.Public
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean? ToBoolean(this MethodVisibilityType type)
        {
            return type switch
            {
                MethodVisibilityType.Unavailable => null,
                MethodVisibilityType.Private => false,
                MethodVisibilityType.Public => true,
                _ => throw new EnumUndefinedOrNotSupportedException<MethodVisibilityType>(type, nameof(type), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreateDelegate(this MethodInfo? info, [MaybeNullWhen(false)] out Delegate result)
        {
            return TryCreateDelegate<Delegate>(info, out result);
        }

        public static Boolean TryCreateDelegate<T>(this MethodInfo? info, [MaybeNullWhen(false)] out T result) where T : Delegate
        {
            if (info is null)
            {
                result = default;
                return false;
            }

            try
            {
                result = info.CreateDelegate<T>();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Gets the field information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo GetFieldInfo(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetFieldInfo(@object.GetType(), name);
        }

        /// <summary>
        /// Gets the field information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        public static FieldInfo GetFieldInfo(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            FieldInfo? field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return field;
        }

        /// <summary>
        /// Returns the first definition of generic type of this generic type.
        /// </summary>
        /// <param name="type">The type from which to get the generic type.</param>
        public static Type FirstGenericArgument(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type[] arguments = type.GetGenericArguments();

            if (arguments.Length <= 0)
            {
                throw new ArgumentException($"The provided type ({type}) is not a generic type.", nameof(type));
            }

            return arguments[0];
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            return GetConstants(type, true);
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        /// <param name="inherited">Determines whether or not to include inherited constants.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetConstants(this Type type, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Static | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetFields(binding).Where(static field => field is { IsLiteral: true, IsInitOnly: false });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetAccessibleFields(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeFields);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAccessibleProperties(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeProperties);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetAccessibleEvents(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeEvents);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetAccessibleMethods(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeMethods);
        }

        private static IEnumerable<T> GetAccessibleMembers<T>(this Type type, Func<Type, IEnumerable<T>> selector) where T : MemberInfo
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            List<T> result = new List<T>(selector(type));

            if (!type.IsInterface)
            {
                return result.ToArray();
            }

            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsVisible)
                {
                    result.AddRange(selector.Invoke(@interface));
                }
            }

            result.AddRange(selector.Invoke(typeof(Object)));
            return result.ToArray();
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasSignature(this Type type, TypeSignature signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return signature is TypeSignature.Any ||
                   !(
                       signature.HasFlag(TypeSignature.Public) && !type.IsPublic ||
                       signature.HasFlag(TypeSignature.NotPublic) && !type.IsNotPublic ||
                       signature.HasFlag(TypeSignature.AutoLayout) && !type.IsAutoLayout ||
                       signature.HasFlag(TypeSignature.SequentialLayout) && !type.IsLayoutSequential ||
                       signature.HasFlag(TypeSignature.ExplicitLayout) && !type.IsExplicitLayout ||
                       signature.HasFlag(TypeSignature.Array) && !type.IsArray ||
                       signature.HasFlag(TypeSignature.SZArray) && !type.IsSZArray ||
                       signature.HasFlag(TypeSignature.VariableBoundArray) && !type.IsVariableBoundArray ||
                       signature.HasFlag(TypeSignature.ElementType) && !type.HasElementType ||
                       signature.HasFlag(TypeSignature.SignatureType) && !type.IsSignatureType ||
                       signature.HasFlag(TypeSignature.ByRef) && !type.IsByRef ||
                       signature.HasFlag(TypeSignature.MarshalByRef) && !type.IsMarshalByRef ||
                       signature.HasFlag(TypeSignature.ByRefLike) && !type.IsByRefLike ||
                       signature.HasFlag(TypeSignature.Pointer) && !type.IsPointer ||
                       signature.HasFlag(TypeSignature.Value) && !type.IsValueType ||
                       signature.HasFlag(TypeSignature.Primitive) && !type.IsPrimitive ||
                       signature.HasFlag(TypeSignature.Enum) && !type.IsEnum ||
                       signature.HasFlag(TypeSignature.Class) && !type.IsClass ||
                       signature.HasFlag(TypeSignature.Interface) && !type.IsInterface ||
                       signature.HasFlag(TypeSignature.Abstract) && !type.IsAbstract ||
                       signature.HasFlag(TypeSignature.Sealed) && !type.IsSealed ||
                       signature.HasFlag(TypeSignature.Nested) && !type.IsNested ||
                       signature.HasFlag(TypeSignature.GenericType) && !type.IsGenericType ||
                       signature.HasFlag(TypeSignature.GenericTypeDefinition) && !type.IsGenericTypeDefinition ||
                       signature.HasFlag(TypeSignature.ConstructedGenericType) && !type.IsConstructedGenericType ||
                       signature.HasFlag(TypeSignature.GenericParameter) && !type.IsGenericParameter ||
                       signature.HasFlag(TypeSignature.GenericTypeParameter) && !type.IsGenericTypeParameter ||
                       signature.HasFlag(TypeSignature.GenericMethodParameter) && !type.IsGenericMethodParameter ||
                       signature.HasFlag(TypeSignature.SpecialName) && !type.IsSpecialName ||
                       signature.HasFlag(TypeSignature.RTSpecialName) && !type.Attributes.HasFlag(TypeAttributes.RTSpecialName) ||
                       signature.HasFlag(TypeSignature.Import) && !type.IsImport ||
                       signature.HasFlag(TypeSignature.Serializable) && !type.IsSerializable ||
                       signature.HasFlag(TypeSignature.Contextful) && !type.IsContextful ||
                       signature.HasFlag(TypeSignature.AnsiClass) && !type.IsAnsiClass ||
                       signature.HasFlag(TypeSignature.UnicodeClass) && !type.IsUnicodeClass ||
                       signature.HasFlag(TypeSignature.AutoClass) && !type.IsAutoClass ||
                       signature.HasFlag(TypeSignature.COMObject) && !type.IsCOMObject ||
                       signature.HasFlag(TypeSignature.IsSecurityCritical) && !type.IsSecurityCritical ||
                       signature.HasFlag(TypeSignature.IsSecuritySafeCritical) && !type.IsSecuritySafeCritical ||
                       signature.HasFlag(TypeSignature.IsSecurityTransparent) && !type.IsSecurityTransparent
                   );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method)
        {
            return HasSignature(method, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method, params ReflectionParameterSignature[]? signature)
        {
            return HasSignature(method, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method, ReflectionParameterSignature @return)
        {
            return HasSignature(method, @return, ReflectionParameterSignature.Any);
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasSignature(this MethodBase method, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            MethodInfo? info = method as MethodInfo;
            if (signature is null)
            {
                return info is null || @return.Equals(info.ReturnParameter);
            }

            if (method.GetSafeParameters() is not { } parameters || info is not null && !@return.Equals(info.ReturnParameter))
            {
                return false;
            }

            if (ReferenceEquals(signature, ReflectionParameterSignature.NotEmpty))
            {
                return parameters.Length > 0;
            }

            if (signature.Length != parameters.Length)
            {
                return false;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            for (Int32 i = 0; i < signature.Length; i++)
            {
                if (signature[i].IsEmpty)
                {
                    continue;
                }
                
                if (!signature[i].Equals(parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source) where T : MethodBase
        {
            return WithSignature(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            return WithSignature(source, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, ReflectionParameterSignature @return) where T : MethodBase
        {
            return WithSignature(source, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T method in source)
            {
                if (HasSignature(method, @return, signature))
                {
                    yield return method;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T[] WithSignature<T>(T[] methods, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            return WithSignature(methods, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static T[] WithSignature<T>(T[] methods, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            if (signature is null && @return.IsEmpty)
            {
                return methods;
            }

            List<T> result = new List<T>(methods.Length);

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T method in methods)
            {
                if (HasSignature(method, @return, signature))
                {
                    result.Add(method);
                }
            }

            return result.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type)
        {
            return HasMethods(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return HasMethods(type, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, ReflectionParameterSignature @return)
        {
            return HasMethods(type, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, @return, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding)
        {
            return HasMethods(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return HasMethods(type, binding, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return HasMethods(type, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, binding, @return, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type)
        {
            return GetMethods(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, ReflectionParameterSignature @return)
        {
            return GetMethods(type, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetMethods(), @return, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding)
        {
            return GetMethods(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, binding, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return GetMethods(type, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetMethods(binding), @return, signature);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IEnumerable<KeyValuePair<Type, T[]>> Where<T>(this IEnumerable<KeyValuePair<Type, T[]>> source, Func<T, Boolean> predicate) where T : MethodBase
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            List<T> capacitor = new List<T>(16);
            foreach (KeyValuePair<Type, T[]> pair in source)
            {
                capacitor.AddRange(pair.Value.Where(predicate));

                if (capacitor.Count <= 0)
                {
                    continue;
                }

                KeyValuePair<Type, T[]> result = new KeyValuePair<Type, T[]>(pair.Key, capacitor.ToArray());
                capacitor.Clear();
                yield return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source)
        {
            return WithMethods(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, ReflectionParameterSignature @return)
        {
            return WithMethods(source, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetMethods(@return, signature) is { Length: > 0 } methods)
                {
                    yield return new KeyValuePair<Type, MethodInfo[]>(type, methods);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate)
        {
            return WithMethods(source, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, predicate, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return)
        {
            return WithMethods(source, predicate, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, MethodInfo[]>> methods = WithMethods(source, @return, signature);
            return predicate is not null ? methods.Where(predicate) : methods;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding)
        {
            return WithMethods(source, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, binding, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return WithMethods(source, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetMethods(binding, @return, signature) is { Length: > 0 } methods)
                {
                    yield return new KeyValuePair<Type, MethodInfo[]>(type, methods);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate)
        {
            return WithMethods(source, binding, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, binding, predicate, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return)
        {
            return WithMethods(source, binding, predicate, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, MethodInfo[]>> methods = WithMethods(source, binding, @return, signature);
            return predicate is not null ? methods.Where(predicate) : methods;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo? GetStaticConstructor(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return type.GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type)
        {
            return HasConstructors(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return HasConstructors(type, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, BindingFlags binding)
        {
            return HasConstructors(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return GetConstructors(type, binding, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return GetConstructors(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetConstructors(), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source)
        {
            return WithConstructors(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetConstructors(signature) is { Length: > 0 } constructors)
                {
                    yield return new KeyValuePair<Type, ConstructorInfo[]>(type, constructors);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, Func<ConstructorInfo, Boolean>? predicate)
        {
            return WithConstructors(source, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, Func<ConstructorInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> constructors = WithConstructors(source, signature);
            return predicate is not null ? constructors.Where(predicate) : constructors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags binding)
        {
            return GetConstructors(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetConstructors(binding), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding)
        {
            return WithConstructors(source, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetConstructors(binding, signature) is { Length: > 0 } constructors)
                {
                    yield return new KeyValuePair<Type, ConstructorInfo[]>(type, constructors);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, Func<ConstructorInfo, Boolean>? predicate)
        {
            return WithConstructors(source, binding, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, Func<ConstructorInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> constructors = WithConstructors(source, binding, signature);
            return predicate is not null ? constructors.Where(predicate) : constructors;
        }
        
        public static MethodInfo[] SignatureEqualityMethodAnalyzer(this Type type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.CreateInstance;
            MethodInfo[] available = type.GetMethods(binding);
            List<MethodInfo> result = new List<MethodInfo>(available.Length);
            //TODO:
            return result.ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasImplementation(this MethodInfo method)
        {
            return HasImplementation(method, out Boolean? _);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasImplementation(this MethodInfo method, out Boolean @virtual)
        {
            Boolean result = HasImplementation(method, out Boolean? state);
            @virtual = state is true;
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasImplementation(this MethodInfo method, out Boolean? @virtual)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            if (method.IsAbstract)
            {
                @virtual = true;
                return false;
            }
            
            @virtual = method switch
            {
                { IsVirtual: true } => true,
                { IsStatic: true } => null,
                _ => false
            };
            
            if (method.GetMethodBody() is not { } body)
            {
                return false;
            }
            
            if (body.GetILAsByteArray() is not { } array)
            {
                return false;
            }
            
            ReadOnlySpan<OpByte> code = array.AsSpan().As<Byte, OpByte>();

            Int32 nop = 0;
            while (code[nop] == OpCodes.Nop)
            {
                nop++;
            }
            
            return nop < code.Length && HasImplementation(method, code.Slice(nop));
        }
        
        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HasImplementation(this MethodInfo method, ReadOnlySpan<OpByte> code)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsThrow(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code.Length <= i + 6 && code[i++] == OpCodes.Newobj && code[i + sizeof(Int32)] == OpCodes.Throw;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsNull(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code.Length <= i + 6 && code[i++] == OpCodes.Ldnull && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }
            
            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsVoid(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return i + 1 == code.Length && code[i] == OpCodes.Ret;
            }
            
            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsDefault(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return Read<Int32>(code, ref i) != 0 && code[i++] == OpCodes.Ldloc_0 && code[i++] == OpCodes.Stloc_1 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_1 && code[++i] == OpCodes.Ret;
            }
            
            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsField(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return Read<Int32>(code, ref i) != 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsInt32(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsInt64(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code[i++] == OpCodes.Conv_I8 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static unsafe T Read<T>(ReadOnlySpan<OpByte> code, ref Int32 start) where T : unmanaged
            {
                T value = code.Slice(start, sizeof(T)).As<OpByte, Byte>().Read<T>();
                start += sizeof(T);
                return value;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static T ReadNext<T>(ReadOnlySpan<OpByte> code, ref Int32 start) where T : unmanaged
            {
                ++start;
                return Read<T>(code, ref start);
            }
            
            Int32 i = 0;
            if (IsThrow(code, i))
            {
                return false;
            }
            
            if (!method.ReturnType.IsValueType && IsNull(code, i))
            {
                return false;
            }
            
            if (method.ReturnType.IsVoid())
            {
                return !IsVoid(code, i);
            }
            
            if (!method.ReturnType.IsValueType)
            {
                return true;
            }
            
            i = 0;
            if ((code[i++] == OpCodes.Ldc_I4_0 || code[i = 0] == OpCodes.Ldc_I4 && ReadNext<Int32>(code, ref i) == 0) && (IsInt32(code, i) || IsInt64(code, i)))
            {
                return false;
            }
            
            if (code[i = 0] == OpCodes.Ldc_R4 && ReadNext<Single>(code, ref i) == 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret)
            {
                return false;
            }
            
            if (code[i = 0] == OpCodes.Ldc_R8 && ReadNext<Double>(code, ref i) == 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret)
            {
                return false;
            }
            
            if (code[i = 0] == OpCodes.Ldloca_S && code[i += 2] == unchecked((UInt16) OpCodes.Initobj.Value).High() && code[++i] == OpCodes.Initobj.Value.Low())
            {
                return !IsDefault(code, ++i);
            }
            
            if (code[i = 0] == OpCodes.Ldsfld)
            {
                return !IsField(code, ++i);
            }
            
            return true;
        }

        /// <summary>
        /// Sets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The objector type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="value">The value to set.</param>
        public static T SetValue<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (@object is not Type type)
            {
                type = @object.GetType();
            }

            PropertyInfo? property = type.GetProperty(name);
            if (property is not null)
            {
                property.SetValue(@object, value);
                return @object;
            }

            FieldInfo? field = type.GetField(name);

            if (field is null)
            {
                throw new Exception($"'{name}' is neither a property or a field of type '{type}'.");
            }

            field.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified field to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the field.</param>
        /// <param name="name">The name of the field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        public static T SetField<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            FieldInfo field = GetFieldInfo(@object, name);
            field.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static T SetProperty<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(@object, name);
            property.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetProperty<T>(this T @object, String name, Object? value, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(@object, name, binding);
            property.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        public static T SetPropertyFromString<T>(this T @object, String name, String? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(@object, name);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value!);
            property.SetValue(@object, result);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetPropertyFromString<T>(this T @object, String name, String? value, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            PropertyInfo property = GetPropertyInfo(@object, name, binding);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value!);
            property.SetValue(@object, result);
            return @object;
        }

        /// <summary>
        /// Sets all fields and properties of the specified type in the provided object to the specified value.
        /// <para>Internal, protected and private fields are included, static are not.</para>
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="TValue">The type of the properties.</typeparam>
        /// <param name="object">The object.</param>
        /// <param name="value">The value to set the properties to.</param>
        public static T SetAllPropertiesOfType<T, TValue>(this T @object, TValue? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            FieldInfo[] fields = @object.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(TValue))
                {
                    field.SetValue(@object, value);
                }
            }

            return @object;
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Object @object, String name)
        {
            return HasProperty(@object, name, true);
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Object @object, String name, Boolean inherited)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }
            
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            Type type = @object.GetType();
            return HasProperty(type, name, inherited);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Type type, String name)
        {
            return HasProperty(type, name, true);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Type type, String name, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            const BindingFlags binding = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo? property = type.GetProperty(name, binding | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly));
            return property is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsMulticastDelegateFieldType(this Type? type)
        {
            return type is not null && (type == typeof(MulticastDelegate) || type.IsSubclassOf(typeof(MulticastDelegate)));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static FieldInfo? GetEventField(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type? current = type;
            FieldInfo? field = null;
            while (current is not null)
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic;
                field = current.GetField(name, binding);

                if (field is not null && field.FieldType.IsMulticastDelegateFieldType())
                {
                    return field;
                }

                field = current.GetField("EVENT_" + name.ToUpper(), binding);

                if (field is not null && field.FieldType.IsMulticastDelegateFieldType())
                {
                    return field;
                }

                current = current.BaseType;
            }

            return field;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static FieldInfo? GetEventField<T>(this Type type) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            Type? current = type;
            while (current is not null)
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic;
                FieldInfo? field = current.GetFields(binding).FirstOrDefault(field => field.FieldType.IsMulticastDelegateFieldType() && field.FieldType.IsAssignableFrom(typeof(T)));
                
                if (field is not null)
                {
                    return field;
                }

                current = current.BaseType;
            }

            return null;
        }

        public static FieldInfo[] GetEventFields(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type? current = type;
            List<FieldInfo> fields = new List<FieldInfo>(8);
            while (current is not null)
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic;
                fields.AddRange(current.GetFields(binding).Where(field => field.FieldType.IsMulticastDelegateFieldType()));
                current = current.BaseType;
            }

            return fields.ToArray();
        }
        
        public static FieldInfo[] GetEventFields<T>(this Type type) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type? current = type;
            List<FieldInfo> fields = new List<FieldInfo>(8);
            while (current is not null)
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic;
                fields.AddRange(current.GetFields(binding).Where(field => field.FieldType.IsMulticastDelegateFieldType() && field.FieldType.IsAssignableFrom(typeof(T))));
                current = current.BaseType;
            }

            return fields.ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetEventRaiseMethod(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return type.GetEventField(name)?.FieldType.GetMethod(nameof(Action.Invoke));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetEventRaiseMethod<T>(this Type type) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetEventField<T>()?.FieldType.GetMethod(nameof(Action.Invoke));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MulticastDelegate? GetEventDelegate(this Type type, String name, Object? @object)
        {
            return GetEventDelegate(type, name, @object, out _);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MulticastDelegate? GetEventDelegate(this Type type, String name, Object? @object, out FieldInfo? field)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            field = type.GetEventField(name);
            return (MulticastDelegate?) field?.GetValue(@object);
        }

        public static T? GetEventDelegate<T>(this Type type, Object? @object) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            KeyValuePair<FieldInfo, T>[] delegates = GetEventDelegates<T>(type, @object);

            return delegates.Length switch
            {
                <= 0 => null,
                1 => delegates[0].Value,
                _ => throw new AmbiguousMatchException()
            };
        }

        public static T? GetEventDelegate<T>(this Type type, Object? @object, out FieldInfo? field) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            KeyValuePair<FieldInfo, T>[] delegates = GetEventDelegates<T>(type, @object);

            switch (delegates.Length)
            {
                case <= 0:
                    field = null;
                    return null;
                case 1:
                    (field, T @delegate) = delegates[0];
                    return @delegate;
                default:
                    throw new AmbiguousMatchException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetEventDelegate<T>(this Type type, String name, Object? @object) where T : Delegate
        {
            return GetEventDelegate(type, name, @object) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetEventDelegate<T>(this Type type, String name, Object? @object, out FieldInfo? field) where T : Delegate
        {
            return GetEventDelegate(type, name, @object, out field) as T;
        }
        
        public static KeyValuePair<FieldInfo, T>[] GetEventDelegates<T>(this Type type, Object? @object) where T : Delegate
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetEventFields<T>().Select(field => new KeyValuePair<FieldInfo, T?>(field, field.GetValue(@object) as T)).WhereValueNotNull().ToArray();
        }

        public static Boolean ClearEventInvocations(Object value, String name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                FieldInfo? info = value.GetType().GetEventField(name);

                if (info is null)
                {
                    return false;
                }

                info.SetValue(value, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean Implements<TValue>(Object? value)
        {
            return Implements(value, typeof(TValue));
        }

        public static Boolean Implements(Object? value, Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value is not null && Implements(value.GetType(), type);
        }

        public static Boolean Implements<TValue>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Implements(type, typeof(TValue));
        }

        public static Boolean Implements(this Type type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                return false;
            }

            return type.IsGenericTypeDefinition ? type.ImplementsGeneric(type) : type.IsAssignableFrom(type);
        }

        public static Boolean ImplementsGeneric(this Type type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.ImplementsGeneric(@interface, out _);
        }

        public static Boolean ImplementsGeneric(this Type type, Type @interface, out Type? result)
        {
            result = null;

            if (@interface.IsInterface)
            {
                result = type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == @interface);

                if (result is not null)
                {
                    return true;
                }
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == @interface)
            {
                result = type;
                return true;
            }

            Type? @base = type.BaseType;
            return @base is not null && @base.ImplementsGeneric(@interface, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSerializable<T>(this IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return typeof(T).IsValueType ? typeof(T).IsSerializable : source.All(static value => value?.GetType().IsSerializable is not false);
        }
        
        public static String? StripQualifier(this Type type)
        {
            return StripQualifier(type, AssemblyNameType.Default);
        }

        public static String? StripQualifier(this Type type, AssemblyNameType assemblyname)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String? name = type.AssemblyQualifiedName;
            return name is not null ? StripQualifier(name, assemblyname) : null;
        }

        public static String StripQualifier(String type)
        {
            return StripQualifier(type, AssemblyNameType.Default);
        }

        public static String StripQualifier(String type, AssemblyNameType assemblyname)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            const String version = ", Version=";
            const String culture = ", Culture=";
            const String publickey = ", PublicKeyToken=";

            NameValueCollection tokens = new NameValueCollection();

            type = $"[{type.Trim('[', ']')}]";

            foreach (String token in new[] { version, culture, publickey })
            {
                Int32 index = type.IndexOf(token, StringComparison.InvariantCulture);

                while (index > 0)
                {
                    Int32 lastindex = type.IndexOfAny(new[] { ']', ',' }, index + 2);
                    String value = type.Substring(index, lastindex - index);
                    tokens[token] = value;
                    type = type.Remove(value);
                    index = type.IndexOf(token, StringComparison.InvariantCulture);
                }
            }

            type = type.TrimStart('[').TrimEnd(']');

            if (assemblyname.HasFlag(AssemblyNameType.Version))
            {
                type += tokens[version];
            }

            if (assemblyname.HasFlag(AssemblyNameType.Culture))
            {
                type += tokens[culture];
            }

            if (assemblyname.HasFlag(AssemblyNameType.PublicKeyToken))
            {
                type += tokens[publickey];
            }

            return type;
        }

        /// <summary>
        /// Determines whether the specified type to check derives from this generic type.
        /// </summary>
        /// <param name="generic">The parent generic type.</param>
        /// <param name="type">The type to check if it derives from the specified generic type.</param>
        public static Boolean IsSubclassOfRawGeneric(this Type generic, Type? type)
        {
            if (generic is null)
            {
                throw new ArgumentNullException(nameof(generic));
            }

            while (type is not null && type != typeof(Object))
            {
                if (generic == type.TryGetGenericTypeDefinition())
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSuperclassOfRawGeneric(this Type type, Type? generic)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return generic is not null && IsSubclassOfRawGeneric(generic, type);
        }
        
        /// <inheritdoc cref="IsPrimitive(System.Type,PrimitiveType)"/>
        public static Boolean IsPrimitive(this Type type)
        {
            return IsPrimitive(type, PrimitiveType.All);
        }

        /// <summary>
        /// Determines whether this type is a primitive.
        /// <para><see cref="string"/> is considered a primitive.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="primitive">What type is primitive</param>
        public static Boolean IsPrimitive(this Type type, PrimitiveType primitive)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsPrimitive ||
                   primitive.HasFlag(PrimitiveType.String) && type == typeof(String) ||
                   primitive.HasFlag(PrimitiveType.Decimal) && type == typeof(Decimal) ||
                   primitive.HasFlag(PrimitiveType.Complex) && type == typeof(Complex) ||
                   primitive.HasFlag(PrimitiveType.TimeSpan) && type == typeof(TimeSpan) ||
                   primitive.HasFlag(PrimitiveType.DateTime) && type == typeof(DateTime) ||
                   primitive.HasFlag(PrimitiveType.DateTimeOffset) && type == typeof(DateTimeOffset);
        }

        public static Boolean IsCompilerGenerated(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsDefined(typeof(CompilerGeneratedAttribute));
        }

        private static class SizeStorage<T> where T : struct
        {
            // ReSharper disable once StaticMemberInGenericType
            public static Int32 Size { get; }

            static SizeStorage()
            {
                Size = GetSize(typeof(T));
            }
        }

        private static class SizeStorage
        {
            private static ConcurrentDictionary<Type, Int32> Cache { get; } = new ConcurrentDictionary<Type, Int32>();

            public static Int32 GetTypeSize(Type type)
            {
                if (Cache.TryGetValue(type, out Int32 size))
                {
                    return size;
                }

                if (!type.IsValueType)
                {
                    throw new ArgumentException(@"Is not value type", nameof(type));
                }

                DynamicMethod method = new DynamicMethod("SizeOfType", typeof(Int32), Type.EmptyTypes);
                ILGenerator il = method.GetILGenerator();
                il.Emit(OpCodes.Sizeof, type);
                il.Emit(OpCodes.Ret);

                Object? value = method.Invoke(null, null);

                if (value is null)
                {
                    return 0;
                }

                size = (Int32) value;
                Cache.TryAdd(type, size);
                return size;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>(this T _) where T : struct
        {
            return GetSize<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>(this T[] array) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return SizeStorage<T>.Size * array.Length;
        }

        public static Boolean GetSize<T>(this T[] array, out Int64 size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeStorage<T>.Size * array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        public static Boolean GetSize<T>(this T[] array, out BigInteger size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeStorage<T>.Size * (BigInteger) array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>() where T : struct
        {
            return SizeStorage<T>.Size;
        }

        public static Int32 GetSize(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                throw new ArgumentException(@"Is not value type", nameof(type));
            }

            return SizeStorage.GetTypeSize(type);
        }

        private static class GenericDefaultStorage
        {
            private static ConcurrentDictionary<Type, ValueType> Values { get; } = new ConcurrentDictionary<Type, ValueType>();

            // ReSharper disable once MemberHidesStaticFromOuterClass
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static Object? Default(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return type.IsValueType ? Values.GetOrAdd(type, static type => (ValueType) Activator.CreateInstance(type)!) : null;
            }
        }

        public static IEnumerator<StackFrame> GetEnumerator(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            Int32 i = 0;
            while (i < stack.FrameCount && stack.GetFrame(i++) is { } frame)
            {
                yield return frame;
            }
        }

        public static IEnumerable<Type> GetStackTypes(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            foreach (StackFrame frame in stack)
            {
                if (frame.GetMethod()?.DeclaringType is { } type)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Type> GetStackTypesUnique(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            return stack.GetStackTypes().Distinct();
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? Default(Type type)
        {
            return GenericDefaultStorage.Default(type);
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        /// <param name="nullable"><see cref="Nullable{T}"/> is value or null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? Default(Type type, Boolean nullable)
        {
            return nullable || Nullable.GetUnderlyingType(type) is null ? Default(type) : null;
        }

        /// <summary>
        /// Gets the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type for which to get the default value.</typeparam>
        public static T? Default<T>()
        {
            return default;
        }
    }
}