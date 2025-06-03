// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.JWT.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT
{
    public class JWTUrlEncoder : IJWTUrlEncoder
    {
        public static IJWTUrlEncoder Default { get; } = new Encoder();

        public virtual String Encode(ReadOnlySpan<Byte> source)
        {
            if (source.Length <= 0)
            {
                return String.Empty;
            }
            
            Span<Char> buffer = stackalloc Char[Algorithms.JWT.StackAlloc / sizeof(Char)];
            buffer = Convert.TryToBase64Chars(source, buffer, out Int32 written) ? buffer.Slice(0, written) : Convert.ToBase64String(source).ToCharArray();
            buffer = buffer.IndexOf('=') is var end and >= 0 ? buffer.Slice(0, end) : buffer;

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                switch (buffer[i])
                {
                    case '+':
                        buffer[i] = '-';
                        continue;
                    case '/':
                        buffer[i] = '_';
                        continue;
                    default:
                        continue;
                }
            }

            return new String(buffer);
        }

        public virtual Byte[] Decode(String source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(source, nameof(source));
            }

            String output = source.Replace('-', '+').Replace('_', '/');
            return (output.Length % 4) switch
            {
                0 => Convert.FromBase64String(output),
                2 => Convert.FromBase64String(output + "=="),
                3 => Convert.FromBase64String(output + "="),
                _ => throw new FormatException("Illegal Base64Url format.")
            };
        }

        public virtual Boolean TryDecode(ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
        {
            if (source.Length > Algorithms.JWT.StackAlloc / sizeof(Char))
            {
                return TryDecode(new String(source), destination, out written);
            }

            Int32 size = (source.Length % 4) switch
            {
                0 => source.Length,
                2 => source.Length + 2,
                3 => source.Length + 1,
                _ => -1
            };

            if (size < 0)
            {
                written = default;
                return false;
            }
            
            Span<Char> buffer = stackalloc Char[size];
            source.CopyTo(buffer);
            buffer.Slice(source.Length, buffer.Length - source.Length).Fill('=');

            for (Int32 i = 0; i < source.Length; i++)
            {
                switch (buffer[i])
                {
                    case '-':
                        buffer[i] = '+';
                        continue;
                    case '_':
                        buffer[i] = '/';
                        continue;
                }
            }

            return Convert.TryFromBase64Chars(buffer, destination, out written);
        }

        public virtual Boolean TryDecode(String source, Span<Byte> destination, out Int32 written)
        {
            written = default;
            if (String.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            if (source.Length <= Algorithms.JWT.StackAlloc)
            {
                return TryDecode((ReadOnlySpan<Char>) source, destination, out written);
            }

            String output = source.Replace('-', '+').Replace('_', '/');
            return (output.Length % 4) switch
            {
                0 => Convert.TryFromBase64String(output, destination, out written),
                2 => Convert.TryFromBase64String(output + "==", destination, out written),
                3 => Convert.TryFromBase64String(output + "=", destination, out written),
                _ => false
            };
        }

        public class Encoder : JWTUrlEncoder
        {
            public sealed override String Encode(ReadOnlySpan<Byte> source)
            {
                return base.Encode(source);
            }

            public sealed override Byte[] Decode(String source)
            {
                return base.Decode(source);
            }

            public sealed override Boolean TryDecode(ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
            {
                return base.TryDecode(source, destination, out written);
            }

            public sealed override Boolean TryDecode(String source, Span<Byte> destination, out Int32 written)
            {
                return base.TryDecode(source, destination, out written);
            }
        }
    }
}