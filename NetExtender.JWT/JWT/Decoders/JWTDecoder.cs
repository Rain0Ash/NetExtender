// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.JWT.Algorithms;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.JWT.Interfaces;
using NetExtender.JWT.Utilities;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    public class JWTDecoder : IJWTDecoder
    {
        protected IJWTUrlEncoder Url { get; }
        protected IJWTAlgorithmFactory? Algorithm { get; }
        protected IJWTSerializer Serializer { get; }
        protected IJWTValidator? Validator { get; }

        public JWTDecoder(IJWTUrlEncoder url, IJWTSerializer serializer)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public JWTDecoder(IJWTUrlEncoder url, IJWTAlgorithm algorithm, IJWTSerializer serializer, IJWTValidator? validator)
            : this(url, serializer)
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            Algorithm = algorithm is not null ? new JWT.Algorithms.JWT.Factory.Instance(algorithm) : throw new ArgumentNullException(nameof(algorithm));
        }

        public JWTDecoder(IJWTUrlEncoder url, IJWTAlgorithmFactory algorithm, IJWTSerializer serializer, IJWTValidator? validator)
            : this(url, serializer)
        {
            Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [return: NotNullIfNotNull("payload")]
        protected virtual Object? Deserialize(Type type, String? payload)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return !String.IsNullOrEmpty(payload) ? Serializer.Deserialize(type, payload) : ReflectionUtilities.Default(type);
        }

        [return: NotNullIfNotNull("payload")]
        protected virtual T Deserialize<T>(String? payload)
        {
            return !String.IsNullOrEmpty(payload) ? Serializer.Deserialize<T>(payload) : default!;
        }

        protected virtual Boolean TryValidate(JWTToken jwt, [MaybeNullWhen(false)] out IJWTAlgorithm algorithm, [MaybeNullWhen(false)] out IJWTValidator validator, [MaybeNullWhen(false)] out String payload, [MaybeNullWhen(true)] out Exception exception)
        {
            algorithm = null;
            payload = null;
            
            if ((validator = Validator) is null)
            {
                exception = new InvalidOperationException("Cannot validate the JWT token. Validator is not available.");
                return false;
            }

            if (String.IsNullOrEmpty(jwt.Payload))
            {
                exception = new ArgumentNullOrEmptyStringException(jwt.Payload, nameof(jwt) + '.' + nameof(JWTToken.Payload));
                return false;
            }

            if (String.IsNullOrEmpty(jwt.Signature))
            {
                exception = new ArgumentNullOrEmptyStringException(jwt.Signature, nameof(jwt) + '.' + nameof(JWTToken.Signature));
                return false;
            }

            JWTHeaderInfo header = DecodeHeader<JWTHeaderInfo>(jwt);
            payload = Algorithms.JWT.Encoding.GetString(Url.Decode(jwt.Payload));

            if ((algorithm = Algorithm?.Create(jwt, header, payload)) is not null)
            {
                return (exception = null) is null;
            }

            exception = new InvalidOperationException("Can't create algorithm from JWT token.");
            return false;
        }

        public virtual Exception? TryValidate(JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            if (!TryValidate(jwt, out IJWTAlgorithm? algorithm, out IJWTValidator? validator, out String? payload, out Exception? exception))
            {
                return exception;
            }

            if (!Algorithms.JWT.Encoding.GetByteCount(jwt.Header, jwt.Payload, '.', out EncodingCount count))
            {
                throw new InvalidOperationException("Can't get byte count of JWT token.");
            }

            Span<Byte> signature = stackalloc Byte[Algorithms.JWT.StackAlloc];
            if (!Url.TryDecode(jwt.Signature, ref signature))
            {
                signature = Url.Decode(jwt.Signature);
            }

            Span<Byte> source = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            if (!Algorithms.JWT.Encoding.TryGetBytes(jwt.Header, jwt.Payload, '.', ref source))
            {
                source = Algorithms.JWT.Encoding.GetBytes(jwt.Header, jwt.Payload, '.');
            }
            
            return algorithm is IJWTAsymmetricAlgorithm asymmetric ? validator.TryValidate(asymmetric, payload, source, signature) : ValidateSymmetricAlgorithm(algorithm, validator, key, payload, source, signature);
        }
        
        public virtual Exception? TryValidate(JWTToken jwt, JWTKey key)
        {
            return TryValidate(jwt, (ReadOnlySpan<Byte>) key);
        }

        public virtual Exception? TryValidate(JWTToken jwt, JWTKeys keys)
        {
            if (!TryValidate(jwt, out IJWTAlgorithm? algorithm, out IJWTValidator? validator, out String? payload, out Exception? exception))
            {
                return exception;
            }

            if (!Algorithms.JWT.Encoding.GetByteCount(jwt.Header, jwt.Payload, '.', out EncodingCount count))
            {
                throw new InvalidOperationException("Can't get byte count of JWT token.");
            }

            Span<Byte> signature = stackalloc Byte[Algorithms.JWT.StackAlloc];
            if (!Url.TryDecode(jwt.Signature, ref signature))
            {
                signature = Url.Decode(jwt.Signature);
            }

            Span<Byte> source = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            if (!Algorithms.JWT.Encoding.TryGetBytes(jwt.Header, jwt.Payload, '.', ref source))
            {
                source = Algorithms.JWT.Encoding.GetBytes(jwt.Header, jwt.Payload, '.');
            }

            return algorithm is IJWTAsymmetricAlgorithm asymmetric ? validator.TryValidate(asymmetric, payload, source, signature) : ValidateSymmetricAlgorithm(algorithm, validator, keys, payload, source, signature);
        }
        
        protected virtual Exception? ValidateSymmetricAlgorithm(IJWTAlgorithm algorithm, IJWTValidator? validator, ReadOnlySpan<Byte> key, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
        {
            if (validator is null)
            {
                return new ArgumentNullException(nameof(validator));
            }

            if (key.Length <= 0)
            {
                return new ArgumentNullException(nameof(key));
            }

            Span<Byte> buffer = stackalloc Byte[Algorithms.JWT.StackAlloc];

            if (!algorithm.TrySign(key, source, ref buffer))
            {
                buffer = algorithm.Sign(key.ToArray(), signature.ToArray());
            }

            String raw = Convert.ToBase64String(signature);
            String @new = Convert.ToBase64String(buffer);
            return validator.TryValidate(payload, raw, @new);
        }
        
        protected virtual Exception? ValidateSymmetricAlgorithm(IJWTAlgorithm algorithm, IJWTValidator? validator, JWTKeys keys, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
        {
            if (validator is null)
            {
                return new ArgumentNullException(nameof(validator));
            }

            if (keys.IsEmpty)
            {
                return new ArgumentNullException(nameof(keys));
            }

            String raw = Convert.ToBase64String(signature);

            Span<Byte> buffer = stackalloc Byte[Algorithms.JWT.StackAlloc];
            Span<Byte> slice = buffer;
            Byte[]? array = null;
            
            String[] signatures = new String[keys.Count];

            Int32 i = 0;
            foreach (JWTKey key in keys)
            {
                if (!algorithm.TrySign(key, source, ref slice))
                {
                    array ??= signature.ToArray();
                    slice = algorithm.Sign(key, array);
                }
                
                signatures[i++] = Convert.ToBase64String(slice);
                slice = buffer;
            }

            return validator.TryValidate(payload, raw, signatures);
        }

        protected virtual JWTHeaderInfo ValidateHeaderInfo(JWTToken jwt)
        {
            JWTHeaderInfo header = DecodeHeader<JWTHeaderInfo>(jwt);

            if (String.IsNullOrEmpty(header.Type) && String.IsNullOrEmpty(header.Algorithm))
            {
                throw new InvalidOperationException("Error deserializing JWT header.");
            }

            if (String.Equals(header.Algorithm, nameof(JWTAlgorithmType.None), StringComparison.OrdinalIgnoreCase) && !String.IsNullOrEmpty(jwt.Signature))
            {
                throw new InvalidOperationException("Signature is not acceptable for algorithm None");
            }

            return header;
        }

        public virtual String DecodeHeader(JWTToken jwt)
        {
            if (String.IsNullOrEmpty(jwt.Header))
            {
                throw new ArgumentNullException(nameof(jwt));
            }
            
            Span<Byte> buffer = stackalloc Byte[Algorithms.JWT.StackAlloc];
            
            if (!Url.TryDecode(jwt.Header, ref buffer))
            {
                buffer = Url.Decode(jwt.Header);
            }

            return Algorithms.JWT.Encoding.GetString(buffer);
        }
        
        public Object DecodeHeader(Type type, JWTToken jwt)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String payload = DecodeHeader(jwt);
            return Deserialize(type, payload);
        }

        public T DecodeHeader<T>(JWTToken jwt)
        {
            String payload = DecodeHeader(jwt);
            return Deserialize<T>(payload);
        }
        
        public String Decode(JWTToken jwt)
        {
            return Decode(jwt, true);
        }

        public String Decode(JWTToken jwt, Boolean verify)
        {
            return Decode(jwt, default(JWTKey), verify);
        }

        public String Decode(JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            return Decode(jwt, key, true);
        }

        public virtual String Decode(JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify)
        {
            if (!verify)
            {
                ValidateHeaderInfo(jwt);
                return Decode(jwt);
            }

            if (Validator is null)
            {
                throw new InvalidOperationException("This instance was constructed without validator so cannot be used for signature validation");
            }

            if (Algorithm is null)
            {
                throw new InvalidOperationException("This instance was constructed without algorithm factory so cannot be used for signature validation");
            }

            this.Validate(jwt, key);
            return Decode(jwt);
        }
        
        public String Decode(JWTToken jwt, JWTKey key)
        {
            return Decode(jwt, key, true);
        }

        public virtual String Decode(JWTToken jwt, JWTKey key, Boolean verify)
        {
            return Decode(jwt, (ReadOnlySpan<Byte>) key, verify);
        }

        public String Decode(JWTToken jwt, JWTKeys keys)
        {
            return Decode(jwt, keys, true);
        }

        public virtual String Decode(JWTToken jwt, JWTKeys keys, Boolean verify)
        {
            if (!verify)
            {
                ValidateHeaderInfo(jwt);
                return Decode(jwt);
            }

            if (Validator is null)
            {
                throw new InvalidOperationException("This instance was constructed without validator so cannot be used for signature validation");
            }

            if (Algorithm is null)
            {
                throw new InvalidOperationException("This instance was constructed without algorithm factory so cannot be used for signature validation");
            }

            this.Validate(jwt, keys);
            return Decode(jwt);
        }
        
        public Object Decode(Type type, JWTToken jwt)
        {
            return Decode(type, jwt, true);
        }

        public Object Decode(Type type, JWTToken jwt, Boolean verify)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String payload = Decode(jwt, verify);
            return Deserialize(type, payload);
        }

        public Object Decode(Type type, JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            return Decode(type, jwt, key, true);
        }

        public Object Decode(Type type, JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String payload = Decode(jwt, key, verify);
            return Deserialize(type, payload);
        }

        public Object Decode(Type type, JWTToken jwt, JWTKey key)
        {
            return Decode(type, jwt, key, true);
        }

        public Object Decode(Type type, JWTToken jwt, JWTKey key, Boolean verify)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String payload = Decode(jwt, key, verify);
            return Deserialize(type, payload);
        }

        public Object Decode(Type type, JWTToken jwt, JWTKeys keys)
        {
            return Decode(type, jwt, keys, true);
        }

        public Object Decode(Type type, JWTToken jwt, JWTKeys keys, Boolean verify)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String payload = Decode(jwt, keys, verify);
            return Deserialize(type, payload);
        }
        
        public T Decode<T>(JWTToken jwt)
        {
            return Decode<T>(jwt, true);
        }

        public T Decode<T>(JWTToken jwt, Boolean verify)
        {
            String payload = Decode(jwt, verify);
            return Deserialize<T>(payload);
        }

        public T Decode<T>(JWTToken jwt, ReadOnlySpan<Byte> key)
        {
            return Decode<T>(jwt, key, true);
        }

        public T Decode<T>(JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify)
        {
            String payload = Decode(jwt, key, verify);
            return Deserialize<T>(payload);
        }

        public T Decode<T>(JWTToken jwt, JWTKey key)
        {
            return Decode<T>(jwt, key, true);
        }

        public T Decode<T>(JWTToken jwt, JWTKey key, Boolean verify)
        {
            String payload = Decode(jwt, key, verify);
            return Deserialize<T>(payload);
        }

        public T Decode<T>(JWTToken jwt, JWTKeys keys)
        {
            return Decode<T>(jwt, keys, true);
        }

        public T Decode<T>(JWTToken jwt, JWTKeys keys, Boolean verify)
        {
            String payload = Decode(jwt, keys, verify);
            return Deserialize<T>(payload);
        }

        public class Decoder : JWTDecoder
        {
            public Decoder(IJWTUrlEncoder url, IJWTSerializer serializer)
                : base(url, serializer)
            {
            }

            public Decoder(IJWTUrlEncoder url, IJWTAlgorithm algorithm, IJWTSerializer serializer, IJWTValidator? validator)
                : base(url, algorithm, serializer, validator)
            {
            }

            public Decoder(IJWTUrlEncoder url, IJWTAlgorithmFactory algorithm, IJWTSerializer serializer, IJWTValidator? validator)
                : base(url, algorithm, serializer, validator)
            {
            }

            [return: NotNullIfNotNull("payload")]
            protected override Object? Deserialize(Type type, String? payload)
            {
                return base.Deserialize(type, payload);
            }

            [return: NotNullIfNotNull("payload")]
            protected override T Deserialize<T>(String? payload)
            {
                return base.Deserialize<T>(payload);
            }

            protected override Boolean TryValidate(JWTToken jwt, [MaybeNullWhen(false)] out IJWTAlgorithm algorithm, [MaybeNullWhen(false)] out IJWTValidator validator, [MaybeNullWhen(false)] out String payload, [MaybeNullWhen(true)] out Exception exception)
            {
                return base.TryValidate(jwt, out algorithm, out validator, out payload, out exception);
            }

            public sealed override Exception? TryValidate(JWTToken jwt, ReadOnlySpan<Byte> key)
            {
                return base.TryValidate(jwt, key);
            }

            public sealed override Exception? TryValidate(JWTToken jwt, JWTKey key)
            {
                return base.TryValidate(jwt, key);
            }

            public sealed override Exception? TryValidate(JWTToken jwt, JWTKeys keys)
            {
                return base.TryValidate(jwt, keys);
            }

            protected sealed override Exception? ValidateSymmetricAlgorithm(IJWTAlgorithm algorithm, IJWTValidator? validator, ReadOnlySpan<Byte> key, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
            {
                return base.ValidateSymmetricAlgorithm(algorithm, validator, key, payload, source, signature);
            }

            protected sealed override Exception? ValidateSymmetricAlgorithm(IJWTAlgorithm algorithm, IJWTValidator? validator, JWTKeys keys, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
            {
                return base.ValidateSymmetricAlgorithm(algorithm, validator, keys, payload, source, signature);
            }

            protected sealed override JWTHeaderInfo ValidateHeaderInfo(JWTToken jwt)
            {
                return base.ValidateHeaderInfo(jwt);
            }

            public sealed override String DecodeHeader(JWTToken jwt)
            {
                return base.DecodeHeader(jwt);
            }

            public sealed override String Decode(JWTToken jwt, ReadOnlySpan<Byte> key, Boolean verify)
            {
                return base.Decode(jwt, key, verify);
            }

            public sealed override String Decode(JWTToken jwt, JWTKey key, Boolean verify)
            {
                return base.Decode(jwt, key, verify);
            }

            public sealed override String Decode(JWTToken jwt, JWTKeys keys, Boolean verify)
            {
                return base.Decode(jwt, keys, verify);
            }
        }
    }
}
