using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface IInheritUtf8SpanParsable<TSelf> : INetExtenderUtf8SpanParsable<TSelf>
#if NET8_0_OR_GREATER
        , IUtf8SpanParsable<TSelf> where TSelf : IInheritUtf8SpanParsable<TSelf>?
#endif
    {
        public new const NumericsInterfaceGroup Group = INetExtenderUtf8SpanParsable<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.Parse(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static TSelf Parse(ReadOnlySpan<Byte> value, IFormatProvider? provider)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.Parse(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.TryParse(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static Boolean TryParse(ReadOnlySpan<Byte> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return INetExtenderUtf8SpanParsable<TSelf>.TryParse(value, provider, out result);
        }

        protected new class OperatorHandler : NoHandler
        {
        }
    }

    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public interface INetExtenderUtf8SpanParsable<TSelf> : INetExtenderParsable, INetExtenderMethodOperator<TSelf>,
        INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>
    {
        public new const NumericsInterfaceGroup Group = NumericsInterfaceGroup.Utf8Parse | INetExtenderParsable.Group | INetExtenderMethodOperator<TSelf>.Group;

        public new static Boolean IsSupported
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.IsSupported;
            }
        }

        protected internal new static OperatorHandler Handler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Handler;
            }
        }

        protected internal new static OperatorHandler SafeHandler
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.SafeHandler;
            }
        }

        protected internal new static OperatorHandler.Set Storage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Storage;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static AggregateException? Ensure()
        {
            return INetExtenderOperator<TSelf, INetExtenderUtf8SpanParsable<TSelf>, OperatorHandler, OperatorHandler.Set>.Ensure();
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Byte> value)
        {
            return Storage.Parse.Invoke(value);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelf Parse(ReadOnlySpan<Byte> value, IFormatProvider? provider)
        {
            return Storage.FormatParse.Invoke(value, provider);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Byte> value, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.TryParse.Invoke(value, out result);
        }

        [ReflectionSignature]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Byte> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
        {
            return Storage.FormatTryParse.Invoke(value, provider, out result);
        }

        private protected const Int32 Maximum = 256;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected static Boolean ToUtf16(ReadOnlySpan<Byte> value, Span<Char> buffer, out Int32 written)
        {
            return Utf8.ToUtf16(value, buffer, out _, out written, false) is OperationStatus.Done;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TSelf Parse(ReadOnlySpan<Byte> value, ParseHandler<TSelf> handler)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];
                return ToUtf16(value, buffer, out Int32 written) ? handler(buffer.Slice(0, written)) : throw Exception();
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array, true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8ParseHandler<TSelf> Convert(ParseHandler<TSelf> handler)
        {
            TSelf Core(ReadOnlySpan<Byte> value)
            {
                return Parse(value, handler);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TSelf Parse(ReadOnlySpan<Byte> value, IFormatProvider? provider, FormatParseHandler<TSelf> handler)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];
                return ToUtf16(value, buffer, out Int32 written) ? handler(buffer.Slice(0, written), provider) : throw Exception();
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array, true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8FormatParseHandler<TSelf> Convert(FormatParseHandler<TSelf> handler)
        {
            TSelf Core(ReadOnlySpan<Byte> value, IFormatProvider? provider)
            {
                return Parse(value, provider, handler);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Byte> value, TryParseHandler<TSelf> handler, [MaybeNullWhen(false)] out TSelf result)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];

                if (ToUtf16(value, buffer, out Int32 written))
                {
                    return handler(buffer.Slice(0, written), out result);
                }

                result = default;
                return false;
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array, true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8TryParseHandler<TSelf> Convert(TryParseHandler<TSelf> handler)
        {
            Boolean Core(ReadOnlySpan<Byte> value, [MaybeNullWhen(false)] out TSelf result)
            {
                return TryParse(value, handler, out result);
            }

            return Core;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Byte> value, IFormatProvider? provider, FormatTryParseHandler<TSelf> handler, [MaybeNullWhen(false)] out TSelf result)
        {
            Char[]? array = Encoding.UTF8.GetMaxCharCount(value.Length) is var count && count > Maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[Maximum];

                if (ToUtf16(value, buffer, out Int32 written))
                {
                    return handler(buffer.Slice(0, written), provider, out result);
                }

                result = default;
                return false;
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array, true);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Utf8FormatTryParseHandler<TSelf> Convert(FormatTryParseHandler<TSelf> handler)
        {
            Boolean Core(ReadOnlySpan<Byte> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
            {
                return TryParse(value, provider, handler, out result);
            }

            return Core;
        }

        public new sealed class OperatorHandler : INetExtenderOperator.OperatorHandler<TSelf, INetExtenderUtf8SpanParsable<TSelf>, INetExtenderUtf8SpanParsable<TSelf>.OperatorHandler.Set>
        {
            public sealed class Set : OperatorSet
            {
                internal readonly Utf8ParseHandler<TSelf> Parse = null!;
                internal readonly Utf8FormatParseHandler<TSelf> FormatParse = null!;
                internal readonly Utf8TryParseHandler<TSelf> TryParse = null!;
                internal readonly Utf8FormatTryParseHandler<TSelf> FormatTryParse = null!;
            }

            protected override IEnumerable<Result<INetExtenderOperator.OperatorHandler>> Initialize(Set set)
            {
                Utf8ParseHandler<TSelf>? uparse = Initialize<Utf8ParseHandler<TSelf>>(this, Parse);
                Utf8FormatParseHandler<TSelf>? ufparse = Initialize<Utf8FormatParseHandler<TSelf>>(this, Parse);
                Utf8TryParseHandler<TSelf>? utparse = Initialize<Utf8TryParseHandler<TSelf>>(this, TryParse);
                Utf8FormatTryParseHandler<TSelf>? uftparse = Initialize<Utf8FormatTryParseHandler<TSelf>>(this, TryParse);

                if (uparse is null && ufparse is null && utparse is null && uftparse is null)
                {
                    if (!INetExtenderSpanParsable<TSelf>.IsSupported)
                    {
                        yield return Exception<Utf8FormatParseHandler<TSelf>>(this, Parse);
                        yield break;
                    }

                    Set(in set.Parse) = Convert((ParseHandler<TSelf>) INetExtenderSpanParsable<TSelf>.Parse);
                    Set(in set.FormatParse) = Convert((FormatParseHandler<TSelf>) INetExtenderSpanParsable<TSelf>.Parse);
                    Set(in set.TryParse) = Convert((TryParseHandler<TSelf>) INetExtenderSpanParsable<TSelf>.TryParse);
                    Set(in set.FormatTryParse) = Convert((FormatTryParseHandler<TSelf>) INetExtenderSpanParsable<TSelf>.TryParse);
                    yield break;
                }

                static Boolean TryParseFallback(ReadOnlySpan<Byte> value, [MaybeNullWhen(false)] out TSelf result)
                {
                    return TryParse(value, default(IFormatProvider), out result);
                }

                static Boolean TryParseFormatFallback(ReadOnlySpan<Byte> value, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result)
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

                Set(in set.Parse) = uparse ?? (static value => Parse(value, default(IFormatProvider)));
                Set(in set.FormatParse) = ufparse ?? (static (value, provider) => TryParse(value, provider, out TSelf? result) ? result : throw Exception());
                Set(in set.TryParse) = utparse ?? TryParseFallback;
                Set(in set.FormatTryParse) = uftparse ?? TryParseFormatFallback;
            }
        }
    }
}