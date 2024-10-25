using System;
using System.Collections;
using System.Windows;
using NetExtender.Types.Comparers;

namespace NetExtender.WindowsPresentation.Types.Clipboard
{
    public enum ClipboardType
    {
        NoContent,
        Text,
        Image,
        Audio,
        Files,
        Raw,
        Data
    }
    
    public readonly struct ClipboardObject : IEquatable<ClipboardObject>, IComparable<ClipboardObject>, IComparable
    {
        public static Boolean operator ==(ClipboardObject first, ClipboardObject second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(ClipboardObject first, ClipboardObject second)
        {
            return !(second == first);
        }
        
        public ClipboardType Type { get; }
        public Object? Value { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Type == ClipboardType.NoContent && Value is null;
            }
        }

        public ClipboardObject(ClipboardType type, Object? value)
        {
            Type = type;
            Value = value;
        }
        
        public Int32 CompareTo(Object? other)
        {
            if (other is ClipboardObject @object)
            {
                return CompareTo(@object);
            }
            
            try
            {
                return Comparer.Default.Compare(Value, other);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public Int32 CompareTo(ClipboardObject other)
        {
            try
            {
                return Comparer.Default.Compare(Value, other);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                ClipboardObject @object => Equals(@object),
                _ => Equals(Value, other)
            };
        }
        
        public Boolean Equals(ClipboardObject other)
        {
            if (Type != other.Type)
            {
                return false;
            }
            
            if (Value is IEnumerable first && other.Value is IEnumerable second)
            {
                return EnumerableEqualityComparer.Default.Equals(first, second);
            }
            
            return Equals(Value, other.Value);
        }
        
        public override String ToString()
        {
            return $"{{ {nameof(Type)}: {Type}, {nameof(Value)}: {(Value is Delegate ? "?" : Value)} }}";
        }
    }
}