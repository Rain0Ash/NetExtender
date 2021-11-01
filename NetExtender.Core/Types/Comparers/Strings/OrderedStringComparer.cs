// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public class OrderedStringComparer : OrderedComparer<String?>
    {
        public StringComparison ComparisonType { get; set; }
        
        public OrderedStringComparer()
            : this(null)
        {
        }
        
        public OrderedStringComparer(StringComparison comparison)
            : this(null, comparison)
        {
        }
        
        public OrderedStringComparer(IEnumerable<String?>? order)
            : this(order, StringComparison.Ordinal)
        {
        }
        
        public OrderedStringComparer(IEnumerable<String?>? order, StringComparison comparison)
            : base(order)
        {
            ComparisonType = comparison;
        }

        public override Int32 Compare(String? x, String? y)
        {
            return Compare(Order.FindIndex(item => String.Equals(item, x, ComparisonType)), Order.FindIndex(item => String.Equals(item, y, ComparisonType)));
        }
    }
}