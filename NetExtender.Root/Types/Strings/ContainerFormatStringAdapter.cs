// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public sealed class ContainerFormatStringAdapter : ContainerFormatStringBase
    {
        public static IContainerFormatString Create(String value)
        {
            return new ContainerFormatStringAdapter(value);
        }

        public static IContainerFormatString Create(String value, params Object[]? format)
        {
            return new ContainerFormatStringAdapter(value);
        }

        public static IContainerFormatString Create(IString value)
        {
            return new ContainerFormatIStringAdapter(value);
        }

        public static IContainerFormatString Create(IString value, params Object[]? format)
        {
            return new ContainerFormatIStringAdapter(value, format);
        }

        public static explicit operator String(ContainerFormatStringAdapter adapter)
        {
            return adapter.ToString();
        }

        public static implicit operator ContainerFormatStringAdapter(String value)
        {
            return new ContainerFormatStringAdapter(value);
        }

        public override String Text { get; protected set; }
        public override Int32 Arguments { get; }
        protected override Object[] FormatArguments { get; }

        private ContainerFormatStringAdapter(String value)
            : this(value, null)
        {
        }

        private ContainerFormatStringAdapter(String value, params Object[]? format)
        {
            Text = value ?? throw new ArgumentNullException(nameof(value));
            Arguments = value.CountExpectedFormatArguments();
            FormatArguments = format ?? Array.Empty<Object>();
        }

        private sealed class ContainerFormatIStringAdapter : ContainerFormatStringBase
        {
            public static explicit operator String(ContainerFormatIStringAdapter adapter)
            {
                return adapter.ToString();
            }

            private IString Value { get; set; }

            public override String Text
            {
                get
                {
                    return Value.Text;
                }
                protected set
                {
                    Value = new StringAdapter(value);
                }
            }

            public override Int32 Arguments
            {
                get
                {
                    return Value.ToString().CountExpectedFormatArguments();
                }
            }

            protected override Object[] FormatArguments { get; }

            public ContainerFormatIStringAdapter(IString value)
                : this(value, null)
            {
            }

            public ContainerFormatIStringAdapter(IString value, params Object[]? format)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                FormatArguments = format ?? Array.Empty<Object>();
            }
        }
    }
}