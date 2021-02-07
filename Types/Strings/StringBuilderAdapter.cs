// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;

#error Дописать

namespace NetExtender.Types.Strings
{
    public sealed class StringBuilderAdapter : StringBase, IString
    {
        public static explicit operator StringBuilder(StringBuilderAdapter adapter)
        {
            return adapter.Builder;
        }

        public static implicit operator StringBuilderAdapter(StringBuilder value)
        {
            return new StringBuilderAdapter(value);
        }

        private StringBuilder Builder { get; }

        public override Boolean Immutable
        {
            get
            {
                return false;
            }
        }

        public override Int32 Length
        {
            get
            {
                return Builder.Length;
            }
        }

        public override String Text
        {
            get
            {
                return ToString();
            }
            protected set
            {
                throw new NotSupportedException();
            }
        }

        public StringBuilderAdapter(String value)
        {
            Builder = new StringBuilder(value);
        }

        public StringBuilderAdapter([NotNull] StringBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
        
        

        Char IString.this[Int32 index]
        {
            get
            {
                return Builder[index];
            }
        }

        Char IString.this[Index index]
        {
            get
            {
                return Builder[index];
            }
        }
        
        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) ||
                   obj is StringBuilderAdapter adapter && Builder.Equals(adapter.Builder) ||
                   obj is StringBuilder builder && Builder.Equals(builder);
        }

        public override Int32 GetHashCode()
        {
            return Builder.GetHashCode();
        }

        public override String ToString()
        {
            return Builder.ToString();
        }

        public override String ToString(IFormatProvider? provider)
        {
            return ToString();
        }
    }
}