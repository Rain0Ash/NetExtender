// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using BounceBigInteger = Org.BouncyCastle.Math.BigInteger;

namespace NetExtender.BouncyCastle.Types.Numerics
{
    public readonly struct BouncyBigIntegerWrapper : IEquatable<BigInteger>, IEquatable<BounceBigInteger>, IEquatable<BouncyBigIntegerWrapper>,
        IComparable<BigInteger>, IComparable<BounceBigInteger>, IComparable<BouncyBigIntegerWrapper>
    {
        public static implicit operator BouncyBigIntegerWrapper(Int64 value)
        {
            return new BouncyBigIntegerWrapper(value);
        }
        
        public static implicit operator BouncyBigIntegerWrapper(UInt64 value)
        {
            return new BouncyBigIntegerWrapper(value);
        }
        
        public static implicit operator BigInteger(BouncyBigIntegerWrapper wrapper)
        {
            return wrapper.Value;
        }
        
        public static implicit operator BouncyBigIntegerWrapper(BigInteger value)
        {
            return new BouncyBigIntegerWrapper(value);
        }
        
        public static implicit operator BounceBigInteger(BouncyBigIntegerWrapper wrapper)
        {
            BigInteger integer = wrapper.Value;
            return new BounceBigInteger(integer.Sign, BigInteger.Abs(integer).ToByteArray(true, true));
        }
        
        public static implicit operator BouncyBigIntegerWrapper(BounceBigInteger? value)
        {
            return value is not null ? new BouncyBigIntegerWrapper(value) : default;
        }
        
        public static Boolean operator ==(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return first.Value == second.Value;
        }

        public static Boolean operator !=(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return !(first == second);
        }
        
        public static Boolean operator >(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return first.Value > second.Value;
        }
        
        public static Boolean operator <(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return first.Value < second.Value;
        }
        
        public static Boolean operator >=(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return first.Value >= second.Value;
        }
        
        public static Boolean operator <=(BouncyBigIntegerWrapper first, BouncyBigIntegerWrapper second)
        {
            return first.Value <= second.Value;
        }

        public BigInteger Value { get; }

        public BouncyBigIntegerWrapper(Int64 value)
        {
            Value = value;
        }
        
        public BouncyBigIntegerWrapper(UInt64 value)
        {
            Value = value;
        }
        
        public BouncyBigIntegerWrapper(BounceBigInteger? value)
        {
            Value = value is not null ? new BigInteger(value.ToByteArrayUnsigned(), value.SignValue >= 0, true) : BigInteger.Zero;
        }
        
        public BouncyBigIntegerWrapper(BigInteger value)
        {
            Value = value;
        }

        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                BouncyBigIntegerWrapper wrapper => Equals(wrapper),
                BounceBigInteger integer => Equals(integer),
                BigInteger integer => Equals(integer),
                _ => false
            };
        }

        public Boolean Equals(BigInteger other)
        {
            return Value.Equals(other);
        }

        public Boolean Equals(BounceBigInteger? other)
        {
            return other is not null && Equals(new BouncyBigIntegerWrapper(other));
        }

        public Boolean Equals(BouncyBigIntegerWrapper other)
        {
            return Value.Equals(other.Value);
        }

        public Int32 CompareTo(BigInteger other)
        {
            return Value.CompareTo(other);
        }

        public Int32 CompareTo(BounceBigInteger? other)
        {
            return other is not null ? CompareTo(new BouncyBigIntegerWrapper(other)) : 1;
        }

        public Int32 CompareTo(BouncyBigIntegerWrapper other)
        {
            return Value.CompareTo(other.Value);
        }

        public override String ToString()
        {
            return Value.ToString();
        }
        
        public static BouncyBigIntegerWrapper CreateRandomInRange(BouncyBigIntegerWrapper min, BouncyBigIntegerWrapper max, SecureRandom random)
        {
            return BigIntegers.CreateRandomInRange(min, max, random);
        }
    }
}