// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Comparers
{
    public class EnumerableComparer<T> : IComparer<IEnumerable<T>>
    {
        protected IComparer<T> Comparer { get; }
        
        public EnumerableComparer()
            : this(null)
        {
        }

        public EnumerableComparer(IComparer<T>? comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual Int32 Compare(IEnumerable<T>? x, IEnumerable<T>? y)
        {
            if (x is null || y is null)
            {
                return ReferenceEquals(x, y) ? 0 : x is null ? -1 : 1;
            }
            
            if (ReferenceEquals(x, y))
            {
                return 0;
            }
            
            using IEnumerator<T> xenumerator = x.GetEnumerator();
            using IEnumerator<T> yenumerator = y.GetEnumerator();
            
            if (Equals(xenumerator, yenumerator))
            {
                return 0;
            }
            
            while (true)
            {
                Boolean left = xenumerator.MoveNext();
                Boolean right = yenumerator.MoveNext();
                
                if (!left && !right)
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
    
    public class EnumerableComparer : IComparer<IEnumerable>, IComparer<IEnumerable<Object?>>
    {
        public static EnumerableComparer Default { get; } = new EnumerableComparer();
        protected IComparer Comparer { get; }
        
        public EnumerableComparer()
            : this(null)
        {
        }
        
        public EnumerableComparer(IComparer? comparer)
        {
            Comparer = comparer ?? Comparer<Object?>.Default;
        }
        
        public Int32 Compare(IEnumerable? x, IEnumerable? y)
        {
            return Compare(x?.Cast<Object>(), y?.Cast<Object>());
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual Int32 Compare(IEnumerable<Object?>? x, IEnumerable<Object?>? y)
        {
            if (x is null || y is null)
            {
                return ReferenceEquals(x, y) ? 0 : x is null ? -1 : 1;
            }
            
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            using IEnumerator<Object?> xenumerator = x.GetEnumerator();
            using IEnumerator<Object?> yenumerator = y.GetEnumerator();
            
            if (Equals(xenumerator, yenumerator))
            {
                return 0;
            }
            
            while (true)
            {
                Boolean left = xenumerator.MoveNext();
                Boolean right = yenumerator.MoveNext();
                
                if (!left && !right)
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