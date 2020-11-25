using System;
using System.IO;
using System.Net;
using NetExtender.Network.Networking.Common;
using NetExtender.Network.Networking.SSL;

namespace NetExtender.Network.Networking.Http.Secure
{
    /// <summary>
    ///     HTTPS server is used to create secured HTTPS Web server and communicate with clients using secure HTTPS protocol.
    ///     It allows to receive GET, POST, PUT, DELETE requests and send HTTP responses.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class HttpsNetworkServer : SslNetworkServer
    {
        /// <summary>
        ///     Initialize HTTPS server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpsNetworkServer(SslNetworkContext context, IPAddress address, Int32 port)
            : base(context, address, port)
        {
            Cache = new NetworkFileCache();
        }

        /// <summary>
        ///     Initialize HTTPS server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpsNetworkServer(SslNetworkContext context, String address, Int32 port)
            : base(context, address, port)
        {
            Cache = new NetworkFileCache();
        }

        /// <summary>
        ///     Initialize HTTPS server with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public HttpsNetworkServer(SslNetworkContext context, IPEndPoint endpoint)
            : base(context, endpoint)
        {
            Cache = new NetworkFileCache();
        }

        /// <summary>
        ///     Get the static content cache
        /// </summary>
        public NetworkFileCache Cache { get; }

        private static Boolean Handler(NetworkFileCache cache, String key, Byte[] value, TimeSpan timespan)
        {
            HttpNetworkResponse header = new HttpNetworkResponse();
            header.SetBegin(200);
            header.SetContentType(Path.GetExtension(key));
            header.SetHeader("Cache-Control", $"max-age={timespan.Seconds}");
            header.SetBody(value);
            return cache.Add(key, header.Cache.Data, timespan);
        }
        
        /// <summary>
        ///     Add static content cache
        /// </summary>
        /// <param name="path">Static content path</param>
        /// <param name="prefix">Cache prefix (default is "/")</param>
        /// <param name="timeout">Refresh cache timeout (default is 1 hour)</param>
        public void AddStaticContent(String path, String prefix = "/", TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromHours(1);
            
            Cache.InsertPath(path, prefix, timeout.Value, Handler);
        }

        /// <summary>
        ///     Remove static content cache
        /// </summary>
        /// <param name="path">Static content path</param>
        public void RemoveStaticContent(String path)
        {
            Cache.RemovePath(path);
        }

        /// <summary>
        ///     Clear static content cache
        /// </summary>
        public void ClearStaticContent()
        {
            Cache.Clear();
        }

        /// <summary>
        ///     Watchdog the static content cache
        /// </summary>
        public void Watchdog(DateTime utc)
        {
            Cache.Watchdog(utc);
        }

        protected override SslNetworkSession CreateSession()
        {
            return new HttpsNetworkSession(this);
        }
    }
}