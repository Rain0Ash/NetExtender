using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IStringable : IFormattable
    {
        public String ToString(String? format);
        public String ToString(IFormatProvider? provider);
        public String? GetString();
        public String? GetString(EscapeType escape);
        public String? GetString(String? format);
        public String? GetString(EscapeType escape, String? format);
        public String? GetString(IFormatProvider? provider);
        public String? GetString(EscapeType escape, IFormatProvider? provider);
        public String? GetString(String? format, IFormatProvider? provider);
        public String? GetString(EscapeType escape, String? format, IFormatProvider? provider);
    }
}