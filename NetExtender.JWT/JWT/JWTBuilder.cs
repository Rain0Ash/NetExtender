// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Text.Json.Serialization;
using NetExtender.JWT.Algorithms.Interfaces;
using NetExtender.JWT.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Times.Interfaces;
using NetExtender.Utilities.JWT;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.JWT.Algorithms
{
    public static partial class JWT
    {
        public class Builder
        {
            private JWTInfo Info { get; } = new JWTInfo();
            public ITimeProvider Time { get; private set; } = TimeProvider.System;
            public IJWTUrlEncoder? Url { get; private set; } = JWTUrlEncoder.Default;
            public IJWTAlgorithm? Algorithm { get; private set; }
            public IJWTAlgorithmFactory? AlgorithmFactory { get; private set; }
            public IJWTEncoder? Encoder { get; private set; }
            public IJWTDecoder? Decoder { get; private set; }
            public IJWTValidator? Validator { get; private set; }
            public IJWTSerializerFactory? Serializer { get; private set; } = NewtonsoftJWTSerializerFactory.Default;
            public JWTValidationInfo Validation { get; private set; } = JWTValidationInfo.Default;
            private JWTKeys Secrets { get; set; }

            private Boolean CanEncode
            {
                get
                {
                    return Info.HasPayload && Url is not null && (Algorithm is not null || AlgorithmFactory is not null) && Serializer is not null;
                }
            }

            private Boolean CanDecodeHeader
            {
                get
                {
                    return Url is not null && Serializer is not null;
                }
            }

            private Boolean CanDecode
            {
                get
                {
                    return Url is not null && (!Validation.ValidateSignature || Validator is not null && (Algorithm is not null || AlgorithmFactory is not null));
                }
            }

            public static Builder Create()
            {
                return new Builder();
            }

            public Builder AddHeader(JWTHeader header, Object value)
            {
                if (header.GetDescription() is not { } name)
                {
                    throw new EnumUndefinedOrNotSupportedException<JWTHeader>(header, nameof(header), null);
                }
                
                Info.Header.Add(name, value);
                return this;
            }

            public Builder AddHeader(String name, Object value)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullOrEmptyStringException(name, nameof(name));
                }

                Info.Header.Add(name, value);
                return this;
            }

            public Builder AddClaim(String name, Object value)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullOrEmptyStringException(name, nameof(name));
                }

                Info.Payload.Add(name, value);
                return this;
            }

            public Builder WithTimeProvider(ITimeProvider provider)
            {
                Time = provider ?? throw new ArgumentNullException(nameof(provider));
                return this;
            }

            public Builder WithUrlEncoder(IJWTUrlEncoder encoder)
            {
                Url = encoder ?? throw new ArgumentNullException(nameof(encoder));
                return this;
            }

            public Builder WithAlgorithm(IJWTAlgorithm algorithm)
            {
                if (AlgorithmFactory is not null)
                {
                    throw new JWTBuilderException("Can't use algorithm and algorithm factory at same time.");
                }
                
                Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

                if (Algorithm is NoneAlgorithm)
                {
                    Validation.ValidateSignature = false;
                }

                return this;
            }

            public Builder WithAlgorithm(IJWTAlgorithmFactory factory)
            {
                if (Algorithm is not null)
                {
                    throw new JWTBuilderException("Can't use algorithm and algorithm factory at same time.");
                }
                
                AlgorithmFactory = factory ?? throw new ArgumentNullException(nameof(factory));
                return this;
            }

            public Builder WithJsonSerializer(IJWTSerializer serializer)
            {
                if (serializer is null)
                {
                    throw new ArgumentNullException(nameof(serializer));
                }

                return WithJsonSerializerFactory(new DelegateJWTSerializerFactory(serializer));
            }

            public Builder WithJsonSerializer(Func<IJWTSerializer> factory)
            {
                if (factory is null)
                {
                    throw new ArgumentNullException(nameof(factory));
                }

                return WithJsonSerializerFactory(new DelegateJWTSerializerFactory(factory));
            }

            public Builder WithJsonSerializerFactory(IJWTSerializerFactory serializer)
            {
                Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
                return this;
            }

            public Builder WithEncoder(IJWTEncoder encoder)
            {
                Encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
                return this;
            }

            public Builder WithDecoder(IJWTDecoder decoder)
            {
                Decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
                return this;
            }

            public Builder WithValidator(IJWTValidator validator)
            {
                Validator = validator ?? throw new ArgumentNullException(nameof(validator));
                return this;
            }

            public Builder WithValidation(JWTValidationInfo validation)
            {
                if (validation is null)
                {
                    throw new ArgumentNullException(nameof(validation));
                }

                if (validation.ValidateSignature && Algorithm is NoneAlgorithm)
                {
                    throw new InvalidOperationException("Verify signature is not allowed for algorithm 'None'.");
                }

                Validation = validation;
                return this;
            }

            public Builder WithValidation(Action<JWTValidationInfo> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                return WithValidation(Validation.With(action));
            }

            public Builder MustVerifySignature()
            {
                return WithVerifySignature(true);
            }

            public Builder DoNotVerifySignature()
            {
                return WithVerifySignature(false);
            }

            public Builder WithVerifySignature(Boolean verify)
            {
                return WithValidation(verify ? static validation => validation.ValidateSignature = true : static validation => validation.ValidateSignature = false);
            }

            public Builder WithSecrets(params String?[] secrets)
            {
                if (secrets is null)
                {
                    throw new ArgumentNullException(nameof(secrets));
                }

                return WithSecrets(new JWTKeys(secrets));
            }

            public Builder WithSecrets(params Byte[]?[] secrets)
            {
                if (secrets is null)
                {
                    throw new ArgumentNullException(nameof(secrets));
                }

                return WithSecrets(new JWTKeys(secrets));
            }

            public Builder WithSecrets(params JWTKey[] secrets)
            {
                if (secrets is null)
                {
                    throw new ArgumentNullException(nameof(secrets));
                }

                return WithSecrets(new JWTKeys(secrets));
            }

            public Builder WithSecrets(JWTKeys secrets)
            {
                if (secrets.Count <= 0)
                {
                    Secrets = default;
                    throw new ArgumentException("All secrets is null or empty.", nameof(secrets));
                }

                Secrets = secrets;
                return this;
            }

            private Result<IJWTEncoder> CreateEncoder()
            {
                if (Algorithm is null && AlgorithmFactory is null)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTEncoder), nameof(WithAlgorithm));
                }

                if (Url is not { } url)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTEncoder), nameof(WithUrlEncoder));
                }

                if (Serializer?.Create() is not { } serializer)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTEncoder), nameof(WithJsonSerializer));
                }

                if (Algorithm is { } algorithm)
                {
                    return new JWTEncoder(url, algorithm, serializer);
                }

                if (AlgorithmFactory is { } factory)
                {
                    return new JWTEncoder(url, factory, serializer);
                }

                return new JWTBuilderInstantiateException(typeof(JWTEncoder), nameof(WithAlgorithm));
            }

            private Result<IJWTDecoder> CreateHeaderDecoder()
            {
                if (Url is not { } url)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTDecoder), nameof(WithUrlEncoder));
                }

                if (Serializer?.Create() is not { } serializer)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTDecoder), nameof(WithJsonSerializer));
                }

                return new JWTDecoder(url, serializer);
            }

            private Result<IJWTDecoder> CreateDecoder()
            {
                Validator ??= CreateValidator().Value;

                if (Url is not { } url)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTDecoder), nameof(WithUrlEncoder));
                }

                if (Serializer?.Create() is not { } serializer)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTDecoder), nameof(WithJsonSerializer));
                }

                if (Algorithm is { } algorithm)
                {
                    return new JWTDecoder(url, algorithm, serializer, Validator);
                }

                if (AlgorithmFactory is { } factory)
                {
                    return new JWTDecoder(url, factory, serializer, Validator);
                }

                if (!Validation.ValidateSignature)
                {
                    return new JWTDecoder(url, serializer);
                }

                return new JWTBuilderInstantiateException(typeof(JWTDecoder), nameof(WithAlgorithm));
            }

            private Result<IJWTValidator> CreateValidator()
            {
                if (Validator is { } validator)
                {
                    return new Result<IJWTValidator>(validator);
                }

                if (Time is not { } time)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTValidator), nameof(WithTimeProvider));
                }

                if (Serializer?.Create() is not { } serializer)
                {
                    return new JWTBuilderInstantiateException(typeof(JWTValidator), nameof(WithJsonSerializer));
                }

                return new JWTValidator(time, serializer, Validation);
            }

            public String Encode()
            {
                Encoder ??= CreateEncoder().Value;

                if (!CanEncode)
                {
                    throw new JWTBuilderEncodeException(nameof(WithAlgorithm), nameof(WithJsonSerializer), nameof(WithUrlEncoder));
                }

                return Encoder.Encode(Secrets.Primary, Info.Payload, Info.Header);
            }

            public String Encode(Object payload)
            {
                if (payload is null)
                {
                    throw new ArgumentNullException(nameof(payload));
                }

                Encoder ??= CreateEncoder().Value;

                if (!CanEncode)
                {
                    throw new JWTBuilderEncodeException(nameof(WithAlgorithm), nameof(WithJsonSerializer), nameof(WithUrlEncoder));
                }

                if (Info.HasPayload)
                {
                    throw new JWTBuilderException("Supplying both key-value pairs and implicit payload is not supported.");
                }

                return Encoder.Encode(Secrets.Primary, payload, Info.Header);
            }

            public String DecodeHeader(JWTToken token)
            {
                Decoder ??= CreateDecoder().Value;

                if (!CanDecode)
                {
                    throw new JWTBuilderDecodeException(nameof(WithJsonSerializer), nameof(WithValidator), nameof(WithUrlEncoder));
                }
                
                return Decoder.DecodeHeader(token);
            }

            public T DecodeHeader<T>(JWTToken token)
            {
                Decoder ??= CreateHeaderDecoder().Value;

                if (!CanDecodeHeader)
                {
                    throw new JWTBuilderDecodeException(nameof(WithJsonSerializer), nameof(WithUrlEncoder));
                }

                return Decoder.DecodeHeader<T>(token);
            }

            public String Decode(JWTToken token)
            {
                Decoder ??= CreateDecoder().Value;

                if (!CanDecode)
                {
                    throw new JWTBuilderDecodeException(nameof(WithJsonSerializer), nameof(WithValidator), nameof(WithUrlEncoder));
                }

                return Decoder.Decode(token, Secrets, Validation.ValidateSignature);
            }

            public Object Decode(Type type, JWTToken token)
            {
                Decoder ??= CreateDecoder().Value;

                if (!CanDecode)
                {
                    throw new JWTBuilderDecodeException(nameof(WithJsonSerializer), nameof(WithValidator), nameof(WithUrlEncoder));
                }

                return Decoder.Decode(type, token, Secrets, Validation.ValidateSignature);
            }

            public T Decode<T>(JWTToken token)
            {
                Decoder ??= CreateDecoder().Value;

                if (!CanDecode)
                {
                    throw new JWTBuilderDecodeException(nameof(WithJsonSerializer), nameof(WithValidator), nameof(WithUrlEncoder));
                }

                return Decoder.Decode<T>(token, Secrets, Validation.ValidateSignature);
            }

            private String GetPropertyName(MemberInfo property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                return Serializer?.Create() switch
                {
                    NewtonsoftJWTSerializer => property.GetCustomAttribute<JsonPropertyAttribute>(true)?.PropertyName ?? property.Name,
                    TextJsonJWTSerializer => property.GetCustomAttribute<JsonPropertyNameAttribute>(true)?.Name ?? property.Name,
                    _ => property.Name
                };
            }
        }
    }
}