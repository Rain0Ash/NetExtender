// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys
{
    public readonly struct HotKeyAction
    {
        public static implicit operator WindowsHotKeyAction(HotKeyAction value)
        {
            return new WindowsHotKeyAction((Char) value.Key, (HotKeyModifierKeys) value.Modifier);
        }
        
        public static implicit operator Key(HotKeyAction value)
        {
            return value.Key;
        }
        
        public static implicit operator ModifierKeys(HotKeyAction value)
        {
            return value.Modifier;
        }
        
        public Key Key { get; }
        public ModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }
        
        public HotKeyAction(Key key, ModifierKeys modifier)
        {
            Key = key;
            Modifier = modifier;
        }
    }
    
    public readonly struct HotKeyAction<T> where T : unmanaged, IConvertible
    {
        public static implicit operator HotKeyAction(HotKeyAction<T> value)
        {
            return new HotKeyAction(value.Key, value.Modifier);
        }
        
        public static implicit operator WindowsHotKeyAction(HotKeyAction<T> value)
        {
            return new WindowsHotKeyAction((Char) value.Key, (HotKeyModifierKeys) value.Modifier);
        }
        
        public static implicit operator WindowsHotKeyAction<T>(HotKeyAction<T> value)
        {
            return new WindowsHotKeyAction<T>(value.Id, (Char) value.Key, (HotKeyModifierKeys) value.Modifier);
        }
        
        public static implicit operator T(HotKeyAction<T> value)
        {
            return value.Id;
        }
        
        public static implicit operator Key(HotKeyAction<T> value)
        {
            return value.Key;
        }
        
        public static implicit operator ModifierKeys(HotKeyAction<T> value)
        {
            return value.Modifier;
        }
        
        public T Id { get; }
        public Key Key { get; }
        public ModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }
        
        public HotKeyAction(T id, Key key, ModifierKeys modifier)
        {
            Id = id;
            Key = key;
            Modifier = modifier;
        }
        
        public HotKeyAction(T id, HotKeyAction hotkey)
            : this(id, hotkey, hotkey)
        {
        }
    }
}