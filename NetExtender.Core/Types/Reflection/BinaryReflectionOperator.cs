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
    public class BinaryReflectionOperator<TFirst, TSecond, TResult> : BinaryReflectionOperator, IBinaryReflectionOperator<TFirst, TSecond, TResult>
    {
        protected BinaryOperatorHandler<TFirst, TSecond, TResult>? Handler { get; }
        
        internal BinaryReflectionOperator(BinaryOperator @operator)
            : this(Find(typeof(TFirst), typeof(TSecond), @operator), @operator)
        {
        }
        
        [ReflectionSignature(typeof(BinaryReflectionOperator))]
        internal BinaryReflectionOperator(MethodInfo? method, BinaryOperator @operator)
            : base(method, @operator)
        {
            try
            {
                Handler = Method?.CreateDelegate<BinaryOperatorHandler<TFirst, TSecond, TResult>>();
            }
            catch (ArgumentException)
            {
                throw new ReflectionOperationException($"Can't bind method '{Method}' to '{Operator}' for '{GetType()}'.");
            }
        }
        
        public TResult Invoke(TFirst first, TSecond second)
        {
            return Handler is not null ? Handler.Invoke(first, second) : throw new InvalidOperationException();
        }
        
        public override Object? Invoke(Object? first, Object? second)
        {
            if (first is not TFirst)
            {
                first = first is not null ? (TFirst) Convert.ChangeType(first, typeof(TFirst), CultureInfo.InvariantCulture) : default!;
            }
            
            if (second is not TSecond)
            {
                second = second is not null ? (TSecond) Convert.ChangeType(second, typeof(TSecond), CultureInfo.InvariantCulture) : default!;
            }
            
            return Invoke((TFirst) first, (TSecond) second);
        }
    }
    
    public abstract class BinaryReflectionOperator : ReflectionOperator, IBinaryReflectionOperator
    {
        protected static ConcurrentDictionary<(Type, Type, BinaryOperator), IBinaryReflectionOperator?> Storage { get; } = new ConcurrentDictionary<(Type, Type, BinaryOperator), IBinaryReflectionOperator?>();
        
        public sealed override String Name
        {
            get
            {
                return Operator.Operator();
            }
        }

        public BinaryOperator Operator { get; }
        
        protected BinaryReflectionOperator(MethodInfo? method, BinaryOperator @operator)
            : base(method)
        {
            Operator = @operator;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static MethodInfo? Find(Type first, Type second, BinaryOperator @operator)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            
            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            
            String name = @operator.Operator();
            if (first.GetMethods(binding).SingleOrDefault(method => method.Name.Contains(name) && method.GetParameterTypes() is { Length: 2 } parameters && parameters[0] == first && parameters[1] == second) is { } result)
            {
                return result;
            }
            
            if (first.GetMethods(binding).Where(method => method.Name.Contains(name)).Cast<MethodBase>().ToArray() is not { Length: > 0 } methods)
            {
                return null;
            }
            
            return (MethodInfo?) Type.DefaultBinder.SelectMethod(binding, methods, new[] { first, second }, null);
        }
        
        public abstract Object? Invoke(Object? first, Object? second);
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public new static IBinaryReflectionOperator? Get(Type first, Type second, BinaryOperator @operator)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            
            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            
            return Storage.GetOrAdd((first, second, @operator), static key =>
            {
                (Type first, Type second, BinaryOperator @operator) = key;
                
                if (Find(first, second, @operator) is not { } method)
                {
                    return Find(second, first, @operator) is not null ? Get(second, first, @operator) : null;
                }
                
                if (method.GetParameterTypes() is { Length: 2 } parameters && (parameters[0] != first || parameters[1] != second))
                {
                    return Storage.GetOrAdd((parameters[0], parameters[1], @operator), static (key, method) =>
                    {
                        (Type first, Type second, BinaryOperator @operator) = key;
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                        return (IBinaryReflectionOperator?) Activator.CreateInstance(typeof(BinaryReflectionOperator<,,>).MakeGenericType(first, second, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
                    }, method);
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                return (IBinaryReflectionOperator?) Activator.CreateInstance(typeof(BinaryReflectionOperator<,,>).MakeGenericType(first, second, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IBinaryReflectionOperator<TFirst, TSecond, TResult>? Get<TFirst, TSecond, TResult>(BinaryOperator @operator)
        {
            return Get(typeof(TFirst), typeof(TSecond), @operator) as IBinaryReflectionOperator<TFirst, TSecond, TResult>;
        }
    }
}