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
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ANSIDifferentColor : IColor<ANSIDifferentColor>
    {
        public static explicit operator Color(ANSIDifferentColor color)
        {
            return color.ToColor();
        }

        public static explicit operator ANSIDifferentColor(Color color)
        {
            return new ANSIDifferentColor(color.R, color.G, color.B, color.R, color.G, color.B);
        }

        public static Boolean operator ==(ANSIDifferentColor first, ANSIDifferentColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ANSIDifferentColor first, ANSIDifferentColor second)
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

        public Byte ForegroundR { get; init; }
        public Byte ForegroundG { get; init; }
        public Byte ForegroundB { get; init; }

        public Byte BackgroundR { get; init; }
        public Byte BackgroundG { get; init; }
        public Byte BackgroundB { get; init; }

        public ANSIDifferentColor(Byte foregroundR, Byte foregroundG, Byte foregroundB, Byte backgroundR, Byte backgroundG, Byte backgroundB)
        {
            ForegroundR = foregroundR;
            ForegroundG = foregroundG;
            ForegroundB = foregroundB;
            BackgroundR = backgroundR;
            BackgroundG = backgroundG;
            BackgroundB = backgroundB;
        }

        public ANSIDifferentColor(Color foreground, Color background)
        {
            ForegroundR = foreground.R;
            ForegroundG = foreground.G;
            ForegroundB = foreground.B;
            BackgroundR = background.R;
            BackgroundG = background.G;
            BackgroundB = background.B;
        }

        public Color ToColor()
        {
            return Color.FromArgb(ForegroundR, ForegroundG, ForegroundB);
        }

        public Color ToColor(AnsiColorSequenceMode mode)
        {
            return mode switch
            {
                AnsiColorSequenceMode.None => default,
                AnsiColorSequenceMode.Foreground => Color.FromArgb(ForegroundR, ForegroundG, ForegroundB),
                AnsiColorSequenceMode.Background => Color.FromArgb(BackgroundR, BackgroundG, BackgroundB),
                AnsiColorSequenceMode.Fill => default,
                _ => throw new EnumUndefinedOrNotSupportedException<AnsiColorSequenceMode>(mode, nameof(mode), null)
            };
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }

        public Boolean ToColor(out Color color, AnsiColorSequenceMode mode)
        {
            switch (mode)
            {
                case AnsiColorSequenceMode.None:
                    color = default;
                    return false;
                case AnsiColorSequenceMode.Foreground:
                    color = Color.FromArgb(ForegroundR, ForegroundG, ForegroundB);
                    return true;
                case AnsiColorSequenceMode.Background:
                    color = Color.FromArgb(BackgroundR, BackgroundG, BackgroundB);
                    return true;
                case AnsiColorSequenceMode.Fill:
                    color = default;
                    return false;
                default:
                    throw new EnumUndefinedOrNotSupportedException<AnsiColorSequenceMode>(mode, nameof(mode), null);
            }
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(ForegroundR, ForegroundG, ForegroundB, BackgroundR, BackgroundG, BackgroundB);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is ANSIDifferentColor result && Equals(result);
        }

        public Boolean Equals(ANSIDifferentColor other)
        {
            return ForegroundR == other.ForegroundR && ForegroundG == other.ForegroundG && ForegroundB == other.ForegroundB
                && BackgroundR == other.BackgroundR && BackgroundG == other.BackgroundG && BackgroundB == other.BackgroundB;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"\x1b[38;2;{ForegroundR};{ForegroundG};{ForegroundB};48;2;{ForegroundR};{ForegroundG};{ForegroundB}m";
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

            String foregroundR = ForegroundR.ToString(provider);
            String foregroundG = ForegroundG.ToString(provider);
            String foregroundB = ForegroundB.ToString(provider);
            String backgroundR = BackgroundR.ToString(provider);
            String backgroundG = BackgroundG.ToString(provider);
            String backgroundB = BackgroundB.ToString(provider);

            String result = new StringBuilder(format)
                .Replace("{FILL}", $"\x1b[38;2;{foregroundR};{foregroundG};{foregroundB};48;2;{backgroundR};{backgroundG};{backgroundB}m")
                .Replace("{FOREGROUND}", $"\x1b[38;2;{foregroundR};{foregroundG};{foregroundB}m")
                .Replace("{BACKGROUND}", $"\x1b[48;2;{backgroundR};{backgroundG};{backgroundB}m")
                .Replace("{END}", "\x1b[0m")
                .ToString();

            return format != result ? format : $"\x1b[38;2;{foregroundR};{foregroundG};{foregroundB};48;2;{backgroundR};{backgroundG};{backgroundB}m{format}\x1b[0m";
        }
    }
}