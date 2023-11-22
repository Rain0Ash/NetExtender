// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Collections
{
    public sealed class EnumerableWrapper<T> : IEnumerable<T>
    {
        private IEnumerable<T> Source { get; }

        public EnumerableWrapper()
        {
            Source = Enumerable.Empty<T>();
        }

        public EnumerableWrapper(IEnumerable<T> source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Source.GetEnumerator();
        }
    }
}