// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Strings
{
    public sealed class FormatStringAdapter : FormatStringBase
    {
        public static explicit operator String(FormatStringAdapter adapter)
        {
            return adapter.ToString();
        }

        public static implicit operator FormatStringAdapter(String value)
        {
            return new FormatStringAdapter(value);
        }
        
        public override Int32 Arguments { get; }

        public FormatStringAdapter([NotNull] String value)
        {
            Text = value ?? throw new ArgumentNullException(nameof(value));
            Arguments = value.CountExpectedFormatArgs();
        }
    }
}