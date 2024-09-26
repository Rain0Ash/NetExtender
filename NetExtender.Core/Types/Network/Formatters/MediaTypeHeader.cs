using System;
using System.Net.Http.Headers;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.Formatters
{
    public readonly struct MediaTypeHeader : IEquatable<MediaTypeHeader>, IEquatable<MediaTypeHeaderValue>, IComparable<MediaTypeHeader>, IComparable<MediaTypeHeaderValue>
    {
        public static implicit operator MediaTypeHeader(MediaTypeHeaderValue? value)
        {
            return value is not null ? new MediaTypeHeader(value) : default;
        }

        public static Boolean operator ==(MediaTypeHeader first, MediaTypeHeader second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(MediaTypeHeader first, MediaTypeHeader second)
        {
            return !(first == second);
        }
        
        internal MediaTypeHeaderValue Value { get; }
        
        public String? CharSet
        {
            get
            {
                return Value?.CharSet;
            }
        }
        
        public String? MediaType
        {
            get
            {
                return Value?.MediaType;
            }
        }
        
        public Boolean IsEmpty
        {
            get
            {
                return Value is null;
            }
        }
        
        public MediaTypeHeader(MediaTypeHeaderValue value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public MediaTypeHeaderValue? Clone()
        {
            return Value?.Clone();
        }
        
        public Int32 CompareTo(MediaTypeHeader other)
        {
            return CompareTo(other.Value);
        }
        
        public Int32 CompareTo(MediaTypeHeaderValue? other)
        {
            return String.Compare(Value?.MediaType, other?.MediaType, StringComparison.OrdinalIgnoreCase);
        }
        
        public override Int32 GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Value is null,
                MediaTypeHeaderValue value => Equals(value),
                MediaTypeHeader value => Equals(value),
                _ => false
            };
        }
        
        public Boolean Equals(MediaTypeHeader other)
        {
            return Equals(other.Value);
        }

        public Boolean Equals(MediaTypeHeaderValue? other)
        {
            return ReferenceEquals(Value, other) || Value is not null && Value.Equals(other);
        }
        
        public override String? ToString()
        {
            return Value?.ToString();
        }
    }
}