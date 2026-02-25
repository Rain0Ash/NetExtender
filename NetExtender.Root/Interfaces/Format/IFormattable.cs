using System;
using System.Buffers;
using System.Text;
using System.Text.Unicode;
using NetExtender.Exceptions;

namespace NetExtender.Interfaces
{
    public interface INetExtenderFormattable : ISpanFormattable, INetExtenderUtf8SpanFormattable
    {
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
                        throw new InvalidUtf8Exception<ISpanFormattable>();
                }
            }
            finally
            {
                if (array is not null)
                {
                    ArrayPool<Char>.Shared.Return(array, true);
                }
            }
        }
    }
}