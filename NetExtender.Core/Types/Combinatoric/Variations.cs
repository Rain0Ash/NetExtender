// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Combinatoric.Interfaces;

namespace NetExtender.Types.Combinatoric
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
    public class Variations<T> : ICombinatoricCollection<T>
    {
        /// <summary>
        /// Copy of values object is intialized with, required for enumerator reset.
        /// </summary>
        private List<T> Values { get; }

        /// <summary>
        /// Permutations object that handles permutations on int for variation inclusion and ordering.
        /// </summary>
        private Permutations<Int32>? Permutations { get; }
        
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
                return Permutations?.Count ?? (Int64) Math.Pow(UpperIndex, LowerIndex);
            }
        }

        /// <summary>
        /// The type of Variations set that is generated.
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
        /// Create a variation set from the indicated list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// Collection type defaults to MetaCollectionType.WithoutRepetition
        /// </summary>
        /// <param name="values">List of values to select Variations from.</param>
        /// <param name="index">The size of each variation set to return.</param>
        public Variations(IEnumerable<T> values, Int32 index)
            : this(values, index, false)
        {
        }

        /// <summary>
        /// Create a variation set from the indicated list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// </summary>
        /// <param name="values">List of values to select variations from.</param>
        /// <param name="index">The size of each vatiation set to return.</param>
        /// <param name="repetition">Type indicates whether to use repetition in set generation.</param>
        public Variations(IEnumerable<T> values, Int32 index, Boolean repetition)
        {
            LowerIndex = index;
            Values = new List<T>(values);
            
            Repetition = repetition;
            
            if (Repetition)
            {
                return;
            }

            Int32 current = 0;
            List<Int32> map = Values.Select((_, i) => i >= Values.Count - LowerIndex ? current++ : Int32.MaxValue).ToList();
            Permutations = new Permutations<Int32>(map);
        }

        /// <summary>
        /// Gets an enumerator for the collection of Variations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IList<T>> GetEnumerator()
        {
            if (Repetition)
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
            if (Repetition)
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
            /// Parent object this is an enumerator for.
            /// </summary>
            private Variations<T> Parent { get; }

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T>? CurrentList { get; set; }

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private List<Int32>? Indexes { get; set; }
            
            /// <summary>
            /// The current variation
            /// </summary>
            public IList<T> Current
            {
                get
                {
                    return Next();
                }
            }

            /// <summary>
            /// The current variation.
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
            /// <param name="source">The source Variations object.</param>
            public EnumeratorWithRepetition(Variations<T> source)
            {
                Parent = source;
                Reset();
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
                if (Indexes is null)
                {
                    Indexes = new List<Int32>();
                    for (Int32 i = 0; i < Parent.LowerIndex; ++i)
                    {
                        Indexes.Add(0);
                    }

                    carry = 0;
                }
                else
                {
                    for (Int32 i = Indexes.Count - 1; i >= 0 && carry > 0; --i)
                    {
                        Indexes[i] += carry;
                        carry = 0;
                        
                        if (Indexes[i] < Parent.UpperIndex)
                        {
                            continue;
                        }

                        Indexes[i] = 0;
                        carry = 1;
                    }
                }

                CurrentList = null;
                return carry != 1;
            }
            
            /// <summary>
            /// Computes the current list based on the internal list index.
            /// </summary>
            private IList<T> Next()
            {
                if (CurrentList is not null)
                {
                    return CurrentList;
                }

                CurrentList = new List<T>();
                foreach (Int32 index in Indexes!)
                {
                    CurrentList.Add(Parent.Values[index]);
                }

                return CurrentList;
            }
            
            /// <summary>
            /// Resets the Variations enumerator to the first variation.  
            /// </summary>
            public void Reset()
            {
                CurrentList = null;
                Indexes = null;
            }

            /// <summary>
            /// Cleans up non-managed resources, of which there are none used here.
            /// </summary>
            public void Dispose()
            {
            }
        }

        /// <summary>
        /// An enumerator for Variations when the type is set to WithoutRepetition.
        /// </summary>
        public class EnumeratorWithoutRepetition : IEnumerator<IList<T>>
        {
            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private Variations<T> Parent { get; }

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T>? CurrentList { get; set; }

            /// <summary>
            /// An enumertor of the parents list of lexicographic orderings.
            /// </summary>
            private Permutations<Int32>.Enumerator Permutations { get; }
            
            /// <summary>
            /// The current variation.
            /// </summary>
            public IList<T> Current
            {
                get
                {
                    return Next();
                }
            }

            /// <summary>
            /// The current variation.
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
            /// <param name="source">The source Variations object.</param>
            public EnumeratorWithoutRepetition(Variations<T> source)
            {
                Parent = source;
                Permutations = (Permutations<Int32>.Enumerator) Parent.Permutations!.GetEnumerator();
            }
            
            /// <summary>
            /// Resets the Variations enumerator to the first variation.  
            /// </summary>
            public void Reset()
            {
                Permutations.Reset();
            }

            /// <summary>
            /// Advances to the next variation.
            /// </summary>
            /// <returns>True if successfully moved to next variation, False if no more variations exist.</returns>
            public Boolean MoveNext()
            {
                Boolean successful = Permutations.MoveNext();
                CurrentList = null;
                return successful;
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
            private IList<T> Next()
            {
                if (CurrentList is not null)
                {
                    return CurrentList;
                }

                CurrentList = new List<T>();
                IList<Int32> permutation = (IList<Int32>) Permutations.Current;
                
                Int32 index = 0;
                for (Int32 i = 0; i < Parent.LowerIndex; ++i)
                {
                    CurrentList.Add(Parent.Values[0]);
                }

                foreach (Int32 position in permutation)
                {
                    if (position != Int32.MaxValue)
                    {
                        CurrentList[position] = Parent.Values[index];
                        if (!Parent.Repetition)
                        {
                            ++index;
                        }
                        
                        continue;
                    }

                    ++index;
                }

                return CurrentList;
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