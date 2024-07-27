// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Random;
using NetExtender.Types.Random.Interfaces;

namespace NetExtender.Utilities.Numerics
{
    public enum RandomType
    {
        Default,
        MersenneTwister
    }

    /// <inheritdoc cref="System.Random"/>
    public static partial class RandomUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class GeneratorStorage
        {
            [ThreadStatic]
            private static IRandom? generator;
            public static IRandom Generator
            {
                get
                {
                    return generator ??= new MersenneTwister();
                }
                set
                {
                    generator = value;
                }
            }
        }

        public static IRandom Generator
        {
            get
            {
                return GeneratorStorage.Generator;
            }
            private set
            {
                GeneratorStorage.Generator = value;
            }
        }

        public static IRandom Create()
        {
            return Create(RandomType.Default);
        }

        public static IRandom Create(Int32 seed)
        {
            return Create(RandomType.Default, seed);
        }

        public static IRandom Create(this RandomType type)
        {
            return type switch
            {
                RandomType.Default => new RandomAdapter(),
                RandomType.MersenneTwister => new MersenneTwister(),
                _ => throw new EnumUndefinedOrNotSupportedException<RandomType>(type, nameof(type), null)
            };
        }

        public static IRandom Create(this RandomType type, Int32 seed)
        {
            return type switch
            {
                RandomType.Default => new RandomAdapter(seed),
                RandomType.MersenneTwister => new MersenneTwister(seed),
                _ => throw new EnumUndefinedOrNotSupportedException<RandomType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> Fill(this Random random, Span<Byte> value)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            random.NextBytes(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Fill(this Random random, Byte[] value)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            random.NextBytes(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> Fill<T>(this T random, Span<Byte> value) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            random.NextBytes(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Fill<T>(this T random, Byte[] value) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            
            random.NextBytes(value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next()
        {
            return NextInt32();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this Random random)
        {
            return NextInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next<T>(this T random) where T : IRandom
        {
            return NextInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(Int32 max)
        {
            return NextInt32(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this Random random, Int32 max)
        {
            return NextInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next<T>(this T random, Int32 max) where T : IRandom
        {
            return NextInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(Int32 min, Int32 max)
        {
            return NextInt32(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this Random random, Int32 min, Int32 max)
        {
            return NextInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next<T>(this T random, Int32 min, Int32 max) where T : IRandom
        {
            return NextInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative()
        {
            return NextNonNegativeInt32();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative(this Random random)
        {
            return NextNonNegativeInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative<T>(this T random) where T : IRandom
        {
            return NextNonNegativeInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative(Int32 max)
        {
            return NextNonNegativeInt32(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative(this Random random, Int32 max)
        {
            return NextNonNegativeInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative<T>(this T random, Int32 max) where T : IRandom
        {
            return NextNonNegativeInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative(Int32 min, Int32 max)
        {
            return NextNonNegativeInt32(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative(this Random random, Int32 min, Int32 max)
        {
            return NextNonNegativeInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonNegative<T>(this T random, Int32 min, Int32 max) where T : IRandom
        {
            return NextNonNegativeInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero()
        {
            return NextNonZeroInt32();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero(this Random random)
        {
            return NextNonZeroInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero<T>(this T random) where T : IRandom
        {
            return NextNonZeroInt32(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero(Int32 max)
        {
            return NextNonZeroInt32(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero(this Random random, Int32 max)
        {
            return NextNonZeroInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero<T>(this T random, Int32 max) where T : IRandom
        {
            return NextNonZeroInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero(Int32 min, Int32 max)
        {
            return NextNonZeroInt32(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero(this Random random, Int32 min, Int32 max)
        {
            return NextNonZeroInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextNonZero<T>(this T random, Int32 min, Int32 max) where T : IRandom
        {
            return NextNonZeroInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle()
        {
            return NextSingle(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this Random random)
        {
            return NextSingleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle<T>(this T random) where T : IRandom
        {
            return NextSingleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Boolean include)
        {
            return NextSingle(Generator, include);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this Random random, Boolean include)
        {
            return include ? NextSingleWithOne(random) : NextSingleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle<T>(this T random, Boolean include) where T : IRandom
        {
            return include ? NextSingleWithOne(random) : NextSingleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne()
        {
            return NextSingleWithOne(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne(this Random random)
        {
            return (Single) NextDoubleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne<T>(this T random) where T : IRandom
        {
            return (Single) NextDoubleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne()
        {
            return NextSingleWithoutOne(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne(this Random random)
        {
            return (Single) NextDoubleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne<T>(this T random) where T : IRandom
        {
            return (Single) NextDoubleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Single max)
        {
            return NextSingle(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this Random random, Single max)
        {
            return NextSingle(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle<T>(this T random, Single max) where T : IRandom
        {
            return NextSingle(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Single min, Single max)
        {
            return NextSingle(Generator, min, max);
        }

        public static Single NextSingle(this Random random, Single min, Single max)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (Math.Abs(max - min) < Single.Epsilon)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Single value = NextSingle(random);
            return value * max - value * min + min;
        }

        public static Single NextSingle<T>(this T random, Single min, Single max) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (Math.Abs(max - min) < Single.Epsilon)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Single value = NextSingle(random);
            return value * max - value * min + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble()
        {
            return NextDouble(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this Random random)
        {
            return NextDoubleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble<T>(this T random) where T : IRandom
        {
            return NextDoubleWithOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(Boolean include)
        {
            return NextDouble(Generator, include);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this Random random, Boolean include)
        {
            return include ? NextDoubleWithOne(random) : NextDoubleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble<T>(this T random, Boolean include) where T : IRandom
        {
            return include ? NextDoubleWithOne(random) : NextDoubleWithoutOne(random);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne()
        {
            return NextDoubleWithOne(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne(this Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithOne();
                }

                return random.NextDouble().Round(15);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne<T>(this T random) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithOne();
                }

                return random.NextDouble().Round(15);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithoutOne()
        {
            return NextDoubleWithoutOne(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithoutOne(this Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithoutOne();
                }

                return random.NextDouble();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithoutOne<T>(this T random) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithoutOne();
                }

                return random.NextDouble();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(Double max)
        {
            return NextDouble(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this Random random, Double max)
        {
            return NextDouble(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble<T>(this T random, Double max) where T : IRandom
        {
            return NextDouble(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(Double min, Double max)
        {
            return NextDouble(Generator, min, max);
        }

        public static Double NextDouble(this Random random, Double min, Double max)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (Math.Abs(max - min) < Double.Epsilon)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Double value = NextDouble(random);
            return value * max - value * min + min;
        }

        public static Double NextDouble<T>(this T random, Double min, Double max) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (Math.Abs(max - min) < Double.Epsilon)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Double value = NextDouble(random);
            return value * max - value * min + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal()
        {
            return NextDecimal(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(this Random random)
        {
            return NextDecimal(random, Decimal.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal<T>(this T random) where T : IRandom
        {
            return NextDecimal(random, Decimal.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(Decimal max)
        {
            return NextDecimal(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(this Random random, Decimal max)
        {
            return new Decimal(NextDouble(random)) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal<T>(this T random, Decimal max) where T : IRandom
        {
            return new Decimal(NextDouble(random)) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(Decimal min, Decimal max)
        {
            return NextDecimal(Generator, min, max);
        }

        public static Decimal NextDecimal(this Random random, Decimal min, Decimal max)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max - min == 0)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Decimal value = new Decimal(NextDouble(random));
            return value * max - value * min + min;
        }

        public static Decimal NextDecimal<T>(this T random, Decimal min, Decimal max) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max - min == 0)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            Decimal value = new Decimal(NextDouble(random));
            return value * max - value * min + min;
        }

        /// <inheritdoc cref="NextBoolean(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean()
        {
            return NextBoolean(Generator);
        }

        /// <inheritdoc cref="NextBoolean(System.Random,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(this Random random)
        {
            return NextBoolean(random, 0.5D);
        }

        /// <inheritdoc cref="NextBoolean{T}(T,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean<T>(this T random) where T : IRandom
        {
            return NextBoolean(random, 0.5D);
        }

        /// <inheritdoc cref="NextBoolean{T}(T,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(Double chance)
        {
            return NextBoolean(Generator, chance);
        }

        /// <inheritdoc cref="NextBoolean{T}(T,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(this Random random, Double chance)
        {
            return chance switch
            {
                >= 1 => true,
                <= 0 => false,
                _ => NextDouble(random) <= chance
            };
        }

        /// <summary>
        /// Randomize boolean
        /// </summary>
        /// <param name="random">Random implementation</param>
        /// <param name="chance">Abs chance to true. Between [0;1]</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean<T>(this T random, Double chance) where T : IRandom
        {
            return chance switch
            {
                >= 1 => true,
                <= 0 => false,
                _ => NextDouble(random) <= chance
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex()
        {
            return NextComplex(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this Random random)
        {
            return new Complex(NextDouble(random), NextDouble(random));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex<T>(this T random) where T : IRandom
        {
            return new Complex(NextDouble(random), NextDouble(random));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double max)
        {
            return NextComplex(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this Random random, Double max)
        {
            return new Complex(NextDouble(random, max), NextDouble(random, max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex<T>(this T random, Double max) where T : IRandom
        {
            return new Complex(NextDouble(random, max), NextDouble(random, max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double min, Double max)
        {
            return NextComplex(Generator, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this Random random, Double min, Double max)
        {
            return NextComplex(random, min, max, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex<T>(this T random, Double min, Double max) where T : IRandom
        {
            return NextComplex(random, min, max, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double min, Double max, Double imin, Double imax)
        {
            return NextComplex(Generator, min, max, imin, imax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this Random random, Double min, Double max, Double imin, Double imax)
        {
            return new Complex(NextDouble(random, min, max), NextDouble(random, imin, imax));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex<T>(this T random, Double min, Double max, Double imin, Double imax) where T : IRandom
        {
            return new Complex(NextDouble(random, min, max), NextDouble(random, imin, imax));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan()
        {
            return NextTimeSpan(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this Random random)
        {
            return NextTimeSpan(random, TimeSpan.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan<T>(this T random) where T : IRandom
        {
            return NextTimeSpan(random, TimeSpan.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(TimeSpan max)
        {
            return NextTimeSpan(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this Random random, TimeSpan max)
        {
            return NextTimeSpan(random, TimeSpan.Zero, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan<T>(this T random, TimeSpan max) where T : IRandom
        {
            return NextTimeSpan(random, TimeSpan.Zero, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(TimeSpan min, TimeSpan max)
        {
            return NextTimeSpan(Generator, min, max);
        }

        public static TimeSpan NextTimeSpan(this Random random, TimeSpan min, TimeSpan max)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            return min.Add(TimeSpan.FromTicks(NextInt64(random, 0, max.Ticks - min.Ticks)));
        }

        public static TimeSpan NextTimeSpan<T>(this T random, TimeSpan min, TimeSpan max) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            return min.Add(TimeSpan.FromTicks(NextInt64(random, 0, max.Ticks - min.Ticks)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime()
        {
            return NextDateTime(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime<T>(this T random) where T : IRandom
        {
            return NextDateTime(random, DateTime.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(DateTime max)
        {
            return NextDateTime(Generator, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this Random random, DateTime max)
        {
            return NextDateTime(random, DateTime.MinValue, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime<T>(this T random, DateTime max) where T : IRandom
        {
            return NextDateTime(random, DateTime.MinValue, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(DateTime min, DateTime max)
        {
            return NextDateTime(Generator, min, max);
        }

        public static DateTime NextDateTime(this Random random, DateTime min, DateTime max)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            TimeSpan span = NextTimeSpan(random, max - min);
            return min.Add(span);
        }

        public static DateTime NextDateTime<T>(this T random, DateTime min, DateTime max) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            TimeSpan span = NextTimeSpan(random, max - min);
            return min.Add(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance()
        {
            return RangeChance(Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(Int32 count)
        {
            return RangeChance(Generator, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(Double chance)
        {
            return RangeChance(Generator, chance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(Double chance, Int32 count)
        {
            return RangeChance(Generator, chance, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(this Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            while (true)
            {
                yield return random.NextBoolean();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(this Random random, Int32 count)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < count; i++)
            {
                yield return random.NextBoolean();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(this Random random, Double chance)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            while (true)
            {
                yield return random.NextBoolean(chance);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance(this Random random, Double chance, Int32 count)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < count; i++)
            {
                yield return random.NextBoolean(chance);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance<T>(this T random) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            while (true)
            {
                yield return random.NextBoolean();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance<T>(this T random, Int32 count) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < count; i++)
            {
                yield return random.NextBoolean();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance<T>(this T random, Double chance) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            while (true)
            {
                yield return random.NextBoolean(chance);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Boolean> RangeChance<T>(this T random, Double chance, Int32 count) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < count; i++)
            {
                yield return random.NextBoolean(chance);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(Int32 count)
        {
            return Summary(UInt32.MaxValue, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(Int32 max, Int32 count)
        {
            return Summary(default(Int32), max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(Int32 min, Int32 max, Int32 count)
        {
            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            return Summary((UInt32) min, (UInt32) max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(UInt32 max, Int32 count)
        {
            return Summary(default(UInt32), max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(UInt32 min, UInt32 max, Int32 count)
        {
            return Summary(Generator, min, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(this Random random, Int32 count)
        {
            return Summary(random, UInt32.MaxValue, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(this Random random, Int32 max, Int32 count)
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(this Random random, Int32 min, Int32 max, Int32 count)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            return Summary(random, (UInt32) min, (UInt32) max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary(this Random random, UInt32 max, Int32 count)
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double[] Summary(this Random random, UInt32 min, UInt32 max, Int32 count)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (count <= 0)
            {
                return Array.Empty<Double>();
            }

            Double[] values = new Double[count];

            UInt64 sum = 0;
            for (Int32 i = 0; i < values.Length; i++)
            {
                UInt32 value = random.NextUInt32(min, max);
                sum += value;
                values[i] = value;
            }

            if (sum != 0)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    values[i] /= sum;
                }
            }

            Array.Sort(values, (first, second) => second.CompareTo(first));
            return values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary<T>(this T random, Int32 count) where T : IRandom
        {
            return Summary(random, UInt32.MaxValue, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary<T>(this T random, Int32 max, Int32 count) where T : IRandom
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary<T>(this T random, Int32 min, Int32 max, Int32 count) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            return Summary(random, (UInt32) min, (UInt32) max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Summary<T>(this T random, UInt32 max, Int32 count) where T : IRandom
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Double[] Summary<T>(this T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (count <= 0)
            {
                return Array.Empty<Double>();
            }

            Double[] values = new Double[count];

            UInt64 sum = 0;
            for (Int32 i = 0; i < values.Length; i++)
            {
                UInt32 value = random.NextUInt32(min, max);
                sum += value;
                values[i] = value;
            }

            if (sum != 0)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    values[i] /= sum;
                }
            }

            Array.Sort(values, (first, second) => second.CompareTo(first));
            return values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal[] Summary(this Random random, Decimal max, Int32 count)
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal[] Summary(this Random random, Decimal min, Decimal max, Int32 count)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            if (count <= 0)
            {
                return Array.Empty<Decimal>();
            }

            Decimal[] values = new Decimal[count];

            Decimal sum = 0;
            for (Int32 i = 0; i < values.Length; i++)
            {
                Decimal value = random.NextDecimal(min, max);
                sum += value;
                values[i] = value;
            }

            if (sum != 0)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    values[i] /= sum;
                }
            }

            Array.Sort(values, (first, second) => second.CompareTo(first));
            return values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal[] Summary<T>(this T random, Decimal max, Int32 count) where T : IRandom
        {
            return Summary(random, default, max, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Decimal[] Summary<T>(this T random, Decimal min, Decimal max, Int32 count) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, null);
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), max, null);
            }

            if (count <= 0)
            {
                return Array.Empty<Decimal>();
            }

            Decimal[] values = new Decimal[count];

            Decimal sum = 0;
            for (Int32 i = 0; i < values.Length; i++)
            {
                Decimal value = random.NextDecimal(min, max);
                sum += value;
                values[i] = value;
            }

            if (sum != 0)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    values[i] /= sum;
                }
            }

            Array.Sort(values, (first, second) => second.CompareTo(first));
            return values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Action(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (NextBoolean())
            {
                action.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Action(Action action, Double chance)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (NextBoolean(chance))
            {
                action.Invoke();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T first, ref T second)
        {
            if (NextBoolean())
            {
                (first, second) = (second, first);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T first, ref T second, Double chance)
        {
            if (NextBoolean(chance))
            {
                (first, second) = (second, first);
            }
        }

        public static void Reset()
        {
            Generator = new MersenneTwister();
        }

        public static void Reset(Int32 seed)
        {
            Generator = new MersenneTwister(seed);
        }

        public static void Reset(RandomType type)
        {
            Generator = type.Create();
        }

        public static void Reset(RandomType type, Int32 seed)
        {
            Generator = type.Create(seed);
        }

        public static void Reset(Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            Generator = random as IRandom ?? new RandomAdapter(random);
        }

        public static void Reset<T>(T random) where T : IRandom
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            Generator = random;
        }
    }
}