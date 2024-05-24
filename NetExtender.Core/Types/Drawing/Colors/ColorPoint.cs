// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Types.Drawing.Colors
{
    public readonly struct ColorPoint : IEquatable<ColorPoint>, IEquatable<Point>, IEquatable<Color>
    {
        public static implicit operator Point(ColorPoint value)
        {
            return value.Point;
        }
        
        public static implicit operator Color(ColorPoint value)
        {
            return value.Color;
        }
        
        public Point Point { get; }

        public Int32 X
        {
            get
            {
                return Point.X;
            }
        }

        public Int32 Y
        {
            get
            {
                return Point.Y;
            }
        }
        
        public Color Color { get; }

        public Int32 A
        {
            get
            {
                return Color.A;
            }
        }
        
        public Int32 R
        {
            get
            {
                return Color.R;
            }
        }
        
        public Int32 G
        {
            get
            {
                return Color.G;
            }
        }
        
        public Int32 B
        {
            get
            {
                return Color.B;
            }
        }
        
        public ColorPoint(Point point, Color color)
        {
            Point = point;
            Color = color;
        }

        public Boolean Equals(ColorPoint other)
        {
            return Point == other.Point && Color == other.Color;
        }

        public Boolean Equals(Point other)
        {
            return Point == other;
        }

        public Boolean Equals(Color other)
        {
            return Color == other;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Point, Color);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                ColorPoint point => Equals(point),
                Point point => Equals(point),
                Color color => Equals(color),
                _ => false
            };
        }

        public override String ToString()
        {
            return Point + " " + Color;
        }
    }
}