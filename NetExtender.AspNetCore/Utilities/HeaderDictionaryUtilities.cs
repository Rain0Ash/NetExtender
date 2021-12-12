// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HeaderDictionaryUtilities
    {
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