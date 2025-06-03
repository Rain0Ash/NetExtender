// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetExtender.AspNetCore.Identity;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.Types.Exceptions;
using Newtonsoft.Json;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static partial class IdentityUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTTimeService<TId, TUser, TRole>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return AddJWTTimeService<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>>(services);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTTimeService<TId, TUser, TRole, TTime>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTTimeService<TId, TUser, TRole>, TTime>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTTimeService<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTTimeService<TId, TUser, TRole> service) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return services.AddSingleton(service);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTTimeService<TId, TUser, TRole>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return TryAddJWTTimeService<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>>(services);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTTimeService<TId, TUser, TRole, TTime>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTTimeService<TId, TUser, TRole>, TTime>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTTimeService<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTTimeService<TId, TUser, TRole> service) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            services.TryAddSingleton(service);
            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSecret<TId, TUser, TRole, TSecret>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTSecret<TId, TUser, TRole>, TSecret>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSecret<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return AddJWTSecret<TId, TUser, TRole, IIdentityJWTSecret<TId, TUser, TRole>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSecret<TId, TUser, TRole, TSecret>(this IServiceCollection services, TSecret secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : IIdentityJWTSecret<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (secret is null)
            {
                throw new ArgumentNullException(nameof(secret));
            }

            if (secret.IsEmpty)
            {
                throw new ArgumentException("The secret is empty.", nameof(secret));
            }

            return services.AddSingleton<IIdentityJWTSecret<TId, TUser, TRole>>(secret);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSecret<TId, TUser, TRole, TSecret>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTSecret<TId, TUser, TRole>, TSecret>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSecret<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return TryAddJWTSecret<TId, TUser, TRole, IIdentityJWTSecret<TId, TUser, TRole>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSecret<TId, TUser, TRole, TSecret>(this IServiceCollection services, TSecret secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : IIdentityJWTSecret<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (secret is null)
            {
                throw new ArgumentNullException(nameof(secret));
            }

            if (secret.IsEmpty)
            {
                throw new ArgumentException("The secret is empty.", nameof(secret));
            }

            services.TryAddSingleton<IIdentityJWTSecret<TId, TUser, TRole>>(secret);
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTAlgorithm<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IIdentityJWTAlgorithm<TId, TUser, TRole>, TAlgorithm>().TryAddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>(static provider => new IdentityJWTAlgorithmFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTAlgorithm<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTAlgorithm<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTAlgorithm<TId, TUser, TRole> algorithm) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            services.AddSingleton(algorithm).TryAddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>(static provider => new IdentityJWTAlgorithmFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTAlgorithm<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTAlgorithmFactory<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>, TAlgorithm>().TryAddTransient<IIdentityJWTAlgorithm<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT algorithm factory isn't support default algorithm."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTAlgorithmFactory<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> algorithm) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            services.AddSingleton(algorithm).TryAddTransient<IIdentityJWTAlgorithm<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT algorithm factory isn't support default algorithm."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTAlgorithm<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTAlgorithm<TId, TUser, TRole>, TAlgorithm>();
            services.TryAddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>(static provider => new IdentityJWTAlgorithmFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTAlgorithm<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTAlgorithm<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTAlgorithm<TId, TUser, TRole> algorithm) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            services.TryAddSingleton(algorithm);
            services.TryAddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>(static provider => new IdentityJWTAlgorithmFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTAlgorithm<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTAlgorithmFactory<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>, TAlgorithm>();
            services.TryAddTransient<IIdentityJWTAlgorithm<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT algorithm factory isn't support default algorithm."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTAlgorithmFactory<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> algorithm) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (algorithm is null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            services.TryAddSingleton(algorithm);
            services.TryAddTransient<IIdentityJWTAlgorithm<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTAlgorithmFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT algorithm factory isn't support default algorithm."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTEncoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTEncoder<TId, TUser, TRole> encoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return services.AddSingleton(encoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTEncoder<TId, TUser, TRole, TEncoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTEncoder<TId, TUser, TRole>, TEncoder>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTEncoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTEncoder<TId, TUser, TRole> encoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            services.TryAddSingleton(encoder);
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTEncoder<TId, TUser, TRole, TEncoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTEncoder<TId, TUser, TRole>, TEncoder>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTDecoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTDecoder<TId, TUser, TRole> decoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            return services.AddSingleton(decoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTDecoder<TId, TUser, TRole, TDecoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTDecoder<TId, TUser, TRole>, TDecoder>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTDecoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTDecoder<TId, TUser, TRole> decoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (decoder is null)
            {
                throw new ArgumentNullException(nameof(decoder));
            }

            services.TryAddSingleton(decoder);
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTDecoder<TId, TUser, TRole, TDecoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTDecoder<TId, TUser, TRole>, TDecoder>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTUrlEncoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTUrlEncoder<TId, TUser, TRole> encoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            return services.AddSingleton(encoder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTUrlEncoder<TId, TUser, TRole>, TUrlEncoder>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTUrlEncoder<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTUrlEncoder<TId, TUser, TRole> encoder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (encoder is null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            services.TryAddSingleton(encoder);
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTUrlEncoder<TId, TUser, TRole>, TUrlEncoder>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return serializer switch
            {
                JWTSerializerType.Unknown => services,
                JWTSerializerType.TextJson => AddJWTSerializer<TId, TUser, TRole, IdentityTextJsonJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Newtonsoft => AddJWTSerializer<TId, TUser, TRole, IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Other => services,
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, JsonSerializer serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            return AddJWTSerializer(services, new IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>(serializer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, System.Text.Json.JsonSerializerOptions options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return AddJWTSerializer<TId, TUser, TRole>(services, options, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, System.Text.Json.JsonSerializerOptions serialize, System.Text.Json.JsonSerializerOptions deserialize) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serialize is null)
            {
                throw new ArgumentNullException(nameof(serialize));
            }

            if (deserialize is null)
            {
                throw new ArgumentNullException(nameof(deserialize));
            }

            return AddJWTSerializer(services, new IdentityTextJsonJWTSerializer<TId, TUser, TRole>(serialize, deserialize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TSerializer) == typeof(IdentityNoneJWTSerializer<TId, TUser, TRole>))
            {
                return services;
            }

            services.AddSingleton<IIdentityJWTSerializer<TId, TUser, TRole>, TSerializer>().TryAddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>>(static provider => new IdentityJWTSerializerFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTSerializer<TId, TUser, TRole>>()));
            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSerializer<TId, TUser, TRole> serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            services.AddSingleton(serializer).TryAddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>>(static provider => new IdentityJWTSerializerFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTSerializer<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializerFactory<TId, TUser, TRole, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TSerializer) == typeof(IdentityJWTSerializerFactory<TId, TUser, TRole>.None))
            {
                return services;
            }

            services.AddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>, TSerializer>().TryAddTransient<IIdentityJWTSerializer<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTSerializerFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT serializer factory isn't support default serializer."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializerFactory<TId, TUser, TRole>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return serializer switch
            {
                JWTSerializerType.Unknown => services,
                JWTSerializerType.TextJson => AddJWTSerializerFactory<TId, TUser, TRole, IdentityJWTSerializerFactory<TId, TUser, TRole>.TextJson>(services),
                JWTSerializerType.Newtonsoft => AddJWTSerializerFactory<TId, TUser, TRole, IdentityJWTSerializerFactory<TId, TUser, TRole>.Newtonsoft>(services),
                JWTSerializerType.Other => services,
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTSerializerFactory<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSerializerFactory<TId, TUser, TRole> serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            services.AddSingleton(serializer).TryAddTransient<IIdentityJWTSerializer<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTSerializerFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT serializer factory isn't support default serializer."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return serializer switch
            {
                JWTSerializerType.Unknown => services,
                JWTSerializerType.TextJson => TryAddJWTSerializer<TId, TUser, TRole, IdentityTextJsonJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Newtonsoft => TryAddJWTSerializer<TId, TUser, TRole, IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Other => services,
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, JsonSerializer serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            
            return TryAddJWTSerializer(services, new IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>(serializer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, System.Text.Json.JsonSerializerOptions options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            return TryAddJWTSerializer<TId, TUser, TRole>(services, options, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, System.Text.Json.JsonSerializerOptions serialize, System.Text.Json.JsonSerializerOptions deserialize) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serialize is null)
            {
                throw new ArgumentNullException(nameof(serialize));
            }

            if (deserialize is null)
            {
                throw new ArgumentNullException(nameof(deserialize));
            }

            return TryAddJWTSerializer(services, new IdentityTextJsonJWTSerializer<TId, TUser, TRole>(serialize, deserialize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TSerializer) == typeof(IdentityNoneJWTSerializer<TId, TUser, TRole>))
            {
                return services;
            }

            services.TryAddSingleton<IIdentityJWTSerializer<TId, TUser, TRole>, TSerializer>();
            services.TryAddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>>(static provider => new IdentityJWTSerializerFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTSerializer<TId, TUser, TRole>>()));
            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializer<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSerializer<TId, TUser, TRole> serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            services.TryAddSingleton(serializer);
            services.TryAddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>>(static provider => new IdentityJWTSerializerFactory<TId, TUser, TRole>(provider.GetRequiredService<IIdentityJWTSerializer<TId, TUser, TRole>>()));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializerFactory<TId, TUser, TRole, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (typeof(TSerializer) == typeof(IdentityJWTSerializerFactory<TId, TUser, TRole>.None))
            {
                return services;
            }

            services.TryAddSingleton<IIdentityJWTSerializerFactory<TId, TUser, TRole>, TSerializer>();
            services.TryAddTransient<IIdentityJWTSerializer<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTSerializerFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT serializer factory isn't support default serializer."));
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializerFactory<TId, TUser, TRole>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return serializer switch
            {
                JWTSerializerType.Unknown => services,
                JWTSerializerType.TextJson => TryAddJWTSerializerFactory<TId, TUser, TRole, IdentityJWTSerializerFactory<TId, TUser, TRole>.TextJson>(services),
                JWTSerializerType.Newtonsoft => TryAddJWTSerializerFactory<TId, TUser, TRole, IdentityJWTSerializerFactory<TId, TUser, TRole>.Newtonsoft>(services),
                JWTSerializerType.Other => services,
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTSerializerFactory<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSerializerFactory<TId, TUser, TRole> serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            services.TryAddSingleton(serializer);
            services.TryAddTransient<IIdentityJWTSerializer<TId, TUser, TRole>>(static provider => provider.GetRequiredService<IIdentityJWTSerializerFactory<TId, TUser, TRole>>().Create().Identity<TId, TUser, TRole>() ?? throw new NotSupportedException("JWT serializer factory isn't support default serializer."));
            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTValidator<TId, TUser, TRole, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IIdentityJWTValidator<TId, TUser, TRole>, TValidator>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddJWTValidator<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTValidator<TId, TUser, TRole> validator) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            return services.AddSingleton(validator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTValidator<TId, TUser, TRole, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<IIdentityJWTValidator<TId, TUser, TRole>, TValidator>();
            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection TryAddJWTValidator<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTValidator<TId, TUser, TRole> validator) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            services.TryAddSingleton(validator);
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AuthenticationBuilder AddJWT<TId, TUser, TRole>(this AuthenticationBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return AddJWT<TId, TUser, TRole>(builder, BearerScheme.Scheme);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AuthenticationBuilder AddJWT<TId, TUser, TRole>(this AuthenticationBuilder builder, String scheme) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return AddJWT<TId, TUser, TRole>(builder, scheme, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AuthenticationBuilder AddJWT<TId, TUser, TRole>(this AuthenticationBuilder builder, Action<JWTAuthenticationOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return AddJWT<TId, TUser, TRole>(builder, BearerScheme.Scheme, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AuthenticationBuilder AddJWT<TId, TUser, TRole>(this AuthenticationBuilder builder, String scheme, Action<JWTAuthenticationOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (String.IsNullOrEmpty(scheme))
            {
                throw new ArgumentNullOrEmptyStringException(scheme, nameof(scheme));
            }

            builder.Services.TryAddSingleton<IIdentityJWTIdentityFactory<TId, TUser, TRole>, IdentityJWTClaimsIdentityFactory<TId, TUser, TRole>>();
            builder.Services.TryAddSingleton<IIdentityJWTTicketFactory<TId, TUser, TRole>, IdentityJWTTicketFactory<TId, TUser, TRole>>();
            return builder.AddScheme<IdentityJWTAuthenticationOptions<TId, TUser, TRole>, IdentityJWTAuthenticationHandler<TId, TUser, TRole>>(scheme, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, IdentityJWTAlgorithm<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.HMACSHA256Algorithm>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, IdentityJWTAlgorithm<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.HMACSHA256Algorithm>>(services, secret, serializer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, IdentityJWTAlgorithm<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.HMACSHA256Algorithm>>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, IdentityJWTAlgorithm<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.HMACSHA256Algorithm>>(services, serializer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, IdentityJWTAlgorithmFactory<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.Factory.HMACSHA>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, IdentityJWTAlgorithmFactory<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.Factory.HMACSHA>>(services, secret, serializer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, IdentityJWTAlgorithmFactory<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.Factory.HMACSHA>>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, IdentityJWTAlgorithmFactory<TId, TUser, TRole, NetExtender.JWT.Algorithms.JWT.Factory.HMACSHA>>(services, serializer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm>(services, secret, JWTSerializerType.Unknown);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            return serializer switch
            {
                JWTSerializerType.Unknown => RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, IdentityNoneJWTSerializer<TId, TUser, TRole>>(services, secret),
                JWTSerializerType.TextJson => RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, IdentityTextJsonJWTSerializer<TId, TUser, TRole>>(services, secret),
                JWTSerializerType.Newtonsoft => RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>>(services, secret),
                JWTSerializerType.Other => RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, IdentityNoneJWTSerializer<TId, TUser, TRole>>(services, secret),
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm>(services, JWTSerializerType.Unknown);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole>
        {
            return serializer switch
            {
                JWTSerializerType.Unknown => RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityNoneJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.TextJson => RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityTextJsonJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Newtonsoft => RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityNewtonsoftJWTSerializer<TId, TUser, TRole>>(services),
                JWTSerializerType.Other => RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityNoneJWTSerializer<TId, TUser, TRole>>(services),
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm>(services, secret, JWTSerializerType.Unknown);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            return serializer switch
            {
                JWTSerializerType.Unknown => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, IdentityJWTSerializerFactory<TId, TUser, TRole>.None>(services, secret),
                JWTSerializerType.TextJson => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, IdentityTextJsonJWTSerializerFactory<TId, TUser, TRole>>(services, secret),
                JWTSerializerType.Newtonsoft => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, IdentityNewtonsoftJWTSerializerFactory<TId, TUser, TRole>>(services, secret),
                JWTSerializerType.Other => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, IdentityJWTSerializerFactory<TId, TUser, TRole>.None>(services, secret),
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm>(services, JWTSerializerType.Unknown);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm>(this IServiceCollection services, JWTSerializerType serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole>
        {
            return serializer switch
            {
                JWTSerializerType.Unknown => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityJWTSerializerFactory<TId, TUser, TRole>.None>(services),
                JWTSerializerType.TextJson => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityTextJsonJWTSerializerFactory<TId, TUser, TRole>>(services),
                JWTSerializerType.Newtonsoft => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityNewtonsoftJWTSerializerFactory<TId, TUser, TRole>>(services),
                JWTSerializerType.Other => RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityJWTSerializerFactory<TId, TUser, TRole>.None>(services),
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, TSerializer>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, IdentityJWTAlgorithmEncoder<TId, TUser, TRole>, IdentityJWTAlgorithmDecoder<TId, TUser, TRole>, IdentityJWTUrlEncoder<TId, TUser, TRole>, TSerializer, IdentityJWTValidator<TId, TUser, TRole>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityJWTAlgorithmEncoder<TId, TUser, TRole>, IdentityJWTAlgorithmDecoder<TId, TUser, TRole>, IdentityJWTUrlEncoder<TId, TUser, TRole>, TSerializer, IdentityJWTValidator<TId, TUser, TRole>>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, TSerializer>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, IdentityJWTAlgorithmFactoryEncoder<TId, TUser, TRole>, IdentityJWTAlgorithmFactoryDecoder<TId, TUser, TRole>, IdentityJWTUrlEncoder<TId, TUser, TRole>, TSerializer, IdentityJWTValidator<TId, TUser, TRole>>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, TSerializer>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, IdentityJWTAlgorithmFactoryEncoder<TId, TUser, TRole>, IdentityJWTAlgorithmFactoryDecoder<TId, TUser, TRole>, IdentityJWTUrlEncoder<TId, TUser, TRole>, TSerializer, IdentityJWTValidator<TId, TUser, TRole>>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TTime, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (secret is null)
            {
                throw new ArgumentNullException(nameof(secret));
            }

            if (secret.IsEmpty)
            {
                throw new ArgumentException("The secret is empty.", nameof(secret));
            }

            services.TryAddJWTTimeService<TId, TUser, TRole, TTime>();
            services.TryAddJWTSecret(secret);
            services.TryAddJWTAlgorithm<TId, TUser, TRole, TAlgorithm>();
            services.TryAddJWTEncoder<TId, TUser, TRole, TEncoder>();
            services.TryAddJWTDecoder<TId, TUser, TRole, TDecoder>();
            services.TryAddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>();
            services.TryAddJWTSerializer<TId, TUser, TRole, TSerializer>();
            services.TryAddJWTValidator<TId, TUser, TRole, TValidator>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            return RegisterJWTIdentityServices<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityServices<TId, TUser, TRole, TTime, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithm<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializer<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddJWTTimeService<TId, TUser, TRole, TTime>();
            services.TryAddJWTSecret<TId, TUser, TRole, TSecret>();
            services.TryAddJWTAlgorithm<TId, TUser, TRole, TAlgorithm>();
            services.TryAddJWTEncoder<TId, TUser, TRole, TEncoder>();
            services.TryAddJWTDecoder<TId, TUser, TRole, TDecoder>();
            services.TryAddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>();
            services.TryAddJWTSerializer<TId, TUser, TRole, TSerializer>();
            services.TryAddJWTValidator<TId, TUser, TRole, TValidator>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(services, secret);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TTime, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services, IIdentityJWTSecret<TId, TUser, TRole> secret) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (secret is null)
            {
                throw new ArgumentNullException(nameof(secret));
            }

            if (secret.IsEmpty)
            {
                throw new ArgumentException("The secret is empty.", nameof(secret));
            }

            services.TryAddJWTTimeService<TId, TUser, TRole, TTime>();
            services.TryAddJWTSecret(secret);
            services.TryAddJWTAlgorithmFactory<TId, TUser, TRole, TAlgorithm>();
            services.TryAddJWTEncoder<TId, TUser, TRole, TEncoder>();
            services.TryAddJWTDecoder<TId, TUser, TRole, TDecoder>();
            services.TryAddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>();
            services.TryAddJWTSerializerFactory<TId, TUser, TRole, TSerializer>();
            services.TryAddJWTValidator<TId, TUser, TRole, TValidator>();
            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            return RegisterJWTIdentityFactoryServices<TId, TUser, TRole, IdentityJWTTimeService<TId, TUser, TRole>, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(services);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection RegisterJWTIdentityFactoryServices<TId, TUser, TRole, TTime, TSecret, TAlgorithm, TEncoder, TDecoder, TUrlEncoder, TSerializer, TValidator>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole> where TTime : class, IIdentityJWTTimeService<TId, TUser, TRole> where TSecret : class, IIdentityJWTSecret<TId, TUser, TRole> where TAlgorithm : class, IIdentityJWTAlgorithmFactory<TId, TUser, TRole> where TEncoder : class, IIdentityJWTEncoder<TId, TUser, TRole> where TDecoder : class, IIdentityJWTDecoder<TId, TUser, TRole> where TUrlEncoder : class, IIdentityJWTUrlEncoder<TId, TUser, TRole> where TSerializer : class, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TValidator : class, IIdentityJWTValidator<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddJWTTimeService<TId, TUser, TRole, TTime>();
            services.TryAddJWTSecret<TId, TUser, TRole, TSecret>();
            services.TryAddJWTAlgorithmFactory<TId, TUser, TRole, TAlgorithm>();
            services.TryAddJWTEncoder<TId, TUser, TRole, TEncoder>();
            services.TryAddJWTDecoder<TId, TUser, TRole, TDecoder>();
            services.TryAddJWTUrlEncoder<TId, TUser, TRole, TUrlEncoder>();
            services.TryAddJWTSerializerFactory<TId, TUser, TRole, TSerializer>();
            services.TryAddJWTValidator<TId, TUser, TRole, TValidator>();
            return services;
        }
    }
}