// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public class UnaryReflectionOperator<TSelf, TResult> : UnaryReflectionOperator, IUnaryReflectionOperator<TSelf, TResult>
    {
        protected UnaryOperatorHandler<TSelf, TResult>? Handler { get; }
        
        internal UnaryReflectionOperator(UnaryOperator @operator)
            : this(Find(typeof(TSelf), @operator), @operator)
        {
        }
        
        [ReflectionSignature(typeof(UnaryReflectionOperator))]
        internal UnaryReflectionOperator(MethodInfo? method, UnaryOperator @operator)
            : base(method, @operator)
        {
            try
            {
                Handler = Method?.CreateDelegate<UnaryOperatorHandler<TSelf, TResult>>();
            }
            catch (ArgumentException)
            {
                throw new ReflectionOperationException($"Can't bind method '{Method}' to '{Operator}' for '{GetType()}'.");
            }
        }

        protected UnaryReflectionOperator(UnaryOperator @operator, UnaryOperatorHandler<TSelf, TResult>? handler)
            : base(@operator)
        {
            Handler = handler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UnaryReflectionOperator<TSelf, TResult> Exception(UnaryOperator @operator)
        {
            return new NotImplemented(@operator);
        }
        
        public virtual TResult Invoke(TSelf value)
        {
            return Handler is not null ? Handler.Invoke(value) : throw new UnaryOperatorNotImplementedException<TSelf, TResult>(Operator);
        }
        
        public override Object? Invoke(Object? value)
        {
            if (value is not TSelf)
            {
                value = value is not null ? (TSelf?) Convert.ChangeType(value, typeof(TSelf), CultureInfo.InvariantCulture)! : default!;
            }
            
            return Invoke((TSelf) value);
        }

        public override String ToString()
        {
            return Operator.ToString<TSelf, TResult>();
        }

        internal sealed class Seal : UnaryReflectionOperator<TSelf, TResult>
        {
            public Seal(UnaryOperator @operator)
                : base(@operator)
            {
            }

            public Seal(MethodInfo? method, UnaryOperator @operator)
                : base(method, @operator)
            {
            }
        }

        private sealed class NotImplemented : UnaryReflectionOperator<TSelf, TResult>
        {
            public NotImplemented(UnaryOperator @operator)
                : base(@operator)
            {
            }

            public override Object Invoke(Object? value)
            {
                throw new UnaryOperatorNotImplementedException<TSelf, TResult>(Operator);
            }

            public override TResult Invoke(TSelf value)
            {
                throw new UnaryOperatorNotImplementedException<TSelf, TResult>(Operator);
            }
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

        protected UnaryReflectionOperator(UnaryOperator @operator)
        {
            Operator = @operator;
        }

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
            MethodInfo[] exists = type.GetMethods(binding);

            if (exists.SingleOrDefault(method => method.HasName(name) && method.GetParameterTypes() is { Length: 1 } parameters && parameters[0] == type) is { } result)
            {
                return result;
            }

            if (exists.Where(name, static (name, method) => method.HasName(name)).ToArray() is { Length: > 0 } methods)
            {
                return Type.DefaultBinder.SelectMethod(binding, methods, new[] { type });
            }

            return null;
        }

        public abstract Object? Invoke(Object? value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean Get(Type type, UnaryOperator @operator, [MaybeNullWhen(false)] out IUnaryReflectionOperator result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            result = Storage.GetOrAdd((type, @operator), static key =>
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
                        return (IUnaryReflectionOperator?) Activator.CreateInstance(typeof(UnaryReflectionOperator<,>.Seal).MakeGenericType(type, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
                    }, method);
                }

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                return (IUnaryReflectionOperator?) Activator.CreateInstance(typeof(UnaryReflectionOperator<,>.Seal).MakeGenericType(type, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
            });

            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public new static IUnaryReflectionOperator? Get(Type type, UnaryOperator @operator)
        {
            UnaryOperator flags = @operator & UnaryOperator.Flags;
            @operator &= ~UnaryOperator.Flags;

            return flags switch
            {
                UnaryOperator.Checked => Get(type, @operator | UnaryOperator.Checked, out IUnaryReflectionOperator? result) ? result : null,
                UnaryOperator.Unchecked => Get(type, @operator | UnaryOperator.Unchecked, out IUnaryReflectionOperator? result) ? result : null,
                UnaryOperator.PreferChecked or UnaryOperator.Flags => Get(type, @operator | UnaryOperator.Checked, out IUnaryReflectionOperator? result) ? result : Get(type, @operator | UnaryOperator.Unchecked, out result) ? result : null,
                UnaryOperator.Unknown or UnaryOperator.Prefer or UnaryOperator.PreferUnchecked => Get(type, @operator | UnaryOperator.Unchecked, out IUnaryReflectionOperator? result) ? result : Get(type, @operator | UnaryOperator.Checked, out result) ? result : null,
                _ => throw new EnumUndefinedOrNotSupportedException<UnaryOperator>(flags, nameof(@operator), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IUnaryReflectionOperator<TSelf, TSelf>? Get<TSelf>(UnaryOperator @operator)
        {
            return Get<TSelf, TSelf>(@operator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IUnaryReflectionOperator<TSelf, TResult>? Get<TSelf, TResult>(UnaryOperator @operator)
        {
            return (IUnaryReflectionOperator<TSelf, TResult>?) Get(typeof(TSelf), @operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new static IUnaryReflectionOperator<TSelf, TSelf> Exception<TSelf>(UnaryOperator @operator)
        {
            return Exception<TSelf, TSelf>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new static IUnaryReflectionOperator<TSelf, TResult> Exception<TSelf, TResult>(UnaryOperator @operator)
        {
            return UnaryReflectionOperator<TSelf, TResult>.Exception(@operator);
        }

        public override String ToString()
        {
            return Operator.Operator();
        }
    }

    public class UnaryOperatorNotImplementedException<TSelf, TResult> : UnaryOperatorNotImplementedException
    {
        public sealed override String Identifier
        {
            get
            {
                return Operator.ToString<TSelf, TResult>();
            }
        }

        public UnaryOperatorNotImplementedException(UnaryOperator @operator)
            : base(@operator, @operator.ToString<TSelf, TResult>())
        {
        }

        public UnaryOperatorNotImplementedException(UnaryOperator @operator, Exception? exception)
            : base(@operator, @operator.ToString<TSelf, TResult>(), exception)
        {
        }

        public UnaryOperatorNotImplementedException(UnaryOperator @operator, String? message)
            : base(@operator, @operator.ToString<TSelf, TResult>(), message)
        {
        }

        public UnaryOperatorNotImplementedException(UnaryOperator @operator, String? message, Exception? exception)
            : base(@operator, @operator.ToString<TSelf, TResult>(), message, exception)
        {
        }

        protected UnaryOperatorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    public abstract class UnaryOperatorNotImplementedException : ReflectionOperatorNotImplementedException
    {
        public UnaryOperator Operator { get; }

        protected UnaryOperatorNotImplementedException(UnaryOperator @operator, String? identifier)
            : base(identifier)
        {
            Operator = @operator;
        }

        protected UnaryOperatorNotImplementedException(UnaryOperator @operator, String? identifier, Exception? exception)
            : base(identifier, exception)
        {
            Operator = @operator;
        }

        protected UnaryOperatorNotImplementedException(UnaryOperator @operator, String? identifier, String? message)
            : base(identifier, message)
        {
            Operator = @operator;
        }

        protected UnaryOperatorNotImplementedException(UnaryOperator @operator, String? identifier, String? message, Exception? exception)
            : base(identifier, message, exception)
        {
            Operator = @operator;
        }

        protected UnaryOperatorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Operator = (UnaryOperator) info.GetUInt32(nameof(Operator));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Operator), (UInt32) Operator);
        }
    }
}