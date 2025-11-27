using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderSpanParsable<TSelf> : INetExtenderParsable<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, INetExtenderSpanParsable<TSelf>.OperatorHandler, INetExtenderSpanParsable<TSelf>.OperatorHandler.Set>
#if NET7_0_OR_GREATER
    , ISpanParsable<TSelf>
#endif
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.SpanParse | INetExtenderParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderSpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Char> value)
        {
            return Storage.Parse.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Char> value, IFormatProvider? provider)
        {
            return Storage.FormatParse.Invoke(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.FormatTryParse.Invoke(value, provider, out result);
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderSpanParsable<TSelf>, INetExtenderSpanParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly ParseHandler<TSelf> Parse = null!;
                internal readonly FormatParseHandler<TSelf> FormatParse = null!;
                internal readonly TryParseHandler<TSelf> TryParse = null!;
                internal readonly FormatTryParseHandler<TSelf> FormatTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderParsable<TSelf>.SafeHandler;

                ParseHandler<TSelf>? parse = Method<ParseHandler<TSelf>>(this, Parse);
                FormatParseHandler<TSelf>? fparse = Method<FormatParseHandler<TSelf>>(this, Parse);
                TryParseHandler<TSelf>? tparse = Method<TryParseHandler<TSelf>>(this, TryParse);
                FormatTryParseHandler<TSelf>? ftparse = Method<FormatTryParseHandler<TSelf>>(this, TryParse);

                if (parse is null && fparse is null && tparse is null && ftparse is null)
                {
                    yield return Exception<FormatParseHandler<TSelf>>(this, Parse);
                    yield break;
                }

                static Boolean TryParseFallback(ReadOnlySpan<Char> value, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, null, out result);
                }

                static Boolean TryParseFormatFallback(ReadOnlySpan<Char> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
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
}