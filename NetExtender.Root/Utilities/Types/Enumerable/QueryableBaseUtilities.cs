using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    internal static class QueryableBaseUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IQueryable<T> WhereNotNull<T>(IQueryable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static item => item != null)!;
        }
    }
}