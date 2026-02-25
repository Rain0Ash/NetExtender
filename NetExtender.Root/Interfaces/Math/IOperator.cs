using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Types.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace System.Numerics
{
    [Flags]
    [ReflectionSignature]
    public enum NumericsInterfaceGroup : UInt32
    {
        Unknown = 0,
        Operator = 1,
        UnaryOperator = 2 | Operator,
        BinaryOperator = 4 | Operator,
        ComplexOperator = UnaryOperator | BinaryOperator,
        BitwiseOperator = 8 | Operator,
        Property = 16,
        MinMax = 32 | Property,
        AdditiveIdentity = 64 | Property,
        MultiplicativeIdentity = 128 | Property,
        Method = 256,
        Factory = 512 | Method,
        Math = 1024 | Method,
        Trigonometry = 2048 | Math,
        Hyperbolic = 4096 | Trigonometry,
        Number = 8192 | ComplexOperator | MinMax | AdditiveIdentity | MultiplicativeIdentity | Method | Parse | Compare,
        SignedNumber = 16384 | Number,
        UnsignedNumber = 32768 | Number,
        BinaryNumber = 65536 | Number | BitwiseOperator,
        Integer = 131072 | Number,
        BinaryInteger = Integer | BinaryNumber,
        Float = 262144 | Number,
        FloatIeee754 = 524288 | Float | Hyperbolic,
        BinaryFloatIeee754 = FloatIeee754 | BinaryNumber,
        Equality = 1048576 | BinaryOperator,
        Compare = 2097152 | BinaryOperator,
        Parse = 4194304 | Method,
        SpanParse = 8388608 | Parse,
        NumericParse = 16777216 | Parse,
        NumericSpanParse = NumericParse | SpanParse,
        Utf8Parse = 33554432 | Parse,
        Utf8NumericParse = 67108864 | Utf8Parse
    }
}

