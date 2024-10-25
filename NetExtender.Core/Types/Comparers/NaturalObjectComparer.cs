using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers
{
    public class NaturalObjectComparer : IComparer<Object>
    {
        public static NaturalObjectComparer Default { get; } = new NaturalObjectComparer();
        protected IComparer<String> Comparer { get; }
        
        private NaturalObjectComparer()
            : this(null)
        {
        }
        
        private NaturalObjectComparer(IComparer<String>? comparer)
        {
            Comparer = comparer ?? new NaturalStringComparer();
        }
        
        public Int32 Compare(Object? x, Object? y)
        {
            if (Equals(x, y))
            {
                return 0;
            }
            
            if (x is null)
            {
                return -1;
            }
            
            if (y is null)
            {
                return 1;
            }
            
            return x switch
            {
                String first when y is String second => Comparer.Compare(first, second),
                IComparable comparable when x.GetType() == y.GetType() => comparable.CompareTo(y),
                _ => Comparer.Compare(x.ToString(), y.ToString())
            };
        }
    }
}