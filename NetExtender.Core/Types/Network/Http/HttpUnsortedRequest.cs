// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network
{
    public class HttpUnsortedRequest
    {
        public HttpHeaders HttpHeaders { get; protected set; } = new HttpUnsortedHeaders();
        public HttpMethod? Method { get; set; }
        public Version? Version { get; set; }
        public String? RequestUri { get; set; }

        public HttpUnsortedRequest()
        {
        }
        
        public HttpUnsortedRequest(HttpMethod method, Version version, String requestUri)
        {
            Method = method;
            Version = version;
            RequestUri = requestUri;
        }
    }
}