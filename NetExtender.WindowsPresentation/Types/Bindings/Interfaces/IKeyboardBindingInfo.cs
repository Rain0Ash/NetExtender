// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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