// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
        public static IQueryable<IGrouping<Type, T>> GroupByType<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().GroupBy(item => item!.GetType());
        }
    }
}