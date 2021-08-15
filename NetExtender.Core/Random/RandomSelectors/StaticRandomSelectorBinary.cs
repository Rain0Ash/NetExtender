// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Random
{
    /// <summary>
    /// Uses Binary Search for picking random items
    /// Good for large sized number of items
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class StaticRandomSelectorBinary<T> : RandomSelector<T>
    {
        private IRandom Random { get; }
        private T[] Items { get; }
        private Double[] Distribution { get; }

        public StaticRandomSelectorBinary(T[] items, Double[] cda)
            : this(items ?? throw new ArgumentNullException(nameof(items)), cda ?? throw new ArgumentNullException(nameof(cda)), RandomUtilities.Create())
        {
        }
        
        public StaticRandomSelectorBinary(T[] items, Double[] cda, Int32 seed)
            : this(items ?? throw new ArgumentNullException(nameof(items)), cda ?? throw new ArgumentNullException(nameof(cda)), RandomUtilities.Create(seed))
        {
        }
        
        /// <summary>
        /// Constructor, used by StaticRandomSelectorBuilder
        /// Needs array of items and CDA (Cummulative Distribution Array). 
        /// </summary>
        /// <param name="items">Items of type T</param>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="random">Random generator</param>
        public StaticRandomSelectorBinary(T[] items, Double[] cda, IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Distribution = cda ?? throw new ArgumentNullException(nameof(cda));
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses binary search for random selection.
        /// </summary>
        /// <returns>Returns item</returns>
        public override T GetRandom()
        {
            if (Items.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Items), "Container is empty");
            }
            
            return Items[SelectIndexBinarySearch(Distribution, Random.NextDouble())];
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses binary search for random selection.
        /// </summary>
        /// <param name="value">Random value from your uniform generator</param>
        /// <returns>Returns item</returns>
        public override T GetRandom(Double value)
        {
            if (Items.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Items), "Container is empty");
            }
            
            return Items[SelectIndexBinarySearch(Distribution, value)];
        }
    }
}