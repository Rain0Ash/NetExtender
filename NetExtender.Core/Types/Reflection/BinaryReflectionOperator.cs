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
    public class BinaryReflectionOperator<TSelf, TOther, TResult> : BinaryReflectionOperator, IBinaryReflectionOperator<TSelf, TOther, TResult>
    {
        protected BinaryOperatorHandler<TSelf, TOther, TResult>? Handler { get; }

        internal BinaryReflectionOperator(BinaryOperator @operator)
            : this(Find(typeof(TSelf), typeof(TOther), @operator), @operator)
        {
        }

        [ReflectionSignature(typeof(BinaryReflectionOperator))]
        internal BinaryReflectionOperator(MethodInfo? method, BinaryOperator @operator)
            : base(method, @operator)
        {
            try
            {
                Handler = Method?.CreateDelegate<BinaryOperatorHandler<TSelf, TOther, TResult>>();
            }
            catch (ArgumentException)
            {
                throw new ReflectionOperationException($"Can't bind method '{Method}' to '{Operator}' for '{GetType()}'.");
            }
        }

        protected BinaryReflectionOperator(BinaryOperator @operator, BinaryOperatorHandler<TSelf, TOther, TResult>? handler)
            : base(@operator)
        {
            Handler = handler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BinaryReflectionOperator<TSelf, TOther, TResult> Exception(BinaryOperator @operator)
        {
            return new NotImplemented(@operator);
        }

        public virtual TResult Invoke(TSelf first, TOther second)
        {
            return Handler is not null ? Handler.Invoke(first, second) : throw new BinaryOperatorNotImplementedException<TSelf, TOther, TResult>(Operator);
        }

        public override Object? Invoke(Object? first, Object? second)
        {
            if (first is not TSelf)
            {
                first = first is not null ? (TSelf) Convert.ChangeType(first, typeof(TSelf), CultureInfo.InvariantCulture) : default!;
            }

            if (second is not TOther)
            {
                second = second is not null ? (TOther) Convert.ChangeType(second, typeof(TOther), CultureInfo.InvariantCulture) : default!;
            }

            return Invoke((TSelf) first, (TOther) second);
        }

        public override String ToString()
        {
            return $"{typeof(TResult).Name} {Operator.Operator()}<{typeof(TSelf).Name}, {typeof(TOther).Name}>";
        }

        internal sealed class Seal : BinaryReflectionOperator<TSelf, TOther, TResult>
        {
            public Seal(BinaryOperator @operator)
                : base(@operator)
            {
            }

            public Seal(MethodInfo? method, BinaryOperator @operator)
                : base(method, @operator)
            {
            }
        }

        private sealed class NotImplemented : BinaryReflectionOperator<TSelf, TOther, TResult>
        {
            public NotImplemented(BinaryOperator @operator)
                : base(@operator)
            {
            }

            public override Object Invoke(Object? first, Object? second)
            {
                throw new BinaryOperatorNotImplementedException<TSelf, TOther, TResult>(Operator);
            }

            public override TResult Invoke(TSelf first, TOther second)
            {
                throw new BinaryOperatorNotImplementedException<TSelf, TOther, TResult>(Operator);
            }
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

        protected BinaryReflectionOperator(BinaryOperator @operator)
        {
            Operator = @operator;
        }

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
            MethodInfo[] exists = first.GetMethods(binding);

            if (exists.SingleOrDefault(method => method.HasName(name) && method.GetParameterTypes() is { Length: 2 } parameters && parameters[0] == first && parameters[1] == second) is { } result)
            {
                return result;
            }

            if (exists.Where(name, static (name, method) => method.HasName(name)).ToArray() is { Length: > 0 } methods)
            {
                return Type.DefaultBinder.SelectMethod(binding, methods, new[] { first, second });
            }

            return null;
        }

        public abstract Object? Invoke(Object? first, Object? second);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean Get(Type first, Type second, BinaryOperator @operator, [MaybeNullWhen(false)] out IBinaryReflectionOperator result)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            result = Storage.GetOrAdd((first, second, @operator), static key =>
            {
                (Type first, Type second, BinaryOperator @operator) = key;

                if (Find(first, second, @operator) is not { } method)
                {
                    return Find(second, first, @operator) is not null && Get(second, first, @operator, out IBinaryReflectionOperator? result) ? result : null;
                }

                if (method.GetParameterTypes() is { Length: 2 } parameters && (parameters[0] != first || parameters[1] != second))
                {
                    return Storage.GetOrAdd((parameters[0], parameters[1], @operator), static (key, method) =>
                    {
                        (Type first, Type second, BinaryOperator @operator) = key;
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                        return (IBinaryReflectionOperator?) Activator.CreateInstance(typeof(BinaryReflectionOperator<,,>.Seal).MakeGenericType(first, second, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
                    }, method);
                }

                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                return (IBinaryReflectionOperator?) Activator.CreateInstance(typeof(BinaryReflectionOperator<,,>.Seal).MakeGenericType(first, second, method.ReturnType), binding, null, new Object[] { method, @operator }, null);
            });

            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public new static IBinaryReflectionOperator? Get(Type first, Type second, BinaryOperator @operator)
        {
            BinaryOperator flags = @operator & BinaryOperator.Flags;
            @operator &= ~BinaryOperator.Flags;

            return flags switch
            {
                BinaryOperator.Checked => Get(first, second, @operator | BinaryOperator.Checked, out IBinaryReflectionOperator? result) ? result : null,
                BinaryOperator.Unchecked => Get(first, second, @operator | BinaryOperator.Unchecked, out IBinaryReflectionOperator? result) ? result : null,
                BinaryOperator.PreferChecked or BinaryOperator.Flags => Get(first, second, @operator | BinaryOperator.Checked, out IBinaryReflectionOperator? result) ? result : Get(first, second, @operator | BinaryOperator.Unchecked, out result) ? result : null,
                BinaryOperator.Unknown or BinaryOperator.Prefer or BinaryOperator.PreferUnchecked => Get(first, second, @operator | BinaryOperator.Unchecked, out IBinaryReflectionOperator? result) ? result : Get(first, second, @operator | BinaryOperator.Checked, out result) ? result : null,
                _ => throw new EnumUndefinedOrNotSupportedException<BinaryOperator>(flags, nameof(@operator), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IBinaryReflectionOperator<TSelf, TSelf, TSelf>? Get<TSelf>(BinaryOperator @operator)
        {
            return Get<TSelf, TSelf, TSelf>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IBinaryReflectionOperator<TSelf, TSelf, TResult>? Get<TSelf, TResult>(BinaryOperator @operator)
        {
            return Get<TSelf, TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static IBinaryReflectionOperator<TSelf, TOther, TResult>? Get<TSelf, TOther, TResult>(BinaryOperator @operator)
        {
            return Get(typeof(TSelf), typeof(TOther), @operator) as IBinaryReflectionOperator<TSelf, TOther, TResult>;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new static IBinaryReflectionOperator<TSelf, TSelf, TSelf> Exception<TSelf>(BinaryOperator @operator)
        {
            return Exception<TSelf, TSelf, TSelf>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new static IBinaryReflectionOperator<TSelf, TSelf, TResult> Exception<TSelf, TResult>(BinaryOperator @operator)
        {
            return Exception<TSelf, TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal new static IBinaryReflectionOperator<TSelf, TOther, TResult> Exception<TSelf, TOther, TResult>(BinaryOperator @operator)
        {
            return BinaryReflectionOperator<TSelf, TOther, TResult>.Exception(@operator);
        }

        public override String ToString()
        {
            return Operator.Operator();
        }
    }

    public class BinaryOperatorNotImplementedException<TSelf, TOther, TResult> : BinaryOperatorNotImplementedException
    {
        public sealed override String Identifier
        {
            get
            {
                return Operator.ToString<TSelf, TOther, TResult>();
            }
        }

        public BinaryOperatorNotImplementedException(BinaryOperator @operator)
            : base(@operator, @operator.ToString<TSelf, TOther, TResult>())
        {
        }

        public BinaryOperatorNotImplementedException(BinaryOperator @operator, Exception? exception)
            : base(@operator, @operator.ToString<TSelf, TOther, TResult>(), exception)
        {
        }

        public BinaryOperatorNotImplementedException(BinaryOperator @operator, String? message)
            : base(@operator, @operator.ToString<TSelf, TOther, TResult>(), message)
        {
        }

        public BinaryOperatorNotImplementedException(BinaryOperator @operator, String? message, Exception? exception)
            : base(@operator, @operator.ToString<TSelf, TOther, TResult>(), message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected BinaryOperatorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public abstract class BinaryOperatorNotImplementedException : ReflectionOperatorNotImplementedException
    {
        public BinaryOperator Operator { get; }

        protected BinaryOperatorNotImplementedException(BinaryOperator @operator, String? identifier)
            : base(identifier)
        {
            Operator = @operator;
        }

        protected BinaryOperatorNotImplementedException(BinaryOperator @operator, String? identifier, Exception? exception)
            : base(identifier, exception)
        {
            Operator = @operator;
        }

        protected BinaryOperatorNotImplementedException(BinaryOperator @operator, String? identifier, String? message)
            : base(identifier, message)
        {
            Operator = @operator;
        }

        protected BinaryOperatorNotImplementedException(BinaryOperator @operator, String? identifier, String? message, Exception? exception)
            : base(identifier, message, exception)
        {
            Operator = @operator;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected BinaryOperatorNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Operator = (BinaryOperator) info.GetUInt32(nameof(Operator));
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Operator), (UInt32) Operator);
        }
    }
}