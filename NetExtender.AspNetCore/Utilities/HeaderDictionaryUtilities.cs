// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Http;
using NetExtender.Utilities.Network;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HeaderDictionaryUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this IHeaderDictionary source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HttpHeaderUtilities.ToHeaderString(source.Select(static pair => new KeyValuePair<String, IEnumerable<String>>(pair.Key, pair.Value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this IHeaderDictionary source, Int32 buffer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HttpHeaderUtilities.ToHeaderString(source.Select(static pair => new KeyValuePair<String, IEnumerable<String>>(pair.Key, pair.Value)), buffer);
        }
        
        public static void RemoveRange(this IHeaderDictionary dictionary, params String[] items)
        {
            RemoveRange(dictionary, (IEnumerable<String>) items);
        }

        public static void RemoveRange(this IHeaderDictionary dictionary, IEnumerable<String> items)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (dictionary.IsReadOnly)
            {
                throw new NotSupportedException();
            }

            foreach (String item in items)
            {
                dictionary.Remove(item);
            }
        }
    }
}