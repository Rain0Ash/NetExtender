using System;
using System.Net.Http;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network
{
    public class NoCertificateHttpClient : HttpClient
    {
        public NoCertificateHttpClient()
            : this(new Handler())
        {
        }

        public NoCertificateHttpClient(HttpMessageHandler handler)
            : base(handler)
        {
        }

        public NoCertificateHttpClient(HttpMessageHandler handler, Boolean dispose)
            : base(handler, dispose)
        {
        }

        public class Handler : HttpClientHandler
        {
            public Handler()
            {
                this.SetNoCertificate();
            }
        }
    }
}