// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Anonymous;
using NetExtender.Types.Anonymous.Interfaces;

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
            
            static IEnumerable<KeyValuePair<String, Type>> Enumerate(ExpandoObject value)
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

            return DefineAnonymousType(generator, Enumerate(value));
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
    }
}