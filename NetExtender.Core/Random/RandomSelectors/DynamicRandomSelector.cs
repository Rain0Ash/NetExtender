// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Random
{
    /// <summary>
    /// DynamicRandomSelector allows you adding or removing items.
    /// Call "Build" after you finished modification.
    /// Switches between linear or binary search after each Build(), 
    /// depending on count of items, making it more performant for general use case.
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class DynamicRandomSelector<T> : RandomSelectorBuilder<T> where T : notnull
    {
        protected List<Double> Distribution { get; }
        protected Func<List<Double>, Double, Int32>? Selector { get; set; }

        public DynamicRandomSelector()
        {
            Distribution = new List<Double>(DefaultCapacity);
        }
        
        public DynamicRandomSelector(Int32 capacity)
            : base(capacity)
        {
            Distribution = new List<Double>(capacity);
        }

        public DynamicRandomSelector(Int32 capacity, Int32 seed)
            : base(capacity, seed)
        {
            Distribution = new List<Double>(capacity);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="random">Random generator</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(Int32 capacity, IRandom random)
            : base(capacity, random)
        {
            capacity = capacity.Clamp(DefaultCapacity, Int32.MaxValue);
            Distribution = new List<Double>(capacity);
        }
        
        public DynamicRandomSelector(IEnumerable<KeyValuePair<T, Double>>? items)
            : this(items.Materialize())
        {
        }

        public DynamicRandomSelector(IEnumerable<KeyValuePair<T, Double>>? items, Int32 seed)
            : this(items.Materialize(), seed)
        {
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="items">Items that will get returned on random selections</param>
        /// <param name="random">Random generator</param>
        public DynamicRandomSelector(IEnumerable<KeyValuePair<T, Double>>? items, IRandom random)
            : this(items.Materialize(), random)
        {
        }

        protected DynamicRandomSelector(IReadOnlyCollection<KeyValuePair<T, Double>>? items)
            : this(items, RandomUtilities.Create())
        {
        }

        protected DynamicRandomSelector(IReadOnlyCollection<KeyValuePair<T, Double>>? items, Int32 seed)
            : this(items, RandomUtilities.Create(seed))
        {
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="items">Items that will get returned on random selections</param>
        /// <param name="random">Random generator</param>
        protected DynamicRandomSelector(IReadOnlyCollection<KeyValuePair<T, Double>>? items, IRandom random)
            : base(items, random)
        {
            Distribution = new List<Double>(items?.Count ?? DefaultCapacity);
        }

        /// <summary>
        /// Re/Builds internal CDL (Cummulative Distribution List)
        /// Must be called after modifying (calling Add or Remove), or it will break. 
        /// Switches between linear or binary search, depending on which one will be faster.
        /// Might generate some garbage (list resize) on first few builds.
        /// </summary>
        /// <param name="seed">You can specify seed for internal random gen or leave it alone</param>
        /// <returns>Returns itself</returns>
        public override IRandomSelector<T> Build(Int32 seed)
        {
            if (Items.Count <= 0)
            {
                throw new InvalidOperationException("Cannot build with no items.");
            }
            
            Distribution.Clear();
            Distribution.AddRange(Items.Values);
            BuildCumulativeDistribution(Distribution);

            // default behavior
            // if seed wasn't specified (it is seed==-1), keep same seed - avoids garbage collection from making new random
            if (seed != -1)
            {
                // input -2 if you want to randomize seed
                if (seed == -2)
                {
                    seed = Random.Next();
                }

                Random = RandomUtilities.Create(seed);
            }

            // RandomMath.ListBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDL list is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (Distribution.Count < ListBreakpoint)
            {
                Selector = SelectIndexLinearSearch;
                return this;
            }

            Selector = SelectIndexBinarySearch;
            return this;
        }
        
        /// <summary>
        /// Linear search, good/faster for small lists
        /// </summary>
        /// <param name="cdl">Cummulative Distribution List</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDA</returns>
        protected static Int32 SelectIndexLinearSearch(List<Double> cdl, Double value)
        {
            if (cdl is null)
            {
                throw new ArgumentNullException(nameof(cdl));
            }

            Int32 i = 0;

            // last element, CDL[CDL.Length-1] should always be 1
            while (cdl[i] < value)
            {
                i++;
            }

            return i;
        }

        /// <summary>
        /// Binary search, good/faster for big lists
        /// Code taken out of C# array.cs Binary Search & modified
        /// </summary>
        /// <param name="cdl">Cummulative Distribution List</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDL</returns>
        protected static Int32 SelectIndexBinarySearch(List<Double> cdl, Double value)
        {
            if (cdl is null)
            {
                throw new ArgumentNullException(nameof(cdl));
            }

            Int32 low = 0;
            Int32 high = cdl.Count - 1;
            Int32 index;

            while (low <= high)
            {
                // calculate median
                index = low + ((high - low) >> 1);

                if (Math.Abs(cdl[index] - value) < Double.Epsilon)
                {
                    return index;
                }

                if (cdl[index] < value)
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
        
        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <returns>Returns item</returns>
        public override T GetRandom()
        {
            if (Selector is null)
            {
                throw new NotInitializedException();
            }

            if (Items.Count <= 0)
            {
                throw new InvalidOperationException("Items is empty");
            }

            return Items.GetKeyByIndex(Selector(Distribution, Random.NextDouble()));
        }

        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <param name="value">Random value from your uniform generator</param>
        /// <returns>Returns item</returns>
        public override T GetRandom(Double value)
        {
            if (Selector is null)
            {
                throw new NotInitializedException();
            }
            
            if (Items.Count <= 0)
            {
                throw new InvalidOperationException("Items is empty");
            }

            return Items.GetKeyByIndex(Selector(Distribution, value));
        }

        
        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(T? alternate)
        {
            if (Selector is null)
            {
                throw new NotInitializedException();
            }

            return Items.Count > 0 ? Items.GetKeyByIndex(Selector(Distribution, Random.NextDouble())) : alternate;
        }

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(Double value, T? alternate)
        {
            if (Selector is null)
            {
                throw new NotInitializedException();
            }
            
            return Items.Count > 0 ? Items.GetKeyByIndex(Selector(Distribution, value)) : alternate;
        }

        public override void Clear()
        {
            base.Clear();
            Distribution.Clear();
        }

        public override IEnumerator<T> GetEnumerator()
        {
            while (Selector is not null)
            {
                yield return GetRandom();
            }
        }
    }
}