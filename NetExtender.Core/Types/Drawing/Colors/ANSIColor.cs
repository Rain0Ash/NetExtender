// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors
{
    public enum AnsiColorSequenceMode : Byte
    {
        None,
        Foreground,
        Background,
        Fill
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ANSIColor : IColor<ANSIColor>
    {
        public static implicit operator Color(ANSIColor color)
        {
            return color.ToColor();
        }

        public static implicit operator ANSIColor(Color color)
        {
            return new ANSIColor(color.R, color.G, color.B);
        }

        public static Boolean operator ==(ANSIColor first, ANSIColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ANSIColor first, ANSIColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.ANSI;
            }
        }

        public AnsiColorSequenceMode Mode { get; }

        public Byte R { get; init; }
        public Byte G { get; init; }
        public Byte B { get; init; }

        public ANSIColor(Byte r, Byte g, Byte b)
            : this(r, g, b, AnsiColorSequenceMode.Foreground)
        {
        }

        public ANSIColor(Byte r, Byte g, Byte b, AnsiColorSequenceMode mode)
        {
            R = r;
            G = g;
            B = b;
            Mode = mode;
        }

        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ANSIColor result && Equals(result);
        }

        public Boolean Equals(ANSIColor other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public ANSIColor Clone(AnsiColorSequenceMode mode)
        {
            return new ANSIColor(R, G, B, mode);
        }

        public override String ToString()
        {
            return $"\x1b[38;2;{R};{G};{B}m";
        }

        public String ToString(String? format)
        {
            return ToString(format, null);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            String r = R.ToString(provider);
            String g = G.ToString(provider);
            String b = B.ToString(provider);

            String result = new StringBuilder(format)
                .Replace("{FILL}", $"\x1b[38;2;{r};{g};{b};48;2;{r};{g};{b}m")
                .Replace("{FOREGROUND}", $"\x1b[38;2;{r};{g};{b}m")
                .Replace("{BACKGROUND}", $"\x1b[48;2;{r};{g};{b}m")
                .Replace("{END}", "\x1b[0m")
                .ToString();

            return format != result ? format : Mode switch
            {
                AnsiColorSequenceMode.None => format,
                AnsiColorSequenceMode.Foreground => $"\x1b[38;2;{r};{g};{b}m{format}\x1b[0m",
                AnsiColorSequenceMode.Background => $"\x1b[48;2;{r};{g};{b}m{format}\x1b[0m",
                AnsiColorSequenceMode.Fill => $"\x1b[38;2;{r};{g};{b};48;2;{r};{g};{b}m{format}\x1b[0m",
                _ => throw new EnumUndefinedOrNotSupportedException<AnsiColorSequenceMode>(Mode, nameof(Mode), null)
            };
        }
    }
}