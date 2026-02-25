using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritParsable<TSelf> : INetExtenderParsable<TSelf>
#if NET7_0_OR_GREATER
        , IParsable<TSelf> where TSelf : IInheritParsable<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderParsable<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderParsable<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderParsable<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value)
        {
            return INetExtenderParsable<TSelf>.Parse(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(String value, IFormatProvider? provider)
        {
            return INetExtenderParsable<TSelf>.Parse(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderParsable<TSelf>.TryParse(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderParsable<TSelf>.TryParse(value, provider, out result);
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderParsable<TSelf> : INetExtenderParsable, INetExtenderMethodOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler, INetExtenderParsable<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = INetExtenderParsable.Group | INetExtenderMethodOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value)
        {
            return Storage.Parse.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(String value, IFormatProvider? provider)
        {
            return Storage.FormatParse.Invoke(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.FormatTryParse.Invoke(value, provider, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderParsable<TSelf>, INetExtenderParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly ParseHandler<String, TSelf> Parse = null!;
                internal readonly FormatParseHandler<String, TSelf> FormatParse = null!;
                internal readonly TryParseHandler<String, TSelf> TryParse = null!;
                internal readonly FormatTryParseHandler<String, TSelf> FormatTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                ParseHandler<String, TSelf>? parse = Initialize<ParseHandler<String, TSelf>>(this, Parse);
                FormatParseHandler<String, TSelf>? fparse = Initialize<FormatParseHandler<String, TSelf>>(this, Parse);
                TryParseHandler<String, TSelf>? tparse = Initialize<TryParseHandler<String, TSelf>>(this, TryParse);
                FormatTryParseHandler<String, TSelf>? ftparse = Initialize<FormatTryParseHandler<String, TSelf>>(this, TryParse);

                if (parse is null && fparse is null && tparse is null && ftparse is null)
                {
                    yield return Exception<FormatParseHandler<String, TSelf>>(this, Parse);
                    yield break;
                }

                static Boolean TryParseFallback(String value, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, null, out result);
                }

                static Boolean TryParseFormatFallback(String value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
                {
                    try
                    {
                        result = Parse(value, provider);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = default;
                        return false;
                    }
                }

                Set(in set.Parse) = parse ?? (static value => Parse(value, null));
                Set(in set.FormatParse) = fparse ?? (static (value, provider) => TryParse(value, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = tparse ?? TryParseFallback;
                Set(in set.FormatTryParse) = ftparse ?? TryParseFormatFallback;
            }
        }
    }

    public interface INetExtenderParsable : INetExtenderOperator
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Parse;

        protected static FormatException Exception()
        {
            return new FormatInvalidStringException<Int32>();
        }
    }
}