// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Random.Interfaces;

namespace NetExtender.Random
{
    public static class RandomMath
    {
        /// <summary>
        /// Breaking point between using Linear vs. Binary search for arrays (StaticSelector). 
        /// Was calculated empirically.
        /// </summary>
        public const Int32 ArrayBreakpoint = 51;

        /// <summary>
        /// Breaking point between using Linear vs. Binary search for lists (DynamicSelector). 
        /// Was calculated empirically. 
        /// </summary>
        public const Int32 ListBreakpoint = 26;

        /// <summary>
        /// Builds cummulative distribution out of non-normalized weights inplace.
        /// </summary>
        /// <param name="cdl">List of Non-normalized weights</param>
        public static void BuildCumulativeDistribution(List<Double> cdl)
        {
            Int32 length = cdl.Count;

            // Use double for more precise calculation
            Double sum = 0;

            // Sum of weights
            for (Int32 i = 0; i < length; i++)
            {
                sum += cdl[i];
            }

            // k is normalization constant
            // calculate inverse of sum and convert to float
            // use multiplying, since it is faster than dividing      
            Double k = 1f / sum;

            sum = 0;

            // Make Cummulative Distribution Array
            for (Int32 i = 0; i < length; i++)
            {
                sum += cdl[i] * k; //k, the normalization constant is applied here
                cdl[i] = sum;
            }

            cdl[length - 1] = 1f; //last item of CDA is always 1, I do this because numerical inaccurarcies add up and last item probably wont be 1
        }

        /// <summary>
        /// Builds cummulative distribution out of non-normalized weights inplace.
        /// </summary>
        /// <param name="cda">Array of Non-normalized weights</param>
        public static void BuildCumulativeDistribution(Double[] cda)
        {
            Int32 length = cda.Length;

            // Use double for more precise calculation
            Double sum = 0;

            // Sum of weights
            for (Int32 i = 0; i < length; i++)
            {
                sum += cda[i];
            }

            // k is normalization constant
            // calculate inverse of sum and convert to float
            // use multiplying, since it is faster than dividing   
            Double k = 1f / sum;

            sum = 0;

            // Make Cummulative Distribution Array
            for (Int32 i = 0; i < length; i++)
            {
                sum += cda[i] * k; //k, the normalization constant is applied here
                cda[i] = sum;
            }

            cda[length - 1] = 1f; //last item of CDA is always 1, I do this because numerical inaccurarcies add up and last item probably wont be 1
        }


        /// <summary>
        /// Linear search, good/faster for small arrays
        /// </summary>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDA</returns>
        public static Int32 SelectIndexLinearSearch(this Double[] cda, Double value)
        {
            Int32 i = 0;

            // last element, CDA[CDA.Length-1] should always be 1
            while (cda[i] < value)
            {
                i++;
            }

            return i;
        }


        /// <summary>
        /// Binary search, good/faster for big array
        /// Code taken out of C# array.cs Binary Search & modified
        /// </summary>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDA</returns>
        public static Int32 SelectIndexBinarySearch(this Double[] cda, Double value)
        {
            Int32 lo = 0;
            Int32 hi = cda.Length - 1;
            Int32 index;

            while (lo <= hi)
            {
                // calculate median
                index = lo + ((hi - lo) >> 1);

                if (Math.Abs(cda[index] - value) < Double.Epsilon)
                {
                    return index;
                }

                if (cda[index] < value)
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
        /// Linear search, good/faster for small lists
        /// </summary>
        /// <param name="cdl">Cummulative Distribution List</param>
        /// <param name="value">Uniform random value</param>
        /// <returns>Returns index of an value inside CDA</returns>
        public static Int32 SelectIndexLinearSearch(this List<Double> cdl, Double value)
        {
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
        public static Int32 SelectIndexBinarySearch(this List<Double> cdl, Double value)
        {
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
        /// Returns identity, array[i] = i
        /// </summary>
        /// <param name="length">Length of an array</param>
        /// <returns>Identity array</returns>
        public static Double[] IdentityArray(Int32 length)
        {
            Double[] array = new Double[length];

            for (Int32 i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            return array;
        }

        /// <summary>
        /// Gemerates uniform random values for all indexes in array.
        /// </summary>
        /// <param name="array">The array where all values will be randomized.</param>
        /// <param name="random">Random generator</param>
        public static void RandomWeightsArray(ref Double[] array, IRandom random)
        {
            for (Int32 i = 0; i < array.Length; i++)
            {
                array[i] = random.NextDouble();

                if (Math.Abs(array[i]) < Double.Epsilon)
                {
                    i--;
                }
            }
        }

        /// <summary>
        /// Creates new array with uniform random variables. 
        /// </summary>
        /// <param name="random">Random generator</param>
        /// <param name="length">Length of new array</param>
        /// <returns>Array with random uniform random variables</returns>
        public static Double[] RandomWeightsArray(IRandom random, Int32 length)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            Double[] array = new Double[length];

            for (Int32 i = 0; i < length; i++)
            {
                array[i] = random.NextDouble();

                if (Math.Abs(array[i]) < Double.Epsilon)
                {
                    i--;
                }
            }

            return array;
        }

        /// <summary>
        /// Returns identity, list[i] = i
        /// </summary>
        /// <param name="length">Length of an list</param>
        /// <returns>Identity list</returns>
        public static List<Double> IdentityList(Int32 length)
        {
            List<Double> list = new List<Double>(length);

            for (Int32 i = 0; i < length; i++)
            {
                list.Add(i);
            }

            return list;
        }

        /// <summary>
        /// Gemerates uniform random values for all indexes in list.
        /// </summary>
        /// <param name="list">The list where all values will be randomized.</param>
        /// <param name="random">Random generator</param>
        public static void RandomWeightsList(ref List<Double> list, IRandom random)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < list.Count; i++)
            {
                list[i] = random.NextDouble();

                if (Math.Abs(list[i]) < Double.Epsilon)
                {
                    i--;
                }
            }
        }
    }
}