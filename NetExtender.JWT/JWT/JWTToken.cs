// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT
{
    public readonly struct JWTToken : IEqualityStruct<JWTToken>, IEquatable<String>
    {
        public enum Index : Byte
        {
            Scheme = 0,
            Header = 1,
            Payload = 2,
            Signature = 3
        }

        public static explicit operator JWTToken(String? value)
        {
            return TryParse(value, out JWTToken token) ? token : default;
        }

        private readonly String _scheme;
        public String Scheme
        {
            get
            {
                return _scheme ?? JWTScheme.Bearer;
            }
            private init
            {
                _scheme = String.IsNullOrEmpty(value) ? JWTScheme.Bearer : value;
            }
        }

        public String Header { get; private init; }
        public String Payload { get; private init; }
        public String Signature { get; private init; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return String.IsNullOrEmpty(Header) || String.IsNullOrEmpty(Payload) || String.IsNullOrEmpty(Signature);
            }
        }

        public JWTToken(ReadOnlySpan<String> content)
        {
            switch (content.Length)
            {
                case 3 when String.IsNullOrEmpty(content[0]) || String.IsNullOrEmpty(content[1]) || String.IsNullOrEmpty(content[2]):
                    throw new JWTFormatException(nameof(content));
                case 3:
                    (_scheme, Header, Payload, Signature) = (JWTScheme.Bearer, content[0], content[1], content[2]);
                    return;
                case 4 when String.IsNullOrEmpty(content[1]) || String.IsNullOrEmpty(content[2]) || String.IsNullOrEmpty(content[3]):
                    throw new JWTFormatException(nameof(content));
                case 4:
                    (_scheme, Header, Payload, Signature) = (String.IsNullOrEmpty(content[0]) ? JWTScheme.Bearer : content[0], content[1], content[2], content[3]);
                    return;
                default:
                    throw new JWTFormatException(nameof(content));
            }
        }

        public JWTToken(String token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(token, nameof(token));
            }

            if (!Split(token, out ReadOnlySpan<Char> scheme, out ReadOnlySpan<Char> header, out ReadOnlySpan<Char> payload, out ReadOnlySpan<Char> signature))
            {
                throw new JWTFormatException(nameof(token));
            }

            if (header.Length <= 0 || payload.Length <= 0 || signature.Length <= 0)
            {
                throw new JWTFormatException(nameof(token));
            }

            (_scheme, Header, Payload, Signature) = (scheme.Length <= 0 ? JWTScheme.Bearer : new String(scheme), new String(header), new String(payload), new String(signature));
        }

        public JWTToken(String header, String payload, String signature)
            : this(JWTScheme.Bearer, header, payload, signature)
        {
        }

        public JWTToken(String scheme, String header, String payload, String signature)
        {
            if (String.IsNullOrEmpty(header))
            {
                throw new ArgumentNullOrEmptyStringException(header, nameof(header));
            }

            if (String.IsNullOrEmpty(payload))
            {
                throw new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            if (String.IsNullOrEmpty(signature))
            {
                throw new ArgumentNullOrEmptyStringException(signature, nameof(signature));
            }

            (_scheme, Header, Payload, Signature) = (String.IsNullOrEmpty(scheme) ? JWTScheme.Bearer : scheme, header, payload, signature);
        }

        public JWTToken(String[] content)
            : this(content is not null ? (ReadOnlySpan<String>) content : throw new ArgumentNullException(nameof(content)))
        {
        }

        public static Boolean TryParse(ReadOnlySpan<Char> token, out JWTToken result)
        {
            if (token.Length <= 0)
            {
                result = default;
                return false;
            }

            if (!Split(token, out ReadOnlySpan<Char> scheme, out ReadOnlySpan<Char> header, out ReadOnlySpan<Char> payload, out ReadOnlySpan<Char> signature))
            {
                result = default;
                return false;
            }

            if (header.Length <= 0 || payload.Length <= 0 || signature.Length <= 0)
            {
                result = default;
                return false;
            }

            result = new JWTToken
            {
                Scheme = scheme.Length <= 0 ? JWTScheme.Bearer : new String(scheme),
                Header = new String(header),
                Payload = new String(payload),
                Signature = new String(signature)
            };
            
            return true;
        }

        public static Boolean TryParse(String? token, out JWTToken result)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                result = default;
                return false;
            }

            if (!Split(token, out ReadOnlySpan<Char> scheme, out ReadOnlySpan<Char> header, out ReadOnlySpan<Char> payload, out ReadOnlySpan<Char> signature))
            {
                result = default;
                return false;
            }

            if (header.Length <= 0 || payload.Length <= 0 || signature.Length <= 0)
            {
                result = default;
                return false;
            }

            result = new JWTToken
            {
                Scheme = scheme.Length <= 0 ? JWTScheme.Bearer : new String(scheme),
                Header = new String(header),
                Payload = new String(payload),
                Signature = new String(signature)
            };
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean Slice(ReadOnlySpan<Char> source, out ReadOnlySpan<Char> slice, Char character)
        {
            Int32 index = source.IndexOf(character);
            if (index < 0)
            {
                slice = default;
                return false;
            }

            slice = source.Slice(0, index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean SplitWithoutScheme(ReadOnlySpan<Char> source, out ReadOnlySpan<Char> header, out ReadOnlySpan<Char> payload, out ReadOnlySpan<Char> signature)
        {
            payload = default;
            signature = default;
            
            if (!Slice(source, out header, '.'))
            {
                return false;
            }

            if (!Slice(source = source.Slice(header.Length + 1), out payload, '.'))
            {
                return false;
            }
            
            if (!Slice(source = source.Slice(payload.Length + 1), out signature, '.'))
            {
                return false;
            }
            
            return !Slice(source.Slice(signature.Length + 1), out _, '.');
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean Split(ReadOnlySpan<Char> source, out ReadOnlySpan<Char> scheme, out ReadOnlySpan<Char> header, out ReadOnlySpan<Char> payload, out ReadOnlySpan<Char> signature)
        {
            source = source.Trim();
            scheme = source.Slice(0, source.IndexOf(' ') is var index && index >= 0 ? index : 0);
            return SplitWithoutScheme(source.Slice(scheme.Length + 1), out header, out payload, out signature);
        }

        public Int32 CompareTo(JWTToken other)
        {
            Int32 scheme = String.Compare(Scheme, other.Scheme, StringComparison.Ordinal);
            return scheme != 0 ? scheme : String.Compare(Header, other.Header, StringComparison.Ordinal);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Scheme, Header, Payload, Signature);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                JWTToken value => Equals(value),
                String value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(JWTToken other)
        {
            return Scheme == other.Scheme && Header == other.Header && Payload == other.Payload && Signature == other.Signature;
        }

        public Boolean Equals(String? other)
        {
            return !String.IsNullOrEmpty(other) ? TryParse(other, out JWTToken result) && Equals(result) : IsEmpty;
        }

        public Boolean Equals(ReadOnlySpan<Char> other)
        {
            return other.Length > 0 ? TryParse(other, out JWTToken result) && Equals(result) : IsEmpty;
        }

        public String this[Index index]
        {
            get
            {
                return index switch
                {
                    Index.Scheme => Scheme,
                    Index.Header => Header,
                    Index.Payload => Payload,
                    Index.Signature => Signature,
                    _ => throw new EnumUndefinedOrNotSupportedException<Index>(index, nameof(index), null)
                };
            }
        }
    }
}