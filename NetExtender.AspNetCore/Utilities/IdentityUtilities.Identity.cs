// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static partial class IdentityUtilities
    {
    }
    
    public class IdentityOptions
    {
        public Maybe<String?> AuthenticationScheme { get; init; } = new Maybe<String?>(null);
        public Maybe<Action<AuthenticationOptions>?> Authentication { get; init; }
        public Maybe<Action<AuthorizationOptions>?> Authorization { get; init; } = new Maybe<Action<AuthorizationOptions>?>(null);
        public Func<AuthenticationBuilder, AuthenticationBuilder>? Builder { get; init; } = static builder => builder.AddCookie(IdentityConstants.ApplicationScheme);
    }

    public record EmailIdentityRoute : IdentityRoute
    {
        private Action<EndpointBuilder> _confirm;
        public Action<EndpointBuilder> Confirm
        {
            get
            {
                return _confirm;
            }
            set
            {
                _confirm = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public EmailIdentityRoute(HttpMethod method, Action<EndpointBuilder> confirm, [CallerMemberName] String? route = null)
            : base(method, route)
        {
            _confirm = confirm ?? throw new ArgumentNullException(nameof(confirm));
        }

        public EmailIdentityRoute(String? route, HttpMethod method, Action<EndpointBuilder> confirm)
            : base(route, method)
        {
            _confirm = confirm ?? throw new ArgumentNullException(nameof(confirm));
        }
    }
    
    public record IdentityRoute : RouteHandler
    {
        public IdentityRoute(HttpMethod method, [CallerMemberName] String? route = null)
            : base(method, route)
        {
        }

        public IdentityRoute(String? route, HttpMethod method)
            : base(route, method)
        {
        }
    }

    public record IdentityNaming
    {
        private JsonNamingPolicy _arguments = JsonKnownNamingPolicy.Unspecified.ToNamingPolicy();
        public JsonNamingPolicy Arguments
        {
            get
            {
                return _arguments;
            }
            set
            {
                _arguments = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        private JsonNamingPolicy _controllers = JsonKnownNamingPolicy.Unspecified.ToNamingPolicy();
        public JsonNamingPolicy Controllers
        {
            get
            {
                return _controllers;
            }
            set
            {
                _controllers = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        private JsonNamingPolicy _routes = JsonKnownNamingPolicy.Unspecified.ToNamingPolicy();
        public JsonNamingPolicy Routes
        {
            get
            {
                return _routes;
            }
            set
            {
                _routes = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        private JsonNamingPolicy _endpoints = JsonKnownNamingPolicy.Unspecified.ToNamingPolicy();
        public JsonNamingPolicy Endpoints
        {
            get
            {
                return _endpoints;
            }
            set
            {
                _endpoints = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }

    public record IdentityOptions<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private readonly IdentityNaming _naming = new IdentityNaming();
        public IdentityNaming Naming
        {
            get
            {
                return _naming;
            }
            init
            {
                _naming = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public String? MapGroup { get; set; } = "Api";
        public String? Tag { get; set; } = "Identity";
        public IdentityRoute? Login { get; set; }
        public IdentityRoute? Register { get; set; }
        public IdentityRoute? Refresh { get; set; }
        public EmailIdentityRoute? ConfirmEmail { get; set; }
        public IdentityRoute? ResendConfirmEmail { get; set; }
        public IdentityRoute? ForgotPassword { get; set; }
        public IdentityRoute? ResetPassword { get; set; }

        private IdentityAccountOptions<TId, TUser, TRole>? _account;
        public IdentityAccountOptions<TId, TUser, TRole> Account
        {
            get
            {
                return _account ??= new IdentityAccountOptions<TId, TUser, TRole>();
            }
            set
            {
                _account = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private IdentitySpecialOptions<TId, TUser, TRole>? _special;
        public IdentitySpecialOptions<TId, TUser, TRole> Special
        {
            get
            {
                return _special ??= new IdentitySpecialOptions<TId, TUser, TRole>();
            }
            set
            {
                _special = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }

    // ReSharper disable once UnusedTypeParameter
    public record IdentityAccountOptions<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        public String? MapGroup { get; set; } = "Account";
        public IdentityRoute? GetInfo { get; set; }
        public IdentityRoute? SetInfo { get; set; }
        public IdentityRoute? TwoFactor { get; set; }
    }

    // ReSharper disable once UnusedTypeParameter
    public record IdentitySpecialOptions<TId, TUser, TRole> where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
    {
        private Dictionary<String, String> Map { get; } = new Dictionary<String, String>(8);
        private HashSet<String> Routes { get; } = new HashSet<String>(StringComparer.OrdinalIgnoreCase);
        
        public String ChangeEmail
        {
            get
            {
                return Get(nameof(ChangeEmail), "ChangedEmail");
            }
            set
            {
                Set(nameof(ChangeEmail), value);
            }
        }

        public String UserId
        {
            get
            {
                return Get(nameof(UserId), nameof(UserId));
            }
            set
            {
                Set(nameof(UserId), value);
            }
        }

        public String Code
        {
            get
            {
                return Get(nameof(Code), nameof(Code));
            }
            set
            {
                Set(nameof(Code), value);
            }
        }

        public Byte RecoveryCodes { get; set; } = 10;

        protected String Get(String key, String @default)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Map.TryGetValue(key, out String? value))
            {
                return value;
            }

            Map[key] = @default;
            Routes.Add(@default);
            return @default;
        }

        protected void Set(String key, String? value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            String? old;
            if (value is null)
            {
                if (Map.Remove(key, out old))
                {
                    Routes.Remove(old);
                }
                
                return;
            }

            if (value.Length <= 0)
            {
                throw new ArgumentException($"Argument '{nameof(value)}' cannot be empty.", nameof(value));
            }

            if (Routes.Contains(value))
            {
                throw new ArgumentException($"Route '{nameof(value)}' is already used.", nameof(value));
            }

            if (Map.TryGetValue(key, out old))
            {
                Routes.Remove(old);
            }

            Map[key] = value;
            Routes.Add(value);
        }
    }

    public sealed record CreateIdentityOptions
    {
        public static CreateIdentityOptions Default { get; } = new CreateIdentityOptions();

        private readonly IdentityNaming _naming = new IdentityNaming();
        public IdentityNaming Naming
        {
            get
            {
                return _naming;
            }
            init
            {
                _naming = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Boolean Bearer { get; init; }
    }
}