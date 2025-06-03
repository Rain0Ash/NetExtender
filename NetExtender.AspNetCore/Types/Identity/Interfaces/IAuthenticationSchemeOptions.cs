// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IAuthenticationSchemeOptions
    {
        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ClaimsIssuer"/>
        public String? ClaimsIssuer { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.Events"/>
        public Object? Events { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.EventsType"/>
        public Type? EventsType { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardDefault"/>
        public String? ForwardDefault { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardDefaultSelector"/>
        public Func<HttpContext, String?>? ForwardDefaultSelector { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardAuthenticate"/>
        public String? ForwardAuthenticate { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardChallenge"/>
        public String? ForwardChallenge { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardForbid"/>
        public String? ForwardForbid { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardSignIn"/>
        public String? ForwardSignIn { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.ForwardSignOut"/>
        public String? ForwardSignOut { get; }

        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.Validate()"/>
        public void Validate();
        
        /// <inheritdoc cref="Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions.Validate(System.String)"/>
        public void Validate(String scheme);
    }
}