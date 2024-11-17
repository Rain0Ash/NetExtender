using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Reflection;

namespace NetExtender.Utilities.Core
{
    public enum ReflectionEqualityType : Byte
    {
        NotEquals,
        NameNotEquals,
        TypeNotEquals,
        AccessNotEquals,
        AttributeNotEquals,
        CallingConventionNotEquals,
        SignatureNotEquals,
        ReturnTypeNotEquals,
        AccessorNotEquals,
        Equals
    }

    [Flags]
    public enum ConstructorDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        Access = 2,
        Attribute = 4 | Access,
        CallingConvention = 8,
        Strict = Name | Access | CallingConvention,
        NotStrict = Name | CallingConvention,
        All = Name | Attribute | CallingConvention
    }

    [Flags]
    public enum FieldDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        Access = 2,
        InitOnly = 4,
        Attribute = 8 | Access | InitOnly,
        Strict = Name | Access | InitOnly,
        NotStrict = Name | InitOnly,
        All = Name | Attribute
    }

    [Flags]
    public enum PropertyDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        Access = 2,
        Attribute = 4 | Access,
        Accessor = 8 | Access,
        Strict = Name | Access,
        NotStrict = Name,
        All = Name | Attribute
    }

    [Flags]
    public enum MethodDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        Access = 2,
        Attribute = 4 | Access,
        CallingConvention = 8,
        Strict = Name | Access | CallingConvention,
        NotStrict = Name | CallingConvention,
        All = Name | Attribute | CallingConvention
    }

    [Flags]
    public enum EventDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        Access = 2,
        Multicast = 4,
        Attribute = 8 | Access | Multicast,
        Strict = Name | Attribute,
        NotStrict = Name | Multicast,
        All = Name | Attribute
    }

    [Flags]
    public enum ParameterDifferenceStrictMode : Byte
    {
        None = 0,
        Name = 1,
        In = 2,
        Out = 4,
        Retval = 8,
        Attribute = 16 | In | Out | Retval,
        Optional = 32,
        DefaultValue = 64,
        DefaultValueEquals = 128 | DefaultValue,
        Strict = Name | Attribute | DefaultValueEquals,
        NotStrict = Name | In | Out,
        All = Name | Attribute | Optional | DefaultValueEquals
    }
    
    public static partial class ReflectionUtilities
    {
        // ReSharper disable once CognitiveComplexity
        internal static T? Find<T>(this Type? type, T[] source, T member) where T : MemberInfo
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            
            List<T> members = new List<T>(4);
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T info in source)
            {
                if (!Equality(info, member, true))
                {
                    continue;
                }
                
                members.Add(info);
            }

            switch (members.Count)
            {
                case 0:
                {
                    return null;
                }
                case 1:
                {
                    return members[0];
                }
                default:
                {
                    do
                    {
                        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
                        foreach (T info in members)
                        {
                            if (info.DeclaringType == type)
                            {
                                return info;
                            }
                        }

                        type = type?.BaseType;
                    } while (type is not null);
            
                    return null;
                }
            }
        }

        public static MemberInfo? Find(this Type type, MemberInfo member)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member switch
            {
                ConstructorInfo constructor => Find(type, constructor),
                MethodInfo method => Find(type, method),
                PropertyInfo property => Find(type, property),
                FieldInfo field => Find(type, field),
                EventInfo @event => Find(type, @event),
                _ => throw new NotSupportedException($"Type '{member.GetType()}' not supported.")
            };
        }

        public static FieldInfo? Find(this Type type, FieldInfo field)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            return Find(type, type.GetFields(binding), field);
        }

        public static PropertyInfo? Find(this Type type, PropertyInfo property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            return Find(type, type.GetProperties(binding), property);
        }

        public static MethodInfo? Find(this Type type, MethodInfo method)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            return Find(type, type.GetMethods(binding), method);
        }

        public static ConstructorInfo? Find(this Type type, ConstructorInfo constructor)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            return Find(type, type.GetConstructors(binding), constructor);
        }

        public static EventInfo? Find(this Type type, EventInfo @event)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            return Find(type, type.GetEvents(binding), @event);
        }

        private static MethodDifferenceStrictMode ToMethodDifferenceStrictMode(this PropertyDifferenceStrictMode strict)
        {
            MethodDifferenceStrictMode result = MethodDifferenceStrictMode.None;

            if (strict.HasFlag(PropertyDifferenceStrictMode.Name))
            {
                result |= MethodDifferenceStrictMode.Name;
            }

            if (strict.HasFlag(PropertyDifferenceStrictMode.Access))
            {
                result |= MethodDifferenceStrictMode.Access;
            }

            if (strict.HasFlag(PropertyDifferenceStrictMode.Attribute))
            {
                result |= MethodDifferenceStrictMode.Access;
            }

            return result;
        }

        private static MethodDifferenceStrictMode ToMethodDifferenceStrictMode(this EventDifferenceStrictMode strict)
        {
            MethodDifferenceStrictMode result = MethodDifferenceStrictMode.None;

            if (strict.HasFlag(EventDifferenceStrictMode.Name))
            {
                result |= MethodDifferenceStrictMode.Name;
            }

            if (strict.HasFlag(EventDifferenceStrictMode.Access))
            {
                result |= MethodDifferenceStrictMode.Access;
            }

            if (strict.HasFlag(EventDifferenceStrictMode.Attribute))
            {
                result |= MethodDifferenceStrictMode.Access;
            }

            return result;
        }

        private static Boolean Equality<T>(T source, T other) where T : MemberInfo
        {
            return Equality(source, other, false);
        }

        private static Boolean Equality<T>(T source, T other, Boolean strict) where T : MemberInfo
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (typeof(T) == typeof(ConstructorInfo))
            {
                return Equality((ConstructorInfo) (Object) source, (ConstructorInfo) (Object) other, strict);
            }

            if (typeof(T) == typeof(MethodInfo))
            {
                return Equality((MethodInfo) (Object) source, (MethodInfo) (Object) other, strict);
            }

            if (typeof(T) == typeof(FieldInfo))
            {
                return Equality((FieldInfo) (Object) source, (FieldInfo) (Object) other, strict);
            }

            if (typeof(T) == typeof(PropertyInfo))
            {
                return Equality((PropertyInfo) (Object) source, (PropertyInfo) (Object) other, strict);
            }

            if (typeof(T) == typeof(EventInfo))
            {
                return Equality((EventInfo) (Object) source, (EventInfo) (Object) other, strict);
            }
            
            return Equality((MemberInfo) source, other);
        }

        public static Boolean Equality(this MethodBody source, MethodBody other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return true;
            }

            Byte[]? il1 = source.GetILAsByteArray();
            Byte[]? il2 = other.GetILAsByteArray();

            return il1 == il2 || il1 is not null && il2 is not null && il1.SequenceEqual(il2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ConstructorInfo source, ConstructorInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ConstructorInfo source, ConstructorInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ConstructorInfo source, ConstructorInfo other, ConstructorDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<ConstructorInfo> Difference(this ConstructorInfo source, ConstructorInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<ConstructorInfo> Difference(this ConstructorInfo source, ConstructorInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? ConstructorDifferenceStrictMode.Strict : ConstructorDifferenceStrictMode.NotStrict);
        }

        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<ConstructorInfo> Difference(this ConstructorInfo source, ConstructorInfo other, ConstructorDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (source.DeclaringType != other.DeclaringType)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.TypeNotEquals);
            }

            if (strict.HasFlag(ConstructorDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }
            
            if (strict.HasFlag(ConstructorDifferenceStrictMode.CallingConvention) && source.CallingConvention != other.CallingConvention)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.CallingConventionNotEquals);
            }

            if (strict.HasFlag(ConstructorDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(ConstructorDifferenceStrictMode.Access) && source.Attributes.Access() != other.Attributes.Access())
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.AccessNotEquals);
            }

            ParameterInfo[]? first = source.GetSafeParameters();
            ParameterInfo[]? second = other.GetSafeParameters();

            if (first is null || second is null)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReferenceEquals(first, second) ? ReflectionEqualityType.Equals : ReflectionEqualityType.SignatureNotEquals);
            }

            if (first.Length != second.Length)
            {
                return new ReflectionDifferenceItem<ConstructorInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            static ReflectionDifferenceItem<ParameterInfo> Check((ParameterInfo First, ParameterInfo Second) value)
            {
                return Difference(value.First, value.Second);
            }

            Boolean equals = first.Zip(second).Select(Check).All(difference => difference.Equality == ReflectionEqualityType.Equals);
            return new ReflectionDifferenceItem<ConstructorInfo>(source, other, equals ? ReflectionEqualityType.Equals : ReflectionEqualityType.SignatureNotEquals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this FieldInfo source, FieldInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this FieldInfo source, FieldInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this FieldInfo source, FieldInfo other, FieldDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<FieldInfo> Difference(this FieldInfo source, FieldInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<FieldInfo> Difference(this FieldInfo source, FieldInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? FieldDifferenceStrictMode.Strict : FieldDifferenceStrictMode.NotStrict);
        }

        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<FieldInfo> Difference(this FieldInfo source, FieldInfo other, FieldDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (strict.HasFlag(FieldDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }

            if (source.FieldType != other.FieldType)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (source.IsStatic != other.IsStatic)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(FieldDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(FieldDifferenceStrictMode.Access) && source.Attributes.Access() != other.Attributes.Access())
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.AccessNotEquals);
            }

            if (strict.HasFlag(FieldDifferenceStrictMode.InitOnly) && source.IsInitOnly != other.IsInitOnly)
            {
                return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            return new ReflectionDifferenceItem<FieldInfo>(source, other, ReflectionEqualityType.Equals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this PropertyInfo source, PropertyInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this PropertyInfo source, PropertyInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this PropertyInfo source, PropertyInfo other, PropertyDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<PropertyInfo> Difference(this PropertyInfo source, PropertyInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<PropertyInfo> Difference(this PropertyInfo source, PropertyInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? PropertyDifferenceStrictMode.Strict : PropertyDifferenceStrictMode.NotStrict);
        }

        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<PropertyInfo> Difference(this PropertyInfo source, PropertyInfo other, PropertyDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (strict.HasFlag(PropertyDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }

            if (source.PropertyType != other.PropertyType)
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(PropertyDifferenceStrictMode.Accessor) && (source.CanRead != other.CanRead || source.CanWrite != other.CanWrite))
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.AccessorNotEquals);
            }

            if (strict.HasFlag(PropertyDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (!strict.HasFlag(PropertyDifferenceStrictMode.Access))
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.Equals);
            }

            MethodDifferenceStrictMode difference = strict.ToMethodDifferenceStrictMode();

            if (source.GetMethod is { } fget && other.GetMethod is { } sget && !Equality(fget, sget, difference))
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.AccessorNotEquals);
            }

            if (source.SetMethod is { } fset && other.SetMethod is { } sset && !Equality(fset, sset, difference))
            {
                return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.AccessorNotEquals);
            }

            return new ReflectionDifferenceItem<PropertyInfo>(source, other, ReflectionEqualityType.Equals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this MethodInfo source, MethodInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this MethodInfo source, MethodInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this MethodInfo source, MethodInfo other, MethodDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<MethodInfo> Difference(this MethodInfo source, MethodInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<MethodInfo> Difference(this MethodInfo source, MethodInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? MethodDifferenceStrictMode.Strict : MethodDifferenceStrictMode.NotStrict);
        }

        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<MethodInfo> Difference(this MethodInfo source, MethodInfo other, MethodDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (strict.HasFlag(MethodDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }

            if (source.ReturnType != other.ReturnType)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.ReturnTypeNotEquals);
            }

            if (strict.HasFlag(MethodDifferenceStrictMode.CallingConvention) && source.CallingConvention != other.CallingConvention)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.CallingConventionNotEquals);
            }

            if (strict.HasFlag(MethodDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(MethodDifferenceStrictMode.Access) && source.Attributes.Access() != other.Attributes.Access())
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.AccessNotEquals);
            }

            ParameterInfo[]? first = source.GetSafeParameters();
            ParameterInfo[]? second = other.GetSafeParameters();

            if (first is null || second is null)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReferenceEquals(first, second) ? ReflectionEqualityType.Equals : ReflectionEqualityType.SignatureNotEquals);
            }

            if (first.Length != second.Length)
            {
                return new ReflectionDifferenceItem<MethodInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            static ReflectionDifferenceItem<ParameterInfo> Check((ParameterInfo First, ParameterInfo Second) value)
            {
                return Difference(value.First, value.Second);
            }

            Boolean equals = first.Zip(second).Select(Check).All(static difference => difference.Equality == ReflectionEqualityType.Equals);
            return new ReflectionDifferenceItem<MethodInfo>(source, other, equals ? ReflectionEqualityType.Equals : ReflectionEqualityType.SignatureNotEquals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this EventInfo source, EventInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this EventInfo source, EventInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this EventInfo source, EventInfo other, EventDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<EventInfo> Difference(this EventInfo source, EventInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<EventInfo> Difference(this EventInfo source, EventInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? EventDifferenceStrictMode.Strict : EventDifferenceStrictMode.NotStrict);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<EventInfo> Difference(this EventInfo source, EventInfo other, EventDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (strict.HasFlag(EventDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }

            if (source.EventHandlerType != other.EventHandlerType)
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(EventDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(EventDifferenceStrictMode.Multicast) && source.IsMulticast != other.IsMulticast)
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }
            
            MethodDifferenceStrictMode difference = strict.ToMethodDifferenceStrictMode();
            
            if (source.AddMethod is { } fadd && other.AddMethod is { } sadd && !Equality(fadd, sadd, difference))
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.AccessorNotEquals);
            }
            
            if (source.RemoveMethod is { } fremove && other.RemoveMethod is { } sremove && !Equality(fremove, sremove, difference))
            {
                return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.AccessorNotEquals);
            }

            return new ReflectionDifferenceItem<EventInfo>(source, other, ReflectionEqualityType.Equals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ParameterInfo source, ParameterInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ParameterInfo source, ParameterInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equality(this ParameterInfo source, ParameterInfo other, ParameterDifferenceStrictMode strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<ParameterInfo> Difference(this ParameterInfo source, ParameterInfo other)
        {
            return Difference(source, other, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReflectionDifferenceItem<ParameterInfo> Difference(this ParameterInfo source, ParameterInfo other, Boolean strict)
        {
            return Difference(source, other, strict ? ParameterDifferenceStrictMode.Strict : ParameterDifferenceStrictMode.NotStrict);
        }

        // ReSharper disable once CognitiveComplexity
        public static ReflectionDifferenceItem<ParameterInfo> Difference(this ParameterInfo source, ParameterInfo other, ParameterDifferenceStrictMode strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.Equals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.Name) && source.Name != other.Name)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.NameNotEquals);
            }

            if (source.Position != other.Position)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (source.ParameterType != other.ParameterType)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.Attribute) && source.Attributes != other.Attributes)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.AttributeNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.In) && source.IsIn != other.IsIn)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.Out) && source.IsOut != other.IsOut)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.Retval) && source.IsRetval != other.IsRetval)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.Optional) && source.IsOptional != other.IsOptional)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.DefaultValue) && source.HasDefaultValue != other.HasDefaultValue)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            if (strict.HasFlag(ParameterDifferenceStrictMode.DefaultValueEquals) && source.DefaultValue != other.DefaultValue)
            {
                return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.SignatureNotEquals);
            }

            return new ReflectionDifferenceItem<ParameterInfo>(source, other, ReflectionEqualityType.Equals);
        }

        public static Boolean Equality(this MemberInfo source, MemberInfo other)
        {
            return Difference(source, other) == ReflectionEqualityType.Equals;
        }

        public static Boolean Equality(this MemberInfo source, MemberInfo other, Boolean strict)
        {
            return Difference(source, other, strict) == ReflectionEqualityType.Equals;
        }

        public static ReflectionDifferenceItem<MemberInfo> Difference(this MemberInfo source, MemberInfo other)
        {
            return Difference(source, other, false);
        }

        public static ReflectionDifferenceItem<MemberInfo> Difference(this MemberInfo source, MemberInfo other, Boolean strict)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (source == other)
            {
                return new ReflectionDifferenceItem<MemberInfo>(source, other, ReflectionEqualityType.Equals);
            }

            /*if (source.GetType() != other.GetType())
            {
                throw new ArgumentException($"Type of source argument '{source.GetType()}' is not equals type of other argument '{other.GetType()}'", nameof(other));
            }*/

            switch (source)
            {
                case ConstructorInfo first when other is ConstructorInfo second:
                {
                    ReflectionDifferenceItem<ConstructorInfo> difference = Difference(first, second, strict);
                    return new ReflectionDifferenceItem<MemberInfo>(difference.Current, difference.Other, difference.Equality);
                }
                case FieldInfo first when other is FieldInfo second:
                {
                    ReflectionDifferenceItem<FieldInfo> difference = Difference(first, second, strict);
                    return new ReflectionDifferenceItem<MemberInfo>(difference.Current, difference.Other, difference.Equality);
                }
                case PropertyInfo first when other is PropertyInfo second:
                {
                    ReflectionDifferenceItem<PropertyInfo> difference = Difference(first, second, strict);
                    return new ReflectionDifferenceItem<MemberInfo>(difference.Current, difference.Other, difference.Equality);
                }
                case MethodInfo first when other is MethodInfo second:
                {
                    ReflectionDifferenceItem<MethodInfo> difference = Difference(first, second, strict);
                    return new ReflectionDifferenceItem<MemberInfo>(difference.Current, difference.Other, difference.Equality);
                }
                case EventInfo first when other is EventInfo second:
                {
                    ReflectionDifferenceItem<EventInfo> difference = Difference(first, second, strict);
                    return new ReflectionDifferenceItem<MemberInfo>(difference.Current, difference.Other, difference.Equality);
                }
                default:
                {
                    return new ReflectionDifferenceItem<MemberInfo>(source, other, ReflectionEqualityType.TypeNotEquals);
                }
            }
        }
    }
}