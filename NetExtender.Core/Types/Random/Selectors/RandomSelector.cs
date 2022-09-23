// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Types.Random
{
    public abstract class RandomSelector<T> : IRandomSelector<T>
    {
        public static IRandomSelector<T> Default
        {
            get
            {
                return DefaultRandomSelector<T>.Instance;
            }
        }

        /// <summary>
        /// Breaking point between using Linear vs. Binary search for arrays (StaticSelector). 
        /// Was calculated empirically.
        /// </summary>
        protected const Int32 ArrayBreakpoint = 51;

        /// <summary>
        /// Breaking point between using Linear vs. Binary search for lists (DynamicSelector). 
        /// Was calculated empirically. 
        /// </summary>
        protected const Int32 ListBreakpoint = 26;
        
        public abstract IReadOnlyCollection<T> Collection { get; }

        public abstract Int32 Count { get; }

        public abstract T GetRandom();
        public abstract T GetRandom(Double value);

        public T? GetRandomOrDefault()
        {
            return GetRandomOrDefault(default(T));
        }
        
        [return: NotNullIfNotNull("alternate")]
        public abstract T? GetRandomOrDefault(T? alternate);

        public T? GetRandomOrDefault(Double value)
        {
            return GetRandomOrDefault(value, default);
        }
        
        [return: NotNullIfNotNull("alternate")]
        public abstract T? GetRandomOrDefault(Double value, T? alternate);

        public abstract T[] ToItemArray();
        public abstract List<T> ToItemList();
        public abstract NullableDictionary<T, Double> ToItemDictionary();

        /// <summary>
        /// Builds cummulative distribution out of non-normalized weights inplace.
        /// </summary>
        /// <param name="source">List of Non-normalized weights</param>
        protected static void BuildCumulativeDistribution(IList<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 length = source.Count;

            Double sum = 0;

            for (Int32 i = 0; i < length; i++)
            {
                sum += source[i];
            }
            
            Double k = 1F / sum;

            sum = 0;
            for (Int32 i = 0; i < length; i++)
            {
                sum += source[i] * k;
                source[i] = sum;
            }

            source[length - 1] = 1F;
        }

        /// <summary>
        /// Binary search, good/faster for big array
        /// Code taken out of C# array.cs Binary Search & modified
        /// </summary>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDA</returns>
        protected static Int32 SelectIndexBinarySearch(Double[] cda, Double value)
        {
            if (cda is null)
            {
                throw new ArgumentNullException(nameof(cda));
            }

            Int32 low = 0;
            Int32 high = cda.Length - 1;
            Int32 index;

            while (low <= high)
            {
                // calculate median
                index = low + ((high - low) >> 1);

                if (Math.Abs(cda[index] - value) < Double.Epsilon)
                {
                    return index;
                }

                if (cda[index] < value)
                {
                    // shrink left
                    low = index + 1;
                    continue;
                }

                // shrink right
                high = index - 1;
            }

            index = low;

            return index;
        }
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return GetRandom();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}