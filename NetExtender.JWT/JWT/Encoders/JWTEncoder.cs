// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.JWT.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.JWT;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    public class JWTEncoder : IJWTEncoder
    {
        protected String TypeHeader { get; } = JWTHeader.Type.GetDescription() ?? throw new NeverOperationException();
        protected String AlgorithmHeader { get; } = JWTHeader.Algorithm.GetDescription() ?? throw new NeverOperationException();

        protected IJWTUrlEncoder Url { get; }
        protected IJWTAlgorithmFactory Algorithm { get; }
        protected IJWTSerializer Serializer { get; }

        public JWTEncoder(IJWTUrlEncoder encoder, IJWTAlgorithm algorithm, IJWTSerializer serializer)
        {
            Url = encoder ?? throw new ArgumentNullException(nameof(encoder));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Algorithm = algorithm is not null ? new Algorithms.JWT.Factory.Instance(algorithm) : throw new ArgumentNullException(nameof(algorithm));
        }

        public JWTEncoder(IJWTUrlEncoder encoder, IJWTAlgorithmFactory algorithm, IJWTSerializer serializer)
        {
            Url = encoder ?? throw new ArgumentNullException(nameof(encoder));
            Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        protected virtual Dictionary<String, Object> Create(IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            if (extra is null)
            {
                return new Dictionary<String, Object>(2, StringComparer.OrdinalIgnoreCase);
            }
            
            Dictionary<String, Object> result = new Dictionary<String, Object>(extra.CountIfMaterialized(out Int32 count) ? count + 2 : 16, StringComparer.OrdinalIgnoreCase);

            foreach ((String? key, Object? value) in extra)
            {
                if (key is not null && value is not null)
                {
                    result[key] = value;
                }
            }
            
            return result;
        }

        protected virtual String Encode(IJWTAlgorithm algorithm, ReadOnlySpan<Byte> key, Span<Byte> header, Span<Byte> payload)
        {
            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            String encode = Url.Encode(header) + "." + Url.Encode(payload);

            if (!Algorithms.JWT.Encoding.GetByteCount(encode, out EncodingCount count))
            {
                throw new InvalidOperationException("Can't count JWT result.");
            }
            
            Span<Byte> sign = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            count.GetBytes(sign);
            
            String? segment = GetSignatureSegment(algorithm, key, sign);
            return encode + "." + segment;
        }

        protected virtual String Encode<TPayload>(IJWTAlgorithm algorithm, ReadOnlySpan<Byte> key, TPayload payload, IDictionary<String, Object> headers)
        {
            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            headers.TryAdd(TypeHeader, nameof(Algorithms.JWT));
            headers[AlgorithmHeader] = algorithm.Name;

            String serialize = Serializer.Serialize(headers);
            if (!Algorithms.JWT.Encoding.GetByteCount(serialize, out EncodingCount count))
            {
                throw new InvalidOperationException("Can't count JWT header.");
            }
            
            Span<Byte> header = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            count.GetBytes(header);

            serialize = Serializer.Serialize(payload);
            if (!Algorithms.JWT.Encoding.GetByteCount(serialize, out count))
            {
                throw new InvalidOperationException("Can't count JWT payload.");
            }

            Span<Byte> load = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            count.GetBytes(load);
            
            return Encode(algorithm, key, header, load);
        }
        
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload)
        {
            return Encode(key, payload, null, out _);
        }

        public String Encode<TPayload>(JWTKey key, TPayload payload)
        {
            return Encode(key, payload, null, out _);
        }

        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            return Encode(key, payload, extra, out _);
        }

        public String Encode<TPayload>(JWTKey key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra)
        {
            return Encode(key, payload, extra, out _);
        }

        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IDictionary<String, Object>? extra)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (Algorithm.Create() is not { } algorithm)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            if (!algorithm.IsAsymmetric() && key.Length <= 0 && algorithm is not Algorithms.JWT.NoneAlgorithm)
            {
                throw new InvalidOperationException("Key must be not empty when the algorithm is not asymmetric.");
            }

            extra ??= Create(extra!);
            return Encode(algorithm, key, payload, extra);
        }

        public String Encode<TPayload>(JWTKey key, TPayload payload, IDictionary<String, Object>? extra)
        {
            return Encode((ReadOnlySpan<Byte>) key, payload, extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public String Encode<TPayload>(ReadOnlySpan<Byte> key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (Algorithm.Create() is not { } algorithm)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            if (!algorithm.IsAsymmetric() && key.Length <= 0 && algorithm is not Algorithms.JWT.NoneAlgorithm)
            {
                throw new InvalidOperationException("Key must be not empty when the algorithm is not asymmetric.");
            }

            headers = Create(extra);
            return Encode(algorithm, key, payload, headers);
        }
        
        public String Encode<TPayload>(JWTKey key, TPayload payload, IEnumerable<KeyValuePair<String?, Object?>>? extra, out Dictionary<String, Object> headers)
        {
            return Encode((ReadOnlySpan<Byte>) key, payload, extra, out headers);
        }

        protected virtual String? GetSignatureSegment(IJWTAlgorithm? algorithm, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source)
        {
            switch (algorithm)
            {
                case null:
                case Algorithms.JWT.NoneAlgorithm:
                {
                    return null;
                }
                default:
                {
                    Span<Byte> buffer = stackalloc Byte[Algorithms.JWT.StackAlloc];
                    return algorithm.TrySign(key, source, ref buffer) ? Url.Encode(buffer) : Url.Encode(algorithm.Sign(key.ToArray(), source.ToArray()));
                }
            }
        }
        
        public class Encoder : JWTEncoder
        {
            public Encoder(IJWTUrlEncoder encoder, IJWTAlgorithm algorithm, IJWTSerializer serializer)
                : base(encoder, algorithm, serializer)
            {
            }

            public Encoder(IJWTUrlEncoder encoder, IJWTAlgorithmFactory algorithm, IJWTSerializer serializer)
                : base(encoder, algorithm, serializer)
            {
            }

            protected sealed override Dictionary<String, Object> Create(IEnumerable<KeyValuePair<String?, Object?>>? extra)
            {
                return base.Create(extra);
            }

            protected sealed override String Encode(IJWTAlgorithm algorithm, ReadOnlySpan<Byte> key, Span<Byte> header, Span<Byte> payload)
            {
                return base.Encode(algorithm, key, header, payload);
            }

            protected sealed override String Encode<TPayload>(IJWTAlgorithm algorithm, ReadOnlySpan<Byte> key, TPayload payload, IDictionary<String, Object> headers)
            {
                return base.Encode(algorithm, key, payload, headers);
            }

            protected sealed override String? GetSignatureSegment(IJWTAlgorithm? algorithm, ReadOnlySpan<Byte> key, ReadOnlySpan<Byte> source)
            {
                return base.GetSignatureSegment(algorithm, key, source);
            }
        }
    }
}