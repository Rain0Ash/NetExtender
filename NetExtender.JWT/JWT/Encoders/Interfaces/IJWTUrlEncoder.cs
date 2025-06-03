// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.JWT.Interfaces
{
    public interface IJWTUrlEncoder
    {
        public String Encode(ReadOnlySpan<Byte> source);
        public Byte[] Decode(String source);
        public Boolean TryDecode(ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written);
        public Boolean TryDecode(String source, Span<Byte> destination, out Int32 written);
    }
}