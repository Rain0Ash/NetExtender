// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Identity;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.AspNetCore.Types.Results;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads.Result;
using NetExtender.Types.Strings;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

#if NET8_0_OR_GREATER
using Microsoft.Extensions.Options;
#else
using System.Reflection;
using NetExtender.Utilities.Numerics;
#endif

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static partial class IdentityUtilities
    {
        private static IServiceCollection Identity(this IServiceCollection services, IdentityOptions? options)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            options ??= new IdentityOptions();
            
            if (options.Authentication.HasValue)
            {
                AuthenticationBuilder builder = options.Authentication.Value is { } authentication ? services.AddAuthentication(authentication) : services.AddAuthentication();
                options.Builder?.Invoke(builder);
            }
            else if (options.AuthenticationScheme.HasValue)
            {
                AuthenticationBuilder builder = options.AuthenticationScheme.Value is { } scheme ? services.AddAuthentication(scheme) : services.AddAuthentication();
                options.Builder?.Invoke(builder);
            }
            
            if (options.Authorization.HasValue)
            {
                services = options.Authorization.Value is { } authorization ? services.AddAuthorization(authorization) : services.AddAuthorization();
            }

            return services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder Identity<TRole>(this IServiceCollection services) where TRole : class, IEquatable<TRole>
        {
            return Identity<TRole>(services, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder Identity<TRole>(this IServiceCollection services, IdentityOptions? options) where TRole : class, IEquatable<TRole>
        {
            return Identity<StringId, NetExtender.AspNetCore.Identity.IdentityUser<TRole>, TRole>(services, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder IdentityCore<TId, TUser, TRole>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            return IdentityCore<TId, TUser, TRole>(services, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder IdentityCore<TId, TUser, TRole>(this IServiceCollection services, IdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            return Identity(services, options).AddIdentityCore<TUser>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder Identity<TId, TUser, TRole>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : class, IEquatable<TRole>
        {
            return Identity<TId, TUser, TRole>(services, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder Identity<TId, TUser, TRole>(this IServiceCollection services, IdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : class, IEquatable<TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            return Identity(services, options).AddIdentity<TUser, TRole>();
        }

#if NET8_0_OR_GREATER
        public static IdentityBuilder RegisterIdentityServices<TId, TUser, TRole>(this IdentityBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole> where TRole : IEquatable<TRole>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            builder.Services.RegisterJWTIdentityServices<TId, TUser, TRole>()
                .Add<IIdentityTimeService<TId, TUser, TRole>, IdentityTimeServiceWrapper<TId, TUser, TRole>>(builder.Services.Find<TimeProvider>(ServiceLifetime.Singleton))
                .Add<IIdentityEmailService<TId, TUser, TRole>, IdentityEmailServiceWrapper<TId, TUser, TRole>>(builder.Services.Find(typeof(IEmailSender<>), ServiceLifetime.Singleton))
                .Add<IIdentityBearerService<TId, TUser, TRole>, BearerTokenOptionsMonitorWrapper<TId, TUser, TRole>>(builder.Services.Find<IOptionsMonitor<BearerTokenOptions>>(ServiceLifetime.Singleton));
            return builder;
        }

        public static IdentityBuilder RegisterIdentityServices<TId, TUser, TRole>(this IdentityBuilder builder, ServiceLifetime lifetime) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return RegisterIdentityServices<TId, TUser, TRole, TimeIdentityServiceWrapper<TId, TUser, TRole>, EmailIdentityServiceWrapper<TId, TUser, TRole>, BearerTokenOptionsMonitorWrapper<TId, TUser, TRole>>(builder, lifetime);
        }
#endif
        
        private static class SignInManagerAuthenticationScheme<TUser> where TUser : class
        {
#if !NET8_0_OR_GREATER
            [ReflectionNaming]
            private static Action<SignInManager<TUser>, String>? AuthenticationScheme { get; }
            
            static SignInManagerAuthenticationScheme()
            {
                try
                {
                    AuthenticationScheme = ExpressionUtilities.CreateSetExpression<SignInManager<TUser>, String>(nameof(AuthenticationScheme)).Compile();
                }
                catch (Exception)
                {
                    AuthenticationScheme = null;
                }
            }
#endif
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean Set(SignInManager<TUser> manager, String scheme)
            {
                if (manager is null)
                {
                    throw new ArgumentNullException(nameof(manager));
                }

#if NET8_0_OR_GREATER
                manager.AuthenticationScheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
                return true;
#else
                Action<SignInManager<TUser>, String>? setter = AuthenticationScheme;
                setter?.Invoke(manager, scheme);
                return setter is not null;
#endif
            }
        }
        
        private static void SetAuthenticationScheme<TUser>(this SignInManager<TUser> manager, Boolean cookie) where TUser : class
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (!SignInManagerAuthenticationScheme<TUser>.Set(manager, cookie ? IdentityConstants.ApplicationScheme : BearerScheme.BearerIdentityScheme))
            {
                throw new NotSupportedException("SignIn authentication scheme is not supported.");
            }
        }

        [ReflectionNaming]
        private static class MapGroup
        {
#if !NET7_0_OR_GREATER
            private static Func<IEndpointRouteBuilder, String, IEndpointRouteBuilder>? GroupMap { get; }
            private static Func<IEndpointRouteBuilder, RoutePattern, IEndpointRouteBuilder>? RouteGroupMap { get; }
            
            static MapGroup()
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public;
                GroupMap = typeof(EndpointRouteBuilderExtensions).GetMethod(nameof(MapGroup), binding, new []{ typeof(IEndpointRouteBuilder), typeof(String) })?.CreateDelegate<Func<IEndpointRouteBuilder, String, IEndpointRouteBuilder>>();
                RouteGroupMap = typeof(EndpointRouteBuilderExtensions).GetMethod(nameof(MapGroup), binding, new []{ typeof(IEndpointRouteBuilder), typeof(RoutePattern) })?.CreateDelegate<Func<IEndpointRouteBuilder, RoutePattern, IEndpointRouteBuilder>>();
            }
#endif
            public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder, String prefix)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }
                
#if !NET7_0_OR_GREATER
                return GroupMap?.Invoke(builder, prefix) ?? throw new NotSupportedException("Group map is not supported.");
#else
                return builder.MapGroup(prefix);
#endif
            }

            public static IEndpointRouteBuilder Map(IEndpointRouteBuilder builder, RoutePattern prefix)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }
                
#if !NET7_0_OR_GREATER
                return RouteGroupMap?.Invoke(builder, prefix) ?? throw new NotSupportedException("Group map is not supported.");
#else
                return builder.MapGroup(prefix);
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder RegisterIdentityServices<TId, TUser, TRole, TTime, TEmail, TBearer>(this IdentityBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TTime : class, IIdentityTimeService<TId, TUser, TRole> where TEmail : class, IIdentityEmailService<TId, TUser, TRole> where TBearer : class, IIdentityBearerService<TId, TUser, TRole>
        {
            return RegisterIdentityServices<TId, TUser, TRole, TTime, TEmail, TBearer>(builder, ServiceLifetime.Singleton);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityBuilder RegisterIdentityServices<TId, TUser, TRole, TTime, TEmail, TBearer>(this IdentityBuilder builder, ServiceLifetime lifetime) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TTime : class, IIdentityTimeService<TId, TUser, TRole> where TEmail : class, IIdentityEmailService<TId, TUser, TRole> where TBearer : class, IIdentityBearerService<TId, TUser, TRole>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.Add<IIdentityTimeService<TId, TUser, TRole>, TTime>(lifetime).Add<IIdentityEmailService<TId, TUser, TRole>, TEmail>(lifetime).Add<IIdentityBearerService<TId, TUser, TRole>, TBearer>(lifetime);
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityOptions<TId, TUser, TRole> CreateIdentityOptions<TId, TUser, TRole>(this IEndpointRouteBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
        {
            return CreateIdentityOptions<TId, TUser, TRole>(builder, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IdentityOptions<TId, TUser, TRole> CreateIdentityOptions<TId, TUser, TRole>(this IEndpointRouteBuilder builder, CreateIdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
        {
            return CreateIdentityOptions<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(builder, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOptions CreateIdentityOptions<TId, TUser, TRole, TOptions>(this IEndpointRouteBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TOptions : IdentityOptions<TId, TUser, TRole>, new()
        {
            return CreateIdentityOptions<TId, TUser, TRole, TOptions>(builder, null);
        }

        // ReSharper disable once CognitiveComplexity
        [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
        [SuppressMessage("ReSharper", "LocalFunctionHidesMethod")]
        public static TOptions CreateIdentityOptions<TId, TUser, TRole, TOptions>(this IEndpointRouteBuilder builder, CreateIdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TOptions : IdentityOptions<TId, TUser, TRole>, new()
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            options ??= Types.CreateIdentityOptions.Default;
            IIdentityBearerService<TId, TUser, TRole> bearer = builder.ServiceProvider.GetRequiredService<IIdentityBearerService<TId, TUser, TRole>>();
            IIdentityTimeService<TId, TUser, TRole> identityTime = builder.ServiceProvider.GetRequiredService<IIdentityTimeService<TId, TUser, TRole>>();
            IIdentityEmailService<TId, TUser, TRole> mail = builder.ServiceProvider.GetRequiredService<IIdentityEmailService<TId, TUser, TRole>>();
            LinkGenerator link = builder.ServiceProvider.GetRequiredService<LinkGenerator>();
            
            String? confirm = null;

            IdentityOptionsBuilder<TOptions> identity = new IdentityOptionsBuilder<TOptions>();

            async Task<AspResult> BearerLogin([FromBody] LoginRequest request, [FromServices] IServiceProvider provider)
            {
                return await Login(request, false, false, provider);
            }

            async Task<AspResult> Login([FromBody] LoginRequest request, [FromQuery] Boolean? UseCookies, [FromQuery] Boolean? UseSessionCookies, [FromServices] IServiceProvider provider)
            {
                SignInManager<TUser> manager = provider.GetRequiredService<SignInManager<TUser>>();
                Boolean persistent = UseCookies is true && UseSessionCookies is not true;

                manager.SetAuthenticationScheme(UseCookies is true || UseSessionCookies is true);
                
                SignInResult result = await manager.PasswordSignInAsync(request.Email, request.Password, persistent, true);

                if (!result.RequiresTwoFactor)
                {
                    return result.Succeeded ? AspResult.None : new BusinessUnauthorized401Exception("Login failed.");
                }

                if (!String.IsNullOrEmpty(request.TwoFactorCode))
                {
                    result = await manager.TwoFactorAuthenticatorSignInAsync(request.TwoFactorCode, persistent, persistent);
                }
                else if (!String.IsNullOrEmpty(request.TwoFactorRecoveryCode))
                {
                    result = await manager.TwoFactorRecoveryCodeSignInAsync(request.TwoFactorRecoveryCode);
                }

                return result.Succeeded ? AspResult.None : new BusinessUnauthorized401Exception("Login failed.");
            }

            async Task<AspResult> Register([FromBody] RegisterRequest request, HttpContext context, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();

                if (!manager.SupportsUserEmail)
                {
                    throw new NotSupportedException($"{nameof(MapIdentityApi)} requires a user store with email support.");
                }

                IUserStore<TUser> users = provider.GetRequiredService<IUserStore<TUser>>();
                IUserEmailStore<TUser> emails = (IUserEmailStore<TUser>) users;
                String email = request.Email;

                if (String.IsNullOrEmpty(email) || !EmailUtilities.Validate(email))
                {
                    IdentityError error = manager.ErrorDescriber.InvalidEmail(email);
                    return new BusinessBadRequest400Exception<String>(error.Description, error.Code);
                }

                TUser user = new TUser();
                await users.SetUserNameAsync(user, email, CancellationToken.None);
                await emails.SetEmailAsync(user, email, CancellationToken.None);
                IdentityResult result = await manager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return new BusinessBadRequest400Exception("Register failed.", result.ToBusinessException());
                }

                await SendConfirmationEmailAsync(user, manager, context, email);
                return default(BusinessResult);
            }

            async Task<AspResult> Refresh([FromBody] RefreshRequest request, HttpContext context, [FromServices] IServiceProvider provider)
            {
                SignInManager<TUser> manager = provider.GetRequiredService<SignInManager<TUser>>();
                AuthenticationTicket? ticket = bearer.RefreshTokenProtector.Unprotect(request.RefreshToken);

                // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                if (ticket?.Properties?.ExpiresUtc is not { } expire || identityTime.GetUtcNow() >= expire || await manager.ValidateSecurityStampAsync(ticket.Principal) is not { } user)
                {
                    await context.ChallengeAsync((AuthenticationProperties?) null);
                    return AspResult.None;
                }

                ClaimsPrincipal principal = await manager.CreateUserPrincipalAsync(user);
                await context.SignInAsync(BearerScheme.BearerIdentityScheme, principal, null);
                return AspResult.None;
            }
            
            async Task<AspResult> ConfirmEmail([FromQuery] String UserId, [FromQuery] String Code, [FromQuery] String? ChangedEmail, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                
                if (await manager.FindByIdAsync(UserId) is not { } user)
                {
                    return new BusinessUnauthorized401Exception();
                }

                try
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
                }
                catch (FormatException)
                {
                    return new BusinessBadRequest400Exception<String>("Code invalid format.", "InvalidCodeFormat");
                }

                IdentityResult result;

                if (String.IsNullOrEmpty(ChangedEmail))
                {
                    result = await manager.ConfirmEmailAsync(user, Code);
                }
                else
                {
                    result = await manager.ChangeEmailAsync(user, ChangedEmail, Code);

                    if (result.Succeeded)
                    {
                        result = await manager.SetUserNameAsync(user, ChangedEmail);
                    }
                }

                return result.Succeeded ? default(BusinessResult) : new BusinessUnauthorized401Exception<String>("Invalid email confirmation.", "InvalidEmailConfirmation");
            }

            void ConfirmEmailPattern(EndpointBuilder builder)
            {
                String? pattern = ((RouteEndpointBuilder) builder).RoutePattern.RawText;
                confirm = $"{nameof(MapIdentityApi)}-{pattern}";
                builder.Metadata.Add(new EndpointNameMetadata(confirm));
            }

            async Task<AspResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, HttpContext context, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                
                if (await manager.FindByEmailAsync(request.Email) is not { } user)
                {
                    return new BusinessNotFound404Exception("User with email not found.");
                }

                await SendConfirmationEmailAsync(user, manager, context, request.Email);
                return default(BusinessResult);
            }
            
            async Task<AspResult> ForgotPassword([FromBody] ForgotPasswordRequest request, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                TUser? user = await manager.FindByEmailAsync(request.Email);

                if (user is null || !await manager.IsEmailConfirmedAsync(user))
                {
                    return default(BusinessResult);
                }

                String code = await manager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                await mail.SendPasswordResetCodeAsync(user, request.Email, HtmlEncoder.Default.Encode(code));
                return default(BusinessResult);
            }

            async Task<AspResult> ResetPassword([FromBody] ResetPasswordRequest request, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                TUser? user = await manager.FindByEmailAsync(request.Email);

                if (user is null || !await manager.IsEmailConfirmedAsync(user))
                {
                    IdentityError error = manager.ErrorDescriber.InvalidToken();
                    return new BusinessBadRequest400Exception<String>(error.Description, error.Code);
                }

                try
                {
                    String code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.ResetCode));
                    IdentityResult result = await manager.ResetPasswordAsync(user, code, request.NewPassword);
                    return result.Succeeded ? default(BusinessResult) : new BusinessBadRequest400Exception("Reset password failed", result.ToBusinessException());
                }
                catch (FormatException)
                {
                    IdentityError error = manager.ErrorDescriber.InvalidToken();
                    return new BusinessBadRequest400Exception<String>(error.Description, error.Code);
                }
            }

            async Task<AspResult<InfoResponse>> GetInfo(ClaimsPrincipal principal, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                
                if (await manager.GetUserAsync(principal) is not { } user)
                {
                    return new BusinessNotFound404Exception<String>("User not found.", "UserNotFound");
                }

                return await manager.GetEmailAsync(user) is { } email ? new InfoResponse
                {
                    Email = email,
                    IsEmailConfirmed = await manager.IsEmailConfirmedAsync(user)
                } : new BusinessForbidden403Exception<String>("User must have an email.", "UserEmailRequired");
            }

            async Task<AspResult<InfoResponse>> SetInfo([FromBody] InfoRequest request, HttpContext context, ClaimsPrincipal principal, [FromServices] IServiceProvider provider)
            {
                UserManager<TUser> manager = provider.GetRequiredService<UserManager<TUser>>();
                
                if (await manager.GetUserAsync(principal) is not { } user)
                {
                    return new BusinessNotFound404Exception("User not found.");
                }

                if (!String.IsNullOrEmpty(request.NewEmail) && !EmailUtilities.Validate(request.NewEmail))
                {
                    IdentityError error = manager.ErrorDescriber.InvalidEmail(request.NewEmail);
                    return new BusinessBadRequest400Exception<String>(error.Description, error.Code);
                }

                if (!String.IsNullOrEmpty(request.NewPassword))
                {
                    if (String.IsNullOrEmpty(request.OldPassword))
                    {
                        return new BusinessBadRequest400Exception<String>("Old password required.", "OldPasswordRequired")
                        {
                            Description = $"The old password is required to set a new password. If the old password is forgotten, {(identity.Options.ResetPassword is not null ? $"use '/{identity.Options.Naming.Endpoints.ConvertName(identity.Options.ResetPassword.Endpoint)}'" : "you can't reset it.")}."
                        };
                    }

                    IdentityResult result = await manager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                    if (!result.Succeeded)
                    {
                        return new BusinessBadRequest400Exception("Profile information update failed.", result.ToBusinessException());
                    }
                }

                if (!String.IsNullOrEmpty(request.NewEmail) && await manager.GetEmailAsync(user) != request.NewEmail)
                {
                    await SendConfirmationEmailAsync(user, manager, context, request.NewEmail, true);
                }
                
                return await manager.GetEmailAsync(user) is { } email ? new InfoResponse
                {
                    Email = email,
                    IsEmailConfirmed = await manager.IsEmailConfirmedAsync(user)
                } : new BusinessForbidden403Exception<String>("User must have an email.", "UserEmailRequired");
            }
            
            async Task<AspResult<TwoFactorResponse>> TwoFactor([FromBody] TwoFactorRequest request, ClaimsPrincipal principal, [FromServices] IServiceProvider provider)
            {
                SignInManager<TUser> manager = provider.GetRequiredService<SignInManager<TUser>>();
                
                if (await manager.UserManager.GetUserAsync(principal) is not { } user)
                {
                    return new BusinessNotFound404Exception("User not found.");
                }

                if (request.Enable is true)
                {
                    if (request.ResetSharedKey)
                    {
                        return new BusinessBadRequest400Exception<String>("Can't reset shared key.", "CannotResetSharedKeyAndEnable")
                        {
                            Description = "Resetting the 2FA shared key must disable 2FA until a 2FA token based on the new shared key is validated."
                        };
                    }

                    if (String.IsNullOrEmpty(request.TwoFactorCode))
                    {
                        return new BusinessBadRequest400Exception<String>("The two-factor authentication code required.", "RequiresTwoFactor")
                        {
                            Description = "No 2FA token was provided by the request. A valid 2FA token is required to enable 2FA."
                        };
                    }

                    if (!await manager.UserManager.VerifyTwoFactorTokenAsync(user, manager.UserManager.Options.Tokens.AuthenticatorTokenProvider, request.TwoFactorCode))
                    {
                        return new BusinessForbidden403Exception<String>("Invalid two-factor code.", "InvalidTwoFactorCode")
                        {
                            Description = "The 2FA token provided by the request was invalid. A valid 2FA token is required to enable 2FA."
                        };
                    }

                    await manager.UserManager.SetTwoFactorEnabledAsync(user, true);
                }
                else if (request.Enable is false || request.ResetSharedKey)
                {
                    await manager.UserManager.SetTwoFactorEnabledAsync(user, false);
                }

                if (request.ResetSharedKey)
                {
                    await manager.UserManager.ResetAuthenticatorKeyAsync(user);
                }

                String[]? recovery = null;
                if (request.ResetRecoveryCodes || request.Enable is true && await manager.UserManager.CountRecoveryCodesAsync(user) <= 0)
                {
                    recovery = (await manager.UserManager.GenerateNewTwoFactorRecoveryCodesAsync(user, identity.Options.Special.RecoveryCodes))?.ToArray();
                }

                if (request.ForgetMachine)
                {
                    await manager.ForgetTwoFactorClientAsync();
                }

                String? key = await manager.UserManager.GetAuthenticatorKeyAsync(user);
                if (String.IsNullOrEmpty(key))
                {
                    await manager.UserManager.ResetAuthenticatorKeyAsync(user);
                    key = await manager.UserManager.GetAuthenticatorKeyAsync(user);

                    if (String.IsNullOrEmpty(key))
                    {
                        throw new NotSupportedException("The user manager must produce an authenticator key after reset.");
                    }
                }

                return new TwoFactorResponse
                {
                    SharedKey = key,
                    RecoveryCodes = recovery,
                    RecoveryCodesLeft = recovery?.Length ?? await manager.UserManager.CountRecoveryCodesAsync(user),
                    IsTwoFactorEnabled = await manager.UserManager.GetTwoFactorEnabledAsync(user),
                    IsMachineRemembered = await manager.IsTwoFactorClientRememberedAsync(user)
                };
            }
            
            async Task SendConfirmationEmailAsync(TUser user, UserManager<TUser> manager, HttpContext context, String email, Boolean change = false)
            {
                if (confirm is null)
                {
                    throw new NotSupportedException("No email confirmation endpoint.");
                }

                String code = change ? await manager.GenerateChangeEmailTokenAsync(user, email) : await manager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                String id = await manager.GetUserIdAsync(user);
                RouteValueDictionary values = new RouteValueDictionary
                {
                    [options.Naming.Routes.ConvertName(identity.Options.Special.UserId)] = id,
                    [options.Naming.Routes.ConvertName(identity.Options.Special.Code)] = code
                };

                if (change)
                {
                    values.Add(options.Naming.Routes.ConvertName(identity.Options.Special.ChangeEmail), email);
                }

                String uri = link.GetUriByName(context, confirm, values) ?? throw new NotSupportedException($"Could not find endpoint named '{confirm}'.");
                await mail.SendConfirmationLinkAsync(user, email, HtmlEncoder.Default.Encode(uri));
            }
            
            return identity.Options = new TOptions
            {
                Login = new IdentityRoute(nameof(Login), HttpMethod.Post) { Handler = options.Bearer ? BearerLogin : Login },
                Register = new IdentityRoute(nameof(Register), HttpMethod.Post) { Handler = Register },
                Refresh = new IdentityRoute(nameof(Refresh), HttpMethod.Post) { Handler = Refresh },
                ConfirmEmail = new EmailIdentityRoute(nameof(ConfirmEmail), HttpMethod.Get, ConfirmEmailPattern) { Handler = ConfirmEmail },
                ResendConfirmEmail = new IdentityRoute(nameof(ResendConfirmationEmail), HttpMethod.Post) { Handler = ResendConfirmationEmail },
                ForgotPassword = new IdentityRoute(nameof(ForgotPassword), HttpMethod.Post) { Handler = ForgotPassword },
                ResetPassword = new IdentityRoute(nameof(ResetPassword), HttpMethod.Post) { Handler = ResetPassword },
                Account = new IdentityAccountOptions<TId, TUser, TRole>
                {
                    GetInfo = new IdentityRoute("Info", HttpMethod.Get) { Handler = GetInfo },
                    SetInfo = new IdentityRoute("Info", HttpMethod.Post) { Handler = SetInfo },
                    TwoFactor = new IdentityRoute("2FA", HttpMethod.Post) { Handler = TwoFactor }
                },
                Special = new IdentitySpecialOptions<TId, TUser, TRole>()
            };
        }

        [return: NotNullIfNotNull("builder")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEndpointConventionBuilder? SetTag(IEndpointConventionBuilder? builder, String? tag)
        {
#if NET7_0_OR_GREATER
            return tag is not null ? builder?.WithTags(tag) : builder;
#else
            return tag is not null ? builder?.WithMetadata(new TagsAttribute(tag)) : builder;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEndpointConventionBuilder? Map<TId, TUser, TRole, TOptions>(IEndpointRouteBuilder builder, TOptions options, Func<TOptions, IdentityRoute?>? selector) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TOptions : IdentityOptions<TId, TUser, TRole>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (selector?.Invoke(options) is not { } route)
            {
                return null;
            }
            
            IEndpointConventionBuilder? result = route.Handler switch
            {
                null => null,
                RequestDelegate @delegate => builder.Map(options.Naming.Endpoints.ConvertName(route.Endpoint), route.Method, @delegate),
                { } @delegate => builder.Map(options.Naming.Endpoints.ConvertName(route.Endpoint), route.Method, @delegate)
            };

            return SetTag(result, options.Tag is { } tag ? options.Naming.Controllers.ConvertName(tag) : null);
        }

#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#else
        public static IEndpointRouteBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#endif
        {
            return MapIdentityApi<TId, TUser, TRole>(builder, false);
        }
        
#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, Boolean bearer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#else
        public static IEndpointRouteBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, Boolean bearer) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#endif
        {
            return MapIdentityApi<TId, TUser, TRole>(builder, new CreateIdentityOptions { Bearer = bearer });
        }
        
#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, CreateIdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#else
        public static IEndpointRouteBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, CreateIdentityOptions? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#endif
        {
            return MapIdentityApi(builder, CreateIdentityOptions<TId, TUser, TRole>(builder, options));
        }
        
#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, IdentityOptions<TId, TUser, TRole> options) where TId : IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#else
        // ReSharper disable once ReturnTypeCanBeNotNullable
        public static IEndpointRouteBuilder? MapIdentityApi<TId, TUser, TRole>(this IEndpointRouteBuilder builder, IdentityOptions<TId, TUser, TRole> options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole>
#endif
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            IEndpointRouteBuilder route = builder;

            if (!String.IsNullOrEmpty(options.MapGroup))
            {
                route = MapGroup.Map(route, options.MapGroup);
            }

            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Login);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Register);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Refresh);
            IEndpointConventionBuilder? convention = Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.ConfirmEmail);

            if (convention is not null && options.ConfirmEmail?.Confirm is { } confirm)
            {
                convention.Add(confirm);
            }
            
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.ResendConfirmEmail);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.ForgotPassword);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.ResetPassword);

            if (!String.IsNullOrEmpty(options.Account.MapGroup))
            {
                route = MapGroup.Map(route, options.Account.MapGroup);
            }

            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Account.GetInfo);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Account.SetInfo);
            Map<TId, TUser, TRole, IdentityOptions<TId, TUser, TRole>>(route, options, static options => options.Account.TwoFactor);

