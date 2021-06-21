// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Combinatorics.Interfaces;

namespace NetExtender.Combinatorics
{
    /// <summary>
    /// Combinations defines a meta-collection, typically a list of lists, of all possible 
    /// subsets of a particular size from the set of values.  This list is enumerable and 
    /// allows the scanning of all possible combinations using a simple foreach() loop.
    /// Within the returned set, there is no prescribed order.  This follows the mathematical
    /// concept of choose.  For example, put 10 dominoes in a hat and pick 5.  The number of possible
    /// combinations is defined as "10 choose 5", which is calculated as (10!) / ((10 - 5)! * 5!).
    /// </summary>
    /// <remarks>
    /// The MetaCollectionType parameter of the constructor allows for the creation of
    /// two types of sets,  those with and without repetition in the output set when 
    /// presented with repetition in the input set.
    /// 
    /// When given a input collect {A B C} and lower index of 2, the following sets are generated:
    /// MetaCollectionType.WithRepetition =>
    /// {A A}, {A B}, {A C}, {B B}, {B C}, {C C}
    /// MetaCollectionType.WithoutRepetition =>
    /// {A B}, {A C}, {B C}
    /// 
    /// Input sets with multiple equal values will generate redundant combinations in proprotion
    /// to the likelyhood of outcome.  For example, {A A B B} and a lower index of 3 will generate:
    /// {A A B} {A A B} {A B B} {A B B}
    /// </remarks>
    /// <typeparam name="T">The type of the values within the list.</typeparam>
    public class Combinations<T> : ICombinatorialCollection<T>
    {
        /// <summary>
        /// Copy of values object is intialized with, required for enumerator reset.
        /// </summary>
        private List<T> Values { get; }

        /// <summary>
        /// Permutations object that handles permutations on booleans for combination inclusion.
        /// </summary>
        private Permutations<Boolean> Permutations { get; }
        
        /// <summary>
        /// The number of unique combinations that are defined in this meta-collection.
        /// This value is mathematically defined as Choose(M, N) where M is the set size
        /// and N is the subset size.  This is M! / (N! * (M-N)!).
        /// </summary>
        public Int64 Count
        {
            get
            {
                return Permutations.Count;
            }
        }

        /// <summary>
        /// The type of Combinations set that is generated.
        /// </summary>
        public Boolean Repetition { get; }

        /// <summary>
        /// The upper index of the meta-collection, equal to the number of items in the initial set.
        /// </summary>
        public Int32 UpperIndex
        {
            get
            {
                return Values.Count;
            }
        }

        /// <summary>
        /// The lower index of the meta-collection, equal to the number of items returned each iteration.
        /// </summary>
        public Int32 LowerIndex { get; }
        
        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// Collection type defaults to MetaCollectionType.WithoutRepetition
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="index">The size of each combination set to return.</param>
        public Combinations(ICollection<T> values, Int32 index = 1)
            : this(values, index, false)
        {
        }

        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="index">The size of each combination set to return.</param>
        /// <param name="repetition">The type of Combinations set to generate.</param>
        public Combinations(ICollection<T> values, Int32 index, Boolean repetition)
        {
            Repetition = repetition;
            LowerIndex = index;
            
            Values = new List<T>();
            Values.AddRange(values);
            
            if (!Repetition)
            {
                Permutations = new Permutations<Boolean>(Values.Select((_, i) => i < Values.Count - LowerIndex).ToList());
                return;
            }

            List<Boolean> map = new List<Boolean>();

            for (Int32 i = 0; i < values.Count - 1; ++i)
            {
                map.Add(true);
            }

            for (Int32 i = 0; i < LowerIndex; ++i)
            {
                map.Add(false);
            }

            Permutations = new Permutations<Boolean>(map);
        }
        
        /// <summary>
        /// Gets an enumerator for collecting the list of combinations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IList<T>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Gets an enumerator for collecting the list of combinations.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// The enumerator that enumerates each meta-collection of the enclosing Combinations class.
        /// </summary>
        public class Enumerator : IEnumerator<IList<T>>
        {
            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private Combinations<T> Parent { get; }

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T>? Values { get; set; }

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private Permutations<Boolean>.Enumerator Permutations { get; }
            
            /// <summary>
            /// The current combination
            /// </summary>
            public IList<T> Current
            {
                get
                {
                    return Next();
                }
            }

            /// <summary>
            /// The current combination
            /// </summary>
            Object IEnumerator.Current
            {
                get
                {
                    return Next();
                }
            }
            
            /// <summary>
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source combinations object.</param>
            public Enumerator(Combinations<T> source)
            {
                Parent = source;
                Permutations = (Permutations<Boolean>.Enumerator) Parent.Permutations.GetEnumerator();
            }

            /// <summary>
            /// Advances to the next combination of items from the set.
            /// </summary>
            /// <returns>True if successfully moved to next combination, False if no more unique combinations exist.</returns>
            /// <remarks>
            /// The heavy lifting is done by the permutations object, the combination is generated
            /// by creating a new list of those items that have a true in the permutation parrellel array.
            /// </remarks>
            public Boolean MoveNext()
            {
                Boolean successful = Permutations.MoveNext();
                Values = null;
                return successful;
            }

            /// <summary>
            /// The only complex function of this entire wrapper, ComputeCurrent() creates
            /// a list of original values from the bool permutation provided.  
            /// The exception for accessing current (InvalidOperationException) is generated
            /// by the call to .Current on the underlying enumeration.
            /// </summary>
            /// <remarks>
            /// To compute the current list of values, the underlying permutation object
            /// which moves with this enumerator, is scanned differently based on the type.
            /// The items have only two values, true and false, which have different meanings:
            /// 
            /// For type WithoutRepetition, the output is a straightforward subset of the input array.  
            /// E.g. 6 choose 3 without repetition
            /// Input array:   {A B C D E F}
            /// Permutations:  {0 1 0 0 1 1}
            /// Generates set: {A   C D    }
            /// Note: size of permutation is equal to upper index.
            /// 
            /// For type WithRepetition, the output is defined by runs of characters and when to 
            /// move to the next element.
            /// E.g. 6 choose 5 with repetition
            /// Input array:   {A B C D E F}
            /// Permutations:  {0 1 0 0 1 1 0 0 1 1}
            /// Generates set: {A   B B     D D    }
            /// Note: size of permutation is equal to upper index - 1 + lower index.
            /// </remarks>
            private IList<T> Next()
            {
                if (Values is not null)
                {
                    return Values;
                }

                Values = new List<T>(Parent.UpperIndex);
                Int32 index = 0;
                IList<Boolean>? current = (IList<Boolean>?) Permutations.Current;
                if (current is null)
                {
                    return Values;
                }

                foreach (Boolean boolean in current)
                {
                    if (boolean)
                    {
                        ++index;
                        continue;
                    }

                    Values.Add(Parent.Values[index]);
                    if (!Parent.Repetition)
                    {
                        ++index;
                    }
                }

                return Values;
            }
            
            /// <summary>
            /// Resets the combinations enumerator to the first combination.  
            /// </summary>
            public void Reset()
            {
                Permutations.Reset();
            }
            
            /// <summary>
            /// Cleans up non-managed resources, of which there are none used here.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}