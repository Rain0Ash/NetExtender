// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Core
{
    public static class ReflectionUtils
    {
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
            return GetTypesInNamespace(Assembly.GetEntryAssembly(), @namespace);
        }

        public static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, String @namespace)
        {
            return assembly.GetTypes().Where(type => String.Equals(type.Namespace, @namespace, StringComparison.Ordinal)).ToArray();
        }

        /// <summary>
        /// Calls the static constructor of this type.
        /// </summary>
        /// <param name="type">The type of which to call the static constructor.</param>
        public static void CallStaticConstructor(this Type type)
        {
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
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Object obj)
        {
            return GetAllPropertiesOfType<T>(obj.GetType());
        }

        /// <summary>
        /// Returns all the public properties of this type whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="type">The type of which to get the properties.</param>
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Type type)
        {
            return type.GetProperties().Where(pi => pi.PropertyType == typeof(T));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static Object GetPropertyValue(this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type;
            if (obj is Type typeObj)
            {
                type = typeObj;
            }
            else
            {
                type = obj.GetType();
            }

            PropertyInfo property = type.GetProperty(name);
            if (property is not null)
            {
                return property.GetValue(obj);
            }

            FieldInfo field = type.GetField(name);
            if (field is not null)
            {
                return field.GetValue(obj);
            }

            throw new Exception($"'{name}' is neither a property or a field of type '{type}'.");
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

            Type type;
            if (obj is Type typeObj)
            {
                // setting static properties/fields
                type = typeObj;
            }
            else
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
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static Type GetPropertyType(this Type type, String propertyName, BindingFlags? bindingAttr = null)
        {
            Type propertyType = GetPropertyTypeReal(type, propertyName, bindingAttr);

            // get the generic type of nullable, not THE nullable
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyType = GetGenericFirst(propertyType);
            }

            return propertyType;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static Type GetPropertyTypeReal(this Type type, String propertyName, BindingFlags? bindingAttr = null)
        {
            PropertyInfo property = GetPropertyInfo(type, propertyName, bindingAttr);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="obj">Object with a type that has the specified property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo(this Object obj, String propertyName, BindingFlags? bindingAttr = null)
        {
            return GetPropertyInfo(obj.GetType(), propertyName, bindingAttr);
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo(this Type type, String propertyName, BindingFlags? bindingAttr = null)
        {
            PropertyInfo property = bindingAttr is null ? type.GetProperty(propertyName) : type.GetProperty(propertyName, bindingAttr.Value);

            if (property is null)
            {
                throw new Exception($"The provided property name ({propertyName}) does not exist in type '{type}'.");
            }

            return property;
        }

        public static Delegate GetDelegate(this MethodInfo info)
        {
            return GetDelegate<Delegate>(info);
        }

        public static T GetDelegate<T>(this MethodInfo info) where T : Delegate
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return (T) Delegate.CreateDelegate(typeof(T), info);
        }

        public static Boolean TryGetDelegate(this MethodInfo info, out Delegate result)
        {
            return TryGetDelegate<Delegate>(info, out result);
        }

        public static Boolean TryGetDelegate<T>(this MethodInfo info, out T result) where T : Delegate
        {
            if (info is null)
            {
                result = default;
                return false;
            }
            
            try
            {
                result = GetDelegate<T>(info);
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
        /// <param name="fieldName">The name of the field.</param>
        public static FieldInfo GetFieldInfo(this Object obj, String fieldName)
        {
            return GetFieldInfo(obj.GetType(), fieldName);
        }

        /// <summary>
        /// Gets the field information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified field.</param>
        /// <param name="fieldName">The name of the field.</param>
        public static FieldInfo GetFieldInfo(this Type type, String fieldName)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field is null)
            {
                throw new Exception($"The provided property name ({fieldName}) does not exist in type '{type}'.");
            }

            return field;
        }

        /// <summary>
        /// Returns the first definition of generic type of this generic type.
        /// </summary>
        /// <param name="type">The type from which to get the generic type.</param>
        public static Type GetGenericFirst(this Type type)
        {
            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        /// <param name="includeInherited">Determines whether or not to include inherited constants.</param>
        public static IEnumerable<FieldInfo> GetConstants(this Type type, Boolean includeInherited)
        {
            BindingFlags bindingFlags;
            if (includeInherited)
            {
                bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
            }
            else
            {
                bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;
            }

            FieldInfo[] fields = type.GetFields(bindingFlags);

            return fields.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static void SetProperty(this Object obj, String propertyName, Object value, BindingFlags? bindingAttr = null)
        {
            PropertyInfo property = GetPropertyInfo(obj, propertyName, bindingAttr);
            property.SetValue(obj, value);
        }

        /// <summary>
        /// Sets the specified field to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the field.</param>
        /// <param name="fieldName">The name of the field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        public static void SetField(this Object obj, String fieldName, Object value)
        {
            FieldInfo field = GetFieldInfo(obj, fieldName);
            field.SetValue(obj, value);
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="valueAsString">The string representation of the value to set to the property.</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static void SetPropertyFromString(this Object obj, String propertyName, String valueAsString, BindingFlags? bindingAttr = null)
        {
            PropertyInfo property = GetPropertyInfo(obj, propertyName, bindingAttr);
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
        public static void SetAllPropertiesOfType<T>(this Object obj, T value)
        {
            Type type = obj.GetType();

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

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
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="includeInherited">Determines whether of not to include inherited properties.</param>
        public static Boolean HasProperty(this Object obj, String propertyName, Boolean includeInherited)
        {
            Type type = obj.GetType();
            return HasProperty(type, propertyName, includeInherited);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="includeInherited">Determines whether of not to include inherited properties.</param>
        public static Boolean HasProperty(this Type type, String propertyName, Boolean includeInherited)
        {
            BindingFlags bindingAttr;
            if (includeInherited)
            {
                bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            }
            else
            {
                bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            }

            PropertyInfo propertyInfo = type.GetProperty(propertyName, bindingAttr);
            return propertyInfo is not null;
        }

        /// <summary>
        /// Determines whether this type implements the specified parent type. Returns false if both types are the same.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parentType">The type to check if it is implemented.</param>
        public static Boolean Implements(this Type type, Type parentType)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (parentType is null)
            {
                throw new ArgumentNullException(nameof(parentType));
            }

            return type != parentType && parentType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type to check derives from this generic type.
        /// </summary>
        /// <param name="generic">The parent generic type.</param>
        /// <param name="toCheck">The type to check if it derives from the specified generic type.</param>
        public static Boolean IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck is not null && toCheck != typeof(Object))
            {
                Type current = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == current)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Determines whether this type is a primitive.
        /// <para><see cref="string"/> is considered a primitive.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        public static Boolean IsPrimitive(this Type type)
        {
            if (type == typeof(String))
            {
                // string is considered as a primitive
                return true;
            }

            return type.IsValueType && type.IsPrimitive;
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        public static Object GetDefault(this Type type)
        {
            if (!type.IsValueType)
            {
                return null;
            }

            Func<Object> f = GetDefaultGeneric<Object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(type).Invoke(null, null);
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