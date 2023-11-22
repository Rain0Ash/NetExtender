// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network
{
    public class HttpUnsortedResponse
    {
        public HttpHeaders HttpHeaders { get; protected set; } = new HttpUnsortedHeaders();
        public HttpStatusCode StatusCode { get; set; }
        public Version? Version { get; set; }
        public String? ReasonPhrase { get; set; }
    }
}