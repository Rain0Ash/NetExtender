// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public sealed class ContainerFormatStringAdapter : ContainerFormatStringBase
    {
        public static explicit operator String(ContainerFormatStringAdapter adapter)
        {
            return adapter.ToString();
        }

        public static implicit operator ContainerFormatStringAdapter(String value)
        {
            return new ContainerFormatStringAdapter(value);
        }
        
        public override Int32 Arguments { get; }
        protected override Object[] FormatArguments { get; }

        public override String Text { get; protected set; }

        public ContainerFormatStringAdapter(String value)
            : this(value, null)
        {
        }

        public ContainerFormatStringAdapter(String value, params Object[]? format)
        {
            Text = value ?? throw new ArgumentNullException(nameof(value));
            Arguments = value.CountExpectedFormatArgs();
            FormatArguments = format ?? Array.Empty<Object>();
        }
    }
}