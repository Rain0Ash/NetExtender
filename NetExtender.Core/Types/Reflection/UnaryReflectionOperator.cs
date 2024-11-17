using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public class UnaryReflectionOperator<T, TResult> : UnaryReflectionOperator, IUnaryReflectionOperator<T, TResult>
    {
        protected UnaryOperatorHandler<T, TResult>? Handler { get; }
        
        internal UnaryReflectionOperator(UnaryOperator @operator)
            : this(Find(typeof(T), @operator), @operator)
        {
        }
        
        [ReflectionSignature(typeof(UnaryReflectionOperator))]
        internal UnaryReflectionOperator(MethodInfo? method, UnaryOperator @operator)
            : base(method, @operator)
        {
            try
            {
                Handler = Method?.CreateDelegate<UnaryOperatorHandler<T, TResult>>();
            }
            catch (ArgumentException)
            {
                throw new ReflectionOperationException($"Can't bind method '{Method}' to '{Operator}' for '{GetType()}'.");
            }
        }
        
        public TResult Invoke(T value)
        {
            return Handler is not null ? Handler.Invoke(value) : throw new InvalidOperationException();
        }
        
        public override Object? Invoke(Object? value)
        {
            if (value is not T)
            {
                value = value is not null ? (T?) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)! : default!;
            }
            
            return Invoke((T) value);
        }
    }
    
    public abstract class UnaryReflectionOperator : ReflectionOperator, IUnaryReflectionOperator
    {
        protected static ConcurrentDictionary<(Type, UnaryOperator), IUnaryReflectionOperator?> Storage { get; } = new ConcurrentDictionary<(Type, UnaryOperator), IUnaryReflectionOperator?>();
        
        public sealed override String Name
        {
            get
            {
                return Operator.Operator();
            }
        }

        public UnaryOperator Operator { get; }
        
        protected UnaryReflectionOperator(MethodInfo? method, UnaryOperator @operator)
            : base(method)
        {
            Operator = @operator;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static MethodInfo? Find(Type type, UnaryOperator @operator)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            
            String name = @operator.Operator();
            if (type.GetMethods(binding).SingleOrDefault(method => method.Name.Contains(name) && method.GetParameterTypes() is { Length: 1 } parameters && parameters[0] == type) is { } result)
            {
                return result;
            }

            if (type.GetMethods(binding).Where(method => method.Name.Contains(name)).ToArray() is { Length: > 0 } methods)
            {
                return Type.DefaultBinder.SelectMethod(binding, methods, new[] { type });
            }

            return null;
        }
        
        public abstract Object? Invoke(Object? value);
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public new static IUnaryReflectionOperator? Get(Type type, UnaryOperator @operator)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Storage.GetOrAdd((type, @operator), static key =>
            {
                (Type type, UnaryOperator @operator) = key;
                
                if (Find(type, @operator) is not { } method)
                {
                    return null;
                }
                
                if (method.GetParameterTypes() is { Length: 1 } parameters && parameters[0] != type)
                {
                    return Storage.GetOrAdd((parameters[0], @operator), static (key, method) =>
                    {
                        (Type type, UnaryOperator @operator) = key;
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                        return (IUnaryReflectionOperator?) Activator.CreateInstance(typeof(UnaryReflectionOperator<,>).MakeGenericType(type, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
                    }, method);
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                return (IUnaryReflectionOperator?) Activator.CreateInstance(typeof(UnaryReflectionOperator<,>).MakeGenericType(type, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IUnaryReflectionOperator<T, TResult>? Get<T, TResult>(UnaryOperator @operator)
        {
            return (IUnaryReflectionOperator<T, TResult>?) Get(typeof(T), @operator);
        }
    }
}