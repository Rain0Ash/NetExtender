// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace NetExtender.Utils.Numerics
{
    public static class PrimeUtils
    {
        public const Int32 LargestPrime = Int32.MaxValue;
        
        private static ImmutableArray<Int32> Primes { get; }

        static PrimeUtils()
        {
            Primes = CalculatePrimes().ToImmutableArray();
        }

        public static Int32 NextPrime(Int32 desired)
        {
            Int32 i = Primes.BinarySearch(desired);

            if (i < 0)
            {
                unchecked
                {
                    i = -i - 1;
                }
            }

            return Primes[i];
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
            Int32 index = 0;
            Int32 prime = Primes[index];
            
            IList<Int32> factors = new List<Int32>();
            
            while (i > 1)
            {
                if (i % prime == 0)
                {
                    factors.Add(prime);
                    i /= prime;
                    continue;
                }

                prime = Primes[++index];
            }

            return factors;
        }
        
        /// <summary>
        /// Given two integers expressed as a list of prime factors, multiplies these numbers
        /// together and returns an integer also expressed as a set of prime factors.
        /// This allows multiplication to overflow well beyond a Int64 if necessary.  
        /// </summary>
        /// <param name="left">Left Hand Side argument, expressed as list of prime factors.</param>
        /// <param name="right">Right Hand Side argument, expressed as list of prime factors.</param>
        /// <returns>Product, expressed as list of prime factors.</returns>
        public static IList<Int32> MultiplyPrimeFactors(IEnumerable<Int32> left, IEnumerable<Int32> right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            List<Int32> product = left.ToList();
            product.AddRange(right);
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
        public static IList<Int32> DividePrimeFactors(IEnumerable<Int32> numerator, IEnumerable<Int32> denominator)
        {
            if (numerator is null)
            {
                throw new ArgumentNullException(nameof(numerator));
            }

            if (denominator is null)
            {
                throw new ArgumentNullException(nameof(denominator));
            }

            IList<Int32> product = numerator.ToList();

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
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Aggregate<Int32, Int64>(1, (current, prime) => current * prime);
        }
        
        /// <summary>
        /// Calculate all primes up to Sqrt(2^32) = 2^16.  
        /// This table will be large enough for all factorizations for Int32's.
        /// Small tables are best built using the Sieve Of Eratosthenes,
        /// Reference: http://primes.utm.edu/glossary/page.php?sort=SieveOfEratosthenes
        /// </summary>
        private static IEnumerable<Int32> CalculatePrimes()
        {
            unchecked
            {
                // Build Sieve Of Eratosthenes
                BitArray sieve = new BitArray(65536, true);
                for (Int32 possible = 2; possible <= 256; ++possible)
                {
                    if (!sieve[possible])
                    {
                        continue;
                    }

                    // It is prime, so remove all future factors...
                    for (Int32 nonprime = 2 * possible; nonprime < 65536; nonprime += possible)
                    {
                        sieve[nonprime] = false;
                    }
                }

                // Scan sieve for primes...
                for (Int32 i = 2; i < 65536; ++i)
                {
                    if (sieve[i])
                    {
                        yield return i;
                    }
                }
            }
        }
    }
}