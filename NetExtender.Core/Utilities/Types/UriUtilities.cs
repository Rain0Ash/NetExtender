// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Utilities.Types
{
    public static class UriUtilities
    {
        public static String File
        {
            get
            {
                return Uri.UriSchemeFile;
            }
        }

        public static String FileDelimiter { get; } = File + Delimiter;

        public static String Ftp
        {
            get
            {
                return Uri.UriSchemeFtp;
            }
        }

        public static String FtpDelimiter { get; } = Ftp + Delimiter;

        public static String Gopher
        {
            get
            {
                return Uri.UriSchemeGopher;
            }
        }

        public static String GopherDelimiter { get; } = Gopher + Delimiter;

        public static String Http
        {
            get
            {
                return Uri.UriSchemeHttp;
            }
        }

        public static String HttpDelimiter { get; } = Http + Delimiter;

        public static String Https
        {
            get
            {
                return Uri.UriSchemeHttps;
            }
        }

        public static String HttpsDelimiter { get; } = Https + Delimiter;

        public static String WebSocket
        {
            get
            {
                return "ws";
            }
        }

        public static String WebSocketDelimiter { get; } = WebSocket + Delimiter;

        public static String WebSocketSecure
        {
            get
            {
                return "wss";
            }
        }

        public static String WebSocketSecureDelimiter { get; } = WebSocketSecure + Delimiter;

        public static String MailTo
        {
            get
            {
                return Uri.UriSchemeMailto;
            }
        }

        public static String MailToDelimiter { get; } = MailTo + Delimiter;

        public static String News
        {
            get
            {
                return Uri.UriSchemeNews;
            }
        }

        public static String NewsDelimiter { get; } = News + Delimiter;

        public static String Nntp
        {
            get
            {
                return Uri.UriSchemeNntp;
            }
        }

        public static String NntpDelimiter { get; } = Nntp + Delimiter;

        public static String NetTcp
        {
            get
            {
                return Uri.UriSchemeNetTcp;
            }
        }

        public static String NetTcpDelimiter { get; } = NetTcp + Delimiter;

        public static String NetPipe
        {
            get
            {
                return Uri.UriSchemeNetPipe;
            }
        }

        public static String NetPipeDelimiter { get; } = NetPipe + Delimiter;

        public static String Delimiter
        {
            get
            {
                return Uri.SchemeDelimiter;
            }
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NameValueCollection ParseQueryString(this Uri address)
        {
            return address is not null ? new FormDataCollection(address).Collection : throw new ArgumentNullException(nameof(address));
        }

        public static Boolean TryParseQueryString(this Uri address, [MaybeNullWhen(false)] out NameValueCollection result)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            try
            {
                result = new FormDataCollection(address).Collection;
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryReadQueryAs(this Uri address, Type type, [MaybeNullWhen(false)] out Object result)
        {
            return TryReadQueryAs(address, type, null, out result);
        }

        public static Boolean TryReadQueryAs(this Uri address, Type type, JsonSerializer? serializer, [MaybeNullWhen(false)] out Object result)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!FormUrlEncodedJson.TryParse(new FormDataCollection(address)!, out JObject? token))
            {
                result = null;
                return false;
            }

            using JTokenReader reader = new JTokenReader(token);
            serializer ??= new JsonSerializer();
            result = serializer.Deserialize(reader, type);
            return result is not null;
        }

        public static Boolean TryReadQueryAs<T>(this Uri address, [MaybeNullWhen(false)] out T result)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (!FormUrlEncodedJson.TryParse(new FormDataCollection(address)!, out JObject? jobject))
            {
                result = default;
                return false;
            }

            result = jobject.ToObject<T>();
            return result is not null;
        }

        public static Boolean TryReadQueryAsJson(this Uri address, [MaybeNullWhen(false)] out JObject result)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return FormUrlEncodedJson.TryParse(new FormDataCollection(address)!, out result);
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

            Match match = Regex.Match(uristring, $"[?&]({Regex.Escape(key)}=?.*?)(?:&|/|$)");

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

            Match match = Regex.Match(uristring, $"/({Regex.Escape(key)}/?.*?)(?:/|$)");

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
        
        public static Uri ToUri(Boolean https, String address, UInt16 port)
        {
            if (String.IsNullOrEmpty(address))
            {
                throw new ArgumentNullOrEmptyStringException(address, nameof(address));
            }

            return new Uri($"{(https ? "https" : "http")}://{address}:{port}");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Uri ToUri(Boolean https, String address, Int32 port)
        {
            return port is >= UInt16.MinValue and <= UInt16.MaxValue ? ToUri(https, address, (UInt16) port) : throw new ArgumentOutOfRangeException(nameof(port), port, $"The port must be between '{UInt16.MinValue}' and '{UInt16.MaxValue}'.");
        }

        public static Uri ToUri(IGetter<Boolean> https, IGetter<String> address, UInt16 port)
        {
            if (https is null)
            {
                throw new ArgumentNullException(nameof(https));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }
            
            return ToUri(https.Get(), address.Get(), port);
        }

        public static Uri ToUri(IGetter<Boolean> https, IGetter<String> address, Int32 port)
        {
            if (https is null)
            {
                throw new ArgumentNullException(nameof(https));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (port is >= UInt16.MinValue and <= UInt16.MaxValue)
            {
                return ToUri(https.Get(), address.Get(), (UInt16) port);
            }
            
            throw new ArgumentOutOfRangeException(nameof(port), port, $"The port must be between {UInt16.MinValue} and {UInt16.MaxValue}.");
        }

        public static Uri ToUri(IGetter<Boolean> https, IGetter<String> address, IGetter<UInt16> port)
        {
            if (https is null)
            {
                throw new ArgumentNullException(nameof(https));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (port is null)
            {
                throw new ArgumentNullException(nameof(port));
            }
            
            return ToUri(https, address, port.Get());
        }
        
        public static Uri ToUri(IGetter<Boolean> https, IGetter<String> address, IGetter<Int32> port)
        {
            if (https is null)
            {
                throw new ArgumentNullException(nameof(https));
            }

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (port is null)
            {
                throw new ArgumentNullException(nameof(port));
            }
            
            return ToUri(https, address, port.Get());
        }
    }
}