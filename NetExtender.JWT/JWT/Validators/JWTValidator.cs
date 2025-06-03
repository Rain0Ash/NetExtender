// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.JWT.Interfaces;
using NetExtender.JWT.Utilities;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Times.Interfaces;
using NetExtender.Utilities.Types;
using Payload = System.Collections.Generic.IReadOnlyDictionary<System.String, System.Object?>;

namespace NetExtender.JWT
{
    public class JWTValidator : IJWTValidator
    {
        protected static String ExpirationTime { get; } = JWTClaim.ExpirationTime.GetDescription() ?? throw new NeverOperationException();
        protected static String NotBefore { get; } = JWTClaim.NotBefore.GetDescription() ?? throw new NeverOperationException();

        protected IJWTUrlEncoder? Url { get; }
        protected ITimeProvider Time { get; }
        protected IJWTSerializer Serializer { get; }
        protected JWTValidationInfo Validation { get; }

        public JWTValidator(ITimeProvider time, IJWTSerializer serializer)
            : this(time, serializer, null)
        {
        }

        public JWTValidator(ITimeProvider time, IJWTSerializer serializer, JWTValidationInfo? validation)
            : this(null, time, serializer, validation)
        {
        }

        public JWTValidator(IJWTUrlEncoder? url, ITimeProvider time, IJWTSerializer serializer)
            : this(url, time, serializer, null)
        {
        }

        public JWTValidator(IJWTUrlEncoder? url, ITimeProvider time, IJWTSerializer serializer, JWTValidationInfo? validation)
        {
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Url = url;
            Validation = validation ?? JWTValidationInfo.Default;
        }

