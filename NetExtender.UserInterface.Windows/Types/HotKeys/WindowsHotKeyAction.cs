// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NetExtender.Types.HotKeys.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys
{
    public readonly struct WindowsHotKeyAction : IWindowsHotKeyAction<WindowsHotKeyAction>
    {
        public static Boolean operator ==(WindowsHotKeyAction first, WindowsHotKeyAction second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WindowsHotKeyAction first, WindowsHotKeyAction second)
        {
            return !(first == second);
        }
        
        public static implicit operator String?(WindowsHotKeyAction value)
        {
            return value.Title;
        }
        
        public static implicit operator Char(WindowsHotKeyAction value)
        {
            return value.Key;
        }
        
        public static implicit operator HotKeyModifierKeys(WindowsHotKeyAction value)
        {
            return value.Modifier;
        }
        
        public String? Title { get; }
        public Char Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Char VirtualKey
        {
            get
            {
                return Key;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }

        public WindowsHotKeyAction(Char key)
            : this(key, HotKeyModifierKeys.None)
        {
        }

        public WindowsHotKeyAction(Char key, HotKeyModifierKeys modifier)
            : this(null, key, modifier)
        {
        }

        public WindowsHotKeyAction(String? title, Char key)
            : this(title, key, HotKeyModifierKeys.None)
        {
        }

        public WindowsHotKeyAction(String? title, Char key, HotKeyModifierKeys modifier)
        {
            Title = title;
            Key = key;
            Modifier = modifier;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Title, Key, Modifier);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is WindowsHotKeyAction action && Equals(action);
        }

        public Boolean Equals(WindowsHotKeyAction other)
        {
            return Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }

        public Boolean Equals(Char key, HotKeyModifierKeys modifier)
        {
            return Key == key && Modifier == modifier;
        }

        public Boolean Equals((Char Key, HotKeyModifierKeys Modifier) other)
        {
            return Equals(other.Key, other.Modifier);
        }

        public Int32 CompareTo(WindowsHotKeyAction other)
        {
            Int32 comparison = Key.CompareTo(other.Key);
            return comparison != 0 ? comparison : Modifier.CompareTo(other.Modifier);
        }

        public override String ToString()
        {
            return Title ?? String.Empty;
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            return new StringBuilder(format)
                .Replace("{TITLE}", Title?.ToString(provider))
                .Replace("{KEY}", Key.ToString(provider))
                .Replace("{MODIFIER}", Modifier.ToString())
                .Replace("{VKEY}", VirtualKey.ToString(provider))
                .Replace("{VIRTUALKEY}", VirtualKey.ToString(provider))
                .ToString();
        }
    }
    
    public readonly struct WindowsHotKeyAction<T> : IWindowsHotKeyAction<WindowsHotKeyAction<T>, T> where T : unmanaged, IComparable<T>, IConvertible
    {
        public static Boolean operator ==(WindowsHotKeyAction<T> first, WindowsHotKeyAction<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(WindowsHotKeyAction<T> first, WindowsHotKeyAction<T> second)
        {
            return !(first == second);
        }
        
        public static implicit operator WindowsHotKeyAction(WindowsHotKeyAction<T> value)
        {
            return new WindowsHotKeyAction(value.VirtualKey, value.Modifier);
        }
        
        public static implicit operator T(WindowsHotKeyAction<T> value)
        {
            return value.Id;
        }
        
        public static implicit operator String?(WindowsHotKeyAction<T> value)
        {
            return value.Title;
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
        public String? Title { get; }
        public Char Key { get; }
        public HotKeyModifierKeys Modifier { get; }

        public Char VirtualKey
        {
            get
            {
                return Key;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return Key <= 0;
            }
        }

        public WindowsHotKeyAction(T id, Char key)
            : this(id, key, HotKeyModifierKeys.None)
        {
        }

        public WindowsHotKeyAction(T id, Char key, HotKeyModifierKeys modifier)
            : this(id, null, key, modifier)
        {
        }

        public WindowsHotKeyAction(T id, String? title, Char key)
            : this(id, title, key, HotKeyModifierKeys.None)
        {
        }

        public WindowsHotKeyAction(T id, String? title, Char key, HotKeyModifierKeys modifier)
        {
            Id = id;
            Title = title;
            Key = key;
            Modifier = modifier;
        }
        
        public WindowsHotKeyAction(T id, WindowsHotKeyAction hotkey)
            : this(id, hotkey, hotkey, hotkey)
        {
        }

        public WindowsHotKeyAction<TId> As<TId>() where TId : unmanaged, IComparable<TId>, IConvertible
        {
            return As(static id => (TId) Convert.ChangeType(id, typeof(TId)));
        }

        public WindowsHotKeyAction<TId> As<TId>(Func<T, TId> selector) where TId : unmanaged, IComparable<TId>, IConvertible
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            TId id = selector(Id);
            return new WindowsHotKeyAction<TId>(id, Title, Key, Modifier);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Id, Title, Key, Modifier);
        }
        
        public override Boolean Equals(Object? obj)
        {
            return obj is WindowsHotKeyAction<T> action && Equals(action);
        }

        public Boolean Equals(WindowsHotKeyAction<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other.Id) && Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }
        
        public Boolean Equals(Char key, HotKeyModifierKeys modifier)
        {
            return Key == key && Modifier == modifier;
        }

        public Boolean Equals((Char Key, HotKeyModifierKeys Modifier) other)
        {
            return Equals(other.Key, other.Modifier);
        }
        
        public Int32 CompareTo(WindowsHotKeyAction<T> other)
        {
            Int32 comparison = Id.CompareTo(other.Id);
            if (comparison != 0)
            {
                return comparison;
            }

            comparison = Key.CompareTo(other.Key);
            return comparison != 0 ? comparison : Modifier.CompareTo(other.Modifier);
        }

        public override String ToString()
        {
            return Title ?? Id.ToString(CultureInfo.InvariantCulture);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return new StringBuilder(format)
                .Replace("{ID}", Id.ToString(provider))
                .Replace("{TITLE}", Title?.ToString(provider))
                .Replace("{KEY}", Key.ToString(provider))
                .Replace("{MODIFIER}", Modifier.ToString())
                .Replace("{VKEY}", VirtualKey.ToString(provider))
                .Replace("{VIRTUALKEY}", VirtualKey.ToString(provider))
                .ToString();
        }
    }
}