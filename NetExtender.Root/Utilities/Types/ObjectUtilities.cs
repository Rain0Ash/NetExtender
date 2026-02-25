using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Newtonsoft.Types.Objects;
using Newtonsoft.Json;

namespace NetExtender.Utilities.Types
{
    public static class ObjectUtilities
    {
        public static Object Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return NullObject.Instance;
            }
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
    }

    [JsonConverter(typeof(NullObjectJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetExtender.Serialization.Json.Objects.NullObjectJsonConverter))]
    internal sealed class NullObject
    {
        public static NullObject Instance { get; } = new NullObject();

        private NullObject()
        {
        }

        public override Int32 GetHashCode()
        {
            return 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other is null || other is NullObject;
        }

        public override String? ToString()
        {
            return null;
        }
    }
}