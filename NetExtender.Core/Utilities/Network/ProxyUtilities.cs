// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Network
{
    public static class ProxyUtilities
    {
        public static async Task<HttpStatusCode> CheckProxyAsync(this WebProxy proxy)
        {
            if (proxy?.Address is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            Boolean ping = await NetworkUtilities.CheckPingAsync(proxy.Address.Host).ConfigureAwait(false);
            if (!ping)
            {
                return HttpStatusCode.ServiceUnavailable;
            }

            const String address = "https://google.com";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(address);
            request.Proxy = proxy;
            request.UserAgent = UserAgentUtilities.CurrentSessionUserAgent;
            request.Timeout = 1000;

            try
            {
                WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
                return response is HttpWebResponse http ? http.StatusCode : HttpStatusCode.OK;
            }
            catch (WebException exception)
            {
                if (exception.Response is HttpWebResponse response)
                {
                    return response.StatusCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return HttpStatusCode.ServiceUnavailable;
        }
    }
}