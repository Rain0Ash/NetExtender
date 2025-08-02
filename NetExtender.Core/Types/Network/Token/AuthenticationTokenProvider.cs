using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetExtender.Types.Network.Interfaces;

namespace NetExtender.Types.Network.Token
{
    // TODO: events
    public class AuthenticationTokenProviderWrapper<T> : AuthenticationTokenProvider<T> where T : IAuthenticationToken
    {
        protected ILogger? Logger { get; }
        protected Func<HttpRequestMessage, CancellationToken, Task<T?>> GetTokenHandler { get; }
        protected Func<HttpRequestMessage, T?, CancellationToken, Task<T>>? RefreshTokenHandler { get; }
        protected Func<HttpRequestMessage, T?, CancellationToken, Task> NotifySessionExpiredHandler { get; }
        protected SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);
        protected T? LastToken { get; set; }

        public AuthenticationTokenProviderWrapper(Func<HttpRequestMessage, CancellationToken, Task<T?>> get, Func<HttpRequestMessage, T?, CancellationToken, Task<T>>? refresh, Func<HttpRequestMessage, T?, CancellationToken, Task> notify)
		{
			GetTokenHandler = get ?? throw new ArgumentNullException(nameof(get));
            RefreshTokenHandler = refresh;
            NotifySessionExpiredHandler = notify ?? throw new ArgumentNullException(nameof(notify));
        }

        public AuthenticationTokenProviderWrapper(ILogger? logger, Func<HttpRequestMessage, CancellationToken, Task<T?>> get, Func<HttpRequestMessage, T?, CancellationToken, Task<T>>? refresh, Func<HttpRequestMessage, T?, CancellationToken, Task> notify)
		    : this(get, refresh, notify)
        {
			Logger = logger;
        }

        public AuthenticationTokenProviderWrapper(ILoggerFactory? logger, Func<HttpRequestMessage, CancellationToken, Task<T?>> get, Func<HttpRequestMessage, T?, CancellationToken, Task<T>>? refresh, Func<HttpRequestMessage, T?, CancellationToken, Task> notify)
            : this(get, refresh, notify)
        {
			Logger = logger?.CreateLogger(GetType());
        }

		public override Task<T?> GetToken(HttpRequestMessage request, CancellationToken token)
        {
            return GetTokenHandler.Invoke(request, token);
        }

		public override async Task<T?> RefreshToken(HttpRequestMessage request, T @default, CancellationToken token)
		{
            if (@default is null)
            {
                throw new ArgumentNullException(nameof(@default));
            }

            if (RefreshTokenHandler is null)
			{
				throw new NotSupportedException("Provider doesn't support token refresh.");
			}

			await Semaphore.WaitAsync(token);

			try
			{
                token = CancellationToken.None;
				T? refresh = await GetToken(request, token);
                
                Logger?.LogDebug("The current token is: '{Token}'.", refresh);

                if (refresh is null || !refresh.CanBeRefreshed)
                {
                    Logger?.LogWarning("The refresh token is null or cannot be refreshed.");
                    return default;
                }

                if (refresh.AccessToken is not null && refresh.AccessToken != @default.AccessToken)
                {
                    Logger?.LogWarning("The access tokens are different. No need to refresh, return current token '{Current}'.", refresh);
                    return refresh;
                }

                try
                {
                    Logger?.LogDebug("Refreshing token: '{Token}'.", @default);

                    refresh = await RefreshTokenHandler(request, refresh, token);

                    Logger?.LogInformation("Refreshed token: '{Old}' to '{New}'.", @default, refresh);
                    return refresh;
                }
                catch (Exception exception)
                {
                    Logger?.LogError(exception, "Failed to refresh token: '{Token}'.", @default);
                    return default;
                }
			}
			finally
			{
				Semaphore.Release();
			}
		}

        public override async Task NotifySessionExpired(HttpRequestMessage request, T? @default, CancellationToken token)
        {
            if (LastToken?.AccessToken == @default?.AccessToken)
            {
                return;
            }

            LastToken = @default;
            await NotifySessionExpiredHandler(request, @default, token);
        }
    }
    
    public abstract class AuthenticationTokenProvider<T> : IAuthenticationTokenProvider<T> where T : IAuthenticationToken
    {
        public abstract Task<T?> GetToken(HttpRequestMessage request, CancellationToken token);
        public abstract Task<T?> RefreshToken(HttpRequestMessage request, T @default, CancellationToken token);
        public abstract Task NotifySessionExpired(HttpRequestMessage request, T? @default, CancellationToken token);
    }
}