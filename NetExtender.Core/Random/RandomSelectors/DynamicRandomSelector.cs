// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Exceptions;
using NetExtender.Random.Interfaces;
using NetExtender.Utils.Numerics;

namespace NetExtender.Random
{
    /// <summary>
    /// DynamicRandomSelector allows you adding or removing items.
    /// Call "Build" after you finished modification.
    /// Switches between linear or binary search after each Build(), 
    /// depending on count of items, making it more performant for general use case.
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class DynamicRandomSelector<T> : RandomSelector<T>, IRandomSelectorBuilder<T>
    {
        public const Int32 DefaultCapacity = 32;
        
        private IRandom Random { get; set; }

        private List<T> Items { get; }
        private List<Double> Weights { get; }
        private List<Double> Distribution { get; }

        private Func<List<Double>, Double, Int32>? Selector { get; set; }

        public DynamicRandomSelector()
            : this(DefaultCapacity)
        {
        }
        
        public DynamicRandomSelector(Int32 capacity)
            : this(capacity, RandomUtils.Create())
        {
        }

        public DynamicRandomSelector(Int32 capacity, Int32 seed)
            : this(capacity, RandomUtils.Create(seed))
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="random">Random generator</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(Int32 capacity, IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));

            if (capacity < 0)
            {
                capacity = 0;
            }

            Items = new List<T>(capacity);
            Weights = new List<Double>(capacity);
            Distribution = new List<Double>(capacity);
        }

        public DynamicRandomSelector(IEnumerable<T> items, IEnumerable<Double> weights)
            : this(items, weights, DefaultCapacity)
        {
        }

        public DynamicRandomSelector(IEnumerable<T> items, IEnumerable<Double> weights, Int32 capacity)
            : this(items ?? throw new ArgumentNullException(nameof(items)), weights ?? throw new ArgumentNullException(nameof(weights)), capacity, RandomUtils.Create())
        {
        }

        public DynamicRandomSelector(IEnumerable<T> items, IEnumerable<Double> weights, Int32 capacity, Int32 seed)
            : this(items ?? throw new ArgumentNullException(nameof(items)), weights ?? throw new ArgumentNullException(nameof(weights)), capacity, RandomUtils.Create(seed))
        {
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="items">Items that will get returned on random selections</param>
        /// <param name="weights">Un-normalized weights/chances of items, should be same length as items array</param>
        /// <param name="random">Random generator</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(IEnumerable<T> items, IEnumerable<Double> weights, Int32 capacity, IRandom random)
            : this(capacity, random)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }

            foreach ((T item, Double weight) in items.Zip(weights))
            {
                Add(item, weight);
            }

            Build();
        }
        
        public DynamicRandomSelector(IDictionary<T, Double> source)
            : this(source, DefaultCapacity)
        {
        }

        public DynamicRandomSelector(IDictionary<T, Double> source, Int32 capacity)
            : this(source ?? throw new ArgumentNullException(nameof(source)), capacity, RandomUtils.Create())
        {
        }

        public DynamicRandomSelector(IDictionary<T, Double> source, Int32 capacity, Int32 seed)
            : this(source ?? throw new ArgumentNullException(nameof(source)), capacity, RandomUtils.Create(seed))
        {
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="source">Items that will get returned on random selections</param>
        /// <param name="random">Random generator</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(IDictionary<T, Double> source, Int32 capacity, IRandom random)
            : this(capacity, random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach ((T item, Double weight) in source)
            {
                Add(item, weight);
            }

            Build();
        }

        /// <summary>
        /// Add new item with weight into collection. Items with zero weight will be ignored.
        /// Do not add duplicate items, because removing them will be buggy (you will need to call remove for duplicates too!).
        /// Be sure to call Build() after you are done adding items.
        /// </summary>
        /// <param name="item">Item that will be returned on random selection</param>
        /// <param name="weight">Non-zero non-normalized weight</param>
        public void Add(T item, Double weight)
        {
            // ignore zero weight items
            if (Math.Abs(weight) < Double.Epsilon)
            {
                return;
            }

            Items.Add(item);
            Weights.Add(weight);
        }

        /// <summary>
        /// Remove existing item with weight into collection.
        /// Be sure to call Build() after you are done removing items.
        /// </summary>
        /// <param name="item">Item that will be removed out of collection, if found</param>
        public void Remove(T item)
        {
            Int32 index = Items.IndexOf(item);

            // nothing was found
            if (index == -1)
            {
                return;
            }

            Items.RemoveAt(index);
            Weights.RemoveAt(index);
            // no need to remove from CDL, should be rebuilt instead
        }

        /// <inheritdoc cref="Build(int)"/>
        public IRandomSelector<T> Build()
        {
            return Build(-1);
        }

        /// <summary>
        /// Re/Builds internal CDL (Cummulative Distribution List)
        /// Must be called after modifying (calling Add or Remove), or it will break. 
        /// Switches between linear or binary search, depending on which one will be faster.
        /// Might generate some garbage (list resize) on first few builds.
        /// </summary>
        /// <param name="seed">You can specify seed for internal random gen or leave it alone</param>
        /// <returns>Returns itself</returns>
        public IRandomSelector<T> Build(Int32 seed)
        {
            if (Items.Count == 0)
            {
                throw new Exception("Cannot build with no items.");
            }

            // clear list and then transfer weights
            Distribution.Clear();
            foreach (Double weight in Weights)
            {
                Distribution.Add(weight);
            }

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

                Random = RandomUtils.Create(seed);
            }

            // RandomMath.ListBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDL list is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (Distribution.Count < ListBreakpoint)
            {
                Selector = SelectIndexLinearSearch;
            }
            else
            {
                Selector = SelectIndexBinarySearch;
            }

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

            Int32 lo = 0;
            Int32 hi = cdl.Count - 1;
            Int32 index;

            while (lo <= hi)
            {
                // calculate median
                index = lo + ((hi - lo) >> 1);

                if (Math.Abs(cdl[index] - value) < Double.Epsilon)
                {
                    return index;
                }

                if (cdl[index] < value)
                {
                    // shrink left
                    lo = index + 1;
                }
                else
                {
                    // shrink right
                    hi = index - 1;
                }
            }

            index = lo;

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
                throw new ArgumentOutOfRangeException(nameof(Items), "Container is empty");
            }

            return Items[Selector(Distribution, Random.NextDouble())];
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
                throw new ArgumentOutOfRangeException(nameof(Items), "Container is empty");
            }

            return Items[Selector(Distribution, value)];
        }

        /// <summary>
        /// Clears internal buffers, should make no garbage (unless internal lists hold objects that aren't referenced anywhere else)
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            Weights.Clear();
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