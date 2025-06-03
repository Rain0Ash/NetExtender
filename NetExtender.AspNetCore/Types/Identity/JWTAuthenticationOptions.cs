using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.JWT;

namespace NetExtender.AspNetCore.Identity
{
    public class JWTAuthenticationOptions : AuthenticationSchemeOptions
    {
        private readonly Type _type = typeof(Dictionary<String, Object>);
        public Type Type
        {
            get
            {
                return _type;
            }
            init
            {
                _type = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public new IJWTAuthenticationEvent Events
        {
            get
            {
                return (IJWTAuthenticationEvent) base.Events!;
            }
            init
            {
                base.Events = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        public JWTKeys Keys { get; init; }
        public Boolean VerifySignature { get; init; } = true;
        public Boolean IncludeAuthenticationScheme { get; init; } = true;
        
        public JWTAuthenticationOptions()
        {
            Events = new JWTAuthenticationLogEvent();
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Type, Events, Keys, VerifySignature, IncludeAuthenticationScheme);
        }
    }
}
