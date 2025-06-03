// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.DependencyInjection.Attributes;
using NetExtender.JWT;
using NetExtender.JWT.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Identity
{
    public static class IdentityJWTSerializer
    {
        [return: NotNullIfNotNull("serializer")]
        public static IIdentityJWTSerializer<TId, TUser, TRole>? Identity<TId, TUser, TRole>(this IJWTSerializer? serializer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return serializer switch
            {
                null => null,
                IIdentityJWTSerializer<TId, TUser, TRole> result => result,
                { } result => new IdentityJWTSerializer<TId, TUser, TRole>(result)
            };
        }
    }
    
    internal sealed class IdentityNoneJWTSerializer<TId, TUser, TRole> : IdentityTextJsonJWTSerializer<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private static System.Text.Json.JsonSerializerOptions Throw
        {
            get
            {
                throw new NotSupportedException();
            }
        }
        
        private IdentityNoneJWTSerializer()
            : base(Throw)
        {
        }
    }

    public class IdentityTextJsonJWTSerializer<TId, TUser, TRole> : TextJsonJWTSerializer.Serializer, IIdentityJWTSerializer<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        protected IdentityTextJsonJWTSerializer(System.Text.Json.JsonSerializerOptions options)
            : base(options, options)
        {
        }
        
        [DependencyConstructor]
        public IdentityTextJsonJWTSerializer(System.Text.Json.JsonSerializerOptions serialize, System.Text.Json.JsonSerializerOptions deserialize)
            : base(serialize, deserialize)
        {
        }
    }

    public class IdentityNewtonsoftJWTSerializer<TId, TUser, TRole> : NewtonsoftJWTSerializer.Serializer, IIdentityJWTSerializer<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityNewtonsoftJWTSerializer(JsonSerializer serializer)
            : base(serializer)
        {
        }
        
        [DependencyConstructor]
        public IdentityNewtonsoftJWTSerializer(JsonSerializerSettings settings)
            : base(settings)
        {
        }
    }

    public sealed class IdentityJWTSerializer<TId, TUser, TRole> : IIdentityJWTSerializer<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IJWTSerializer Internal { get; }
        
        public JWTSerializerType Type
        {
            get
            {
                return Internal.Type;
            }
        }
        
        public IdentityJWTSerializer(IJWTSerializer serializer)
        {
            Internal = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public String Serialize<T>(T value)
        {
            return Internal.Serialize(value);
        }

        public Object Deserialize(Type type, String json)
        {
            return Internal.Deserialize(type, json);
        }

        public T Deserialize<T>(String json)
        {
            return Internal.Deserialize<T>(json);
        }

        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Internal.Equals(other);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }
    }
}