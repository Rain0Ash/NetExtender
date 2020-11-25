// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Comparers
{
    public class OrderedComparer : OrderedComparer<Object>
    {
    }

    public class OrderedStringComparer : OrderedComparer<String>
    {
        public StringComparison ComparisonType { get; set; }
        public OrderedStringComparer(IEnumerable<String> order = null, StringComparison comparison = StringComparison.Ordinal)
            : base(order)
        {
            ComparisonType = comparison;
        }

        public override Int32 Compare(String x, String y)
        {
            return Compare(Order.FindIndex(item => item.Equals(x, ComparisonType)), Order.FindIndex(item => item.Equals(y, ComparisonType)));
        }
    }

    public class OrderedComparer<T> : IComparer<T>
    {
        public List<T> Order { get; }

        public OrderedComparer(IEnumerable<T> order = null)
        {
            Order = (order ?? Array.Empty<T>()).ToList();
        }

        protected Int32 Compare(Int32 x, Int32 y)
        {
            Int32 ix = x != -1 ? x : Order.Count + 1;
            Int32 iy = y != -1 ? y : Order.Count + 1;
            
            return ix.CompareTo(iy);
        }
        
        public virtual Int32 Compare(T x, T y)
        {
            return Compare(Order.IndexOf(x), Order.IndexOf(y));
        }
    }
}