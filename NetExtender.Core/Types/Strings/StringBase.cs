// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Types.Strings
{
    public abstract class StringBase : IString
    {
        public static explicit operator String(StringBase value)
        {
            return value.ToString();
        }

        public virtual Boolean Immutable
        {
            get
            {
                return true;
            }
        }

        public virtual Boolean Constant
        {
            get
            {
                return true;
            }
        }

        public virtual Int32 Length
        {
            get
            {
                return Text.Length;
            }
        }

        public abstract String Text { get; protected set; }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || ToString().Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override String ToString()
        {
            return Text;
        }

        public virtual String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public virtual String ToString(String? format, IFormatProvider? provider)
        {
            return ToString();
        }
    }
}