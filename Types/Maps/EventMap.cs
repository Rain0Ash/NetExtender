// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Maps
{
    public class EventMap<TKey, TValue> : Map<TKey, TValue>, IEventMap<TKey, TValue>, IReadOnlyEventMap<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        public event TypeKeyValueHandler<TKey, TValue> OnAdd;
        public event TypeKeyValueHandler<TKey, TValue> OnSet;
        public event TypeKeyValueHandler<TKey, TValue> OnRemove;
        public event TypeKeyValueHandler<TKey, TValue> OnChange;
        public event EmptyHandler ItemsChanged;
        public event EmptyHandler OnClear;

        public override void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            OnAdd?.Invoke(key, value);
            ItemsChanged?.Invoke();
        }

        public override Boolean TryAdd(TKey key, TValue value)
        {
            if (!base.TryAdd(key, value))
            {
                return false;
            }

            OnAdd?.Invoke(key, value);
            ItemsChanged?.Invoke();

            return true;
        }

        public override Boolean Remove(TKey key, TValue value)
        {
            if (!base.Remove(key, value))
            {
                return false;
            }

            OnRemove?.Invoke(key, value);
            ItemsChanged?.Invoke();

            return true;
        }

        public override void Clear()
        {
            Int32 count = Count;
            base.Clear();
            
            if (count <= 0)
            {
                OnClear?.Invoke();
            }
        }

        public override TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                OnSet?.Invoke(key, value);
            }
        }
    }
}