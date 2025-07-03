using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetExtender.Utilities.Network
{
    public static class HttpHeaderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this HttpHeaders? headers, HttpHeaders? content)
        {
            return ToHeaderString(headers, content, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this HttpHeaders? headers, HttpHeaders? content, Int32 buffer)
        {
            IEnumerable<KeyValuePair<String, IEnumerable<String>>> source = headers ?? Enumerable.Empty<KeyValuePair<String, IEnumerable<String>>>();
            
            if (content is not null)
            {
                source = source.Concat(content);
            }
            
            return ToHeaderString(source, buffer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(IEnumerable<KeyValuePair<String, IEnumerable<String>>> source)
        {
            return ToHeaderString(source, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static String ToHeaderString(IEnumerable<KeyValuePair<String, IEnumerable<String>>> source, Int32 buffer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            StringBuilder builder = new StringBuilder(buffer > 0 ? buffer : 2048);
            
            using IEnumerator<KeyValuePair<String, IEnumerable<String>>> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return builder.ToString();
            }

            (String key, IEnumerable<String> value) = enumerator.Current;
            builder.Append(key).Append(':').Append(' ').AppendJoin(", ", value);

            while (enumerator.MoveNext())
            {
                (key, value) = enumerator.Current;
                builder.AppendLine().Append(key).Append(':').Append(' ').AppendJoin(", ", value);
            }

            return builder.ToString();
        }
    }
}