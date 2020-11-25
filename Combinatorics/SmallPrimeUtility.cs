// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Combinatorics
{
    /// <summary>
    /// Utility class that maintains a small table of prime numbers and provides
    /// simple implementations of Prime Factorization algorithms.  
    /// This is a quick and dirty utility class to support calculations of permutation
    /// sets with indexes under 2^31.
    /// The prime table contains all primes up to Sqrt(2^31) which are all of the primes
    /// requires to factorize any Int32 positive integer.
    /// </summary>
    public class SmallPrimeUtility
    {
        /// <summary>
        /// Utility class, no instances allowed.
        /// </summary>
        private SmallPrimeUtility()
        {
        }

        /// <summary>
        /// Performs a prime factorization of a given integer using the table of primes in PrimeTable.
        /// Since this will only factor Int32 sized integers, a simple list of factors is returned instead
        /// of the more scalable, but more difficult to consume, list of primes and associated exponents.
        /// </summary>
        /// <param name="i">The number to factorize, must be positive.</param>
        /// <returns>A simple list of factors.</returns>
        public static IList<Int32> Factor(Int32 i)
        {
            Int32 primeIndex = 0;
            Int32 prime = PrimeTable[primeIndex];
            List<Int32> factors = new List<Int32>();
            while (i > 1)
            {
                if (i % prime == 0)
                {
                    factors.Add(prime);
                    i /= prime;
                }
                else
                {
                    ++primeIndex;
                    prime = PrimeTable[primeIndex];
                }
            }

            return factors;
        }

        /// <summary>
        /// Given two integers expressed as a list of prime factors, multiplies these numbers
        /// together and returns an integer also expressed as a set of prime factors.
        /// This allows multiplication to overflow well beyond a Int64 if necessary.  
        /// </summary>
        /// <param name="lhs">Left Hand Side argument, expressed as list of prime factors.</param>
        /// <param name="rhs">Right Hand Side argument, expressed as list of prime factors.</param>
        /// <returns>Product, expressed as list of prime factors.</returns>
        public static List<Int32> MultiplyPrimeFactors(IEnumerable<Int32> lhs, IEnumerable<Int32> rhs)
        {
            List<Int32> product = lhs.ToList();
            product.AddRange(rhs);

            product.Sort();
            return product;
        }

        /// <summary>
        /// Given two integers expressed as a list of prime factors, divides these numbers
        /// and returns an integer also expressed as a set of prime factors.
        /// If the result is not a integer, then the result is undefined.  That is, 11 / 5
        /// when divided by this function will not yield a correct result.
        /// As such, this function is ONLY useful for division with combinatorial results where 
        /// the result is known to be an integer AND the division occurs as the last operation(s).
        /// </summary>
        /// <param name="numerator">Numerator argument, expressed as list of prime factors.</param>
        /// <param name="denominator">Denominator argument, expressed as list of prime factors.</param>
        /// <returns>Resultant, expressed as list of prime factors.</returns>
        public static List<Int32> DividePrimeFactors(IEnumerable<Int32> numerator, IEnumerable<Int32> denominator)
        {
            List<Int32> product = numerator.ToList();

            foreach (Int32 prime in denominator)
            {
                product.Remove(prime);
            }

            return product;
        }

        /// <summary>
        /// Given a list of prime factors returns the long representation.
        /// </summary>
        /// <param name="value">Integer, expressed as list of prime factors.</param>
        /// <returns>Standard long representation.</returns>
        public static Int64 EvaluatePrimeFactors(IEnumerable<Int32> value)
        {
            return value.Aggregate<Int32, Int64>(1, (current, prime) => current * prime);
        }

        /// <summary>
        /// Static initializer, set up prime table.
        /// </summary>
        static SmallPrimeUtility()
        {
            CalculatePrimes();
        }

        /// <summary>
        /// Calculate all primes up to Sqrt(2^32) = 2^16.  
        /// This table will be large enough for all factorizations for Int32's.
        /// Small tables are best built using the Sieve Of Eratosthenes,
        /// Reference: http://primes.utm.edu/glossary/page.php?sort=SieveOfEratosthenes
        /// </summary>
        private static void CalculatePrimes()
        {
            // Build Sieve Of Eratosthenes
            BitArray sieve = new BitArray(65536, true);
            for (Int32 possiblePrime = 2; possiblePrime <= 256; ++possiblePrime)
            {
                if (sieve[possiblePrime] != true)
                {
                    continue;
                }

                // It is prime, so remove all future factors...
                for (Int32 nonPrime = 2 * possiblePrime; nonPrime < 65536; nonPrime += possiblePrime)
                {
                    sieve[nonPrime] = false;
                }
            }

            // Scan sieve for primes...
            primes = new List<Int32>();
            for (Int32 i = 2; i < 65536; ++i)
            {
                if (sieve[i])
                {
                    primes.Add(i);
                }
            }
        }

        /// <summary>
        /// A List of all primes from 2 to 2^16.
        /// </summary>
        public static IList<Int32> PrimeTable
        {
            get
            {
                return primes;
            }
        }

        private static List<Int32> primes = new List<Int32>();
    }
}