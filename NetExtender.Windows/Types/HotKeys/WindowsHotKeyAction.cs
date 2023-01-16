// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Windows.Utilities;

namespace NetExtender.Types.HotKeys
{
    public readonly struct WindowsHotKeyAction
    {
        public static implicit operator Char(WindowsHotKeyAction value)
        {
            return value.Key;
        }
        
        public static implicit operator HotKeyModifierKeys(WindowsHotKeyAction value)
        {
            return value.Modifier;
        }
        
        public Char Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }
        
        public WindowsHotKeyAction(Char key, HotKeyModifierKeys modifier)
        {
            Key = key;
            Modifier = modifier;
        }
    }
    
    public readonly struct WindowsHotKeyAction<T> where T : unmanaged, IConvertible
    {
        public static implicit operator WindowsHotKeyAction(WindowsHotKeyAction<T> value)
        {
            return new WindowsHotKeyAction(value.Key, value.Modifier);
        }
        
        public static implicit operator T(WindowsHotKeyAction<T> value)
        {
            return value.Id;
        }
        
        public static implicit operator Char(WindowsHotKeyAction<T> value)
        {
            return value.Key;
        }
        
        public static implicit operator HotKeyModifierKeys(WindowsHotKeyAction<T> value)
        {
            return value.Modifier;
        }
        
        public T Id { get; }
        public Char Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }
        
        public WindowsHotKeyAction(T id, Char key, HotKeyModifierKeys modifier)
        {
            Id = id;
            Key = key;
            Modifier = modifier;
        }
        
        public WindowsHotKeyAction(T id, WindowsHotKeyAction hotkey)
            : this(id, hotkey, hotkey)
        {
        }
    }
}