using System;
using System.Windows.Input;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types
{
    public readonly struct KeyInfo : IEquatable<KeyInfo>, IComparable<KeyInfo>
    {
        public static Boolean operator ==(KeyInfo first, KeyInfo second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(KeyInfo first, KeyInfo second)
        {
            return !(first == second);
        }
        
        public static implicit operator Key(KeyInfo value)
        {
            return value.Key;
        }
        
        public static implicit operator ModifierKeys(KeyInfo value)
        {
            return value.Modifiers;
        }
        
        public static implicit operator KeyStates(KeyInfo value)
        {
            return value.State;
        }
        
        public static implicit operator KeyInfo(Key value)
        {
            return new KeyInfo(value);
        }
        
        public static implicit operator KeyInfo(KeyEventArgs? value)
        {
            return value is not null ? new KeyInfo(value.Key)
            {
                Modifiers = Keyboard.Modifiers,
                State = value.KeyStates,
                IsRepeat = value.IsRepeat
            } : new KeyInfo();
        }
        
        public Key Key { get; }
        public ModifierKeys Modifiers { get; init; }
        
        public Boolean Alt
        {
            get
            {
                return Modifiers.HasFlag(ModifierKeys.Alt);
            }
        }
        
        public Boolean Control
        {
            get
            {
                return Modifiers.HasFlag(ModifierKeys.Control);
            }
        }
        
        public Boolean Shift
        {
            get
            {
                return Modifiers.HasFlag(ModifierKeys.Shift);
            }
        }
        
        public Boolean Windows
        {
            get
            {
                return Modifiers.HasFlag(ModifierKeys.Windows);
            }
        }
        public KeyStates State { get; init; }
        public Boolean IsRepeat { get; init; }
        
        private KeyInfo(Key key)
        {
            Key = key;
            Modifiers = ModifierKeys.None;
            State = KeyStates.None;
            IsRepeat = false;
        }
        
        public static KeyInfo Create(Key key)
        {
            return new KeyInfo(key)
            {
                Modifiers = Keyboard.Modifiers,
                State = Keyboard.GetKeyStates(key)
            };
        }
        
        public Boolean Simplify(out KeyInfo result)
        {
            if (IsRepeat)
            {
                result = new KeyInfo(Key) { Modifiers = Modifiers, State = State };
                return true;
            }
            
            if (State != KeyStates.None)
            {
                result = new KeyInfo(Key) { Modifiers = Modifiers };
                return true;
            }
            
            if (Modifiers != ModifierKeys.None)
            {
                result = new KeyInfo(Key);
                return true;
            }
            
            result = this;
            return false;
        }
        
        public Int32 CompareTo(KeyInfo other)
        {
            Int32 key = Key.CompareTo(other.Key);
            if (key != 0)
            {
                return key;
            }
            
            Int32 modifiers = Modifiers.CompareTo(other.Modifiers);
            if (modifiers != 0)
            {
                return modifiers;
            }
            
            Int32 state = State.CompareTo(other.State);
            return state != 0 ? state : IsRepeat.CompareTo(other.IsRepeat);
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, Modifiers, State, IsRepeat);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other is KeyInfo info && Equals(info);
        }
        
        public Boolean Equals(KeyInfo other)
        {
            return Key == other.Key && Modifiers == other.Modifiers && State == other.State && IsRepeat == other.IsRepeat;
        }
        
        public override String ToString()
        {
            return $"{{ {nameof(Key)}: {Key}, {nameof(Modifiers)}: {String.Join(" + ", Modifiers.Flags())}, {nameof(State)}: {State}, {nameof(IsRepeat)}: {IsRepeat} }}";
        }
    }
}