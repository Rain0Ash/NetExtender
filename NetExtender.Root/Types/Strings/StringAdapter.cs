// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Strings
{
    public sealed class StringAdapter : StringBase
    {
        public static explicit operator String(StringAdapter adapter)
        {
            return adapter.ToString();
        }

        public static implicit operator StringAdapter(String? value)
        {
            return new StringAdapter(value);
        }

        public override String Text { get; protected set; }

        public StringAdapter(String? value)
        {
            Text = value ?? String.Empty;
        }
    }
}