// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Types.Combinatoric.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Combinatoric
{
    /// <summary>
    /// Permutations defines a meta-collection, typically a list of lists, of all
    /// possible orderings of a set of values.  This list is enumerable and allows
    /// the scanning of all possible permutations using a simple foreach() loop.
    /// The MetaCollectionType parameter of the constructor allows for the creation of
    /// two types of sets,  those with and without repetition in the output set when 
    /// presented with repetition in the input set.
    /// </summary>
    /// <remarks>
    /// When given a input collect {A A B}, the following sets are generated:
    /// MetaCollectionType.WithRepetition =>
    /// {A A B}, {A B A}, {A A B}, {A B A}, {B A A}, {B A A}
    /// MetaCollectionType.WithoutRepetition =>
    /// {A A B}, {A B A}, {B A A}
    /// 
    /// When generating non-repetition sets, ordering is based on the lexicographic 
    /// ordering of the lists based on the provided Comparer.  
    /// If no comparer is provided, then T must be IComparable on T.
    /// 
    /// When generating repetition sets, no comparisions are performed and therefore
    /// no comparer is required and T does not need to be IComparable.
    /// </remarks>
    /// <typeparam name="T">The type of the values within the list.</typeparam>
    public class Permutations<T> : ICombinatoricCollection<T>
    {
        /// <summary>
        /// A list of T that represents the order of elements as originally provided, used for Reset.
        /// </summary>
        private List<T> Values { get; }

        /// <summary>
        /// Parrellel array of integers that represent the location of items in the myValues array.
        /// This is generated at Initialization and is used as a performance speed up rather that
        /// comparing T each time, much faster to let the CLR optimize around integers.
        /// </summary>
        private Int32[] Orders { get; }

        /// <summary>
        /// The count of all permutations that will be returned.
        /// If type is MetaCollectionType.WithholdGeneratedSets, then this does not double count permutations with multiple identical values.  
        /// I.e. count of permutations of "AAB" will be 3 instead of 6.  
        /// If type is MetaCollectionType.WithRepetition, then this is all combinations and is therefore N!, where N is the number of values.
        /// </summary>
        public Int64 Count { get; }

        /// <summary>
        /// The type of Permutations set that is generated.
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
        /// For Permutation, this is always equal to the UpperIndex.
        /// </summary>
        public Int32 LowerIndex
        {
            get
            {
                return Values.Count;
            }
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// The values (T) must implement IComparable.  
        /// If T does not implement IComparable use a constructor with an explict IComparer.
        /// The repetition type defaults to MetaCollectionType.WithholdRepetitionSets
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        public Permutations(ICollection<T> values)
            : this(values, null)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// If type is MetaCollectionType.WithholdRepetitionSets, then values (T) must implement IComparable.  
        /// If T does not implement IComparable use a constructor with an explict IComparer.
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="repetition">The type of permutation set to calculate.</param>
        public Permutations(ICollection<T> values, Boolean repetition)
            : this(values, repetition, null)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// The values will be compared using the supplied IComparer.
        /// The repetition type defaults to MetaCollectionType.WithholdRepetitionSets
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="comparer">Comparer used for defining the lexigraphic order.</param>
        public Permutations(ICollection<T> values, IComparer<T>? comparer)
            : this(values, false, comparer)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// The values will be compared using the supplied IComparer.
        /// The repetition type defaults to MetaCollectionType.WithholdRepetitionSets
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="repetition">The type of permutation set to calculate.</param>
        /// <param name="comparer">Comparer used for defining the lexigraphic order.</param>
        public Permutations(ICollection<T> values, Boolean repetition, IComparer<T>? comparer)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = new List<T>(values);
            Orders = new Int32[values.Count];

            Repetition = repetition;

            if (Repetition)
            {
                for (Int32 i = 0; i < Orders.Length; ++i)
                {
                    Orders[i] = i;
                }

                Count = GetCount();
                return;
            }

            comparer ??= new SelfComparer();

            Values.Sort(comparer);

            if (Orders.Length > 0)
            {
                Orders[0] = 1;
            }

            for (Int32 i = 1; i < Orders.Length; ++i)
            {
                Int32 j = 1;
                if (comparer.Compare(Values[i - 1], Values[i]) != 0)
                {
                    ++j;
                }

                Orders[i] = j;
            }

            Count = GetCount();
        }

        /// <summary>
        /// Calculates the total number of permutations that will be returned.  
        /// As this can grow very large, extra effort is taken to avoid overflowing the accumulator.  
        /// While the algorithm looks complex, it really is just collecting numerator and denominator terms
        /// and cancelling out all of the denominator terms before taking the product of the numerator terms.  
        /// </summary>
        /// <returns>The number of permutations.</returns>
        private Int64 GetCount()
        {
            List<Int32> divisors = new List<Int32>();
            List<Int32> numerators = new List<Int32>();

            Int32 count = 1;
            for (Int32 i = 1; i < Orders.Length; ++i)
            {
                numerators.AddRange(PrimeUtilities.Factor(i + 1));

                if (Orders[i] == Orders[i - 1])
                {
                    ++count;
                    continue;
                }

                for (Int32 factor = 2; factor <= count; ++factor)
                {
                    divisors.AddRange(PrimeUtilities.Factor(factor));
                }

                count = 1;
            }

            for (Int32 factor = 2; factor <= count; ++factor)
            {
                divisors.AddRange(PrimeUtilities.Factor(factor));
            }

            return PrimeUtilities.EvaluatePrimeFactors(PrimeUtilities.DividePrimeFactors(numerators, divisors));
        }

        /// <summary>
        /// Gets an enumerator for collecting the list of permutations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator<IList<T>> IEnumerable<IList<T>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Gets an enumerator for collecting the list of permutations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// The enumerator that enumerates each meta-collection of the enclosing Permutations class.
        /// </summary>
        public class Enumerator : IEnumerator<IList<T>>
        {
            /// <summary>
            /// Internal position type for tracking enumertor position.
            /// </summary>
            private enum EnumeratorPosition
            {
                BeforeFirst,
                InSet,
                AfterLast
            }

            /// <summary>
            /// Flag indicating the position of the enumerator.
            /// </summary>
            private EnumeratorPosition Position { get; set; } = EnumeratorPosition.BeforeFirst;

            /// <summary>
            /// Parrellel array of integers that represent the location of items in the values array.
            /// This is generated at Initialization and is used as a performance speed up rather that
            /// comparing T each time, much faster to let the CLR optimize around integers.
            /// </summary>
            private Int32[] Orders { get; }

            /// <summary>
            /// The list of values that are current to the enumerator.
            /// </summary>
            private List<T> Values { get; }

            /// <summary>
            /// The set of permuations that this enumerator enumerates.
            /// </summary>
            private Permutations<T> Parent { get; }

            /// <summary>
            /// The current permutation.
            /// </summary>
            IList<T> IEnumerator<IList<T>>.Current
            {
                get
                {
                    if (Position == EnumeratorPosition.InSet)
                    {
                        return new List<T>(Values);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// The current permutation.
            /// </summary>
            public Object Current
            {
                get
                {
                    if (Position == EnumeratorPosition.InSet)
                    {
                        return new List<T>(Values);
                    }

                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source Permutations object.</param>
            public Enumerator(Permutations<T> source)
            {
                Parent = source;
                Values = new List<T>(Parent.Values.Count);
                Orders = new Int32[source.Orders.Length];
                source.Orders.CopyTo(Orders, 0);
                Reset();
            }

            /// <summary>
            /// Advances to the next permutation.
            /// </summary>
            /// <returns>True if successfully moved to next permutation, False if no more permutations exist.</returns>
            /// <remarks>
            /// Continuation was tried (i.e. yield return) by was not nearly as efficient.
            /// Performance is further increased by using value types and removing generics, that is, the LexicographicOrder parellel array.
            /// This is a issue with the .NET CLR not optimizing as well as it could in this infrequently used scenario.
            /// </remarks>
            public Boolean MoveNext()
            {
                switch (Position)
                {
                    case EnumeratorPosition.BeforeFirst:
                        Values.Clear();
                        Values.AddRange(Parent.Values);

                        Array.Sort(Orders);

                        Position = EnumeratorPosition.InSet;

                        break;
                    case EnumeratorPosition.InSet when Values.Count < 2:
                        Position = EnumeratorPosition.AfterLast;
                        break;
                    case EnumeratorPosition.InSet:
                        if (!NextPermutation())
                        {
                            Position = EnumeratorPosition.AfterLast;
                        }

                        break;
                    case EnumeratorPosition.AfterLast:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return Position != EnumeratorPosition.AfterLast;
            }

            /// <summary>
            /// Calculates the next lexicographical permutation of the set.
            /// This is a permutation with repetition where values that compare as equal will not 
            /// swap positions to create a new permutation.
            /// http://www.cut-the-knot.org/do_you_know/AllPerm.shtml
            /// E. W. Dijkstra, A Discipline of Programming, Prentice-Hall, 1997  
            /// </summary>
            /// <returns>True if a new permutation has been returned, false if not.</returns>
            /// <remarks>
            /// This uses the integers of the lexicographical order of the values so that any
            /// comparison of values are only performed during initialization. 
            /// </remarks>
            private Boolean NextPermutation()
            {
                Int32 i = Orders.Length - 1;
                while (Orders[i - 1] >= Orders[i])
                {
                    --i;
                    if (i == 0)
                    {
                        return false;
                    }
                }

                Int32 j = Orders.Length;
                while (Orders[j - 1] <= Orders[i - 1])
                {
                    --j;
                }

                Swap(i - 1, j - 1);
                ++i;
                j = Orders.Length;
                while (i < j)
                {
                    Swap(i - 1, j - 1);
                    ++i;
                    --j;
                }

                return true;
            }

            /// <summary>
            /// Helper function for swapping two elements within the internal collection.
            /// This swaps both the lexicographical order and the values, maintaining the parallel array.
            /// </summary>
            private void Swap(Int32 i, Int32 j)
            {
                Values.Swap(i, j);
                Orders.Swap(i, j);
            }

            /// <summary>
            /// Resets the permutations enumerator to the first permutation.  
            /// This will be the first lexicographically order permutation.
            /// </summary>
            public void Reset()
            {
                Position = EnumeratorPosition.BeforeFirst;
            }

            /// <summary>
            /// Cleans up non-managed resources, of which there are none used here.
            /// </summary>
            public void Dispose()
            {
            }
        }

        /// <summary>
        /// Inner class that wraps an IComparer around a type T when it is IComparable
        /// </summary>
        private class SelfComparer : IComparer<T>
        {
            public Int32 Compare(T? x, T? y)
            {
                return x is null ? -2 : ((IComparable<T>) x).CompareTo(y);
            }
        }
    }
}