// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Random.Interfaces;

namespace NetExtender.Types.Random
{
    /// <summary>
    /// Generates pseudo-random numbers using the Mersenne Twister algorithm.
    /// </summary>
    public sealed class MersenneTwister : System.Random, IRandomInclude
    {
        /// <summary>
        /// Creates a new pseudo-random number generator with a default seed.
        /// </summary>
        /// <remarks>
        /// <c>new <see cref="System.Random"/>().<see cref="Random.Next()"/></c> 
        /// is used for the seed.
        /// </remarks>
        public MersenneTwister()
            : this(new System.Random().Next(-1, Int32.MaxValue) + 1)
        {
        }

        /// <summary>
        /// Creates a new pseudo-random number generator with a given seed.
        /// </summary>
        /// <param name="seed">A value to use as a seed.</param>
        public MersenneTwister(Int32 seed)
        {
            unchecked
            {
                Init((UInt32) seed);
            }
        }

        /// <summary>
        /// Creates a new pseudo-random number generator with a given seed.
        /// </summary>
        /// <param name="seed">A value to use as a seed.</param>
        public MersenneTwister(UInt32 seed)
        {
            Init(seed);
        }

        /// <summary>
        /// Creates a pseudo-random number generator initialized with the given array.
        /// </summary>
        /// <param name="key">The array for initializing keys.</param>
        public MersenneTwister(IEnumerable<UInt32> key)
        {
            Init(key?.ToArray() ?? throw new ArgumentNullException(nameof(key)));
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="UInt32"/>.
        /// </summary>
        /// <returns>A pseudo-random <see cref="UInt32"/> value.</returns>
        public UInt32 NextUInt32()
        {
            return GenerateUInt32();
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="UInt32"/> 
        /// up to <paramref name="max"/>.
        /// </summary>
        /// <param name="max">
        /// The maximum value of the pseudo-random number to create.
        /// </param>
        /// <returns>
        /// A pseudo-random <see cref="UInt32"/> value which is at most <paramref name="max"/>.
        /// </returns>
        public UInt32 NextUInt32(UInt32 max)
        {
            return (UInt32) (GenerateUInt32() / ((Double) UInt32.MaxValue / max));
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="UInt32"/> at least 
        /// <paramref name="min"/> and up to <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The minimum value of the pseudo-random number to create.</param>
        /// <param name="max">The maximum value of the pseudo-random number to create.</param>
        /// <returns>
        /// A pseudo-random <see cref="UInt32"/> value which is at least 
        /// <paramref name="min"/> and at most <paramref name="max"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <c><paramref name="min"/> &gt;= <paramref name="max"/></c>.
        /// </exception>
        public UInt32 NextUInt32(UInt32 min, UInt32 max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            return (UInt32) (GenerateUInt32() / ((Double) UInt32.MaxValue / (max - min)) + min);
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="Int32"/>.
        /// </summary>
        /// <returns>A pseudo-random <see cref="Int32"/> value.</returns>
        public override Int32 Next()
        {
            return Next(Int32.MaxValue);
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="Int32"/> up to <paramref name="max"/>.
        /// </summary>
        /// <param name="max">The maximum value of the pseudo-random number to create.</param>
        /// <returns>
        /// A pseudo-random <see cref="Int32"/> value which is at most <paramref name="max"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <paramref name="max"/> &lt; 0.
        /// </exception>
        public override Int32 Next(Int32 max)
        {
            return max switch
            {
                >= 1 => (Int32) (NextDouble() * max),
                < 0 => throw new ArgumentOutOfRangeException(nameof(max)),
                _ => 0
            };
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="Int32"/> 
        /// at least <paramref name="min"/> 
        /// and up to <paramref name="max"/>.
        /// </summary>
        /// <param name="min">The minimum value of the pseudo-random number to create.</param>
        /// <param name="max">The maximum value of the pseudo-random number to create.</param>
        /// <returns>A pseudo-random Int32 value which is at least <paramref name="min"/> and at 
        /// most <paramref name="max"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <c><paramref name="min"/> &gt;= <paramref name="max"/></c>.
        /// </exception>
        public override Int32 Next(Int32 min, Int32 max)
        {
            if (max < min)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            if (max == min)
            {
                return min;
            }

            Double value = NextDoubleWithOne();
            return (Int32) (value * max - value * min) + min;
        }

        /// <summary>
        /// Fills a buffer with pseudo-random bytes.
        /// </summary>
        /// <param name="buffer">The buffer to fill.</param>
        /// <exception cref="ArgumentNullException">
        /// If <c><paramref name="buffer"/> == <see langword="null"/></c>.
        /// </exception>
        public override void NextBytes(Byte[] buffer)
        {
            // [codekaizen: corrected this to check null before checking length.]
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            for (Int32 idx = 0; idx < buffer.Length; ++idx)
            {
                buffer[idx] = (Byte) Next(256);
            }
        }

        /// <summary>
        /// Returns the next pseudo-random <see cref="Double"/> value.
        /// </summary>
        /// <returns>A pseudo-random double floating point value.</returns>
        /// <remarks>
        /// <para>
        /// There are two common ways to create a double floating point using MT19937: 
        /// using <see cref="GenerateUInt32"/> and dividing by 0xFFFFFFFF + 1, 
        /// or else generating two double words and shifting the first by 26 bits and 
        /// adding the second.
        /// </para>
        /// <para>
        /// In a newer measurement of the randomness of MT19937 published in the 
        /// journal "Monte Carlo Methods and Applications, Vol. 12, No. 5-6, pp. 385 � 393 (2006)"
        /// entitled "A Repetition Test for Pseudo-Random Number Generators",
        /// it was found that the 32-bit version of generating a double fails at the 95% 
        /// confidence level when measuring for expected repetitions of a particular 
        /// number in a sequence of numbers generated by the algorithm.
        /// </para>
        /// <para>
        /// Due to this, the 53-bit method is implemented here and the 32-bit method
        /// of generating a double is not. If, for some reason,
        /// the 32-bit method is needed, it can be generated by the following:
        /// <code>
        /// (Double)NextUInt32() / ((UInt64)UInt32.MaxValue + 1);
        /// </code>
        /// </para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Double NextDouble()
        {
            return NextDoubleWithoutOne();
        }

        /// <summary>
        /// Returns a pseudo-random number greater than or equal to zero, and 
        /// either strictly less than one, or less than or equal to one, 
        /// depending on the value of the given parameter.
        /// </summary>
        /// <param name="include">
        /// If <see langword="true"/>, the pseudo-random number returned will be 
        /// less than or equal to one; otherwise, the pseudo-random number returned will
        /// be strictly less than one.
        /// </param>
        /// <returns>
        /// If <paramref name="include"/> is <see langword="true"/>, 
        /// this method returns a double-precision pseudo-random number greater than 
        /// or equal to zero, and less than or equal to one. 
        /// If <paramref name="include"/> is <see langword="false"/>, this method
        /// returns a double-precision pseudo-random number greater than or equal to zero and
        /// strictly less than one.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double NextDouble(Boolean include)
        {
            return include ? NextDoubleWithOne() : NextDoubleWithoutOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double NextDoubleWithOne()
        {
            return Compute53BitRandom(0, Inverse53BitsOf1S);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double NextDoubleWithoutOne()
        {
            return Compute53BitRandom(0, InverseOnePlus53BitsOf1S);
        }

        /// <summary>
        /// Returns a pseudo-random number greater than 0.0 and less than 1.0.
        /// </summary>
        /// <returns>A pseudo-random number greater than 0.0 and less than 1.0.</returns>
        public Double NextDoublePositive()
        {
            return Compute53BitRandom(0.5, Inverse53BitsOf1S);
        }

        /// <summary>
        /// Generates a new pseudo-random <see cref="UInt32"/>.
        /// </summary>
        /// <returns>A pseudo-random <see cref="UInt32"/>.</returns>
        private UInt32 GenerateUInt32()
        {
            unchecked
            {
                UInt32 y;

                // _mag01[x] = x * MatrixA  for x=0,1
                if (Mti >= N) // generate N words at one time
                {
                    Int16 kk = 0;

                    for (; kk < N - M; ++kk)
                    {
                        y = (Mt[kk] & UpperMask) | (Mt[kk + 1] & LowerMask);
                        Mt[kk] = Mt[kk + M] ^ (y >> 1) ^ Mag01[y & 0x1];
                    }

                    for (; kk < N - 1; ++kk)
                    {
                        y = (Mt[kk] & UpperMask) | (Mt[kk + 1] & LowerMask);
                        Mt[kk] = Mt[kk + (M - N)] ^ (y >> 1) ^ Mag01[y & 0x1];
                    }

                    y = (Mt[N - 1] & UpperMask) | (Mt[0] & LowerMask);
                    Mt[N - 1] = Mt[M - 1] ^ (y >> 1) ^ Mag01[y & 0x1];

                    Mti = 0;
                }

                y = Mt[Mti++];
                y ^= TemperingShiftU(y);
                y ^= TemperingShiftS(y) & TemperingMaskB;
                y ^= TemperingShiftT(y) & TemperingMaskC;
                y ^= TemperingShiftL(y);

                return y;
            }
        }

        // Period parameters
        private const Int32 N = 624;
        private const Int32 M = 397;
        private const UInt32 MatrixA = 0x9908B0DF; // Constant vector a
        private const UInt32 UpperMask = 0x80000000; // Most significant w-r bits
        private const UInt32 LowerMask = 0x7FFFFFFF; // Least significant r bits

        // Tempering parameters
        private const UInt32 TemperingMaskB = 0x9D2C5680;
        private const UInt32 TemperingMaskC = 0xEFC60000;

        private static UInt32 TemperingShiftU(UInt32 y)
        {
            return y >> 11;
        }

        private static UInt32 TemperingShiftS(UInt32 y)
        {
            return y << 7;
        }

        private static UInt32 TemperingShiftT(UInt32 y)
        {
            return y << 15;
        }

        private static UInt32 TemperingShiftL(UInt32 y)
        {
            return y >> 18;
        }

        private UInt32[] Mt { get; } = new UInt32[N];
        private Int16 Mti { get; set; }

        private static UInt32[] Mag01 { get; } = { 0x0, MatrixA };

        private void Init(UInt32 seed)
        {
            unchecked
            {
                Mt[0] = seed & 0xFFFFFFFFU;

                for (Mti = 1; Mti < N; Mti++)
                {
                    Mt[Mti] = (UInt32) (1812433253U * (Mt[Mti - 1] ^ (Mt[Mti - 1] >> 30)) + Mti);
                    Mt[Mti] &= 0xFFFFFFFFU;
                    // For >32 bit machines
                }
            }
        }

        private void Init(IReadOnlyList<UInt32> key)
        {
            unchecked
            {
                Init(19650218U);

                Int32 length = key.Count;
                Int32 i = 1;
                Int32 j = 0;
                Int32 k = N > length ? N : length;

                for (; k > 0; k--)
                {
                    Mt[i] = (UInt32) ((Mt[i] ^ ((Mt[i - 1] ^ (Mt[i - 1] >> 30)) * 1664525U)) + key[j] + j);
                    Mt[i] &= 0xFFFFFFFFU; // For WORDSIZE > 32 machines
                    i++;
                    j++;
                    if (i >= N)
                    {
                        Mt[0] = Mt[N - 1];
                        i = 1;
                    }

                    if (j >= length)
                    {
                        j = 0;
                    }
                }

                for (k = N - 1; k > 0; k--)
                {
                    Mt[i] = (UInt32) ((Mt[i] ^ ((Mt[i - 1] ^ (Mt[i - 1] >> 30)) * 1566083941U)) - i);
                    Mt[i] &= 0xFFFFFFFFU; // For WORDSIZE > 32 machines
                    i++;

                    if (i < N)
                    {
                        continue;
                    }

                    Mt[0] = Mt[N - 1];
                    i = 1;
                }

                Mt[0] = 0x80000000U; // MSB is 1; assuring non-zero initial array
            }
        }

        // 9007199254740991.0 is the maximum double value which the 53 significand
        // can hold when the exponent is 0.
        private const Double FiftyThreeBitsOf1S = 9007199254740991.0;

        // Multiply by inverse to (vainly?) try to avoid a division.
        private const Double Inverse53BitsOf1S = 1.0 / FiftyThreeBitsOf1S;
        private const Double OnePlus53BitsOf1S = FiftyThreeBitsOf1S + 1;
        private const Double InverseOnePlus53BitsOf1S = 1.0 / OnePlus53BitsOf1S;

        private Double Compute53BitRandom(Double translate, Double scale)
        {
            // get 27 pseudo-random bits
            UInt64 a = (UInt64) GenerateUInt32() >> 5;
            // get 26 pseudo-random bits
            UInt64 b = (UInt64) GenerateUInt32() >> 6;

            // shift the 27 pseudo-random bits (a) over by 26 bits (* 67108864.0) and
            // add another pseudo-random 26 bits (+ b).
            return (a * 67108864.0 + b + translate) * scale;
        }

        void IDisposable.Dispose()
        {
        }
    }
}