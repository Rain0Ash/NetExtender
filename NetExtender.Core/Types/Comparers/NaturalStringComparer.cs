using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public class NaturalStringComparer : IComparer<String>
    {
        public static NaturalStringComparer Default { get; } = new NaturalStringComparer();
        private static Regex Regex { get; } = new Regex("([0-9]+)", RegexOptions.Compiled);
        
        protected IComparer<Object> Comparer { get; }
        
        public NaturalStringComparer()
            : this(null)
        {
        }
        
        public NaturalStringComparer(IComparer<Object>? comparer)
        {
            Comparer = comparer ?? NaturalObjectComparer.Default;
        }
        
        public Int32 Compare(String? x, String? y)
        {
            if (x is null)
            {
                return y is null ? 0 : -1;
            }
            
            if (y is null)
            {
                return 1;
            }
            
            IEnumerable<Object?> xitems = Regex.Split(x.RemoveAllWhiteSpace()).Select(static value => Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out Decimal result) ? (Object?) result : value);
            IEnumerable<Object?> yitems = Regex.Split(y.RemoveAllWhiteSpace()).Select(static value => Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out Decimal result) ? (Object?) result : value);
            
            return Comparer.Compare(xitems, yitems);
        }
    }
}