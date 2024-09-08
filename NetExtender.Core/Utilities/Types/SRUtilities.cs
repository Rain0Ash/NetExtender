using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    internal static class SRUtilities
    {
        private static ConcurrentDictionary<Assembly, Type> SR { get; } = new ConcurrentDictionary<Assembly, Type>();
        private static ConcurrentDictionary<PropertyInfo, Func<String>> SRExpression { get; } = new ConcurrentDictionary<PropertyInfo, Func<String>>();
        
        public static Type SRType(ReflectionSystemResourceAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            
            return SRType(attribute.Assembly);
        }
        
        public static Type SRType(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return SR.GetOrAdd(assembly, static assembly => assembly.GetType(nameof(System) + "." + nameof(SR), true) ?? throw new NotFoundException($"Type '{nameof(System)}.SR' not found."));
        }
        
        public static Type SRType(Assembly assembly, String @namespace)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (@namespace is null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }
            
            return assembly.GetType(@namespace + "." + nameof(SR), true) ?? throw new NotFoundException($"Type '{@namespace}.SR' not found.");
        }
        
        public static Func<String> Get(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            return SRExpression.GetOrAdd(property, static property => Expression(property).Compile());
        }
        
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static Func<String> Get(Type SR, String name)
        {
            if (SR is null)
            {
                throw new ArgumentNullException(nameof(SR));
            }
            
            return Get(Property(SR, name));
        }
        
        public static Boolean TryGet(PropertyInfo property, [MaybeNullWhen(false)] out Func<String> result)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            try
            {
                result = SRExpression.GetOrAdd(property, static property => Expression(property).Compile());
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static Boolean TryGet(Type SR, String name, [MaybeNullWhen(false)] out Func<String> result)
        {
            if (SR is null)
            {
                throw new ArgumentNullException(nameof(SR));
            }
            
            try
            {
                return TryGet(Property(SR, name), out result);
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Func<String> Get(Assembly assembly, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            return Get(SRType(assembly), name);
        }
        
        public static Func<String> Get(Assembly assembly, String @namespace, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            return Get(SRType(assembly, @namespace), name);
        }
        
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static PropertyInfo Property(Type SR, String name)
        {
            if (SR is null)
            {
                throw new ArgumentNullException(nameof(SR));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            if (SR.Name != "SR")
            {
                throw new ArgumentException($"Type '{SR}' is not {nameof(SRUtilities.SR)}.");
            }
            
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo property = SR.GetProperty(name, binding) ?? throw new MissingMemberException($"Property '{name}' not found for type '{SR}'.");
            
            if (property.PropertyType != typeof(String))
            {
                throw new NotSupportedException($"Property '{property}' must be of type '{typeof(String)}'.");
            }
            
            if (!property.CanRead)
            {
                throw new MissingMethodException($"Method 'get_{property.Name}' is missing in property '{property}'.");
            }
            
            return property;
        }
        
        public static PropertyInfo Property(Assembly assembly, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            return Property(SRType(assembly), name);
        }
        
        public static PropertyInfo Property(Assembly assembly, String @namespace, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            return Property(SRType(assembly, @namespace), name);
        }
        
        public static Expression<Func<String>> Expression(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            if (property.PropertyType != typeof(String))
            {
                throw new NotSupportedException($"Property '{property}' must be of type '{typeof(String)}'.");
            }
            
            if (!property.CanRead)
            {
                throw new MissingMethodException($"Method 'get_{property.Name}' is missing in property '{property}'.");
            }
            
            MemberExpression expression = System.Linq.Expressions.Expression.Property(null, property);
            return System.Linq.Expressions.Expression.Lambda<Func<String>>(System.Linq.Expressions.Expression.Convert(expression, typeof(String)));
        }
        
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        public static Expression<Func<String>> Expression(Type SR, String name)
        {
            if (SR is null)
            {
                throw new ArgumentNullException(nameof(SR));
            }
            
            return Expression(Property(SR, name));
        }
        
        public static Expression<Func<String>> Expression(Assembly assembly, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return Expression(SRType(assembly), name);
        }
        
        public static Expression<Func<String>> Expression(Assembly assembly, String @namespace, String name)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(@namespace))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(@namespace));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return Expression(SRType(assembly, @namespace), name);
        }
    }
}