// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Combinatorics.Interfaces
{
    /// <summary>
    /// Interface for Permutations, Combinations and any other classes that present
    /// a collection of collections based on an input collection.  The enumerators that 
    /// this class inherits defines the mechanism for enumerating through the collections.  
    /// </summary>
    /// <typeparam name="T">The of the elements in the collection, not the type of the collection.</typeparam>
    public interface ICombinatorialCollection<T> : IEnumerable<IList<T>>
    {
        /// <summary>
        /// The type of the meta-collection, determining how the collections are 
        /// determined from the inputs.
        /// </summary>
        public Boolean Repetition { get; }
        
        /// <summary>
        /// The upper index of the meta-collection, which is the size of the input collection.
        /// </summary>
        public Int32 UpperIndex { get; }

        /// <summary>
        /// The lower index of the meta-collection, which is the size of each output collection.
        /// </summary>
        public Int32 LowerIndex { get; }
        
        /// <summary>
        /// The count of items in the collection. This is not inherited from
        /// ICollection since this meta-collection cannot be extended by users.
        /// </summary>
        public Int64 Count { get; }
    }
}