using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Comparers
{
    public class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        protected IEqualityComparer<T> Comparer { get; }
        
        public EnumerableEqualityComparer()
            : this(null)
        {
        }

        public EnumerableEqualityComparer(IEqualityComparer<T>? comparer)
        {
            Comparer = comparer ?? EqualityComparer<T>.Default;
        }
        
        public virtual Int32 GetHashCode(IEnumerable<T>? enumerable)
        {
            if (enumerable is null)
            {
                return 0;
            }
            
            HashCode code = new HashCode();
            foreach (T item in enumerable)
            {
                code.Add(item);
            }
            
            return code.ToHashCode();
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual Boolean Equals(IEnumerable<T>? x, IEnumerable<T>? y)
        {
            if (x is null || y is null)
            {
                return ReferenceEquals(x, y);
            }
            
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            
            using IEnumerator<T> xenumerator = x.GetEnumerator();
            using IEnumerator<T> yenumerator = y.GetEnumerator();
            
            if (Equals(xenumerator, yenumerator))
            {
                return true;
            }
            
            while (true)
            {
                Boolean left = xenumerator.MoveNext();
                Boolean right = yenumerator.MoveNext();
                
                if (!left && !right)
                {
                    return true;
                }
                
                if (!left || !right)
                {
                    return false;
                }

                if (!Comparer.Equals(xenumerator.Current, yenumerator.Current))
                {
                    return false;
                }
            }
        }
    }
    
    public class EnumerableEqualityComparer : IEqualityComparer<IEnumerable>, IEqualityComparer<IEnumerable<Object?>>
    {
        public static EnumerableEqualityComparer Default { get; } = new EnumerableEqualityComparer();
        protected IEqualityComparer Comparer { get; }
        
        public EnumerableEqualityComparer()
            : this(null)
        {
        }
        
        public EnumerableEqualityComparer(IEqualityComparer? comparer)
        {
            Comparer = comparer ?? EqualityComparer<Object>.Default;
        }
        
        public Int32 GetHashCode(IEnumerable? enumerable)
        {
            return GetHashCode(enumerable?.Cast<Object>());
        }
        
        public virtual Int32 GetHashCode(IEnumerable<Object?>? enumerable)
        {
            if (enumerable is null)
            {
                return 0;
            }
            
            HashCode code = new HashCode();
            foreach (Object? item in enumerable)
            {
                code.Add(item);
            }
            
            return code.ToHashCode();
        }
        
        public Boolean Equals(IEnumerable? x, IEnumerable? y)
        {
            return Equals(x?.Cast<Object>(), y?.Cast<Object>());
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual Boolean Equals(IEnumerable<Object?>? x, IEnumerable<Object?>? y)
        {
            if (x is null || y is null)
            {
                return ReferenceEquals(x, y);
            }
            
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            using IEnumerator<Object?> xenumerator = x.GetEnumerator();
            using IEnumerator<Object?> yenumerator = y.GetEnumerator();
            
            if (Equals(xenumerator, yenumerator))
            {
                return true;
            }
            
            while (true)
            {
                Boolean left = xenumerator.MoveNext();
                Boolean right = yenumerator.MoveNext();
                
                if (!left && !right)
                {
                    return true;
                }
                
                if (!left || !right)
                {
                    return false;
                }
                
                if (!Comparer.Equals(xenumerator.Current, yenumerator.Current))
                {
                    return false;
                }
            }
        }
    }
}