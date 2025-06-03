// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityFactory<out TIdentity, in TPayload> : IIdentityFactory<TIdentity> where TIdentity : IIdentity where TPayload : IEnumerable<KeyValuePair<String, Object?>>
    {
        public TIdentity CreateIdentity(TPayload payload);
    }

    public interface IIdentityFactory<out TIdentity> : IIdentityFactory where TIdentity : IIdentity
    {
        public new TIdentity CreateIdentity(Object payload);
    }
    
    public interface IIdentityFactory
    {
        public IIdentity CreateIdentity(Object payload);
        public IIdentity CreateIdentity(Type type, Object payload);
    }
}