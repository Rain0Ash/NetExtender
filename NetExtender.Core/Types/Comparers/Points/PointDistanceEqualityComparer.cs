// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Comparers.Points
{
    public class PointDistanceEqualityComparer : IEqualityComparer<Point>, IEqualityComparer<PointF>
    {
        public static PointDistanceEqualityComparer Default { get; } = new PointDistanceEqualityComparer();
        public static PointDistanceEqualityComparer One { get; } = new PointDistanceEqualityComparer(1);

        public Double Epsilon { get; }
        
        public PointDistanceEqualityComparer()
            : this(Double.Epsilon)
        {
        }

        public PointDistanceEqualityComparer(Double epsilon)
        {
            Epsilon = epsilon >= 0 ? epsilon : throw new ArgumentOutOfRangeException(nameof(epsilon), epsilon, null);
        }

        public Boolean Equals(Point x, Point y)
        {
            return x.Distance(y) < Epsilon;
        }

        public Boolean Equals(PointF x, PointF y)
        {
            return x.Distance(y) < Epsilon;
        }

        public Int32 GetHashCode(Point point)
        {
            return point.GetHashCode();
        }

        public Int32 GetHashCode(PointF point)
        {
            return point.GetHashCode();
        }
    }
}