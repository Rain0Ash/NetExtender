using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritModulusOperators<TSelf> : INetExtenderModulusOperators<TSelf>, IInheritModulusOperators<TSelf, TSelf>
#if NET7_0_OR_GREATER
        where TSelf : IInheritModulusOperators<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderModulusOperators<TSelf>.Group | IInheritModulusOperators<TSelf, TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderModulusOperators<TSelf>.Operator | IInheritModulusOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritModulusOperators<TSelf, TResult> : INetExtenderModulusOperators<TSelf, TResult>, IInheritModulusOperators<TSelf, TSelf, TResult>
#if NET7_0_OR_GREATER
        where TSelf : IInheritModulusOperators<TSelf, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderModulusOperators<TSelf, TResult>.Group | IInheritModulusOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderModulusOperators<TSelf, TResult>.Operator | IInheritModulusOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf, TResult>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TResult>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TResult>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritModulusOperators<TSelf, TOther, TResult> : INetExtenderModulusOperators<TSelf, TOther, TResult>
#if NET7_0_OR_GREATER
        , IModulusOperators<TSelf, TOther, TResult> where TSelf : IInheritModulusOperators<TSelf, TOther, TResult>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderModulusOperators<TSelf, TOther, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderModulusOperators<TSelf, TOther, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Modulus(TSelf first, TOther second)
        {
            return INetExtenderModulusOperators<TSelf, TOther, TResult>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return INetExtenderModulusOperators<TSelf, TOther, TResult>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return INetExtenderModulusOperators<TSelf, TOther, TResult>.Unchecked.Modulus(first, second);
            }
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderModulusOperators<TSelf> : INetExtenderModulusOperators<TSelf, TSelf>,
        INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderModulusOperators<TSelf, TSelf>.Group | INetExtenderBinaryOperator<TSelf>.Group;
        public new const BinaryOperator Operator = INetExtenderModulusOperators<TSelf, TSelf>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf, TSelf>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TSelf>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TSelf Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TSelf>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderModulusOperators<TSelf>, INetExtenderModulusOperators<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderModulusOperators<TSelf, TSelf>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderModulusOperators<TSelf, TResult> : INetExtenderModulusOperators<TSelf, TSelf, TResult>,
        INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderModulusOperators<TSelf, TSelf, TResult>.Group;
        public new const BinaryOperator Operator = INetExtenderModulusOperators<TSelf, TSelf, TResult>.Operator;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TResult Modulus(TSelf first, TSelf second)
        {
            return INetExtenderModulusOperators<TSelf, TSelf, TResult>.Modulus(first, second);
        }

        public new static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TSelf, TResult>.Checked.Modulus(first, second);
            }
        }

        public new static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TSelf second)
            {
                return INetExtenderModulusOperators<TSelf, TSelf, TResult>.Unchecked.Modulus(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderModulusOperators<TSelf, TResult>, INetExtenderModulusOperators<TSelf, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderModulusOperators<TSelf, TSelf, TResult>.SafeHandler;
            }
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderModulusOperators<TSelf, TOther, TResult> : INetExtenderBinaryOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderBinaryOperator<TSelf>.Group;
        public const BinaryOperator Operator = BinaryOperator.Modulus | BinaryOperator.Flags;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Modulus(TSelf first, TOther second)
        {
            return Storage.Modulus.Invoke(first, second);
        }

        public static class Checked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return Storage.CheckedModulus.Invoke(first, second);
            }
        }

        public static class Unchecked
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static TResult Modulus(TSelf first, TOther second)
            {
                return Storage.Modulus.Invoke(first, second);
            }
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderModulusOperators<TSelf, TOther, TResult>, INetExtenderModulusOperators<TSelf, TOther, TResult>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> Modulus = null!;
                internal readonly IBinaryReflectionOperator<TSelf, TOther, TResult> CheckedModulus = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                const BinaryOperator @operator = BinaryOperator.Modulus;

                if (Set(in set.Modulus, Initialize<TOther, TResult>(@operator)) is null)
                {
                    yield return Exception<TOther, TResult>(@operator);
                }

                Set(in set.CheckedModulus) = Initialize<TOther, TResult>(@operator | BinaryOperator.Checked) ?? set.Modulus;
            }
        }
    }
}