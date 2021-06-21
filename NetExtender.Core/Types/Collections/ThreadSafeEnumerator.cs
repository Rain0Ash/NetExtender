// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace NetExtender.Types.Collections
{
    public class ThreadSafeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        private readonly Object? _sync;

        public ThreadSafeEnumerator(IEnumerable<T> source)
        {
            _inner = source?.GetEnumerator() ?? throw new ArgumentNullException(nameof(source));

            Monitor.Enter(_inner);
        }

        public ThreadSafeEnumerator(IEnumerable<T> source, Object sync)
            : this(source?.GetEnumerator() ?? throw new ArgumentNullException(nameof(source)), sync)
        {
        }

        public ThreadSafeEnumerator(IEnumerator<T> inner, Object sync)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _sync = sync ?? throw new ArgumentNullException(nameof(sync));

            Monitor.Enter(_sync);
        }

        public void Dispose()
        {
            if (_sync is null)
            {
                Monitor.Exit(_inner);
                return;
            }
            
            Monitor.Exit(_sync);
        }

        public Boolean MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        Object? IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
}