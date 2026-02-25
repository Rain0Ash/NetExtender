using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Cecil;
using NetExtender.Exceptions;
using NetExtender.Types.Comparers;
using NetExtender.Types.Entities;
using NetExtender.Utilities.Types;

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
#if NET8_0_OR_GREATER
        [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        Serializable = TypeAttributes.Serializable,
#pragma warning disable SYSLIB0050
        Contextful = Serializable << 1,
#pragma warning restore SYSLIB0050
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

    public static class TypeUtilities
    {
        public const String Constructor = ".ctor";

        public static Type NullableAttribute { get; }
        public static Type NullableContextAttribute { get; }
        private static HashSet<Type> VarArgTypes { get; }
        private static Predicate<Type> IsByRefLikePredicate { get; }

        static TypeUtilities()
        {
            VarArgTypes = new[] { Type.GetType("System.ArgIterator"), Type.GetType("System.RuntimeArgumentHandle"), Type.GetType("System.TypedReference") }.OfType<Type>().ToHashSet();
            IsByRefLikePredicate = typeof(Type).GetProperty("IsByRefLike")?.GetMethod?.CreateDelegate(typeof(Predicate<Type>)) as Predicate<Type> ?? (static _ => false);
            NullableAttribute = Type.GetType("System.Runtime.CompilerServices.NullableAttribute") ?? throw new InvalidInitializationException();
            NullableContextAttribute = Type.GetType("System.Runtime.CompilerServices.NullableContextAttribute") ?? throw new InvalidInitializationException();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<Type?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).Select(static type => type.Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> FullName(this IEnumerable<Type?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).Select(static type => type.FullName ?? type.Name);
        }

        public static IEnumerable<Type> HasName(this IEnumerable<Type?> source, String name)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? item in source)
            {
                if (item?.Name.Contains(name) is true)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<Type> HasName(this IEnumerable<MonoCecilType?> source, String name)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? item in source)
            {
                if (item?.Name.Contains(name) is true)
                {
                    yield return item;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeCode ToTypeCode(this Type? type)
        {
            return Type.GetTypeCode(type);
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
        public static Boolean IsVoid(this Type? type)
        {
            return type == typeof(void);
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
        public static Boolean IsCompilerGenerated(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.HasAttribute<CompilerGeneratedAttribute>();
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
        public static Int32 GetGenericArgumentsCount(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericArguments().Length : 0;
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

            return type.GetSafeInterfacesUnsafe().Contains(@interface);
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
                1 => interfaces[0] is null || type.GetSafeInterfacesUnsafe().Contains(interfaces[0]!),
                _ => EnumerableBaseUtilities.WhereNotNull(interfaces).All(type.GetSafeInterfacesUnsafe().Contains)
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
                _ => EnumerableBaseUtilities.WhereNotNull(interfaces).All(new HashSet<Type>(type.ImplementedInterfaces).Contains)
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
                _ => EnumerableBaseUtilities.WhereNotNull(interfaces).Any(new HashSet<Type>(type.GetInterfaces()).Contains)
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
                _ => EnumerableBaseUtilities.WhereNotNull(interfaces).Any(new HashSet<Type>(type.ImplementedInterfaces).Contains)
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

            if (!type.IsAbstract)
            {
                return false;
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            return type.GetMembers(binding).Any(MemberInfoUtilities.IsAbstract);
        }

        internal static Boolean Closes(this Type? type, Type? open)
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
        public static Boolean IsAnyType(this Type? value)
        {
            return value is not null && (typeof(Any) == value || typeof(Any.Value) == value);
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
        public static Type[]? TryGetGenericArguments(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericArguments() : null;
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
                FieldInfo? field = current.GetFields(binding).FirstOrDefault(static field => field.FieldType.IsMulticastDelegateFieldType() && field.FieldType.IsAssignableFrom(typeof(T)));

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
                fields.AddRange(current.GetFields(binding).Where(static field => field.FieldType.IsMulticastDelegateFieldType()));
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
                fields.AddRange(current.GetFields(binding).Where(static field => field.FieldType.IsMulticastDelegateFieldType() && field.FieldType.IsAssignableFrom(typeof(T))));
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

            List<KeyValuePair<FieldInfo, T>> result = new List<KeyValuePair<FieldInfo, T>>();

            foreach (FieldInfo field in type.GetEventFields<T>())
            {
                if (field.GetValue(@object) is not T value)
                {
                    continue;
                }

                result.Add(new KeyValuePair<FieldInfo, T>(field, value));
            }

            return result.ToArray();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> OrderBy(this IEnumerable<Type> source, TypeComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TypeComparer comparer = comparison;
            return source.OrderBy(static type => type, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> OrderByDescending(this IEnumerable<Type> source, TypeComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TypeComparer comparer = comparison;
            return source.OrderByDescending(static type => type, comparer);
        }

        private static class AssignStorage<TFrom, TTo>
        {
            public static Func<TFrom, TTo>? Simple { get; } = ExpressionUtilities.CreateAssignExpression<TFrom, TTo>(true)?.Compile();
            public static Func<TFrom, TTo>? Assign { get; } = ExpressionUtilities.CreateAssignExpression<TFrom, TTo>()?.Compile();
        }

        private static class ExpressionStorage<TSource>
        {
            public static Func<TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource>().Compile();
        }

        private static class ExpressionStorage<TSource, T>
        {
            public static Func<T, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2>
        {
            public static Func<T1, T2, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3>
        {
            public static Func<T1, T2, T3, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4>
        {
            public static Func<T1, T2, T3, T4, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5>
        {
            public static Func<T1, T2, T3, T4, T5, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6>
        {
            public static Func<T1, T2, T3, T4, T5, T6, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8>().Compile();
        }

        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>().Compile();
        }
    }
}