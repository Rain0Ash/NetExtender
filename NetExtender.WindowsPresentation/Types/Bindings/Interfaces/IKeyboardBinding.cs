// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using NetExtender.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Bindings.Interfaces
{
    public interface IKeyboardBinding<T> : IDictionary<Key, KeyboardBindingHandler<T>>, IKeyboardBinding, ICloneable<IKeyboardBinding<T>> where T : DependencyObject
    {
        public new Int32 Count { get; }
        public new ICollection<Key> Keys { get; }
        public new ICollection<KeyboardBindingHandler<T>> Values { get; }
        public new Boolean IsReadOnly { get; }
        
        public new Boolean ContainsKey(Key key);
        public new void Add(Key key, KeyboardBindingHandler<T> value);
        public Boolean TryAdd(Key key, KeyboardBindingHandler<T>? value);
        public new Boolean Remove(Key key);
        
        public new void Clear();
        public new IEnumerator<KeyValuePair<Key, KeyboardBindingHandler<T>>> GetEnumerator();
        public new IKeyboardBinding<T> Clone();
    }
    
    public interface IKeyboardBinding : IKeyboardBindingInfo, ICollection, ICloneable<IKeyboardBinding>
    {
        public new Int32 Count { get; }
        public Boolean Remove(Key key);
        public new IKeyboardBinding Clone();
    }
}