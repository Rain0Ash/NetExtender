using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Enumerators
{
    public abstract record EnumeratorStringFiller<T, TEnumerator, TFiller> : EnumeratorStringFiller<T, TEnumerator> where TEnumerator : IReadOnlyCollection<T>, IEnumerator<T>, ICloneable<TEnumerator> where TFiller : EnumeratorStringFiller<T, TEnumerator, TFiller>
    {
        public delegate TFiller Formatter(in TEnumerator enumerator, String? format, IFormatProvider? provider);
        private static ConcurrentDictionary<String, Formatter> Storage { get; } = new ConcurrentDictionary<String, Formatter>();

        protected EnumeratorStringFiller(in TEnumerator enumerator, String? format, IFormatProvider? provider)
            : base(in enumerator, format, provider)
        {
        }

        protected static Formatter? Get(String? filler)
        {
            return filler is not null && Storage.TryGetValue(filler, out Formatter? factory) ? factory : null;
        }

        protected static TFiller? Create(in TEnumerator enumerator, String? format, IFormatProvider? provider)
        {
            return Create(in enumerator, format, format, provider);
        }

        protected static TFiller? Create(in TEnumerator enumerator, String? filler, String? format, IFormatProvider? provider)
        {
            return Get(filler) is { } factory ? factory.Invoke(in enumerator, filler != format ? format : null, provider) : null;
        }

        protected static void Register(String format, Formatter factory)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            Storage[format] = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected static void Unregister(String format)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            Storage.TryRemove(format);
        }
    }

    public abstract record EnumeratorStringFiller<T, TEnumerator> : IReadOnlyCollection<String>, IEnumerator<String> where TEnumerator : IReadOnlyCollection<T>, IEnumerator<T>, ICloneable<TEnumerator>
    {
        protected TEnumerator Enumerator;
        protected IFormatProvider? Provider { get; }
        protected String? Format { get; }

        public String Current
        {
            get
            {
                return Convert(Enumerator.Current!);
            }
        }

        Object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Int32 Count
        {
            get
            {
                return Enumerator.Count;
            }
        }

        protected EnumeratorStringFiller(in TEnumerator enumerator, String? format, IFormatProvider? provider)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            Enumerator = enumerator.Clone();
            Provider = provider;
            Format = format;
        }

        [return: NotNullIfNotNull("value")]
        protected abstract String? Convert(T? value);

        Boolean IEnumerator.MoveNext()
        {
            return Enumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            Enumerator.Reset();
        }

        public virtual IEnumerator<String> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override String? ToString()
        {
            return null;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Enumerator.Dispose();
            }
        }
    }
}