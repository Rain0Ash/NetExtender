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
    public class CmcColorEqualityComparer : IEqualityComparer<Color>
    {
        public static CmcColorEqualityComparer Default { get; } = new CmcColorEqualityComparer();
        
        public Double Epsilon { get; }
        
        public CmcColorEqualityComparer()
            : this(1)
        {
        }
        
        public CmcColorEqualityComparer(Double epsilon)
        {
            Epsilon = epsilon;
        }
        
        public Boolean Equals(Color x, Color y)
        {
            return x.DifferenceCmc(y) < Epsilon;
        }
        
        public Int32 GetHashCode(Color color)
        {
            return color.GetHashCode();
        }
    }
    
    public class CmcColorEqualityComparer<TColor> : CmcColorEqualityComparer, IEqualityComparer<TColor> where TColor : IColor
    {
        public new static CmcColorEqualityComparer<TColor> Default { get; } = new CmcColorEqualityComparer<TColor>();

        public CmcColorEqualityComparer()
        {
        }

        public CmcColorEqualityComparer(Double epsilon)
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
    
    public class CmcColorEqualityComparer<TColor1, TColor2> : CmcColorEqualityComparer<TColor1>, IEqualityComparer<TColor2>, IEqualityComparer<TColor1, TColor2> where TColor1 : IColor where TColor2 : IColor
    {
        public new static CmcColorEqualityComparer<TColor1, TColor2> Default { get; } = new CmcColorEqualityComparer<TColor1, TColor2>();

        public CmcColorEqualityComparer()
        {
        }
        
        public CmcColorEqualityComparer(Double epsilon)
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