namespace NetExtender.Interfaces
{
    public interface INetExtenderUnaryOperator<TSelf> : INetExtenderOperator<TSelf>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.UnaryOperator | INetExtenderOperator<TSelf>.Group;
    }

    public interface INetExtenderBinaryOperator<TSelf> : INetExtenderOperator<TSelf>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.BinaryOperator | INetExtenderOperator<TSelf>.Group;
    }

    public interface INetExtenderPropertyOperator<TSelf> : INetExtenderOperator<TSelf>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Property | INetExtenderOperator<TSelf>.Group;
    }

    public interface INetExtenderMethodOperator<TSelf> : INetExtenderOperator<TSelf>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Method | INetExtenderOperator<TSelf>.Group;
    }

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public interface INetExtenderOperator<TSelf, TOperator, THandler, TSet> : INetExtenderOperator<TSelf, TOperator> where TOperator : class, INetExtenderOperator<TSelf, TOperator, THandler, TSet> where THandler : INetExtenderOperator.OperatorHandler<TSelf, TOperator, TSet>, new() where TSet : INetExtenderOperator.OperatorHandler<TSelf, TOperator, TSet>.OperatorSet
    {
        public new const NumericsInterfaceGroup Group = INetExtenderOperator<TSelf>.Group;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static Func<THandler> invoke = static () =>
        {
            THandler result = new THandler();

            if (!result.Initialize(out AggregateException? exception))
            {
                throw exception;
            }

            lock (SyncRoot)
            {
                ensure = null;
                invoke = () => result;
                return result;
            }
        };

        private static Func<ExceptionStorage?, THandler>? ensure = static storage =>
        {
            THandler result = new THandler();

            if (!result.Initialize(storage))
            {
                return result;
            }

            lock (SyncRoot)
            {
                ensure = null;
                invoke = () => result;
                return result;
            }
        };

        public static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return SafeHandler.IsSupported;
            }
        }

        protected internal static THandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return invoke.Invoke();
            }
        }

        protected internal static THandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ensure?.Invoke(null) ?? Handler;
            }
        }

        protected internal static TSet Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Handler.Storage;
            }
        }

        internal static ExceptionStorage? Ensure()
        {
            Handler.Initialize(out ExceptionStorage result);
            return result.Count > 0 ? result : null;
        }
    }

    public interface INetExtenderOperator<TSelf, TOperator> : INetExtenderOperator<TSelf> where TOperator : class, INetExtenderOperator<TSelf, TOperator>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderOperator<TSelf>.Group;
    }

    public interface INetExtenderOperator<TSelf> : INetExtenderOperator
    {
        public new const NumericsInterfaceGroup Group = INetExtenderOperator.Group;

        protected static THandler Initialize<TOperator, THandler, TSet>() where TOperator : class, INetExtenderOperator<TSelf, TOperator, THandler, TSet> where THandler : OperatorHandler<TSelf, TOperator, TSet>, new() where TSet : OperatorHandler<TSelf, TOperator, TSet>.OperatorSet
        {
            return INetExtenderOperator<TSelf, TOperator, THandler, TSet>.Handler;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IUnaryReflectionOperator<TSelf, TResult>? Initialize<TResult>(UnaryOperator @operator)
        {
            return Initialize<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IBinaryReflectionOperator<TSelf, TOther, TResult>? Initialize<TOther, TResult>(BinaryOperator @operator)
        {
            return Initialize<TSelf, TOther, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static UnaryOperatorNotImplementedException<TSelf, TResult> Exception<TResult>(UnaryOperator @operator)
        {
            return Exception<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static BinaryOperatorNotImplementedException<TSelf, TOther, TResult> Exception<TOther, TResult>(BinaryOperator @operator)
        {
            return Exception<TSelf, TOther, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IUnaryReflectionOperator<TSelf, TResult> NotImplemented<TResult>(UnaryOperator @operator)
        {
            return NotImplemented<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IBinaryReflectionOperator<TSelf, TOther, TResult> NotImplemented<TOther, TResult>(BinaryOperator @operator)
        {
            return NotImplemented<TSelf, TOther, TResult>(@operator);
        }

        protected class NoHandler : OperatorHandler<TSelf>
        {
            public sealed override String Name
            {
                get
                {
                    return nameof(NoHandler);
                }
            }

            public sealed override Int32 Depth
            {
                get
                {
                    return Int32.MinValue;
                }
                protected set
                {
                }
            }

            public sealed override AggregateException Initialize()
            {
                throw new NotSupportedException();
            }

            internal sealed override Boolean Initialize(ExceptionStorage storage)
            {
                throw new NotSupportedException();
            }
        }
    }

    public interface INetExtenderOperator
    {
        public const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Unknown;
        protected static SyncRoot SyncRoot { get; } = SyncRoot.Create();

        public abstract class OperatorHandler<TSelf, TOperator, TSet> : OperatorHandler<TSelf, TOperator> where TOperator : class, INetExtenderOperator<TSelf, TOperator> where TSet : OperatorHandler<TSelf, TOperator, TSet>.OperatorSet
        {
            public sealed override Int32 Depth { get; protected set; } = -1;

            // ReSharper disable once FieldCanBeMadeReadOnly.Local
            private Func<OperatorHandler<TSelf, TOperator, TSet>, TSet> _invoke = static handler =>
            {
                if (handler.Initialize(null, out TSet? container) is not { } exception)
                {
                    handler._invoke = _ => container!;
                    handler.Success();
                    return container!;
                }

                handler._invoke = static handler => throw handler.Initialize(null, out _)!;
                handler.Fail();
                throw exception;
            };

            public TSet Storage
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _invoke.Invoke(this);
                }
            }

            public sealed override String Name
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return typeof(TOperator).Name;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected virtual TSet New()
            {
                return TypeUtilities.New<TSet>().Invoke();
            }

            public sealed override AggregateException? Initialize()
            {
                try
                {
                    _invoke.Invoke(this);
                    return null;
                }
                catch (AggregateException exception)
                {
                    return exception;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private AggregateException? Initialize(ExceptionStorage? storage, out TSet? set)
            {
                if (storage is not null && storage.ContainsKey(this))
                {
                    set = null;
                    return storage;
                }

                Int32 depth = -1;

                storage ??= new ExceptionStorage();
                ImmutableList<Exception>.Builder exceptions = ImmutableList.CreateBuilder<Exception>();
                foreach (Result<OperatorHandler> result in Initialize(set = New()))
                {
                    if (result.Unwrap(out OperatorHandler? handler))
                    {
                        if (!storage.ContainsKey(handler))
                        {
                            handler.Initialize(storage);
                        }

                        depth = Math.Max(depth, handler.Depth);
                    }

                    if (result.Exception is { } exception)
                    {
                        exceptions.Add(exception);
                    }
                }

                storage[this] = exceptions.ToImmutable();
                Depth = ++depth;
                return storage;
            }

            protected abstract IEnumerable<Result<OperatorHandler>> Initialize(TSet set);

            internal sealed override Boolean Initialize(ExceptionStorage? storage)
            {
                return Initialize(storage, out _) is null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected ref T Set<T>(in T handler)
            {
                return ref Unsafe.AsRef(in handler);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [return: NotNullIfNotNull("value")]
            protected T? Set<T>(in T handler, T? value)
            {
                return value is not null ? Set(in handler) = value : value;
            }

            public class OperatorSet
            {
            }
        }

        public abstract class OperatorHandler<TSelf, TOperator> : OperatorHandler<TSelf> where TOperator : class, INetExtenderOperator<TSelf, TOperator>
        {
        }

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public abstract class OperatorHandler<TSelf> : OperatorHandler
        {
            public sealed override Type Type
            {
                get
                {
                    return typeof(TSelf);
                }
            }

            private static readonly WeakReference<PropertyInfo[]> _properties = new WeakReference<PropertyInfo[]>(null!);
            public sealed override PropertyInfo[] Properties
            {
                get
                {
                    if (_properties.TryGetTarget(out PropertyInfo[]? result))
                    {
                        return result;
                    }

                    result = typeof(TSelf).GetProperties(Binding);
                    _properties.SetTarget(result);
                    return result;
                }
            }

            private static readonly WeakReference<MethodInfo[]> _methods = new WeakReference<MethodInfo[]>(null!);
            public sealed override MethodInfo[] Methods
            {
                get
                {
                    if (_methods.TryGetTarget(out MethodInfo[]? result))
                    {
                        return result;
                    }

                    result = typeof(TSelf).GetMethods(Binding);
                    _methods.SetTarget(result);
                    return result;
                }
            }

            private static readonly WeakReference<MethodInfo[]> _generics = new WeakReference<MethodInfo[]>(null!);
            public sealed override MethodInfo[] Generics
            {
                get
                {
                    if (_generics.TryGetTarget(out MethodInfo[]? result))
                    {
                        return result;
                    }

                    result = Methods.Where(static method => method.GetGenericArgumentsCount() == 1).ToArray();
                    _generics.SetTarget(result);
                    return result;
                }
            }

            public sealed override BindingFlags Binding
            {
                get
                {
                    return BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
                }
            }

            public sealed override Binder Binder
            {
                get
                {
                    return Type.DefaultBinder;
                }
            }
        }

        public abstract class OperatorHandler : IComparable<OperatorHandler>
        {
            public abstract String Name { get; }
            public abstract Int32 Depth { get; protected set; }

            private Boolean? _state;
            public Boolean IsSupported
            {
                get
                {
                    return _state ??= Initialize() is null;
                }
            }

            public abstract Type Type { get; }
            public abstract PropertyInfo[] Properties { get; }
            public abstract MethodInfo[] Methods { get; }
            public abstract MethodInfo[] Generics { get; }

            public abstract Binder Binder { get; }
            public abstract BindingFlags Binding { get; }

            public abstract AggregateException? Initialize();

            public Boolean Initialize([MaybeNullWhen(true)] out AggregateException exception)
            {
                return (exception = Initialize()) is null;
            }

            internal abstract Boolean Initialize(ExceptionStorage storage);

            internal Boolean Initialize(out ExceptionStorage storage)
            {
                return Initialize(storage = new ExceptionStorage());
            }

            protected void Success()
            {
                _state = true;
            }

            protected void Fail()
            {
                _state = false;
            }

            public Int32 CompareTo(OperatorHandler? other)
            {
                if (ReferenceEquals(this, other))
                {
                    return 0;
                }

                if (other is null)
                {
                    return 1;
                }

                Int32 compare = Depth.CompareTo(other.Depth);
                return compare != 0 ? compare : String.Compare(Name, other.Name, StringComparison.Ordinal);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IUnaryReflectionOperator<TSelf, TResult>? Initialize<TSelf, TResult>(UnaryOperator @operator)
        {
            return ReflectionOperator.Get<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IBinaryReflectionOperator<TSelf, TOther, TResult>? Initialize<TSelf, TOther, TResult>(BinaryOperator @operator)
        {
            return ReflectionOperator.Get<TSelf, TOther, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static UnaryOperatorNotImplementedException<TSelf, TResult> Exception<TSelf, TResult>(UnaryOperator @operator)
        {
            return new UnaryOperatorNotImplementedException<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static BinaryOperatorNotImplementedException<TSelf, TOther, TResult> Exception<TSelf, TOther, TResult>(BinaryOperator @operator)
        {
            return new BinaryOperatorNotImplementedException<TSelf, TOther, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IUnaryReflectionOperator<TSelf, TResult> NotImplemented<TSelf, TResult>(UnaryOperator @operator)
        {
            return ReflectionOperator.Exception<TSelf, TResult>(@operator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IBinaryReflectionOperator<TSelf, TOther, TResult> NotImplemented<TSelf, TOther, TResult>(BinaryOperator @operator)
        {
            return ReflectionOperator.Exception<TSelf, TOther, TResult>(@operator);
        }

        protected static Func<TResult>? Initialize<TResult>(OperatorHandler handler, String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (EnumerableBaseUtilities.Where(handler.Properties, name, static (name, property) => property.HasName(name) && property.GetMethod is not null).ToArray() is not { Length: > 0 } properties)
            {
                return null;
            }

            Boolean state = true;
            @new:

            try
            {
                return handler.Binder.SelectProperty(handler.Binding, properties, typeof(TResult))?.GetMethod!.CreateDelegate<Func<TResult>>(null);
            }
            catch (AmbiguousMatchException) when (state)
            {
                properties = EnumerableBaseUtilities.Where(properties, name, static (name, method) => method.Name == name).ToArray();
                state = false;
                goto @new;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Exception Exception<TResult>(OperatorHandler handler, String name)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new NotImplementedReflectionException($"Property '{typeof(TResult)} {name}' is not implemented in type {handler.Type.FullName}.");
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static MethodInfo? Initialize(OperatorHandler handler, String name, Type[] parameters)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyStringException(name, nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (EnumerableBaseUtilities.Where(handler.Generics, name, static (name, method) => method.HasName(name)).ToArray() is not { Length: > 0 } methods)
            {
                return null;
            }

            Boolean state = true;
            @new:

            try
            {
                return handler.Binder.SelectMethod(handler.Binding, methods, parameters);
            }
            catch (AmbiguousMatchException) when (state)
            {
                methods = EnumerableBaseUtilities.Where(methods, name, static (name, method) => method.Name == name).ToArray();
                state = false;
                goto @new;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static TDelegate? Initialize<TDelegate>(OperatorHandler handler, TDelegate @delegate) where TDelegate : Delegate
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (@delegate is not { Method: { IsStatic: true } method })
            {
                throw new ArgumentException("Delegate method must be static.", nameof(@delegate));
            }

            if (EnumerableBaseUtilities.Where(handler.Methods, method.Name, static (name, method) => method.HasName(name)).ToArray() is not { Length: > 0 } methods)
            {
                return null;
            }

            Boolean state = true;
            @new:

            try
            {
                return handler.Binder.SelectMethod(handler.Binding, methods, method.GetParameterTypes())?.CreateDelegate<TDelegate>(null);
            }
            catch (AmbiguousMatchException) when (state)
            {
                methods = EnumerableBaseUtilities.Where(methods, method.Name, static (name, method) => method.Name == name).ToArray();
                state = false;
                goto @new;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Exception Exception<TDelegate>(OperatorHandler handler, TDelegate @delegate) where TDelegate : Delegate
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new NotImplementedReflectionException($"Method '{@delegate.Method}' is not implemented in type {handler.Type.FullName}.");
        }

        protected internal sealed class ExceptionStorage : SortedDictionary<OperatorHandler, ImmutableList<Exception>>
        {
            public static implicit operator AggregateException?(ExceptionStorage? value)
            {
                return Exception(value?.Values.SelectMany(static source => source));
            }

            public static ExceptionStorage operator |(ExceptionStorage? storage, (OperatorHandler Handler, ImmutableList<Exception> Exceptions) value)
            {
                storage ??= new ExceptionStorage();
                storage[value.Handler] = value.Exceptions;
                return storage;
            }

            [return: NotNullIfNotNull("first")]
            [return: NotNullIfNotNull("second")]
            public static ExceptionStorage? operator |(ExceptionStorage? first, ExceptionStorage? second)
            {
                if (first is null)
                {
                    return second;
                }

                if (second is null)
                {
                    return first;
                }

                foreach ((OperatorHandler handler, ImmutableList<Exception> exceptions) in second)
                {
                    first.TryAdd(handler, exceptions);
                }

                return first;
            }

            internal static AggregateException? Exception(IEnumerable<Exception>? source)
            {
                Exception[]? result = source?.ToArray();
                return result?.Length > 0 ? ExceptionUtilities.FastAggregate(result) : null;
            }
        }
    }
}