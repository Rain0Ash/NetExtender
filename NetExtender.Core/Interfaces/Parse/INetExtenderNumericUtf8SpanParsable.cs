using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderNumericUtf8SpanParsable<TSelf> : INetExtenderUtf8SpanParsable<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, INetExtenderNumericUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderNumericUtf8SpanParsable<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Utf8NumericParse | INetExtenderUtf8SpanParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style)
        {
            return Storage.Parse.Invoke(value, style);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider)
        {
            return Storage.NumericParse.Invoke(value, style, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, style, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.NumericTryParse.Invoke(value, style, provider, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style, StyleParseHandler<TSelf> handler)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];
                return ToUtf16(value, buffer, out Int32 written) ? handler(buffer.Slice(0, written), style) : throw Exception();
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8StyleParseHandler<TSelf> Convert(StyleParseHandler<TSelf> handler)
        {
            TSelf Core(ReadOnlySpan<Byte> value, NumberStyles style)
            {
                return Parse(value, style, handler);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TSelf Parse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, NumericParseHandler<TSelf> handler)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];
                return ToUtf16(value, buffer, out Int32 written) ? handler(buffer.Slice(0, written), style, provider) : throw Exception();
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8NumericParseHandler<TSelf> Convert(NumericParseHandler<TSelf> handler)
        {
            TSelf Core(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider)
            {
                return Parse(value, style, provider, handler);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, StyleTryParseHandler<TSelf> handler, [MaybeNullWhen(false)] out TSelf result)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];

                if (ToUtf16(value, buffer, out Int32 written))
                {
                    return handler(buffer.Slice(0, written), style, out result);
                }

                result = default;
                return false;
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8StyleTryParseHandler<TSelf> Convert(StyleTryParseHandler<TSelf> handler)
        {
            Boolean Core(ReadOnlySpan<Byte> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
            {
                return TryParse(value, style, handler, out result);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, NumericTryParseHandler<TSelf> handler, [MaybeNullWhen(false)] out TSelf result)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];

                if (ToUtf16(value, buffer, out Int32 written))
                {
                    return handler(buffer.Slice(0, written), style, provider, out result);
                }

                result = default;
                return false;
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8NumericTryParseHandler<TSelf> Convert(NumericTryParseHandler<TSelf> handler)
        {
            Boolean Core(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
            {
                return TryParse(value, style, provider, handler, out result);
            }

            return Core;
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderNumericUtf8SpanParsable<TSelf>, INetExtenderNumericUtf8SpanParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Utf8StyleParseHandler<TSelf> Parse = null!;
                internal readonly Utf8NumericParseHandler<TSelf> NumericParse = null!;
                internal readonly Utf8StyleTryParseHandler<TSelf> TryParse = null!;
                internal readonly Utf8NumericTryParseHandler<TSelf> NumericTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                yield return INetExtenderUtf8SpanParsable<TSelf>.SafeHandler;

                Utf8StyleParseHandler<TSelf>? uparse = Method<Utf8StyleParseHandler<TSelf>>(this, Parse);
                Utf8NumericParseHandler<TSelf>? unparse = Method<Utf8NumericParseHandler<TSelf>>(this, Parse);
                Utf8StyleTryParseHandler<TSelf>? utparse = Method<Utf8StyleTryParseHandler<TSelf>>(this, TryParse);
                Utf8NumericTryParseHandler<TSelf>? untparse = Method<Utf8NumericTryParseHandler<TSelf>>(this, TryParse);

                if (uparse is null && unparse is null && utparse is null && untparse is null)
                {
                    if (!INetExtenderNumericSpanParsable<TSelf>.IsSupported)
                    {
                        yield return Exception<Utf8NumericParseHandler<TSelf>>(this, Parse);
                        yield break;
                    }

                    Set(in set.Parse) = Convert((StyleParseHandler<TSelf>) INetExtenderNumericSpanParsable<TSelf>.Parse);
                    Set(in set.NumericParse) = Convert((NumericParseHandler<TSelf>) INetExtenderNumericSpanParsable<TSelf>.Parse);
                    Set(in set.TryParse) = Convert((StyleTryParseHandler<TSelf>) INetExtenderNumericSpanParsable<TSelf>.TryParse);
                    Set(in set.NumericTryParse) = Convert((NumericTryParseHandler<TSelf>) INetExtenderNumericSpanParsable<TSelf>.TryParse);
                    yield break;
                }

                static Boolean TryParseFallback(ReadOnlySpan<Byte> value, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
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

                static Boolean TryParseFormatFallback(ReadOnlySpan<Byte> value, NumberStyles style, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, style, default(IFormatProvider), out result);
                }

                Set(in set.Parse) = uparse ?? (static (value, style) => Parse(value, style, default(IFormatProvider)));
                Set(in set.NumericParse) = unparse ?? (static (value, style, provider) => TryParse(value, style, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = utparse ?? TryParseFormatFallback;
                Set(in set.NumericTryParse) = untparse ?? TryParseFallback;
            }
        }
    }
}