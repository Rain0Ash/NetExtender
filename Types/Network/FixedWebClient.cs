// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Text;
using NetExtender.Utils.Network;

namespace NetExtender.Types.Network
{
    public class FixedWebClient : WebClient
    {
        public Boolean HeadOnly { get; set; }
        
        public FixedWebClient()
        {
            Encoding = Encoding.UTF8;
            Headers.Add(WebUtils.OtherUserAgent);
        }
        
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest req = base.GetWebRequest(address);
            
            if (HeadOnly && req?.Method == "GET")
            {
                req.Method = "HEAD";
            }
            
            return req;
        }
    }
}