        public void Validate(IJWTAsymmetricAlgorithm algorithm, String payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
        {
            if (TryValidate(algorithm, payload, source, signature) is { } exception)
            {
                throw exception;
            }
        }

        public void Validate(IJWTAsymmetricAlgorithm algorithm, String payload, Byte[] source, Byte[] signature)
        {
            if (TryValidate(algorithm, payload, source, signature) is { } exception)
            {
                throw exception;
            }
        }

        public void Validate(String payload, String crypto, String signature)
        {
            if (TryValidate(payload, crypto, signature) is { } exception)
            {
                throw exception;
            }
        }

        public void Validate(String payload, String crypto, ReadOnlySpan<String?> signatures)
        {
            if (TryValidate(payload, crypto, signatures) is { } exception)
            {
                throw exception;
            }
        }

        public void Validate(String payload, String crypto, params String[] signatures)
        {
            if (TryValidate(payload, crypto, signatures) is { } exception)
            {
                throw exception;
            }
        }

        public Exception? TryValidate(IJWTAsymmetricAlgorithm? algorithm, String? payload, ReadOnlySpan<Byte> source, ReadOnlySpan<Byte> signature)
        {
            if (algorithm is null)
            {
                return new ArgumentNullException(nameof(algorithm));
            }

            if (String.IsNullOrEmpty(payload))
            {
                return new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            if (Validation.ValidateSignature && !algorithm.Verify(source, signature))
            {
                return new JWTVerifyException("The signature is invalid according to the validation procedure.");
            }

            return GetValidationException(payload);
        }

        public Exception? TryValidate(IJWTAsymmetricAlgorithm? algorithm, String? payload, Byte[]? source, Byte[]? signature)
        {
            if (algorithm is null)
            {
                return new ArgumentNullException(nameof(algorithm));
            }

            if (String.IsNullOrEmpty(payload))
            {
                return new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            if (source is null)
            {
                return new ArgumentNullException(nameof(source));
            }

            if (signature is null)
            {
                return new ArgumentNullException(nameof(signature));
            }

            if (Validation.ValidateSignature && !algorithm.Verify(source, signature))
            {
                return new JWTVerifyException("The signature is invalid according to the validation procedure.");
            }

            return GetValidationException(payload);
        }

        public Exception? TryValidate(String? payload, String? crypto, String? signature)
        {
            if (String.IsNullOrEmpty(payload))
            {
                return new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            if (String.IsNullOrEmpty(crypto))
            {
                return new ArgumentNullOrEmptyStringException(crypto, nameof(crypto));
            }

            if (String.IsNullOrEmpty(signature))
            {
                return new ArgumentNullOrEmptyStringException(signature, nameof(signature));
            }

            if (Validation.ValidateSignature && !CompareCryptoWithSignature(crypto, signature))
            {
                return new JWTVerifyException(crypto, signature);
            }

            return GetValidationException(payload);
        }

        protected virtual Exception? TryValidateSignature(ReadOnlySpan<String?> signatures)
        {
            Int32 count = 0;
            foreach (String? signature in signatures)
            {
                if (!String.IsNullOrWhiteSpace(signature))
                {
                    ++count;
                }
            }

            return count > 0 ? null : new ArgumentException("Signatures cannot be null or whitespace.", nameof(signatures));
        }

        protected virtual Exception? TryValidateSignature(String crypto, ReadOnlySpan<String?> signatures)
        {
            Int32 count = 0;
            Int32 successful = 0;
            foreach (String? signature in signatures)
            {
                if (String.IsNullOrWhiteSpace(signature))
                {
                    continue;
                }

                ++count;
                if (CompareCryptoWithSignature(crypto, signature))
                {
                    ++successful;
                }
            }

            if (count <= 0)
            {
                return new ArgumentException("Signatures cannot be null or whitespace.", nameof(signatures));
            }

            if (successful > 0)
            {
                return null;
            }

            Int32 index = 0;
            String[] array = new String[count];
            foreach (String? signature in signatures)
            {
                if (!String.IsNullOrWhiteSpace(signature))
                {
                    array[index++] = signature;
                }
            }

            return new JWTVerifyException(crypto, array);
        }

        protected Exception? TryValidateSignature(String crypto, ReadOnlySpan<String?> signatures, Boolean validate)
        {
            return validate ? TryValidateSignature(crypto, signatures) : TryValidateSignature(signatures);
        }

        public virtual Exception? TryValidate(String? payload, String? crypto, ReadOnlySpan<String?> signatures)
        {
            switch (signatures.Length)
            {
                case 0:
                    return new ArgumentException("Signatures cannot be empty.", nameof(signatures));
                case 1:
                    return TryValidate(payload, crypto, signatures[0]);
                default:
                    break;
            }

            if (String.IsNullOrEmpty(payload))
            {
                return new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            if (String.IsNullOrEmpty(crypto))
            {
                return new ArgumentNullOrEmptyStringException(crypto, nameof(crypto));
            }

            return TryValidateSignature(crypto, signatures, Validation.ValidateSignature) ?? GetValidationException(payload);
        }

        public Exception? TryValidate(String? payload, String? crypto, params String?[]? signatures)
        {
            return signatures is not null ? TryValidate(payload, crypto, (ReadOnlySpan<String?>) signatures) : new ArgumentNullException(nameof(signatures));
        }

        public virtual Exception? GetValidationException(JWTToken jwt)
        {
            if (Url is null)
            {
                return new InvalidOperationException("This instance was constructed without URl encoder so cannot be used for shallow validation");
            }

            if (String.IsNullOrEmpty(jwt.Payload))
            {
                return new ArgumentNullOrEmptyStringException(jwt.Payload, nameof(jwt));
            }

            Span<Byte> buffer = stackalloc Byte[Algorithms.JWT.StackAlloc];
            return Url.TryDecode(jwt.Payload, ref buffer) ? GetValidationException(buffer) : GetValidationException(Url.Decode(jwt.Payload));
        }

        public virtual Exception? GetValidationException(ReadOnlySpan<Byte> source)
        {
            try
            {
                return GetValidationException(Algorithms.JWT.Encoding.GetString(source));
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public Exception? GetValidationException(Byte[]? source)
        {
            return source is not null ? GetValidationException((ReadOnlySpan<Byte>) source) : new ArgumentNullException(nameof(source));
        }

        protected virtual Exception? GetValidationException(String payload)
        {
            if (String.IsNullOrEmpty(payload))
            {
                return new ArgumentNullOrEmptyStringException(payload, nameof(payload));
            }

            Dictionary<String, Object?> data = Serializer.Deserialize<Dictionary<String, Object?>>(payload);
            Double seconds = Math.Round(Time.GetUtcNow().SinceEpoch().TotalSeconds);

            return Validation switch
            {
                { ValidateExpirationTime: true } when ValidateEXPClaim(data, seconds) is { } exception => exception,
                { ValidateNotBefore: true } when ValidateNBFClaim(data, seconds) is { } exception => exception,
                _ => ValidateOtherClaims(data, seconds)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected virtual Boolean CompareCryptoWithSignature(ReadOnlySpan<Byte> crypto, ReadOnlySpan<Byte> signature)
        {
            Byte result = 0;
            for (Int32 i = 0; i < crypto.Length; i++)
            {
                result |= (Byte) (crypto[i] ^ signature[i]);
            }

            return result == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected virtual Boolean CompareCryptoWithSignature(String? crypto, String? signature)
        {
            if (crypto is null || signature is null)
            {
                return false;
            }
            
            if (crypto.Length != signature.Length)
            {
                return false;
            }

            if (!Algorithms.JWT.Encoding.GetByteCount(crypto, out EncodingCount count))
            {
                throw new InvalidOperationException("Can't count JWT crypto.");
            }
            
            Span<Byte> crypt = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            count.GetBytes(crypt);

            if (!Algorithms.JWT.Encoding.GetByteCount(signature, out count))
            {
                throw new InvalidOperationException("Can't count JWT signature.");
            }
            
            Span<Byte> sign = count.IsStack ? stackalloc Byte[count] : new Byte[count];
            count.GetBytes(sign);

            return CompareCryptoWithSignature(crypt, sign);
        }

        /// <summary>Verifies the 'exp' claim.</summary>
        /// <param name="payload">JWT Payload</param>
        /// <param name="seconds">Seconds since epoch</param>
        /// <remarks>See https://tools.ietf.org/html/rfc7519#section-4.1.4</remarks>
        protected virtual Exception? ValidateEXPClaim(Payload? payload, Double seconds)
        {
            if (payload is null)
            {
                return new ArgumentNullException(nameof(payload));
            }

            if (!payload.TryGetValue(ExpirationTime, out Object? exp))
            {
                return null;
            }

            if (exp is null)
            {
                return new JWTVerifyException($"Claim '{ExpirationTime}' must be a number.");
            }

            Double value;
            try
            {
                value = Convert.ToDouble(exp);
            }
            catch (Exception)
            {
                return new JWTVerifyException($"Claim '{ExpirationTime}' must be a number.");
            }

            if (seconds - Validation.TimeMargin >= value)
            {
                return new JWTExpiredException("Token has expired.")
                {
                    PayloadData = payload,
                    Expiration = TimeUtilities.Epoch.AddSeconds(value)
                };
            }

            return null;
        }

        /// <summary>Verifies the 'nbf' claim.</summary>
        /// <param name="payload">JWT Payload</param>
        /// <param name="seconds">Seconds since epoch</param>
        /// <remarks>See https://tools.ietf.org/html/rfc7519#section-4.1.5</remarks>
        protected virtual Exception? ValidateNBFClaim(Payload? payload, Double seconds)
        {
            if (payload is null)
            {
                return new ArgumentNullException(nameof(payload));
            }

            if (!payload.TryGetValue(NotBefore, out Object? nbt))
            {
                return null;
            }

            if (nbt is null)
            {
                return new JWTVerifyException($"Claim '{NotBefore}' must be a number.");
            }

            Double value;
            try
            {
                value = Convert.ToDouble(nbt);
            }
            catch (Exception)
            {
                return new JWTVerifyException($"Claim '{NotBefore}' must be a number.");
            }

            if (seconds + Validation.TimeMargin < value)
            {
                return new JWTNotYetValidException("Token is not yet valid.")
                {
                    PayloadData = payload,
                    NotBefore = TimeUtilities.Epoch.AddSeconds(value)
                };
            }

            return null;
        }

        protected virtual Exception? ValidateOtherClaims(Payload? payload, Double seconds)
        {
            return null;
        }

        public class Validator : JWTValidator
        {
            public Validator(ITimeProvider time, IJWTSerializer serializer)
                : base(time, serializer)
            {
            }

            public Validator(ITimeProvider time, IJWTSerializer serializer, JWTValidationInfo? validation)
                : base(time, serializer, validation)
            {
            }

            public Validator(IJWTUrlEncoder? url, ITimeProvider time, IJWTSerializer serializer)
                : base(url, time, serializer)
            {
            }

            public Validator(IJWTUrlEncoder? url, ITimeProvider time, IJWTSerializer serializer, JWTValidationInfo? validation)
                : base(url, time, serializer, validation)
            {
            }

            protected sealed override Exception? TryValidateSignature(ReadOnlySpan<String?> signatures)
            {
                return base.TryValidateSignature(signatures);
            }

            protected sealed override Exception? TryValidateSignature(String crypto, ReadOnlySpan<String?> signatures)
            {
                return base.TryValidateSignature(crypto, signatures);
            }

            public sealed override Exception? TryValidate(String? payload, String? crypto, ReadOnlySpan<String?> signatures)
            {
                return base.TryValidate(payload, crypto, signatures);
            }

            public sealed override Exception? GetValidationException(JWTToken jwt)
            {
                return base.GetValidationException(jwt);
            }

            public sealed override Exception? GetValidationException(ReadOnlySpan<Byte> source)
            {
                return base.GetValidationException(source);
            }

            protected sealed override Exception? GetValidationException(String payload)
            {
                return base.GetValidationException(payload);
            }

            protected sealed override Boolean CompareCryptoWithSignature(ReadOnlySpan<Byte> crypto, ReadOnlySpan<Byte> signature)
            {
                return base.CompareCryptoWithSignature(crypto, signature);
            }

            protected sealed override Boolean CompareCryptoWithSignature(String? crypto, String? signature)
            {
                return base.CompareCryptoWithSignature(crypto, signature);
            }

            protected sealed override Exception? ValidateEXPClaim(Payload? payload, Double seconds)
            {
                return base.ValidateEXPClaim(payload, seconds);
            }

            protected sealed override Exception? ValidateNBFClaim(Payload? payload, Double seconds)
            {
                return base.ValidateNBFClaim(payload, seconds);
            }

            protected sealed override Exception? ValidateOtherClaims(Payload? payload, Double seconds)
            {
                return base.ValidateOtherClaims(payload, seconds);
            }
        }
    }
}
