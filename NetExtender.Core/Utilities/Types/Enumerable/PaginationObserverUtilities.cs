using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Spans;

namespace NetExtender.Utilities.Types
{
    public static class PaginationObserverUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyPaginationObserver<T> ReadOnlyPaginationObserver<T>(this T[] array, Int32 size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return new ReadOnlyPaginationObserver<T>(array, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PaginationObserver<T> PaginationObserver<T>(this T[] array, Int32 size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return new PaginationObserver<T>(array, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyPaginationObserver<T> ReadOnlyPaginationObserver<T>(this List<T> collection, Int32 size)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new ReadOnlyPaginationObserver<T>(collection.AsReadOnlySpan(), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PaginationObserver<T> PaginationObserver<T>(this List<T> collection, Int32 size)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return new PaginationObserver<T>(collection.AsSpan(), size);
        }
    }
}