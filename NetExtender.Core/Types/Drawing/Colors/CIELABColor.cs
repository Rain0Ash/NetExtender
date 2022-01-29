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
    public readonly struct CIELABColor : IColor<CIELABColor>
    {
        public static implicit operator Color(CIELABColor color)
        {
            return color.ToColor();
        }
        
        public static implicit operator CIELABColor(Color color)
        {
            color.ToCIELAB(out Double h, out Double s, out Double v);
            return new CIELABColor(h, s, v);
        }
        
        public static Boolean operator ==(CIELABColor first, CIELABColor second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(CIELABColor first, CIELABColor second)
        {
            return !(first == second);
        }

        public ColorType Type
        {
            get
            {
                return ColorType.CIELAB;
            }
        }
        
        public Double L { get; init; }
        public Double A { get; init; }
        public Double B { get; init; }

        public CIELABColor(Double l, Double a, Double b)
        {
            L = l;
            A = a;
            B = b;
        }

        public Color ToColor()
        {
            return ColorUtilities.CIELABToRGB(L, A, B);
        }

        public Boolean ToColor(out Color color)
        {
            color = ToColor();
            return true;
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(L, A, B);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is CIELABColor result && Equals(result);
        }

        public Boolean Equals(CIELABColor other)
        {
            return Math.Abs(L - other.L) < Double.Epsilon && Math.Abs(A - other.A) < Double.Epsilon && Math.Abs(B - other.B) < Double.Epsilon;
        }
        
        public Boolean Equals(IColor? color)
        {
            return color is not null && ToColor(out Color first) && color.ToColor(out Color second) && first.Equals(second);
        }

        public override String ToString()
        {
            return $"L:{L} A:{A} B:{B}";
        }
        
        public String ToString(String? format)
        {
            return ToString(format, null);
        }
        
        public String ToString(String? format, IFormatProvider?provider)
        {
            if (String.IsNullOrEmpty(format))
            {
                return ToString();
            }

            String l = L.ToString(provider);
            String a = A.ToString(provider);
            String b = B.ToString(provider);

            return format.Replace("{COLOR}", $"L:{l} A:{a} B:{b}")
                .Replace("{L}", l)
                .Replace("{A}", a)
                .Replace("{B}", b);
        }
    }
}