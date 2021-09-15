// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Network;

namespace NetExtender.Utilities.IO
{
    public class UrlUtilities
    {
        public static Boolean IsValidUrl(String path)
        {
            return !String.IsNullOrEmpty(path) && Uri.TryCreate(path, UriKind.Absolute, out Uri? uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
        
        public static Boolean IsUrlContainData(String path)
        {
            if (!IsValidUrl(path))
            {
                return false;
            }

            using WebClientExtended client = new WebClientExtended
            {
                HeadOnly = true
            };

            return !String.IsNullOrEmpty(client.DownloadString(path));
        }
    }
}