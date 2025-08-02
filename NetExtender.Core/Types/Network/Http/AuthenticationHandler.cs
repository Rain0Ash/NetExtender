using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NetExtender.Types.Network.Interfaces;

namespace NetExtender.Types.Network
{
    // TODO: events
    public class AuthenticationHandler<T> : HttpDelegatingHandler where T : IAuthenticationToken
    {
        protected IAuthenticationTokenProvider<T> Provider { get; }

        protected virtual String? DefaultScheme
        {
            get
            {
                return null;
            }
        }

        public AuthenticationHandler(IAuthenticationTokenProvider<T> provider, ILogger<AuthenticationHandler<T>>? logger)
            : base(logger ?? NullLogger<AuthenticationHandler<T>>.Instance)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        protected virtual Boolean ShouldIncludeToken(HttpRequestMessage request)
        {
            return request.Headers.Authorization is not null;
        }

        protected virtual Boolean IsUnauthorized(HttpRequestMessage request, HttpResponseMessage response)
        {
            return response.StatusCode is HttpStatusCode.Unauthorized;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            if (!ShouldIncludeToken(request))
            {
                Logger?.LogDebug("Request '{Uri}' doesn't require an authentication token.", request.RequestUri);
                return await base.SendAsync(request, token);
            }

            T? access = await Provider.GetToken(request, token);
            HttpResponseMessage response = await SendWithAuthenticationToken(request, access, token);

            if (!IsUnauthorized(request, response))
            {
                return response;
            }

            if (access is null || !access.CanBeRefreshed || await RefreshAuthenticationToken(request, access, token) is not { } refresh)
            {
                Logger?.LogError("Request '{Uri}' was unauthorized and the token '{Token}' cannot be refreshed. Considering the session has expired.", request.RequestUri, access);
                await Provider.NotifySessionExpired(request, access, token);
                return response;
            }

            response = await SendWithAuthenticationToken(request, refresh, token);

            if (!IsUnauthorized(request, response))
            {
                return response;
            }

            Logger?.LogError("Request '{Uri}' was unauthorized, the token '{Access}' was refreshed to '{Refresh}' but the request was still unauthorized. Considering the session has expired.", request.RequestUri, access, refresh);
            await Provider.NotifySessionExpired(request, refresh, token);
            return response;
        }

        protected virtual async Task<HttpResponseMessage> SendWithAuthenticationToken(HttpRequestMessage request, T? authentication, CancellationToken token)
        {
            if (authentication?.AccessToken is null || request.Headers.Authorization?.Scheme is not { } scheme)
            {
                request.Headers.Remove(nameof(HttpRequestHeaders.Authorization));
                return await base.SendAsync(request, token);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(scheme, authentication.AccessToken);
            return await base.SendAsync(request, token);
        }

        protected virtual async Task<T?> RefreshAuthenticationToken(HttpRequestMessage request, T @default, CancellationToken token)
        {
            if (@default is null)
            {
                throw new ArgumentNullException(nameof(@default));
            }

            Logger?.LogDebug("Requesting refresh for token: '{Token}'.", @default);
            return await Provider.RefreshToken(request, @default, token);
        }
    }
}