#if NET7_0_OR_GREATER
            return route is RouteGroupBuilder group ? new IdentityEndpointsConventionBuilder(group) : null;
#else
            return builder;
#endif
        }
        
#if NET7_0_OR_GREATER
        private sealed class IdentityEndpointsConventionBuilder : IEndpointConventionBuilder
        {
            private RouteGroupBuilder Builder { get; }
            private IEndpointConventionBuilder ConventionBuilder
            {
                get
                {
                    return Builder;
                }
            }
            
            public IdentityEndpointsConventionBuilder(RouteGroupBuilder builder)
            {
                Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            }

            public void Add(Action<EndpointBuilder> convention)
            {
                ConventionBuilder.Add(convention);
            }

            public void Finally(Action<EndpointBuilder> convention)
            {
                ConventionBuilder.Finally(convention);
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<String> ToBusinessException(this IdentityError value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return new BusinessException<String>(value.Description, value.Code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException[]? ToBusinessException(this IdentityResult value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Errors.WhereNotNull().Select(ToBusinessException).Cast<BusinessException>().ToArray() is { Length: > 0 } result ? result : null;
        }

        private sealed class IdentityOptionsBuilder<TOptions> where TOptions : class, new()
        {
            private TOptions? _options;
            public TOptions Options
            {
                get
                {
                    return _options ??= new TOptions();
                }
                set
                {
                    _options = value ?? throw new ArgumentNullException(nameof(value));
                }
            }
        }
    }
}