// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys
{
    public readonly struct HotKeyAction : IEquatable<HotKeyAction>, IComparable<HotKeyAction>
    {
        public static Boolean operator ==(HotKeyAction first, HotKeyAction second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(HotKeyAction first, HotKeyAction second)
        {
            return !(first == second);
        }
        
        public static implicit operator WindowsHotKeyAction(HotKeyAction value)
        {
            return new WindowsHotKeyAction((Char) value.Key, value.Modifier);
        }

        public static implicit operator HotKeyAction(WindowsHotKeyAction value)
        {
            return new HotKeyAction(value);
        }
        
        public static implicit operator String?(HotKeyAction value)
        {
            return value.Title;
        }
        
        public static implicit operator Keys(HotKeyAction value)
        {
            return value.Key;
        }
        
        public static implicit operator HotKeyModifierKeys(HotKeyAction value)
        {
            return value.Modifier;
        }
        
        public String? Title { get; }
        public Keys Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }

        public HotKeyAction(Keys key, HotKeyModifierKeys modifier)
            : this(null, key, modifier)
        {
        }

        public HotKeyAction(String? title, Keys key, HotKeyModifierKeys modifier)
        {
            Title = title;
            Key = key;
            Modifier = modifier;
        }
        
        public HotKeyAction(WindowsHotKeyAction value)
        {
            Title = value.Title;
            Key = (Keys) value.Key;
            Modifier = value.Modifier;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Title, Key, Modifier);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is HotKeyAction action && Equals(action);
        }

        public Boolean Equals(HotKeyAction other)
        {
            return Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }

        public Int32 CompareTo(HotKeyAction other)
        {
            Int32 comparison = Key.CompareTo(other.Key);
            return comparison != 0 ? comparison : Modifier.CompareTo(other.Modifier);
        }

        public override String? ToString()
        {
            return Title;
        }
    }
    
    public readonly struct HotKeyAction<T> : IEquatable<HotKeyAction<T>>, IComparable<HotKeyAction<T>> where T : unmanaged, IConvertible
    {
        public static Boolean operator ==(HotKeyAction<T> first, HotKeyAction<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(HotKeyAction<T> first, HotKeyAction<T> second)
        {
            return !(first == second);
        }
        
        public static implicit operator HotKeyAction(HotKeyAction<T> value)
        {
            return new HotKeyAction(value.Key, value.Modifier);
        }
        
        public static implicit operator WindowsHotKeyAction(HotKeyAction<T> value)
        {
            return new WindowsHotKeyAction((Char) value.Key, value.Modifier);
        }
        
        public static implicit operator WindowsHotKeyAction<T>(HotKeyAction<T> value)
        {
            return new WindowsHotKeyAction<T>(value.Id, (Char) value.Key, value.Modifier);
        }
        
        public static implicit operator HotKeyAction<T>(WindowsHotKeyAction<T> value)
        {
            return new HotKeyAction<T>(value);
        }
        
        public static implicit operator T(HotKeyAction<T> value)
        {
            return value.Id;
        }
        
        public static implicit operator String?(HotKeyAction<T> value)
        {
            return value.Title;
        }
        
        public static implicit operator Keys(HotKeyAction<T> value)
        {
            return value.Key;
        }
        
        public static implicit operator HotKeyModifierKeys(HotKeyAction<T> value)
        {
            return value.Modifier;
        }
        
        public T Id { get; }
        public String? Title { get; }
        public Keys Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }

        public HotKeyAction(T id, Keys key, HotKeyModifierKeys modifier)
            : this(id, null, key, modifier)
        {
        }

        public HotKeyAction(T id, String? title, Keys key, HotKeyModifierKeys modifier)
        {
            Id = id;
            Title = title;
            Key = key;
            Modifier = modifier;
        }
        
        public HotKeyAction(T id, HotKeyAction hotkey)
            : this(id, hotkey, hotkey, hotkey)
        {
        }
        
        public HotKeyAction(WindowsHotKeyAction<T> value)
        {
            Id = value.Id;
            Title = value.Title;
            Key = (Keys) value.Key;
            Modifier = value.Modifier;
        }
        
        public HotKeyAction<TId> As<TId>() where TId : unmanaged, IConvertible
        {
            return As(static id => (TId) Convert.ChangeType(id, typeof(TId)));
        }

        public HotKeyAction<TId> As<TId>(Func<T, TId> selector) where TId : unmanaged, IConvertible
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            TId id = selector(Id);
            return new HotKeyAction<TId>(id, Title, Key, Modifier);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Id, Title, Key, Modifier);
        }
        
        public override Boolean Equals(Object? obj)
        {
            return obj is HotKeyAction<T> action && Equals(action);
        }

        public Boolean Equals(HotKeyAction<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other.Id) && Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }
        
        public Int32 CompareTo(HotKeyAction<T> other)
        {
            Int32 comparison = Key.CompareTo(other.Key);
            return comparison != 0 ? comparison : Modifier.CompareTo(other.Modifier);
        }

        public override String? ToString()
        {
            return Title;
        }
    }
}