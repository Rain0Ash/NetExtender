// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class ProxyUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpStatusCode> GetProxyStatusAsync(this WebProxy proxy)
        {
            return GetProxyStatusAsync(proxy, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpStatusCode> GetProxyStatusAsync(this WebProxy proxy, CancellationToken token)
        {
            return GetProxyStatusAsync(proxy, Time.Second.Three, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpStatusCode> GetProxyStatusAsync(this WebProxy proxy, TimeSpan timeout)
        {
            return GetProxyStatusAsync(proxy, timeout, CancellationToken.None);
        }

        public static async Task<HttpStatusCode> GetProxyStatusAsync(this WebProxy proxy, TimeSpan timeout, CancellationToken token)
        {
            if (proxy?.Address is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            const String address = "https://google.com";

            using HttpClient client = new HttpClient(new HttpClientHandler { Proxy = proxy }) { Timeout = timeout };
            client.AddUserAgentHeader(UserAgentUtilities.CurrentSessionUserAgent);

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, address);
                using HttpResponseMessage message = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
                return message.StatusCode;
            }
            catch (TaskCanceledException)
            {
                return HttpStatusCode.RequestTimeout;
            }
            catch (HttpRequestException exception)
            {
                return exception.StatusCode ?? HttpStatusCode.ServiceUnavailable;
            }
            catch (Exception)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
        }
    }
}