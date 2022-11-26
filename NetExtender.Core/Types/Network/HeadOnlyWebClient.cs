// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Text;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network
{
    public class HeadOnlyWebClient : WebClient
    {
        public Boolean HeadOnly { get; set; } = true;

#if NET6_0_OR_GREATER
        [Obsolete]
#endif
        public HeadOnlyWebClient()
        {
            Encoding = Encoding.UTF8;
            Headers.Add(UserAgentUtilities.OtherUserAgent);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (HeadOnly && request.Method == "GET")
            {
                request.Method = "HEAD";
            }

            return request;
        }
    }
}