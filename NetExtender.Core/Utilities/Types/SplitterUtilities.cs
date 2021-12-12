// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Splitters.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class SplitterUtilities
    {
        public static IEnumerable<TResult> SplitMany<T, TResult>(this ISplitter<T, TResult> splitter, IEnumerable<T> source)
        {
            if (splitter is null)
            {
                throw new ArgumentNullException(nameof(splitter));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(splitter.Split);
        }
    }
}