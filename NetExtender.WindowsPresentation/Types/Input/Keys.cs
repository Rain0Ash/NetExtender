// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using NetExtender.Initializer.Types.Flags;
using NetExtender.Initializer.Types.Flags.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Input
{
    [Serializable]
    public readonly struct Keys : IFlag, IEquatable<Keys>, IReadOnlyCollection<Key>
    {
        public static implicit operator ReadOnlySpan<Byte>(in Keys value)
        {
            return value.Internal;
        }
        
        public static implicit operator ReadOnlySpan<UInt16>(in Keys value)
        {
            return value.Internal;
        }
        
        public static implicit operator ReadOnlySpan<UInt32>(in Keys value)
        {
            return value.Internal;
        }
        
        public static implicit operator ReadOnlySpan<UInt64>(in Keys value)
        {
            return value.Internal;
        }
        
        public static Boolean operator ==(Keys first, Keys second)
        {
            return first.Internal == second.Internal;
        }
        
        public static Boolean operator !=(Keys first, Keys second)
        {
            return !(first == second);
        }
        
        public static Keys operator |(Keys first, Keys second)
        {
            return new Keys(first.Internal | second.Internal);
        }
        
        public static Keys operator &(Keys first, Keys second)
        {
            return new Keys(first.Internal & second.Internal);
        }
        
        public static Keys operator ^(Keys first, Keys second)
        {
            return new Keys(first.Internal ^ second.Internal);
        }
        
        public static Keys operator ~(Keys value)
        {
            return new Keys(~value.Internal);
        }
        
        Int32 IFlag.Size
        {
            get
            {
                return Internal.Size;
            }
        }
        
        public Int32 Count
        {
            get
            {
                return Internal.PopCount;
            }
        }
        
        Int32 IReadOnlyCollection<Boolean>.Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Int32 IFlag.PopCount
        {
            get
            {
                return Internal.PopCount;
            }
        }
        
        private Flag256 Internal { get; }

        private Keys(Flag256 value)
        {
            Internal = value;
        }

        public Keys(Key key)
        {
            Internal = new Flag256(1) << (Int32) key;
        }
        
        public Keys(params Key[]? keys)
            : this((IEnumerable<Key>?) keys)
        {
        }

        public Keys(IEnumerable<Key>? keys)
        {
            Internal = keys?.Aggregate(new Flag256(), (current, key) => current | new Flag256(1) << (Int32) key) ?? new Flag256();
        }

        public Keys(ReadOnlySpan<Key> keys)
        {
            Internal = keys.Aggregate(new Flag256(), (current, key) => current | new Flag256(1) << (Int32) key);
        }

        ReadOnlySpan<Byte> IFlag.AsSpan()
        {
            return Internal.AsSpan();
        }

        Boolean IEquatable<IFlag>.Equals(IFlag? other)
        {
            return Internal.Equals(other);
        }

        Boolean IFlag.HasFlag(ReadOnlySpan<Byte> value)
        {
            return Internal.HasFlag(value);
        }

        public Boolean HasFlag(Key key)
        {
            return HasFlag(new Keys(key));
        }
        
        public Boolean HasFlag(Keys keys)
        {
            return Internal.HasFlag(keys.Internal);
        }

        Boolean IFlag.HasFlag<T>(T value)
        {
            return Internal.HasFlag(value);
        }

        Boolean IFlag.HasIFlag<T>(T value)
        {
            return Internal.HasIFlag(value);
        }
        
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                Keys keys => Equals(keys),
                _ => Internal.Equals(obj),
            };
        }

        Boolean IFlag.Equals(ReadOnlySpan<Byte> value)
        {
            return Internal.Equals(value);
        }

        public Boolean Equals(Key other)
        {
            return Equals(new Keys(other));
        }
        
        public Boolean Equals(Keys other)
        {
            return Internal.Equals(other.Internal);
        }

        public override String ToString()
        {
            return String.Join(" + ", GetEnumerator().AsEnumerable());
        }
        
        IEnumerable<Int32> IFlag.EnumerateSetBits()
        {
            return Internal.EnumerateSetBits();
        }

        public IEnumerator<Key> GetEnumerator()
        {
            return Internal.EnumerateSetBits().Select(position => (Key) position).GetEnumerator();
        }

        IEnumerator<Boolean> IEnumerable<Boolean>.GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean this[Int32 position]
        {
            get
            {
                return Internal[position];
            }
        }

        public Boolean this[Key key]
        {
            get
            {
                return this[(Int32) key];
            }
        }
    }
}