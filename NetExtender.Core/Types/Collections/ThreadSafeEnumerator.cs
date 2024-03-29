// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace NetExtender.Types.Collections
{
    public class ThreadSafeEnumerator : IEnumerator, IDisposable
    {
        private IEnumerator Enumerator { get; }
        private Object? Synchronization { get; }

        Object? IEnumerator.Current
        {
            get
            {
                return Enumerator.Current;
            }
        }

        public ThreadSafeEnumerator(IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Enumerator = source.GetEnumerator();
            Monitor.Enter(Enumerator);
        }

        public ThreadSafeEnumerator(IEnumerable source, Object synchronization)
            : this(source is not null ? source.GetEnumerator() : throw new ArgumentNullException(nameof(source)), synchronization)
        {
        }

        public ThreadSafeEnumerator(IEnumerator source, Object synchronization)
        {
            Enumerator = source ?? throw new ArgumentNullException(nameof(source));
            Synchronization = synchronization ?? throw new ArgumentNullException(nameof(synchronization));
            Monitor.Enter(Synchronization);
        }

        public Boolean MoveNext()
        {
            return Enumerator.MoveNext();
        }

        public void Reset()
        {
            Enumerator.Reset();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposing)
        {
            Monitor.Exit(Synchronization ?? Enumerator);

            if (disposing)
            {
                (Enumerator as IDisposable)?.Dispose();
            }
        }

        ~ThreadSafeEnumerator()
        {
            Dispose(false);
        }
    }

    public class ThreadSafeEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator<T> Enumerator { get; }
        private Object? Synchronization { get; }

        public T Current
        {
            get
            {
                return Enumerator.Current;
            }
        }

        Object? IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public ThreadSafeEnumerator(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Enumerator = source.GetEnumerator();
            Monitor.Enter(Enumerator);
        }

        public ThreadSafeEnumerator(IEnumerable<T> source, Object synchronization)
            : this(source is not null ? source.GetEnumerator() : throw new ArgumentNullException(nameof(source)), synchronization)
        {
        }

        public ThreadSafeEnumerator(IEnumerator<T> source, Object synchronization)
        {
            Enumerator = source ?? throw new ArgumentNullException(nameof(source));
            Synchronization = synchronization ?? throw new ArgumentNullException(nameof(synchronization));

            Monitor.Enter(Synchronization);
        }

        public Boolean MoveNext()
        {
            return Enumerator.MoveNext();
        }

        public void Reset()
        {
            Enumerator.Reset();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposing)
        {
            Monitor.Exit(Synchronization ?? Enumerator);

            if (disposing)
            {
                Enumerator.Dispose();
            }
        }

        ~ThreadSafeEnumerator()
        {
            Dispose(false);
        }
    }
}