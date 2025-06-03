// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Types.Attributes.Interfaces;

namespace NetExtender.Utilities.Core
{
    public abstract class DynamicInvokeAttribute : DelegateAttribute, IInvokeAttribute
    {
        protected DynamicInvokeAttribute(Type type, String name)
            : base(type ?? throw new ArgumentNullException(nameof(type)), name ?? throw new ArgumentNullException(nameof(name)))
        {
        }
        
        protected DynamicInvokeAttribute(Type type, String name, Int32 order)
            : base(type ?? throw new ArgumentNullException(nameof(type)), name ?? throw new ArgumentNullException(nameof(name)), order)
        {
        }

        public Boolean Invoke()
        {
            return Invoke(null, null);
        }
        public Boolean Invoke(Object? value)
        {
            return Invoke(null, value);
        }
        
        public virtual Boolean Invoke(Object? sender, Object? value)
        {
            if (Type is null || Name is null)
            {
                return false;
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

            if (Type.GetMethods(binding) is not { Length: > 0 } methods)
            {
                return false;
            }
            
            if (value is not null && Type.DefaultBinder.SelectMethod(binding, methods, new []{ value.GetType() }) is { } method)
            {
                return Invoke(method, sender, new[] { value });
            }

            method = Type.GetMethod(Name, binding, Type.EmptyTypes);
            return method is not null && Invoke(method, sender, Array.Empty<Object>());
        }

        protected virtual Boolean Invoke(MethodInfo method, Object? sender, Object?[]? arguments)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return (method.IsStatic || sender is not null) && method.Invoke(method.IsStatic ? null : sender, arguments) switch
            {
                null => method.ReturnType.IsVoid(),
                Boolean result => result,
                _ => true
            };
        }
    }
}