// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text.RegularExpressions;

namespace NetExtender.Utilities.Types
{
    public static class UriUtilities
    {
        public static Uri ToUri(this String uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return new UriBuilder(uri).Uri;
        }
        
        public static Uri ToUri(this String uri, String parent)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return uri.ToUri(parent.ToUri());
        }
        
        public static Uri ToUri(this String uri, Uri parent)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return new Uri(parent, new Uri(uri, UriKind.Relative));
        }
        
        public static Uri SetQueryParameter(this Uri uri, String key, String? value)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            String uristring = uri.ToString();
	
            Match match = Regex.Match(uristring, $@"[?&]({Regex.Escape(key)}=?.*?)(?:&|/|$)");
	
            if (match.Success)
            {
                Group group = match.Groups[1];
	
                uristring = uristring.Remove(group.Index, group.Length);
                uristring = uristring.Insert(group.Index, $"{key}={value}");
                return new Uri(uristring);
            }

            uristring += uristring.Contains('?') ? '&' : '?';
            uristring += $"{key}={value}";

            return new Uri(uristring);
        }
        
        public static Uri SetRouteParameter(this Uri uri, String key, String? value)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            String uristring = uri.ToString();
	
            Match match = Regex.Match(uristring, $@"/({Regex.Escape(key)}/?.*?)(?:/|$)");
	
            if (match.Success)
            {
                Group group = match.Groups[1];
	
                uristring = uristring.Remove(group.Index, group.Length);
                uristring = uristring.Insert(group.Index, $"{key}/{value}");
                return new Uri(uristring);
            }

            if (uristring[^1] != '/')
            {
                uristring += '/';
            }

            uristring += $"{key}/{value}";

            return new Uri(uristring);
        }
    }
}