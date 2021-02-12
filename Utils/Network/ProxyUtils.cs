// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NetExtender.Utils.Network
{
    public static class ProxyUtils
    {
        public static async Task<HttpStatusCode> CheckProxyAsync([NotNull] this WebProxy proxy)
        {
            if (proxy?.Address is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            Boolean ping = await NetworkUtils.CheckPingAsync(proxy.Address.Host).ConfigureAwait(false);
            if (!ping)
            {
                return HttpStatusCode.ServiceUnavailable;
            }

            const String address = "http://google.com";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(address);
            request.Proxy = proxy;
            request.UserAgent = WebUtils.CurrentSessionUserAgent;
            request.Timeout = 1000;

            try
            {
                WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
                return response is HttpWebResponse http ? http.StatusCode : HttpStatusCode.OK;
            }
            catch (WebException e)
            {
                if (e.Response is HttpWebResponse response)
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