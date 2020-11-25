// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    public class EventDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEventDictionary<TKey, TValue>
    {
        public event TypeKeyValueHandler<TKey, TValue> OnAdd;
        public event TypeKeyValueHandler<TKey, TValue> OnSet;
        public event TypeKeyValueHandler<TKey, TValue> OnRemove;
        public event TypeKeyValueHandler<TKey, TValue> OnChange;
        public event EmptyHandler ItemsChanged;
        public event EmptyHandler OnClear;

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnAdd?.Invoke(key, value);
            OnSet?.Invoke(key, value);
            ItemsChanged?.Invoke();
        }

        public void Set(TKey key, TValue value)
        {
            this[key] = value;
        }

        public new void Remove(TKey key)
        {
            Boolean succesfull = TryGetValue(key, out TValue value);
            base.Remove(key);
            if (!succesfull)
            {
                return;
            }

            OnRemove?.Invoke(key, value);
            ItemsChanged?.Invoke();
        }

        public new void Clear()
        {
            Boolean any = this.Any();
            base.Clear();
            if (!any)
            {
                return;
            }

            OnClear?.Invoke();
            ItemsChanged?.Invoke();
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                Boolean succesfull = TryGetValue(key, out TValue val);

                if (succesfull && val?.Equals(value) == true)
                {
                    return;
                }

                base[key] = value;

                if (succesfull)
                {
                    OnChange?.Invoke(key, value);
                }
                else
                {
                    OnAdd?.Invoke(key, value);
                }

                OnSet?.Invoke(key, value);
                ItemsChanged?.Invoke();
            }
        }
    }
}