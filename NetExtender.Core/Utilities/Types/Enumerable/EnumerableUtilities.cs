// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<T> GetEnumerableFrom<T>(T item)
        {
            return EnumerableBaseUtilities.GetEnumerableFrom(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean IsReadOnly<T>(IEnumerable<T> source)
        {
            return EnumerableBaseUtilities.IsReadOnly(source);
        }
    }
}