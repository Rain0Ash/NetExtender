using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Entities;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumberFactory<TSelf> : INetExtenderMethodOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, INetExtenderNumberFactory<TSelf>.OperatorHandler, INetExtenderNumberFactory<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Factory | INetExtenderMethodOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumberFactory<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf CreateChecked<T>(T value)
        {
            return Container<T>.CreateChecked(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf CreateSaturating<T>(T value)
        {
            return Container<T>.CreateSaturating(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf CreateTruncating<T>(T value)
        {
            return Container<T>.CreateTruncating(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertFromChecked<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Container<T>.TryConvertFromChecked(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertFromSaturating<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Container<T>.TryConvertFromSaturating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertFromTruncating<T>(T value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Container<T>.TryConvertFromTruncating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertToChecked<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return Container<T>.TryConvertToChecked(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertToSaturating<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return Container<T>.TryConvertToSaturating(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static Boolean TryConvertToTruncating<T>(TSelf value, [MaybeNullWhen(false)] out T result)
        {
            return Container<T>.TryConvertToTruncating(value, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumberFactory<TSelf>, INetExtenderNumberFactory<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly MethodInfo? CreateChecked = null;
                internal readonly MethodInfo? CreateSaturating = null;
                internal readonly MethodInfo? CreateTruncating = null;
                internal readonly MethodInfo TryConvertFromChecked = null!;
                internal readonly MethodInfo TryConvertFromSaturating = null!;
                internal readonly MethodInfo TryConvertFromTruncating = null!;
                internal readonly MethodInfo TryConvertToChecked = null!;
                internal readonly MethodInfo TryConvertToSaturating = null!;
                internal readonly MethodInfo TryConvertToTruncating = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                {
                    Type[] parameters = { Type.MakeGenericMethodParameter(0) };

                    Set(in set.CreateChecked) = Method(this, nameof(CreateChecked), parameters);
                    Set(in set.CreateSaturating) = Method(this, nameof(CreateSaturating), parameters);
                    Set(in set.CreateTruncating) = Method(this, nameof(CreateTruncating), parameters);
                }

                {
                    Type[] parameters = { Type.MakeGenericMethodParameter(0), typeof(TSelf).MakeByRefType() };

                    if (Set(in set.TryConvertFromChecked, Method(this, nameof(TryConvertFromChecked), parameters)) is null)
                    {
                        yield return Exception<TryConverter<Any.Value, TSelf>>(this, TryConvertFromChecked);
                    }

                    if (Set(in set.TryConvertFromSaturating, Method(this, nameof(TryConvertFromSaturating), parameters)) is null)
                    {
                        yield return Exception<TryConverter<Any.Value, TSelf>>(this, TryConvertFromSaturating);
                    }

                    if (Set(in set.TryConvertFromTruncating, Method(this, nameof(TryConvertFromTruncating), parameters)) is null)
                    {
                        yield return Exception<TryConverter<Any.Value, TSelf>>(this, TryConvertFromTruncating);
                    }
                }

                {
                    Type[] parameters = { typeof(TSelf), Type.MakeGenericMethodParameter(0).MakeByRefType() };

                    if (Set(in set.TryConvertToChecked, Method(this, nameof(TryConvertToChecked), parameters)) is null)
                    {
                        yield return Exception<TryConverter<TSelf, Any.Value>>(this, TryConvertToChecked);
                    }

                    if (Set(in set.TryConvertToSaturating, Method(this, nameof(TryConvertToSaturating), parameters)) is null)
                    {
                        yield return Exception<TryConverter<TSelf, Any.Value>>(this, TryConvertToSaturating);
                    }

                    if (Set(in set.TryConvertToTruncating, Method(this, nameof(TryConvertToTruncating), parameters)) is null)
                    {
                        yield return Exception<TryConverter<TSelf, Any.Value>>(this, TryConvertToTruncating);
                    }
                }
            }
        }

        private static class Container<T>
        {
            public static readonly Func<T, TSelf> CreateChecked;
            public static readonly Func<T, TSelf> CreateSaturating;
            public static readonly Func<T, TSelf> CreateTruncating;
            public static readonly TryConverter<T, TSelf> TryConvertFromChecked;
            public static readonly TryConverter<T, TSelf> TryConvertFromSaturating;
            public static readonly TryConverter<T, TSelf> TryConvertFromTruncating;
            public static readonly TryConverter<TSelf, T> TryConvertToChecked;
            public static readonly TryConverter<TSelf, T> TryConvertToSaturating;
            public static readonly TryConverter<TSelf, T> TryConvertToTruncating;

            static Container()
            {
                OperatorHandler.Set storage = Storage;
                CreateChecked = storage.CreateChecked?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<T, TSelf>>(null) ?? Implementation.CreateChecked;
                CreateSaturating = storage.CreateSaturating?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<T, TSelf>>(null) ?? Implementation.CreateSaturating;
                CreateTruncating = storage.CreateTruncating?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<T, TSelf>>(null) ?? Implementation.CreateTruncating;
                TryConvertFromChecked = storage.TryConvertFromChecked.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<T, TSelf>>(null);
                TryConvertFromSaturating = storage.TryConvertFromSaturating.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<T, TSelf>>(null);
                TryConvertFromTruncating = storage.TryConvertFromTruncating.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<T, TSelf>>(null);
                TryConvertToChecked = storage.TryConvertToChecked.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<TSelf, T>>(null);
                TryConvertToSaturating = storage.TryConvertToSaturating.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<TSelf, T>>(null);
                TryConvertToTruncating = storage.TryConvertToTruncating.MakeGenericMethod(typeof(T)).CreateDelegate<TryConverter<TSelf, T>>(null);
            }

            private static class Implementation
            {
                [ReflectionSignature]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static TSelf CreateChecked(T value)
                {
                    if (typeof(T) == typeof(TSelf))
                    {
                        return Unsafe.As<T, TSelf>(ref value);
                    }

                    return TryConvertFromChecked(value, out TSelf? result) || INetExtenderNumberFactory<T>.TryConvertToChecked<TSelf>(value, out result) ? result : throw new NotSupportedException();
                }

                [ReflectionSignature]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static TSelf CreateSaturating(T value)
                {
                    if (typeof(T) == typeof(TSelf))
                    {
                        return Unsafe.As<T, TSelf>(ref value);
                    }

                    return TryConvertFromSaturating(value, out TSelf? result) || INetExtenderNumberFactory<T>.TryConvertToSaturating<TSelf>(value, out result) ? result : throw new NotSupportedException();
                }

                [ReflectionSignature]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static TSelf CreateTruncating(T value)
                {
                    if (typeof(T) == typeof(TSelf))
                    {
                        return Unsafe.As<T, TSelf>(ref value);
                    }

                    return TryConvertFromTruncating(value, out TSelf? result) || INetExtenderNumberFactory<T>.TryConvertToTruncating<TSelf>(value, out result) ? result : throw new NotSupportedException();
                }
            }
        }
    }
}