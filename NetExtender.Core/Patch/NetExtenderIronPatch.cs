// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class NetExtenderIronPatch : AutoReflectionPatch<NetExtenderIronPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static NetExtenderIronPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }

        public static String Format(DateTimeOffset offset)
        {
            const String format = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
            return offset.UtcDateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        public static HttpResponseMessage Message(DateTimeOffset offset)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Version = HttpVersion.Version11,
                Headers =
                {
                    { "Date", Format(offset) },
                    { "Cache-Control", "private" },
                    { "Transfer-Encoding", "chunked" },
                    { "Request-Context", $"appId=cid-v1:{new Guid():D}" }
                },
                Content = new StringContent("Iron app insights updated sucessfully", Encoding.UTF8, "text/plain")
            };
        }
    }
}