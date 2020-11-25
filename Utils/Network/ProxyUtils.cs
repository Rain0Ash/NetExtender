// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;

namespace NetExtender.Utils.Network
{
    public static class ProxyUtils
    {
        public static WebProxy CreateProxy(String address, Int32 port, String login = null, String password = null)
        {
            if (!NetworkUtils.ValidateIPv4(address))
            {
                throw new ArgumentException("Address is not valid");
            }

            if (!NetworkUtils.ValidatePort(port))
            {
                throw new ArgumentException("Port is not valid");
            }

            login ??= String.Empty;
            password ??= String.Empty;

            return new WebProxy(address, port)
            {
                Credentials = new NetworkCredential(login, password)
            };
        }

        public static async Task<HttpStatusCode> CheckProxyAsync(WebProxy proxy)
        {
            Boolean ping = await NetworkUtils.CheckPingAsync(proxy.Address.Host).ConfigureAwait(false);
            if (!ping)
            {
                return HttpStatusCode.ServiceUnavailable;
            }

            const String checkAddress = "http://google.com";
            const String userAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(checkAddress);
            request.Proxy = proxy;
            request.UserAgent = userAgent;
            request.Timeout = 2000;

            try
            {
                await request.GetResponseAsync().ConfigureAwait(false);
                return request.GetResponse() is HttpWebResponse response ? response.StatusCode : HttpStatusCode.OK;
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