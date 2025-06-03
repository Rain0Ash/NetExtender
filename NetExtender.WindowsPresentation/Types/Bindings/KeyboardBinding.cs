// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using NetExtender.Interfaces;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Bindings.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Bindings
{
    public delegate void KeyboardBindingHandler<in T>(T? sender, KeyInfo info) where T : DependencyObject;
    
    public class KeyboardBinding<T> : IKeyboardBinding<T>, IReadOnlyKeyboardBinding<T>, ICloneable<KeyboardBinding<T>> where T : DependencyObject
    {
        private Dictionary<Key, KeyboardBindingHandler<T>> Storage { get; }
        
        public Int32 Count
        {
            get
            {
                return Storage.Count;
            }
        }
        
        public ICollection<Key> Keys
        {
            get
            {
                return Storage.Keys;
            }
        }
        
        IEnumerable<Key> IKeyboardBindingInfo.Keys
        {
            get
            {
                return Keys;
            }
        }
        
        IEnumerable<Key> IReadOnlyKeyboardBinding<T>.Keys
        {
            get
            {
                return Keys;
            }
        }
        
        IEnumerable<Key> IReadOnlyDictionary<Key, KeyboardBindingHandler<T>>.Keys
        {
            get
            {
                return Keys;
            }
        }
        
        ICollection<KeyboardBindingHandler<T>> IDictionary<Key, KeyboardBindingHandler<T>>.Values
        {
            get
            {
                return Values;
            }
        }
        
        public ICollection<KeyboardBindingHandler<T>> Values
        {
            get
            {
                return Storage.Values;
            }
        }
        
        IEnumerable<KeyboardBindingHandler<T>> IReadOnlyKeyboardBinding<T>.Values
        {
            get
            {
                return Values;
            }
        }
        
        IEnumerable<KeyboardBindingHandler<T>> IReadOnlyDictionary<Key, KeyboardBindingHandler<T>>.Values
        {
            get
            {
                return Values;
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>) Storage).IsReadOnly;
            }
        }
        
        public Boolean IsSynchronized
        {
            get
            {
                return ((ICollection) Storage).IsSynchronized;
            }
        }
        
        public Object SyncRoot
        {
            get
            {
                return ((ICollection) Storage).SyncRoot;
            }
        }
        
        public KeyboardBinding()
        {
            Storage = new Dictionary<Key, KeyboardBindingHandler<T>>();
        }
        
        protected KeyboardBinding(Dictionary<Key, KeyboardBindingHandler<T>> storage)
        {
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }
        
        public Boolean Invoke(Object? sender, KeyInfo key)
        {
            return Invoke(sender, key, out Boolean result) && result;
        }
        
        public Boolean Invoke(Object? sender, KeyInfo key, out Boolean result)
        {
            if (sender is not null && sender is not T)
            {
                result = default;
                return false;
            }
            
            if (!TryGetValue(key, out KeyboardBindingHandler<T>? handler))
            {
                result = default;
                return false;
            }
            
            try
            {
                handler(sender as T, key);
                return result = true;
            }
            catch (Exception)
            {
                result = false;
                return true;
            }
        }
        
        public Boolean ContainsKey(Key key)
        {
            return Storage.ContainsKey(key);
        }
        
        Boolean ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>.Contains(KeyValuePair<Key, KeyboardBindingHandler<T>> item)
        {
            return TryGetValue(item.Key, out KeyboardBindingHandler<T>? handler) && handler == item.Value;
        }
        
        public Boolean TryGetValue(Key key, [MaybeNullWhen(false)] out KeyboardBindingHandler<T> value)
        {
            return Storage.TryGetValue(key, out value);
        }
        
        public void Add(Key key, KeyboardBindingHandler<T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Storage.Add(key, value);
        }
        
        public Boolean TryAdd(Key key, KeyboardBindingHandler<T>? value)
        {
            return value is not null && Storage.TryAdd(key, value);
        }
        
        void ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>.Add(KeyValuePair<Key, KeyboardBindingHandler<T>> item)
        {
            Add(item.Key, item.Value);
        }
        
        public Boolean Remove(Key key)
        {
            return Storage.Remove(key);
        }
        
        public Boolean Remove(Key key, [MaybeNullWhen(false)] out KeyboardBindingHandler<T> handler)
        {
            return Storage.Remove(key, out handler);
        }
        
        Boolean ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>.Remove(KeyValuePair<Key, KeyboardBindingHandler<T>> item)
        {
            return ((ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>) Storage).Remove(item);
        }

        public void Clear()
        {
            Storage.Clear();
        }
        
        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Storage).CopyTo(array, index);
        }
        
        void ICollection<KeyValuePair<Key, KeyboardBindingHandler<T>>>.CopyTo(KeyValuePair<Key, KeyboardBindingHandler<T>>[] array, Int32 index)
        {
            Storage.CopyTo(array, index);
        }
        
        public IEnumerator<KeyValuePair<Key, KeyboardBindingHandler<T>>> GetEnumerator()
        {
            return Storage.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Storage).GetEnumerator();
        }
        
        public virtual KeyboardBinding<T> Clone()
        {
            return new KeyboardBinding<T>();
        }
        
        IKeyboardBinding<T> IKeyboardBinding<T>.Clone()
        {
            return Clone();
        }

        IKeyboardBinding<T> ICloneable<IKeyboardBinding<T>>.Clone()
        {
            return Clone();
        }

        IKeyboardBinding IKeyboardBinding.Clone()
        {
            return Clone();
        }

        IKeyboardBinding ICloneable<IKeyboardBinding>.Clone()
        {
            return Clone();
        }
        
        IReadOnlyKeyboardBinding<T> IReadOnlyKeyboardBinding<T>.Clone()
        {
            return Clone();
        }
        
        IReadOnlyKeyboardBinding<T> ICloneable<IReadOnlyKeyboardBinding<T>>.Clone()
        {
            return Clone();
        }
        
        IReadOnlyKeyboardBinding IReadOnlyKeyboardBinding.Clone()
        {
            return Clone();
        }
        
        IReadOnlyKeyboardBinding ICloneable<IReadOnlyKeyboardBinding>.Clone()
        {
            return Clone();
        }
        
        IKeyboardBindingInfo IKeyboardBindingInfo.Clone()
        {
            return Clone();
        }
        
        IKeyboardBindingInfo ICloneable<IKeyboardBindingInfo>.Clone()
        {
            return Clone();
        }
        
        Object ICloneable.Clone()
        {
            return Clone();
        }
        
        public KeyboardBindingHandler<T> this[Key key]
        {
            get
            {
                return Storage[key];
            }
            set
            {
                Storage[key] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
}