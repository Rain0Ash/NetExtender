// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Types.Collections;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
        public static IOrderedEnumerable<T> AsOrderedEnumerable<T>(this IOrderedQueryable<T> source)
        {
            return new OrderedQueryableWrapper<T>(source ?? throw new ArgumentNullException(nameof(source)));
        }
    }
}