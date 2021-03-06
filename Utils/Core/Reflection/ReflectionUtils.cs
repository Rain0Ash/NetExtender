// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Core
{
    [Flags]
    public enum PrimitiveType
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
    
    public static class ReflectionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type TryGetGenericTypeDefinition([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetGenericTypeDefinitionInterfaces([NotNull] this Type type)
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
        public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element.GetCustomAttribute(typeof(T)) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetCustomAttribute<T>(this MemberInfo element, Boolean inherit) where T : Attribute
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element.GetCustomAttribute(typeof(T), inherit) as T;
        }

        /// <summary>
        /// Gets all the namespaces in this assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static IEnumerable<String> GetNamespaces(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetTypes().Select(t => t.Namespace);
        }

        public static IEnumerable<Type> GetTypesInNamespace(String @namespace)
        {
            return GetTypesInNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), @namespace);
        }

        public static IEnumerable<Type> GetTypesInNamespace([NotNull] this Assembly assembly, [NotNull] String @namespace)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetTypes().Where(type => String.Equals(type.Namespace, @namespace, StringComparison.Ordinal)).ToArray();
        }
        
        public static Boolean IsJITOptimized([NotNull] this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (Object attribute in assembly.GetCustomAttributes(typeof(DebuggableAttribute), false))
            {
                if (attribute is DebuggableAttribute attr)
                {
                    return !attr.IsJITOptimizerDisabled;
                }
            }

            return true;
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
        public static String GetStackInfo(Int32 levels = 2)
        {
            if (levels < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(levels));
            }

            StackFrame frame = new StackFrame(levels, true);
            MethodBase method = frame.GetMethod() ?? throw new InvalidOperationException();

            return $"{Path.GetFileName(frame.GetFileName())}::{method.DeclaringType?.FullName ?? String.Empty}.{method.Name} - Line {frame.GetFileLineNumber()}";
        }

        /// <summary>
        /// Calls the static constructor of this type.
        /// </summary>
        /// <param name="type">The type of which to call the static constructor.</param>
        public static void CallStaticConstructor([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        /// <summary>
        /// Gets the method that called the current method where this method is used. This does not work when used in async methods.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetCallingMethod()
        {
            return new StackFrame(2, false)?.GetMethod();
        }

        /// <summary>
        /// Gets the type that called the current method where this method is used.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Type GetCallingType()
        {
            return new StackFrame(2, false)?.GetMethod()?.DeclaringType;
        }

        /// <summary>
        /// Returns all the public properties of this object whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="obj">The object.</param>
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>([NotNull] this Object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetAllPropertiesOfType<T>(obj.GetType());
        }

        /// <summary>
        /// Returns all the public properties of this type whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="type">The type of which to get the properties.</param>
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties().Where(pi => pi.PropertyType == typeof(T));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static Object GetPropertyValue([NotNull] this Object obj, [NotNull] String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(obj, name, out Object result))
            {
                return result;
            }
            
            throw new Exception($"'{name}' is neither a property or a field of type '{result}'.");
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Object if successful else <see cref="obj"/> <see cref="Type"/></param>
        public static Boolean GetPropertyValue([NotNull] this Object obj, [NotNull] String name, out Object result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (obj is not Type type)
            {
                type = obj.GetType();
            }

            PropertyInfo property = type.GetProperty(name);
            if (property is not null)
            {
                result = property.GetValue(obj);
                return true;
            }

            FieldInfo field = type.GetField(name);
            if (field is not null)
            {
                result = field.GetValue(obj);
                return true;
            }

            result = type;
            return false;
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static T GetPropertyValue<T>([NotNull] this Object obj, [NotNull] String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Object value = GetPropertyValue(obj, name);
            return value switch
            {
                null => default,
                T result => result,
                _ => value.TryConvert(out T result) ? result : throw new InvalidCastException()
            };
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        public static T GetPropertyValue<T>([NotNull] this Object obj, [NotNull] String name, [NotNull] ParseHandler<Object, T> converter)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter(GetPropertyValue(obj, name));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>([NotNull] this Object obj, [NotNull] String name, out T result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(obj, name, out Object value))
            {
                if (value is T resval)
                {
                    result = resval;
                    return true;
                }
                
                try
                {
                    return value.TryConvert(out result);
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
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>([NotNull] this Object obj, [NotNull] String name, [NotNull] TryParseHandler<Object, T> converter, out T result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            
            if (GetPropertyValue(obj, name, out Object value) && converter(value, out result))
            {
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Sets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The objector type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="value">The value to set.</param>
        public static void SetValue(this Object obj, String name, Object value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            
            if (obj is not Type type)
            {
                type = obj.GetType();
            }

            PropertyInfo property = type.GetProperty(name);
            if (property is not null)
            {
                property.SetValue(obj, value);
                return;
            }

            FieldInfo field = type.GetField(name);

            if (field is null)
            {
                throw new Exception($"'{name}' is neither a property or a field of type '{type}'.");
            }

            field.SetValue(obj, value);
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
        public static Type GetPropertyType([NotNull] this Type type, String name, BindingFlags? binding = null)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type property = GetPropertyTypeReal(type, name, binding);

            // get the generic type of nullable, not THE nullable
            if (property.IsGenericType && property.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                property = GetGenericFirst(property);
            }

            return property;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static Type GetPropertyTypeReal([NotNull] this Type type, String name, BindingFlags? binding = null)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo property = GetPropertyInfo(type, name, binding);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="obj">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo([NotNull] this Object obj, String name, BindingFlags? binding = null)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetPropertyInfo(obj.GetType(), name, binding);
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo([NotNull] this Type type, String name, BindingFlags? binding = null)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo property = binding is null ? type.GetProperty(name) : type.GetProperty(name, binding.Value);

            if (property is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return property;
        }

        public static Boolean TryCreateDelegate(this MethodInfo info, out Delegate result)
        {
            return TryCreateDelegate<Delegate>(info, out result);
        }

        public static Boolean TryCreateDelegate<T>(this MethodInfo info, out T result) where T : Delegate
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
        /// <param name="obj">Object with a type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        public static FieldInfo GetFieldInfo([NotNull] this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetFieldInfo(obj.GetType(), name);
        }

        /// <summary>
        /// Gets the field information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        public static FieldInfo GetFieldInfo([NotNull] this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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
        public static Type GetGenericFirst([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        public static IEnumerable<FieldInfo> GetConstants([NotNull] this Type type)
        {
            return GetConstants(type, true);
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        /// <param name="inherited">Determines whether or not to include inherited constants.</param>
        public static IEnumerable<FieldInfo> GetConstants([NotNull] this Type type, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Static | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);

            return type.GetFields(binding).Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static void SetProperty([NotNull] this Object obj, String name, Object value, BindingFlags? binding = null)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name, binding);
            property.SetValue(obj, value);
        }

        /// <summary>
        /// Sets the specified field to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the field.</param>
        /// <param name="name">The name of the field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        public static void SetField([NotNull] this Object obj, String name, Object value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FieldInfo field = GetFieldInfo(obj, name);
            field.SetValue(obj, value);
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="valueAsString">The string representation of the value to set to the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static void SetPropertyFromString([NotNull] this Object obj, String name, String valueAsString, BindingFlags? binding = null)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name, binding);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object value = converter.ConvertFromString(valueAsString);
            property.SetValue(obj, value);
        }

        /// <summary>
        /// Sets all fields and properties of the specified type in the provided object to the specified value.
        /// <para>Internal, protected and private fields are included, static are not.</para>
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value to set the properties to.</param>
        public static void SetAllPropertiesOfType<T>([NotNull] this Object obj, T value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(T))
                {
                    field.SetValue(obj, value);
                }
            }
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name of the property.</param>
        public static Boolean HasProperty([NotNull] this Object obj, String name)
        {
            return HasProperty(obj, name, true);
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        public static Boolean HasProperty([NotNull] this Object obj, String name, Boolean inherited)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();
            return HasProperty(type, name, inherited);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        public static Boolean HasProperty([NotNull] this Type type, [NotNull] String name)
        {
            return HasProperty(type, name, true);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        public static Boolean HasProperty([NotNull] this Type type, [NotNull] String name, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Instance | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);

            PropertyInfo property = type.GetProperty(name, binding);
            return property is not null;
        }

        /// <summary>
        /// Determines whether this type implements the specified parent type. Returns false if both types are the same.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parent">The type to check if it is implemented.</param>
        public static Boolean Implements([NotNull] this Type type, [NotNull] Type parent)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return type != parent && parent.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type to check derives from this generic type.
        /// </summary>
        /// <param name="generic">The parent generic type.</param>
        /// <param name="check">The type to check if it derives from the specified generic type.</param>
        public static Boolean IsSubclassOfRawGeneric([NotNull] Type generic, Type check)
        {
            if (generic is null)
            {
                throw new ArgumentNullException(nameof(generic));
            }

            while (check is not null && check != typeof(Object))
            {
                Type current = check.IsGenericType ? check.GetGenericTypeDefinition() : check;
                if (generic == current)
                {
                    return true;
                }

                check = check.BaseType;
            }

            return false;
        }

        /// <inheritdoc cref="IsPrimitive(System.Type,PrimitiveType)"/>
        public static Boolean IsPrimitive([NotNull] this Type type)
        {
            return IsPrimitive(type, PrimitiveType.All);
        }

        /// <summary>
        /// Determines whether this type is a primitive.
        /// <para><see cref="string"/> is considered a primitive.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="primitive">What type is primitive</param>
        public static Boolean IsPrimitive([NotNull] this Type type, PrimitiveType primitive)
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

        private static class SizeCache<T> where T : struct
        {
            // ReSharper disable once StaticMemberInGenericType
            public static Int32 Size { get; }

            static SizeCache()
            {
                Size = GetSize(typeof(T));
            }
        }

        private static class SizeCache
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

                DynamicMethod method = new DynamicMethod("SizeOfType", typeof(Int32), Array.Empty<Type>());
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
        // ReSharper disable once UnusedParameter.Global
        public static Int32 GetSize<T>(this T _) where T : struct
        {
            return GetSize<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>([NotNull] this T[] array) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return SizeCache<T>.Size * array.Length;
        }
        
        public static Boolean GetSize<T>([NotNull] this T[] array, out Int64 size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeCache<T>.Size * array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }
        
        public static Boolean GetSize<T>([NotNull] this T[] array, out BigInteger size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeCache<T>.Size * (BigInteger) array.LongLength;
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
            return SizeCache<T>.Size;
        }

        public static Int32 GetSize([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                throw new ArgumentException(@"Is not value type", nameof(type));
            }

            return SizeCache.GetTypeSize(type);
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        public static Object GetDefault([NotNull] this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                return null;
            }

            Func<Object> func = GetDefaultGeneric<Object>;
            return func.Method.GetGenericMethodDefinition().MakeGenericMethod(type).Invoke(null, null);
        }

        /// <summary>
        /// Gets the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type for which to get the default value.</typeparam>
        public static T GetDefaultGeneric<T>()
        {
            return default;
        }
    }
}