// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Combinatorics
{
    /// <summary>
    /// Variations defines a meta-collection, typically a list of lists, of all possible 
    /// ordered subsets of a particular size from the set of values.  
    /// This list is enumerable and allows the scanning of all possible Variations using a simple 
    /// foreach() loop even though the variations are not all in memory.
    /// </summary>
    /// <remarks>
    /// The MetaCollectionType parameter of the constructor allows for the creation of
    /// normal Variations and Variations with Repetition.
    /// 
    /// When given an input collect {A B C} and lower index of 2, the following sets are generated:
    /// MetaCollectionType.WithoutRepetition generates 6 sets: =>
    ///     {A B}, {A B}, {B A}, {B C}, {C A}, {C B}
    /// MetaCollectionType.WithRepetition generates 9 sets:
    ///     {A A}, {A B}, {A B}, {B A}, {B B }, {B C}, {C A}, {C B}, {C C}
    /// 
    /// The equality of multiple inputs is not considered when generating variations.
    /// </remarks>
    /// <typeparam name="T">The type of the values within the list.</typeparam>
    public class Variations<T> : IMetaCollection<T>
    {
        /// <summary>
        /// Create a variation set from the indicated list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// Collection type defaults to MetaCollectionType.WithoutRepetition
        /// </summary>
        /// <param name="values">List of values to select Variations from.</param>
        /// <param name="lowerIndex">The size of each variation set to return.</param>
        public Variations(IEnumerable<T> values, Int32 lowerIndex)
        {
            Initialize(values, lowerIndex, GenerateOption.WithoutRepetition);
        }

        /// <summary>
        /// Create a variation set from the indicated list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// </summary>
        /// <param name="values">List of values to select variations from.</param>
        /// <param name="lowerIndex">The size of each vatiation set to return.</param>
        /// <param name="type">Type indicates whether to use repetition in set generation.</param>
        public Variations(IEnumerable<T> values, Int32 lowerIndex, GenerateOption type)
        {
            Initialize(values, lowerIndex, type);
        }

        /// <summary>
        /// Gets an enumerator for the collection of Variations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (Type == GenerateOption.WithRepetition)
            {
                return new EnumeratorWithRepetition(this);
            }

            return new EnumeratorWithoutRepetition(this);
        }

        /// <summary>
        /// Gets an enumerator for the collection of Variations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (Type == GenerateOption.WithRepetition)
            {
                return new EnumeratorWithRepetition(this);
            }

            return new EnumeratorWithoutRepetition(this);
        }


        /// <summary>
        /// An enumerator for Variations when the type is set to WithRepetition.
        /// </summary>
        public class EnumeratorWithRepetition : IEnumerator<IList<T>>
        {
            /// <summary>
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source Variations object.</param>
            public EnumeratorWithRepetition(Variations<T> source)
            {
                _parent = source;
                Reset();
            }


            /// <summary>
            /// Resets the Variations enumerator to the first variation.  
            /// </summary>
            public void Reset()
            {
                _currentList = null;
                _listIndexes = null;
            }

            /// <summary>
            /// Advances to the next variation.
            /// </summary>
            /// <returns>True if successfully moved to next variation, False if no more variations exist.</returns>
            /// <remarks>
            /// Increments the internal myListIndexes collection by incrementing the last index
            /// and overflow/carrying into others just like grade-school arithemtic.  If the 
            /// finaly carry flag is set, then we would wrap around and are therefore done.
            /// </remarks>
            public Boolean MoveNext()
            {
                Int32 carry = 1;
                if (_listIndexes is null)
                {
                    _listIndexes = new List<Int32>();
                    for (Int32 i = 0; i < _parent.LowerIndex; ++i)
                    {
                        _listIndexes.Add(0);
                    }

                    carry = 0;
                }
                else
                {
                    for (Int32 i = _listIndexes.Count - 1; i >= 0 && carry > 0; --i)
                    {
                        _listIndexes[i] += carry;
                        carry = 0;
                        if (_listIndexes[i] < _parent.UpperIndex)
                        {
                            continue;
                        }

                        _listIndexes[i] = 0;
                        carry = 1;
                    }
                }

                _currentList = null;
                return carry != 1;
            }

            /// <summary>
            /// The current variation
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
            /// The current variation.
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
            /// Computes the current list based on the internal list index.
            /// </summary>
            private void ComputeCurrent()
            {
                if (_currentList is not null)
                {
                    return;
                }

                _currentList = new List<T>();
                foreach (Int32 index in _listIndexes)
                {
                    _currentList.Add(_parent._values[index]);
                }
            }


            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private readonly Variations<T> _parent;

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T> _currentList;

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private List<Int32> _listIndexes;
        }

        /// <summary>
        /// An enumerator for Variations when the type is set to WithoutRepetition.
        /// </summary>
        public class EnumeratorWithoutRepetition : IEnumerator<IList<T>>
        {
            /// <summary>
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source Variations object.</param>
            public EnumeratorWithoutRepetition(Variations<T> source)
            {
                _parent = source;
                _permutationsEnumerator = (Permutations<Int32>.Enumerator) _parent._permutations.GetEnumerator();
            }
            
            /// <summary>
            /// Resets the Variations enumerator to the first variation.  
            /// </summary>
            public void Reset()
            {
                _permutationsEnumerator.Reset();
            }

            /// <summary>
            /// Advances to the next variation.
            /// </summary>
            /// <returns>True if successfully moved to next variation, False if no more variations exist.</returns>
            public Boolean MoveNext()
            {
                Boolean ret = _permutationsEnumerator.MoveNext();
                _currentList = null;
                return ret;
            }

            /// <summary>
            /// The current variation.
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
            /// The current variation.
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
            /// Creates a list of original values from the int permutation provided.  
            /// The exception for accessing current (InvalidOperationException) is generated
            /// by the call to .Current on the underlying enumeration.
            /// </summary>
            /// <remarks>
            /// To compute the current list of values, the element to use is determined by 
            /// a permutation position with a non-MaxValue value.  It is placed at the position in the
            /// output that the index value indicates.
            /// 
            /// E.g. Variations of 6 choose 3 without repetition
            /// Input array:   {A B C D E F}
            /// Permutations:  {- 1 - - 3 2} (- is Int32.MaxValue)
            /// Generates set: {B F E}
            /// </remarks>
            private void ComputeCurrent()
            {
                if (_currentList is not null)
                {
                    return;
                }

                _currentList = new List<T>();
                Int32 index = 0;
                IList<Int32> currentPermutation = (IList<Int32>) _permutationsEnumerator.Current;
                for (Int32 i = 0; i < _parent.LowerIndex; ++i)
                {
                    _currentList.Add(_parent._values[0]);
                }

                foreach (Int32 position in currentPermutation)
                {
                    if (position != Int32.MaxValue)
                    {
                        _currentList[position] = _parent._values[index];
                        if (_parent.Type == GenerateOption.WithoutRepetition)
                        {
                            ++index;
                        }
                    }
                    else
                    {
                        ++index;
                    }
                }
            }


            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private readonly Variations<T> _parent;

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T> _currentList;

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private readonly Permutations<Int32>.Enumerator _permutationsEnumerator;
        }

        /// <summary>
        /// The number of unique variations that are defined in this meta-collection.
        /// </summary>
        /// <remarks>
        /// Variations with repetitions does not behave like other meta-collections and it's
        /// count is equal to N^P, where N is the upper index and P is the lower index.
        /// </remarks>
        public Int64 Count
        {
            get
            {
                if (Type == GenerateOption.WithoutRepetition)
                {
                    return _permutations.Count;
                }

                return (Int64) Math.Pow(UpperIndex, LowerIndex);
            }
        }

        /// <summary>
        /// The type of Variations set that is generated.
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
        /// Initialize the variations for constructors.
        /// </summary>
        /// <param name="values">List of values to select variations from.</param>
        /// <param name="lowerIndex">The size of each variation set to return.</param>
        /// <param name="type">The type of variations set to generate.</param>
        private void Initialize(IEnumerable<T> values, Int32 lowerIndex, GenerateOption type)
        {
            Type = type;
            LowerIndex = lowerIndex;
            _values = new List<T>();
            _values.AddRange(values);
            if (type != GenerateOption.WithoutRepetition)
            {
                return;
            }

            Int32 index = 0;
            List<Int32> map = _values.Select((t, i) => i >= _values.Count - LowerIndex ? index++ : Int32.MaxValue).ToList();
            _permutations = new Permutations<Int32>(map);
        }

        /// <summary>
        /// Copy of values object is intialized with, required for enumerator reset.
        /// </summary>
        private List<T> _values;

        /// <summary>
        /// Permutations object that handles permutations on int for variation inclusion and ordering.
        /// </summary>
        private Permutations<Int32> _permutations;
    }
}