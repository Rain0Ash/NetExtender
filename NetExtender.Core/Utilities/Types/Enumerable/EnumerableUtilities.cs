// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    public static partial class EnumerableUtilities
    {
        public static Boolean IsReadOnly<T>(this IEnumerable<T> source)
        {
            return source switch
            {
                null => throw new ArgumentNullException(nameof(source)),
                ICollection<T> collection => collection.IsReadOnly,
                _ => true
            };
        }
    }
}