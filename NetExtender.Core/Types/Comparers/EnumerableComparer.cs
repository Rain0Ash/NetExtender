using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers
{
    public class EnumerableComparer<T>
    {
        private IComparer<T> Comparer { get; }
        
        public EnumerableComparer()
            : this(null)
        {
        }

        public EnumerableComparer(IComparer<T>? comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public Int32 Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            using IEnumerator<T> xenumerator = x.GetEnumerator();
            using IEnumerator<T> yenumerator = y.GetEnumerator();
            
            while (true)
            {
                Boolean left = xenumerator.MoveNext();
                Boolean right = yenumerator.MoveNext();
                
                if (!(left || right))
                {
                    return 0;
                }
                
                if (!left)
                {
                    return -1;
                }
                
                if (!right)
                {
                    return 1;
                }

                Int32 result = Comparer.Compare(xenumerator.Current, yenumerator.Current);
                if (result != 0)
                {
                    return result;
                }
            }
        }
    }
}