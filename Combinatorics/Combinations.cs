// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public class Combinations<T> : IMetaCollection<T>
    {
        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// Collection type defaults to MetaCollectionType.WithoutRepetition
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="lowerIndex">The size of each combination set to return.</param>
        public Combinations(ICollection<T> values, Int32 lowerIndex = 1)
            : this(values, lowerIndex, GenerateOption.WithoutRepetition)
        {
        }

        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="lowerIndex">The size of each combination set to return.</param>
        /// <param name="type">The type of Combinations set to generate.</param>
        public Combinations(ICollection<T> values, Int32 lowerIndex, GenerateOption type)
        {
            Initialize(values, lowerIndex, type);
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
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source combinations object.</param>
            public Enumerator(Combinations<T> source)
            {
                _parent = source;
                _permutationsEnumerator = (Permutations<Boolean>.Enumerator) _parent._permutations.GetEnumerator();
            }


            /// <summary>
            /// Resets the combinations enumerator to the first combination.  
            /// </summary>
            public void Reset()
            {
                _permutationsEnumerator.Reset();
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
                Boolean ret = _permutationsEnumerator.MoveNext();
                _currentList = null;
                return ret;
            }

            /// <summary>
            /// The current combination
            /// </summary>
            public IList<T> Current
            {
                get
                {
                    ComputeCurrent();
                    return _currentList;
                }
            }

            /// <summary>
            /// The current combination
            /// </summary>
            Object IEnumerator.Current
            {
                get
                {
                    ComputeCurrent();
                    return _currentList;
                }
            }

            /// <summary>
            /// Cleans up non-managed resources, of which there are none used here.
            /// </summary>
            public void Dispose()
            {
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
            private void ComputeCurrent()
            {
                if (_currentList is not null)
                {
                    return;
                }

                _currentList = new List<T>();
                Int32 index = 0;
                IList<Boolean> current = (IList<Boolean>) _permutationsEnumerator.Current;
                if (current is null)
                {
                    return;
                }

                foreach (Boolean boolean in current)
                {
                    if (boolean)
                    {
                        ++index;
                        continue;
                    }

                    _currentList.Add(_parent._values[index]);
                    if (_parent.Type == GenerateOption.WithoutRepetition)
                    {
                        ++index;
                    }
                }
            }

            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private readonly Combinations<T> _parent;

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T> _currentList;

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private readonly Permutations<Boolean>.Enumerator _permutationsEnumerator;
        }

        /// <summary>
        /// The number of unique combinations that are defined in this meta-collection.
        /// This value is mathematically defined as Choose(M, N) where M is the set size
        /// and N is the subset size.  This is M! / (N! * (M-N)!).
        /// </summary>
        public Int64 Count
        {
            get
            {
                return _permutations.Count;
            }
        }

        /// <summary>
        /// The type of Combinations set that is generated.
        /// </summary>
        public GenerateOption Type { get; private set; }

        /// <summary>
        /// The upper index of the meta-collection, equal to the number of items in the initial set.
        /// </summary>
        public Int32 UpperIndex
        {
            get
            {
                return _values.Count;
            }
        }

        /// <summary>
        /// The lower index of the meta-collection, equal to the number of items returned each iteration.
        /// </summary>
        public Int32 LowerIndex { get; private set; }

        /// <summary>
        /// Initialize the combinations by settings a copy of the values from the 
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="lowerIndex">The size of each combination set to return.</param>
        /// <param name="type">The type of Combinations set to generate.</param>
        /// <remarks>
        /// Copies the array and parameters and then creates a map of booleans that will 
        /// be used by a permutations object to refence the subset.  This map is slightly
        /// different based on whether the type is with or without repetition.
        /// 
        /// When the type is WithoutRepetition, then a map of upper index elements is
        /// created with lower index false's.  
        /// E.g. 8 choose 3 generates:
        /// Map: {1 1 1 1 1 0 0 0}
        /// Note: For sorting reasons, false denotes inclusion in output.
        /// 
        /// When the type is WithRepetition, then a map of upper index - 1 + lower index
        /// elements is created with the falses indicating that the 'current' element should
        /// be included and the trues meaning to advance the 'current' element by one.
        /// E.g. 8 choose 3 generates:
        /// Map: {1 1 1 1 1 1 1 1 0 0 0} (7 trues, 3 falses).
        /// </remarks>
        private void Initialize(ICollection<T> values, Int32 lowerIndex, GenerateOption type)
        {
            Type = type;
            LowerIndex = lowerIndex;
            _values = new List<T>();
            _values.AddRange(values);
            List<Boolean> map = new List<Boolean>();
            if (type == GenerateOption.WithoutRepetition)
            {
                map.AddRange(_values.Select((t, i) => i < _values.Count - LowerIndex));
            }
            else
            {
                for (Int32 i = 0; i < values.Count - 1; ++i)
                {
                    map.Add(true);
                }

                for (Int32 i = 0; i < LowerIndex; ++i)
                {
                    map.Add(false);
                }
            }

            _permutations = new Permutations<Boolean>(map);
        }
        
        /// <summary>
        /// Copy of values object is intialized with, required for enumerator reset.
        /// </summary>
        private List<T> _values;

        /// <summary>
        /// Permutations object that handles permutations on booleans for combination inclusion.
        /// </summary>
        private Permutations<Boolean> _permutations;
    }
}