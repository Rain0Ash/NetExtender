using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public abstract class IdentityFactory<TIdentity, TPayload> : IIdentityFactory<TIdentity, TPayload> where TIdentity : IIdentity where TPayload : IEnumerable<KeyValuePair<String, Object?>>
    {
        protected virtual IEnumerable<Claim> Read(TPayload payload)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            foreach (KeyValuePair<String, Object?> pair in payload)
            {
                if (pair.Key is { } key && pair.Value?.ToString() is { } value)
                {
                    yield return new Claim(key, value);
                }
            }
        }

        protected abstract TIdentity CreateIdentity(IEnumerable<Claim> claims);

        public TIdentity CreateIdentity(TPayload payload)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            return CreateIdentity(Read(payload));
        }

        TIdentity IIdentityFactory<TIdentity>.CreateIdentity(Object payload)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload is not TPayload convert)
            {
                throw new ArgumentException($"Payload is not of type '{typeof(TPayload).Name}'.", nameof(payload));
            }

            return CreateIdentity(convert);
        }

        IIdentity IIdentityFactory.CreateIdentity(Object payload)
        {
            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload is not TPayload convert)
            {
                throw new ArgumentException($"Payload is not of type '{typeof(TPayload).Name}'.", nameof(payload));
            }

            return CreateIdentity(Read(convert));
        }

        IIdentity IIdentityFactory.CreateIdentity(Type type, Object payload)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type != typeof(TIdentity))
            {
                throw new NotSupportedException($"Identity type is not '{typeof(TIdentity).Name}'.");
            }

            if (payload is null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload is not TPayload convert)
            {
                throw new ArgumentException($"Payload is not of type '{typeof(TPayload).Name}'.", nameof(payload));
            }

            return CreateIdentity(Read(convert));
        }
    }
}