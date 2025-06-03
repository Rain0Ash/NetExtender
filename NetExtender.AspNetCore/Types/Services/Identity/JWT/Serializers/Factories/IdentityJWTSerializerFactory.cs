// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;
using NetExtender.JWT.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class IdentityJWTSerializerFactory<TId, TUser, TRole> : IIdentityJWTSerializerFactory<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private IJWTSerializerFactory Factory { get; }
        private ConditionalWeakTable<IJWTSerializer, IIdentityJWTSerializer<TId, TUser, TRole>> Storage { get; } = new ConditionalWeakTable<IJWTSerializer, IIdentityJWTSerializer<TId, TUser, TRole>>();
        
        public JWTSerializerType Type
        {
            get
            {
                return Factory.Type;
            }
        }

        public IdentityJWTSerializerFactory(JWTSerializerType serializer)
        {
            Factory = serializer switch
            {
                JWTSerializerType.Unknown => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null),
                JWTSerializerType.TextJson => new TextJson(),
                JWTSerializerType.Newtonsoft => new Newtonsoft(),
                JWTSerializerType.Other => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null),
                _ => throw new EnumUndefinedOrNotSupportedException<JWTSerializerType>(serializer, nameof(serializer), null)
            };
        }

        public IdentityJWTSerializerFactory(IJWTSerializer serializer)
        {
            Factory = serializer is not null ? new InstanceJWTSerializerFactory(serializer) : throw new ArgumentNullException(nameof(serializer));
        }

        public IdentityJWTSerializerFactory(IJWTSerializerFactory factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IJWTSerializer Create()
        {
            return Factory.Create() is { } serializer ? Storage.GetValue(serializer, static serializer => serializer.Identity<TId, TUser, TRole>()) : null!;
        }
        
        internal sealed class None : IdentityTextJsonJWTSerializerFactory<TId, TUser, TRole>
        {
            private static TextJsonJWTSerializer Throw
            {
                get
                {
                    throw new NotSupportedException();
                }
            }
        
            private None()
                : base(Throw)
            {
            }
        }

        public class TextJson : IdentityTextJsonJWTSerializerFactory<TId, TUser, TRole>
        {
            public TextJson()
                : base(new TextJsonJWTSerializer.Serializer())
            {
            }
        }

        public class Newtonsoft : IdentityNewtonsoftJWTSerializerFactory<TId, TUser, TRole>
        {
            public Newtonsoft()
                : base(new NewtonsoftJWTSerializer.Serializer())
            {
            }
        }
    }
    
    public class IdentityTextJsonJWTSerializerFactory<TId, TUser, TRole> : TextJsonJWTSerializerFactory.Factory, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityTextJsonJWTSerializerFactory(TextJsonJWTSerializer serializer)
            : base(serializer)
        {
        }
    }
    
    public class IdentityNewtonsoftJWTSerializerFactory<TId, TUser, TRole> : NewtonsoftJWTSerializerFactory.Factory, IIdentityJWTSerializerFactory<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public IdentityNewtonsoftJWTSerializerFactory(NewtonsoftJWTSerializer serializer)
            : base(serializer)
        {
        }
    }
}