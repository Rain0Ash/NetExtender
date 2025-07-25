// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using NetExtender.Types.HotKeys.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys
{
    public readonly struct HotKeyAction : IHotKeyAction<HotKeyAction>
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
            return new WindowsHotKeyAction(value.VirtualKey, value.Modifier);
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
        
        public Char VirtualKey
        {
            get
            {
                return (Char) Key;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Key <= 0;
            }
        }

        public HotKeyAction(Keys key)
            : this(key, HotKeyModifierKeys.None)
        {
        }

        public HotKeyAction(Keys key, HotKeyModifierKeys modifier)
            : this(null, key, modifier)
        {
        }

        public HotKeyAction(String? title, Keys key)
            : this(title, key, HotKeyModifierKeys.None)
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

        public override Boolean Equals(Object? other)
        {
            return other is HotKeyAction action && Equals(action);
        }

        public Boolean Equals(HotKeyAction other)
        {
            return Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }
        
        public Boolean Equals(Keys key, HotKeyModifierKeys modifier)
        {
            return Key == key && Modifier == modifier;
        }

        public Boolean Equals((Keys Key, HotKeyModifierKeys Modifier) other)
        {
            return Equals(other.Key, other.Modifier);
        }

        public Int32 CompareTo(HotKeyAction other)
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
            return new StringBuilder(format)
                .Replace("{TITLE}", Title?.ToString(provider))
                .Replace("{KEY}", Key.ToString())
                .Replace("{MODIFIER}", Modifier.ToString())
                .Replace("{VKEY}", VirtualKey.ToString(provider))
                .Replace("{VIRTUALKEY}", VirtualKey.ToString(provider))
                .ToString();
        }
    }
    
    public readonly struct HotKeyAction<T> : IHotKeyAction<HotKeyAction<T>, T> where T : unmanaged, IComparable<T>, IConvertible
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
            return new WindowsHotKeyAction(value.VirtualKey, value.Modifier);
        }
        
        public static implicit operator WindowsHotKeyAction<T>(HotKeyAction<T> value)
        {
            return new WindowsHotKeyAction<T>(value.Id, value.VirtualKey, value.Modifier);
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
        
        public Char VirtualKey
        {
            get
            {
                return (Char) Key;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Key <= 0;
            }
        }

        public HotKeyAction(T id, Keys key)
            : this(id, key, HotKeyModifierKeys.None)
        {
        }

        public HotKeyAction(T id, Keys key, HotKeyModifierKeys modifier)
            : this(id, null, key, modifier)
        {
        }

        public HotKeyAction(T id, String? title, Keys key)
            : this(id, title, key, HotKeyModifierKeys.None)
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
        
        public HotKeyAction<TId> As<TId>() where TId : unmanaged, IComparable<TId>, IConvertible
        {
            return As(static id => (TId) Convert.ChangeType(id, typeof(TId)));
        }

        public HotKeyAction<TId> As<TId>(Func<T, TId> selector) where TId : unmanaged, IComparable<TId>, IConvertible
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
        
        public override Boolean Equals(Object? other)
        {
            return other is HotKeyAction<T> action && Equals(action);
        }

        public Boolean Equals(HotKeyAction<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Id, other.Id) && Title == other.Title && Key == other.Key && Modifier == other.Modifier;
        }
        
        public Boolean Equals(Keys key, HotKeyModifierKeys modifier)
        {
            return Key == key && Modifier == modifier;
        }

        public Boolean Equals((Keys Key, HotKeyModifierKeys Modifier) other)
        {
            return Equals(other.Key, other.Modifier);
        }
        
        public Int32 CompareTo(HotKeyAction<T> other)
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
                .Replace("{KEY}", Key.ToString())
                .Replace("{MODIFIER}", Modifier.ToString())
                .Replace("{VKEY}", VirtualKey.ToString(provider))
                .Replace("{VIRTUALKEY}", VirtualKey.ToString(provider))
                .ToString();
        }
    }
}