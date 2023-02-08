// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Windows.Types
{
    public readonly struct DrawableString
    {
        public static implicit operator String(DrawableString value)
        {
            return value.Value;
        }

        public String Value { get; }
        public Brush Brush { get; }
        public Font Font { get; }
        public StringFormat? Format { get; }
        public Single Distance { get; init; } = -2;

        public DrawableString(String value, Brush brush, Font font)
            : this(value, brush, font, null)
        {
        }

        public DrawableString(String value, Brush brush, Font font, StringFormat? format)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Brush = brush ?? throw new ArgumentNullException(nameof(brush));
            Font = font ?? throw new ArgumentNullException(nameof(font));
            Format = format;
        }
    }
}