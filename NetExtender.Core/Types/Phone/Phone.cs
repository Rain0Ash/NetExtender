using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Phone
{
    // TODO:
    [Serializable]
    public readonly struct Phone : IEqualityStruct<Phone>, IEquatable<String>
    {
        public static Boolean operator ==(Phone first, Phone second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Phone first, Phone second)
        {
            return !(first == second);
        }
        
        private String? Number { get; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return String.IsNullOrEmpty(Number);
            }
        }

        private Phone(String number)
        {
            Number = number;
        }

        public static Phone Parse(String number)
        {
            if (String.IsNullOrEmpty(number))
            {
                throw new ArgumentNullOrEmptyStringException(number, nameof(number));
            }

            return new Phone(number);
        }

        public static Boolean TryParse(String? number, out Phone result)
        {
            if (String.IsNullOrEmpty(number))
            {
                result = default;
                return false;
            }
            
            result = new Phone(number);
            return true;
        }

        public Int32 CompareTo(Phone other)
        {
            return String.Compare(Number, other.Number, StringComparison.Ordinal);
        }

        public override Int32 GetHashCode()
        {
            return Number?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                String value => Equals(value),
                Phone value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(String? other)
        {
            return other is not null ? TryParse(other, out Phone phone) && Equals(phone) : IsEmpty;
        }

        public Boolean Equals(Phone other)
        {
            return Number == other.Number;
        }

        public override String? ToString()
        {
            return Number;
        }
    }
}