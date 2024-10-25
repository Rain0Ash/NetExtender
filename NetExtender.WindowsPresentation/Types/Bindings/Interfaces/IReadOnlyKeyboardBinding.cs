using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using NetExtender.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Bindings.Interfaces
{
    public interface IReadOnlyKeyboardBinding<T> : IReadOnlyDictionary<Key, KeyboardBindingHandler<T>>, IReadOnlyKeyboardBinding, ICloneable<IReadOnlyKeyboardBinding<T>> where T : DependencyObject
    {
        public new Int32 Count { get; }
        public new IEnumerable<Key> Keys { get; }
        public new IEnumerable<KeyboardBindingHandler<T>> Values { get; }
        
        public new Boolean ContainsKey(Key key);
        public new IEnumerator<KeyValuePair<Key, KeyboardBindingHandler<T>>> GetEnumerator();
        public new IReadOnlyKeyboardBinding<T> Clone();
    }
    
    public interface IReadOnlyKeyboardBinding : IKeyboardBindingInfo, ICollection, ICloneable<IReadOnlyKeyboardBinding>
    {
        public new Int32 Count { get; }
        
        public new IReadOnlyKeyboardBinding Clone();
    }
}