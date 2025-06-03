using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Enumerators
{
    public sealed class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator
    {
        private readonly IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;

        public DictionaryEntry Entry
        {
            get
            {
                return new DictionaryEntry(_enumerator.Current.Key!, _enumerator.Current.Value);
            }
        }
        
        public Object Current
        {
            get
            {
                return _enumerator.Current;
            }
        }

        public Object Key
        {
            get
            {
                return _enumerator.Current.Key!;
            }
        }

        public Object? Value
        {
            get
            {
                return _enumerator.Current.Value;
            }
        }

        public DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }
        
        public Boolean MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }
    }
    
    public sealed class DictionaryEnumerator<TKey, TValue, TEnumerator> : IDictionaryEnumerator where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly TEnumerator _enumerator;

        public DictionaryEntry Entry
        {
            get
            {
                return new DictionaryEntry(_enumerator.Current.Key!, _enumerator.Current.Value);
            }
        }
        
        public Object Current
        {
            get
            {
                return _enumerator.Current;
            }
        }

        public Object Key
        {
            get
            {
                return _enumerator.Current.Key!;
            }
        }

        public Object? Value
        {
            get
            {
                return _enumerator.Current.Value;
            }
        }

        public DictionaryEnumerator(TEnumerator enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }
        
        public Boolean MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}