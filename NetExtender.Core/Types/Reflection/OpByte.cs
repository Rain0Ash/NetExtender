using System;
using System.Reflection.Emit;

namespace NetExtender.Types.Reflection
{
    public readonly struct OpByte : IEquatable<Byte>, IEquatable<OpByte>, IEquatable<OpCode>
    {
        public static implicit operator Byte(OpByte value)
        {
            return value.Value;
        }
        
        public static implicit operator OpByte(Byte value)
        {
            return new OpByte(value);
        }
        
        public static Boolean operator ==(OpByte first, OpByte second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(OpByte first, OpByte second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(OpByte first, OpCode second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(OpByte first, OpCode second)
        {
            return !(first == second);
        }
        
        public Byte Value { get; }
        
        public OpByte(Byte value)
        {
            Value = value;
        }
        
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                Byte value => Equals(value),
                OpByte value => Equals(value),
                OpCode value => Equals(value),
                _ => false
            };
        }
        
        public Boolean Equals(Byte other)
        {
            return Value == other;
        }
        
        public Boolean Equals(OpByte other)
        {
            return Value == other.Value;
        }
        
        public Boolean Equals(OpCode other)
        {
            return Value == other.Value;
        }
        
        public override String ToString()
        {
            return Value.ToString();
        }
    }
}