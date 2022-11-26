// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Anonymous;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class AnonymousTypeUtilities
    {
        private static class AnonymousType
        {
            private const String AnonymousTypeAssembly = nameof(AnonymousTypeAssembly);
            public static AnonymousTypeGenerator Generator { get; }

            static AnonymousType()
            {
                Generator = new AnonymousTypeGenerator(AnonymousTypeAssembly);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this ExpandoObject value)
        {
            return DefineAnonymousType(AnonymousType.Generator, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Type DefineAnonymousType(this AnonymousTypeGenerator generator, ExpandoObject value)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            static IEnumerable<KeyValuePair<String, Type>> Internal(ExpandoObject value)
            {
                foreach ((String name, Object? item) in value)
                {
                    if (item is null)
                    {
                        continue;
                    }

                    yield return new KeyValuePair<String, Type>(name, item.GetType());
                }
            }

            return DefineAnonymousType(generator, Internal(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static AnonymousTypePropertyInfo[] ToProperties(this IEnumerable<AnonymousTypePropertyInfo> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return properties.DistinctByThrow(property => property.Name).OrderBy(property => property.Name, StringComparer.Ordinal).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this IEnumerable<PropertyInfo> properties)
        {
            return DefineAnonymousType(AnonymousType.Generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this AnonymousTypeGenerator generator, IEnumerable<PropertyInfo> properties)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return DefineAnonymousType(generator, properties.Select(property => (AnonymousTypePropertyInfo) property).ToProperties());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this IEnumerable<KeyValuePair<String, Type>> properties)
        {
            return DefineAnonymousType(AnonymousType.Generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this AnonymousTypeGenerator generator, IEnumerable<KeyValuePair<String, Type>> properties)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return DefineAnonymousType(generator, properties.Select(property => (AnonymousTypePropertyInfo) property).ToProperties());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this AnonymousTypePropertyInfo[] properties)
        {
            return DefineAnonymousType(AnonymousType.Generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this AnonymousTypeGenerator generator, AnonymousTypePropertyInfo[] properties)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            lock (generator)
            {
                return generator.DefineType(properties);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T FillAnonymousObject<T>(this T value, IEnumerable<KeyValuePair<String, Object?>> properties) where T : class, IAnonymousObject
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Type type = value.GetType();

            foreach ((String? key, Object? item) in properties)
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                if (type.GetProperty(key, binding) is PropertyInfo property)
                {
                    property.SetValue(value, item);
                    continue;
                }

                if (type.GetField(key, binding) is FieldInfo field)
                {
                    field.SetValue(value, item);
                }

                throw new InvalidOperationException($"Can't set value '{value}' to property or field '{key}'");
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CastToAnonymousObject<T>(Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T) value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CastToAnonymousObject<T>(Object value, out T result)
        {
            result = (T) value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCastToAnonymousObject<T>(Object value, [MaybeNullWhen(false)] out T result)
        {
            try
            {
                result = (T) value;
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this IEnumerable<KeyValuePair<String, Object?>> properties)
        {
            return CreateAnonymousObject<AnonymousObject>(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateAnonymousObject<T>(this IEnumerable<KeyValuePair<String, Object?>> properties) where T : class, IAnonymousObject
        {
            return CreateAnonymousObject<T>(AnonymousType.Generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this AnonymousTypeGenerator generator, IEnumerable<KeyValuePair<String, Object?>> properties)
        {
            return CreateAnonymousObject<AnonymousObject>(generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T CreateAnonymousObject<T>(this AnonymousTypeGenerator generator, IEnumerable<KeyValuePair<String, Object?>> properties) where T : class, IAnonymousObject
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static KeyValuePair<String, (Type Type, Object? Value)> Convert(KeyValuePair<String, Object?> pair)
            {
                (String property, Object? value) = pair;

                if (String.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(property));
                }

                Type type = value?.GetType() ?? typeof(Object);
                return new KeyValuePair<String, (Type Type, Object? Value)>(property, (type, value));
            }

            return CreateAnonymousObject<T>(generator, properties.Select(Convert));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties)
        {
            return CreateAnonymousObject<AnonymousObject>(properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateAnonymousObject<T>(this IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties) where T : class, IAnonymousObject
        {
            return CreateAnonymousObject<T>(AnonymousType.Generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this AnonymousTypeGenerator generator, IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties)
        {
            return CreateAnonymousObject<AnonymousObject>(generator, properties);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T CreateAnonymousObject<T>(this AnonymousTypeGenerator generator, IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties) where T : class, IAnonymousObject
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            List<KeyValuePair<String, Type>> types = new List<KeyValuePair<String, Type>>(16);
            List<KeyValuePair<String, Object?>> values = new List<KeyValuePair<String, Object?>>(16);
            foreach ((String property, (Type type, Object? value)) in properties)
            {
                if (String.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(property));
                }

                if (type is null)
                {
                    throw new ArgumentException(nameof(type));
                }

                types.Add(new KeyValuePair<String, Type>(property, type));
                values.Add(new KeyValuePair<String, Object?>(property, value));
            }

            Type anonymous = generator.DefineAnonymousType(types);
            Object? active = Activator.CreateInstance(anonymous);

            if (active is null)
            {
                throw new InvalidOperationException($"Can't activate '{anonymous.FullName}' instance.");
            }

            if (active is not T result)
            {
                throw new InvalidOperationException($"Can't activate '{anonymous.FullName}' instance. Request: {typeof(T).FullName}. Taken: '{active.GetType().FullName}'.");
            }

            return result.FillAnonymousObject(values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this ExpandoObject value)
        {
            return CreateAnonymousObject<AnonymousObject>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateAnonymousObject<T>(this ExpandoObject value) where T : class, IAnonymousObject
        {
            return CreateAnonymousObject<T>(AnonymousType.Generator, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousObject CreateAnonymousObject(this AnonymousTypeGenerator generator, ExpandoObject value)
        {
            return CreateAnonymousObject<AnonymousObject>(generator, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T CreateAnonymousObject<T>(this AnonymousTypeGenerator generator, ExpandoObject value) where T : class, IAnonymousObject
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type anonymous = generator.DefineAnonymousType(value);
            Object? active = Activator.CreateInstance(anonymous);

            if (active is null)
            {
                throw new InvalidOperationException($"Can't activate '{anonymous.FullName}' instance.");
            }

            if (active is not T result)
            {
                throw new InvalidOperationException($"Can't activate '{anonymous.FullName}' instance. Request: {typeof(T).FullName}. Taken: '{active.GetType().FullName}'.");
            }

            return result.FillAnonymousObject(value);
        }

        private static IStore<Type, IndexDictionary<String, MemberInfo>> Store { get; } = new WeakStore<Type, IndexDictionary<String, MemberInfo>>();

        private static IndexDictionary<String, MemberInfo> Find(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            static Boolean IsAnonymous(MemberInfo info)
            {
                Type? declaring = info.DeclaringType;
                return declaring is null || declaring.HasInterface<IAnonymousObject>();
            }

            static Boolean IsProperty(PropertyInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                return !info.IsSpecialName && !info.IsIndexer() && IsAnonymous(info);
            }

            static Boolean IsField(FieldInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                return !info.IsSpecialName && IsAnonymous(info);
            }

            static IndexDictionary<String, MemberInfo> Internal(Type type)
            {
                IndexDictionary<String, MemberInfo> store = new IndexDictionary<String, MemberInfo>();

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                List<MemberInfo> members = new List<MemberInfo>(16);
                members.AddRange(type.GetProperties(binding).Where(IsProperty));
                members.AddRange(type.GetFields(binding).Where(IsField));

                store.AddRange(members.OrderBy(member => member.Name, StringComparer.Ordinal).Select(member => new KeyValuePair<String, MemberInfo>(member.Name, member)));
                return store;
            }

            return Store.GetOrAdd(type, Internal);
        }

        public static void Register(Type type, IndexDictionary<String, MemberInfo> store)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (store is null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            Store.AddOrUpdate(type, store);
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static KeyValuePair<String, MemberInfo>[] Enumerate(IAnonymousObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);

            lock (properties)
            {
                return properties.ToArray();
            }
        }

        public static IEnumerable<KeyValuePair<String, Object?>> Values(IAnonymousObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            static IEnumerable<KeyValuePair<String, Object?>> Internal(IAnonymousObject value)
            {
                foreach ((String? name, MemberInfo? member) in Enumerate(value))
                {
                    Object? item;
                    switch (member)
                    {
                        case PropertyInfo property:
                            item = property.GetValue(value);
                            break;
                        case FieldInfo field:
                            item = field.GetValue(value);
                            break;
                        default:
                            continue;
                    }

                    yield return new KeyValuePair<String, Object?>(name, item);
                }
            }

            return Internal(value).ToArray();
        }

        private static MemberInfo Member(IAnonymousObject value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);

            try
            {
                return properties.GetValueByIndex(index);
            }
            catch (Exception)
            {
                throw new MissingMemberException(type.FullName, index.ToString());
            }
        }

        private static MemberInfo Member(IAnonymousObject value, String name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type type = value.GetType();
            IndexDictionary<String, MemberInfo> properties = Store.GetOrAdd(type, Find);
            return properties.TryGetValue(name, out MemberInfo? member) ? member : throw new MissingMemberException(type.FullName, name);
        }

        public static Int32 Count(IAnonymousObject type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Store.GetOrAdd(type.GetType(), Find).Count;
        }

        public static Type Type(IAnonymousObject anonymous, Int32 index)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            return Member(anonymous, index) switch
            {
                PropertyInfo property => property.PropertyType,
                FieldInfo field => field.FieldType,
                _ => throw new MissingMemberException(anonymous.GetType().FullName, index.ToString())
            };
        }

        public static Type Type(IAnonymousObject anonymous, String name)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Member(anonymous, name) switch
            {
                PropertyInfo property => property.PropertyType,
                FieldInfo field => field.FieldType,
                _ => throw new MissingMemberException(anonymous.GetType().FullName, name)
            };
        }

        public static (Type Type, Object? Value) Get(IAnonymousObject anonymous, Int32 index)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            return Member(anonymous, index) switch
            {
                PropertyInfo property => (property.PropertyType, property.GetValue(anonymous)),
                FieldInfo field => (field.FieldType, field.GetValue(anonymous)),
                _ => throw new MissingMemberException(anonymous.GetType().FullName, index.ToString())
            };
        }

        public static (Type Type, Object? Value) Get(IAnonymousObject anonymous, String name)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Member(anonymous, name) switch
            {
                PropertyInfo property => (property.PropertyType, property.GetValue(anonymous)),
                FieldInfo field => (field.FieldType, field.GetValue(anonymous)),
                _ => throw new MissingMemberException(anonymous.GetType().FullName, name)
            };
        }

        public static void Set(IAnonymousObject anonymous, Int32 index, Object? value)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            switch (Member(anonymous, index))
            {
                case PropertyInfo property:
                    property.SetValue(anonymous, value);
                    return;
                case FieldInfo field:
                    field.SetValue(anonymous, value);
                    return;
                default:
                    throw new MissingMemberException(anonymous.GetType().FullName, index.ToString());
            }
        }

        public static void Set(IAnonymousObject anonymous, String name, Object? value)
        {
            if (anonymous is null)
            {
                throw new ArgumentNullException(nameof(anonymous));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            switch (Member(anonymous, name))
            {
                case PropertyInfo property:
                    property.SetValue(anonymous, value);
                    return;
                case FieldInfo field:
                    field.SetValue(anonymous, value);
                    return;
                default:
                    throw new MissingMemberException(anonymous.GetType().FullName, name);
            }
        }
    }
}