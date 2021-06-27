// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Network;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public class UrlUtils
    {
        public static Boolean IsValidUrl(String path)
        {
            return !String.IsNullOrEmpty(path) && Uri.TryCreate(path, UriKind.Absolute, out Uri uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        
        public static Boolean IsUrlContainData(String path)
        {
            if (!IsValidUrl(path))
            {
                return false;
            }

            using FixedWebClient client = new FixedWebClient
            {
                HeadOnly = true
            };

            return !String.IsNullOrEmpty(client.DownloadString(path));
        }
    }
}