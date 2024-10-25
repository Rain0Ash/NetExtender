using System;
using System.Collections.Generic;
using System.Windows.Input;
using NetExtender.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Bindings.Interfaces
{
    public interface IKeyboardBindingInfo : ICloneable<IKeyboardBindingInfo>, ICloneable
    {
        public Int32 Count { get; }
        public IEnumerable<Key> Keys { get; }
        
        public Boolean Invoke(Object? sender, KeyInfo key);
        public Boolean Invoke(Object? sender, KeyInfo key, out Boolean result);
        public Boolean ContainsKey(Key key);
        public new IKeyboardBindingInfo Clone();
    }
}