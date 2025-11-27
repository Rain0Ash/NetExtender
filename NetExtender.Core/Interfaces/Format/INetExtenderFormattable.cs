using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Unicode;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Interfaces
{
    public interface INetExtenderFormattable : ISpanFormattable, INetExtenderUtf8SpanFormattable
    {
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [ReflectionSystemResource(typeof(ISpanFormattable))]
        private static class SR
        {
            private static Type SRType { get; } = SRUtilities.SRType(typeof(ISpanFormattable).Assembly);

            [ReflectionSystemResource(typeof(ISpanFormattable))]
            public static SRInfo InvalidOperation_InvalidUtf8 { get; } = new SRInfo(SRType);
        }

        Boolean
#if NET8_0_OR_GREATER
        IUtf8SpanFormattable.TryFormat
#else
        INetExtenderUtf8SpanFormattable.TryFormat
#endif
        (Span<Byte> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            const Int32 maximum = 256;
            Char[]? array = Encoding.UTF8.GetMaxCharCount(destination.Length) is var count and > maximum ? ArrayPool<Char>.Shared.Rent(count) : null;

            try
            {
                Span<Char> buffer = array ?? stackalloc Char[maximum];

                if (!TryFormat(buffer, out written, format, provider))
                {
                    written = 0;
                    return false;
                }

                switch (Utf8.FromUtf16(buffer.Slice(0, written), destination, out _, out written, false))
                {
                    case OperationStatus.Done:
                        return true;
                    case OperationStatus.DestinationTooSmall:
                        written = 0;
                        return false;
                    default:
                        throw new InvalidOperationException(SR.InvalidOperation_InvalidUtf8);
                }
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array);
                }
            }
        }
    }
}