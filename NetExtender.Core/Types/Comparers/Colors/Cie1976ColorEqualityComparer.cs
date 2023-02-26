// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using NetExtender.Types.Comparers.Interfaces;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers.Colors
{
    public class Cie1976ColorEqualityComparer : IEqualityComparer<Color>
    {
        public static Cie1976ColorEqualityComparer Default { get; } = new Cie1976ColorEqualityComparer();
        
        public Double Epsilon { get; }
        
        public Cie1976ColorEqualityComparer()
            : this(1)
        {
        }
        
        public Cie1976ColorEqualityComparer(Double epsilon)
        {
            Epsilon = epsilon;
        }
        
        public Boolean Equals(Color x, Color y)
        {
            return x.DifferenceCie1976(y) < Epsilon;
        }
        
        public Int32 GetHashCode(Color color)
        {
            return color.GetHashCode();
        }
    }
    
    public class Cie1976ColorEqualityComparer<TColor> : Cie1976ColorEqualityComparer, IEqualityComparer<TColor> where TColor : IColor
    {
        public new static Cie1976ColorEqualityComparer<TColor> Default { get; } = new Cie1976ColorEqualityComparer<TColor>();

        public Cie1976ColorEqualityComparer()
        {
        }

        public Cie1976ColorEqualityComparer(Double epsilon)
            : base(epsilon)
        {
        }
        
        public Boolean Equals(TColor? x, TColor? y)
        {
            if (x is null || y is null)
            {
                return !(x is null ^ y is null);
            }
            
            return Equals(x.ToColor(), y.ToColor());
        }

        public Int32 GetHashCode(TColor? color)
        {
            return color is not null ? GetHashCode(color.ToColor()) : 0;
        }
    }
    
    public class Cie1976ColorEqualityComparer<TColor1, TColor2> : Cie1976ColorEqualityComparer<TColor1>, IEqualityComparer<TColor2>, IEqualityComparer<TColor1, TColor2> where TColor1 : IColor where TColor2 : IColor
    {
        public new static Cie1976ColorEqualityComparer<TColor1, TColor2> Default { get; } = new Cie1976ColorEqualityComparer<TColor1, TColor2>();

        public Cie1976ColorEqualityComparer()
        {
        }
        
        public Cie1976ColorEqualityComparer(Double epsilon)
            : base(epsilon)
        {
        }
        
        public Boolean Equals(TColor1? x, TColor2? y)
        {
            if (x is null || y is null)
            {
                return !(x is null ^ y is null);
            }
            
            return Equals(x.ToColor(), y.ToColor());
        }

        public Boolean Equals(TColor2? x, TColor2? y)
        {
            if (x is null || y is null)
            {
                return !(x is null ^ y is null);
            }
            
            return Equals(x.ToColor(), y.ToColor());
        }

        public Int32 GetHashCode(TColor2? color)
        {
            return color is not null ? GetHashCode(color.ToColor()) : 0;
        }
    }
}