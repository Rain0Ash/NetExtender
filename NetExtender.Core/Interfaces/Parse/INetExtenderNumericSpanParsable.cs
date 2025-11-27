using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumericSpanParsable<TSelf> : INetExtenderSpanParsable<TSelf>, INetExtenderNumericParsable<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, INetExtenderNumericSpanParsable<TSelf>.OperatorHandler, INetExtenderNumericSpanParsable<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.NumericSpanParse | INetExtenderSpanParsable<TSelf>.Group | INetExtenderNumericParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumericSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Char> value, NumberStyles style)
        {
            return Storage.Parse.Invoke(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider)
        {
            return Storage.NumericParse.Invoke(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.NumericTryParse.Invoke(value, style, provider, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumericSpanParsable<TSelf>, INetExtenderNumericSpanParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly StyleParseHandler<TSelf> Parse = null!;
                internal readonly NumericParseHandler<TSelf> NumericParse = null!;
                internal readonly StyleTryParseHandler<TSelf> TryParse = null!;
                internal readonly NumericTryParseHandler<TSelf> NumericTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderSpanParsable<TSelf>.SafeHandler;
                yield return INetExtenderNumericParsable<TSelf>.SafeHandler;

                StyleParseHandler<TSelf>? parse = Method<StyleParseHandler<TSelf>>(this, Parse);
                NumericParseHandler<TSelf>? fparse = Method<NumericParseHandler<TSelf>>(this, Parse);
                StyleTryParseHandler<TSelf>? tparse = Method<StyleTryParseHandler<TSelf>>(this, TryParse);
                NumericTryParseHandler<TSelf>? ftparse = Method<NumericTryParseHandler<TSelf>>(this, TryParse);

                if (parse is null && fparse is null && tparse is null && ftparse is null)
                {
                    yield return Exception<NumericParseHandler<TSelf>>(this, Parse);
                    yield break;
                }

                static Boolean TryParseFallback(ReadOnlySpan<Char> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, style, null, out result);
                }

                static Boolean TryParseFormatFallback(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
                {
                    try
                    {
                        result = Parse(value, style, provider);
                        return true;
                    }
                    catch (Exception)
                    {
                        result = default;
                        return false;
                    }
                }

                Set(in set.Parse) = parse ?? (static (value, style) => Parse(value, style, null));
                Set(in set.NumericParse) = fparse ?? (static (value, style, provider) => TryParse(value, style, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = tparse ?? TryParseFallback;
                Set(in set.NumericTryParse) = ftparse ?? TryParseFormatFallback;
            }
        }
    }
}