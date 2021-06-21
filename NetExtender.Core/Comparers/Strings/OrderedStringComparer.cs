// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Utils.Types;

namespace NetExtender.Comparers
{
    public class OrderedStringComparer : OrderedComparer<String?>
    {
        public StringComparison ComparisonType { get; set; }
        
        public OrderedStringComparer(IEnumerable<String?>? order = null, StringComparison comparison = StringComparison.Ordinal)
            : base(order)
        {
            ComparisonType = comparison;
        }

        public override Int32 Compare(String? first, String? second)
        {
            return Compare(Order.FindIndex(item => String.Equals(item, first, ComparisonType)), Order.FindIndex(item => String.Equals(item, second, ComparisonType)));
        }
    }
}