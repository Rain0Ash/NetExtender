using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Enumerators
{
    public sealed class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator
    {
        private readonly IEnumerator<KeyValuePair<TKey, TValue>> Enumerator;

        public DictionaryEntry Entry
        {
            get
            {
                return new DictionaryEntry(Enumerator.Current.Key!, Enumerator.Current.Value);
            }
        }

        public Object Current
        {
            get
            {
                return Enumerator.Current;
            }
        }

        public Object Key
        {
            get
            {
                return Enumerator.Current.Key!;
            }
        }

        public Object? Value
        {
            get
            {
                return Enumerator.Current.Value;
            }
        }

        public DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
        {
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public Boolean MoveNext()
        {
            return Enumerator.MoveNext();
        }

        public void Reset()
        {
            Enumerator.Reset();
        }
    }

    public sealed class DictionaryEnumerator<TKey, TValue, TEnumerator> : IDictionaryEnumerator where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private TEnumerator Enumerator;

        public DictionaryEntry Entry
        {
            get
            {
                return new DictionaryEntry(Enumerator.Current.Key!, Enumerator.Current.Value);
            }
        }

        public Object Current
        {
            get
            {
                return Enumerator.Current;
            }
        }

        public Object Key
        {
            get
            {
                return Enumerator.Current.Key!;
            }
        }

        public Object? Value
        {
            get
            {
                return Enumerator.Current.Value;
            }
        }

        public DictionaryEnumerator(TEnumerator enumerator)
        {
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public Boolean MoveNext()
        {
            return Enumerator.MoveNext();
        }

        public void Reset()
        {
            Enumerator.Reset();
        }
    }
}