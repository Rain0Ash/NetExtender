using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Entities.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.AspNetCore.Identity
{
    public readonly struct IdentityJWTPayload<TId, TUser, TRole> : IEntityId<TId>, IStruct<IdentityJWTPayload<TId, TUser, TRole>> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        private TUser User { get; }
        
        [JsonProperty]
        public TId Id
        {
            get
            {
                return User?.Id ?? throw new InvalidOperationException();
            }
        }

        [JsonProperty]
        public IReadOnlySet<TRole> Roles
        {
            get
            {
                return User is not null ? User.Roles : throw new InvalidOperationException();
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Int32 Expire { get; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return User is null;
            }
        }
        
        public IdentityJWTPayload(TUser user, DateTime expire)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Expire = (Int32) Math.Round(expire.SinceEpoch().TotalSeconds);
        }

        TId IEntity<TId>.Get()
        {
            return Id;
        }
    }
}