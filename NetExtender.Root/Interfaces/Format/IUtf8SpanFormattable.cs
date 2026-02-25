using System;

namespace NetExtender.Interfaces
{
    public interface INetExtenderUtf8SpanFormattable
#if NET8_0_OR_GREATER
    : IUtf8SpanFormattable
#endif
    {
#if !NET8_0_OR_GREATER
        [Utilities.Core.ReflectionSignature]
        public Boolean TryFormat(Span<Byte> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider);
#endif
    }
}