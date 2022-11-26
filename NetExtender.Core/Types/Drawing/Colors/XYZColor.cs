// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Drawing.Colors
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct XYZColor : IColor<XYZColor>
    {
        public static implicit operator Color(XYZColor color)
        {
            return color.ToColor();
        }

        public static implicit operator XYZColor(Color color)
        {
            color.ToXYZ(out Double h, out Double s, out Double v);
            return new XYZColor(h, s, v);
        }

        public static Boolean operator ==(XYZColor first, XYZColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(XYZColor first, XYZColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.XYZ;
            }
        }

        public Double X { get; init; }
        public Double Y { get; init; }
        public Double Z { get; init; }

        public XYZColor(Double x, Double y, Double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Color ToColor()
        {
            return ColorUtilities.XYZToRGB(X, Y, Z);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is XYZColor result && Equals(result);
        }

        public Boolean Equals(XYZColor other)
        {
            return Math.Abs(X - other.X) < Double.Epsilon && Math.Abs(Y - other.Y) < Double.Epsilon && Math.Abs(Z - other.Z) < Double.Epsilon;
        }

        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"X:{X} Y:{Y} Z:{Z}";
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

            String x = X.ToString(provider);
            String y = Y.ToString(provider);
            String z = Z.ToString(provider);

            return format.Replace("{COLOR}", $"X:{x} Y:{y} Z:{z}")
                .Replace("{X}", x)
                .Replace("{Y}", y)
                .Replace("{Z}", z);
        }
    }
}