// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Text;
using NetExtender.Utilities.Network;

#pragma warning disable 8764

namespace NetExtender.Types.Network
{
    public class WebClientExtended : WebClient
    {
        public Boolean HeadOnly { get; set; }
        
        public WebClientExtended()
        {
            Encoding = Encoding.UTF8;
            Headers.Add(UserAgentUtilities.OtherUserAgent);
        }
        
        protected override WebRequest? GetWebRequest(Uri address)
        {
            WebRequest? request = base.GetWebRequest(address);

            if (request is null!)
            {
                return null;
            }
            
            if (HeadOnly && request.Method == "GET")
            {
                request.Method = "HEAD";
            }
            
            return request;
        }
    }
}