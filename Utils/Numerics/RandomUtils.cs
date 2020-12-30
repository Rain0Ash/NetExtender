// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Random;
using NetExtender.Random.Interfaces;

namespace NetExtender.Utils.Numerics
{
    public enum RandomType
    {
        Default,
        MersenneTwister
    }
    
    /// <inheritdoc cref="System.Random"/>
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public static partial class RandomUtils
    {
        public static IRandom Create(this RandomType type)
        {
            return type switch
            {
                RandomType.Default => new RandomAdapter(),
                RandomType.MersenneTwister => new MersenneTwister(),
                _ => throw new NotSupportedException()
            };
        }
        
        public static IRandom Create(this RandomType type, Int32 seed)
        {
            return type switch
            {
                RandomType.Default => new RandomAdapter(seed),
                RandomType.MersenneTwister => new MersenneTwister(seed),
                _ => throw new NotSupportedException()
            };
        }
        
        private static IRandom Generator { get; set; } = new MersenneTwister(DateTime.UtcNow.Millisecond);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next()
        {
            return NextInt32();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this System.Random random)
        {
            return NextInt32(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this IRandom random)
        {
            return NextInt32(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(Int32 max)
        {
            return NextInt32(max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this System.Random random, Int32 max)
        {
            return NextInt32(random, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this IRandom random, Int32 max)
        {
            return NextInt32(random, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(Int32 min, Int32 max)
        {
            return NextInt32(min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this System.Random random, Int32 min, Int32 max)
        {
            return NextInt32(random, min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Next(this IRandom random, Int32 min, Int32 max)
        {
            return NextInt32(random, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle()
        {
            return NextSingle(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this System.Random random)
        {
            return NextSingleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this IRandom random)
        {
            return NextSingleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Boolean include)
        {
            return NextSingle(Generator, include);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this System.Random random, Boolean include)
        {
            return include ? NextSingleWithOne(random) : NextSingleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this IRandom random, Boolean include)
        {
            return include ? NextSingleWithOne(random) : NextSingleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne()
        {
            return NextSingleWithOne(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne(this System.Random random)
        {
            return (Single) NextDoubleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithOne(this IRandom random)
        {
            return (Single) NextDoubleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne()
        {
            return NextSingleWithoutOne(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne(this System.Random random)
        {
            return (Single) NextDoubleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingleWithoutOne(this IRandom random)
        {
            return (Single) NextDoubleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Single max)
        {
            return NextSingle(Generator, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this System.Random random, Single max)
        {
            return NextSingle(random) * max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(this IRandom random, Single max)
        {
            return NextSingle(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single NextSingle(Single min, Single max)
        {
            return NextSingle(Generator, min, max);
        }
        
        public static Single NextSingle(this System.Random random, Single min, Single max)
        {
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
        
        public static Single NextSingle(this IRandom random, Single min, Single max)
        {
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
        public static Double NextDouble(this System.Random random)
        {
            return NextDoubleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this IRandom random)
        {
            return NextDoubleWithOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(Boolean include)
        {
            return NextDouble(Generator, include);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this System.Random random, Boolean include)
        {
            return include ? NextDoubleWithOne(random) : NextDoubleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this IRandom random, Boolean include)
        {
            return include ? NextDoubleWithOne(random) : NextDoubleWithoutOne(random);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne()
        {
            return NextDoubleWithOne(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne(this System.Random random)
        {
            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithOne();
                }

                return MathUtils.RoundBanking(random.NextDouble(), 16);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithOne(this IRandom random)
        {
            lock (random)
            {
                if (random is IRandomInclude include)
                {
                    return include.NextDoubleWithOne();
                }

                return MathUtils.RoundBanking(random.NextDouble(), 16);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithoutOne()
        {
            return NextDoubleWithoutOne(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDoubleWithoutOne(this System.Random random)
        {
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
        public static Double NextDoubleWithoutOne(this IRandom random)
        {
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
        public static Double NextDouble(this System.Random random, Double max)
        {
            return NextDouble(random) * max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(this IRandom random, Double max)
        {
            return NextDouble(random) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble(Double min, Double max)
        {
            return NextDouble(Generator, min, max);
        }

        public static Double NextDouble(this System.Random random, Double min, Double max)
        {
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
        
        public static Double NextDouble(this IRandom random, Double min, Double max)
        {
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
        public static Decimal NextDecimal(this System.Random random)
        {
            return NextDecimal(random, Decimal.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(this IRandom random)
        {
            return NextDecimal(random, Decimal.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(Decimal max)
        {
            return NextDecimal(Generator, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(this System.Random random, Decimal max)
        {
            return new Decimal(NextDouble(random)) * max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(this IRandom random, Decimal max)
        {
            return new Decimal(NextDouble(random)) * max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal NextDecimal(Decimal min, Decimal max)
        {
            return NextDecimal(Generator, min, max);
        }

        public static Decimal NextDecimal(this System.Random random, Decimal min, Decimal max)
        {
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
        
        public static Decimal NextDecimal(this IRandom random, Decimal min, Decimal max)
        {
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
        public static Boolean NextBoolean(this System.Random random)
        {
            return NextBoolean(random, 0.5d);
        }
        
        /// <inheritdoc cref="NextBoolean(IRandom,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(this IRandom random)
        {
            return NextBoolean(random, 0.5d);
        }

        /// <inheritdoc cref="NextBoolean(IRandom,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(Double chance)
        {
            return NextBoolean(Generator, chance);
        }

        /// <inheritdoc cref="NextBoolean(IRandom,Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NextBoolean(this System.Random random, Double chance)
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
        public static Boolean NextBoolean(this IRandom random, Double chance)
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
        public static Complex NextComplex(this System.Random random)
        {
            return new Complex(NextDouble(random), NextDouble(random));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this IRandom random)
        {
            return new Complex(NextDouble(random), NextDouble(random));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double max)
        {
            return NextComplex(Generator, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this System.Random random, Double max)
        {
            return new Complex(NextDouble(random, max), NextDouble(random, max));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this IRandom random, Double max)
        {
            return new Complex(NextDouble(random, max), NextDouble(random, max));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double min, Double max)
        {
            return NextComplex(Generator, min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this System.Random random, Double min, Double max)
        {
            return NextComplex(random, min, max, min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this IRandom random, Double min, Double max)
        {
            return NextComplex(random, min, max, min, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(Double min, Double max, Double imin, Double imax)
        {
            return NextComplex(Generator, min, max, imin, imax);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this System.Random random, Double min, Double max, Double imin, Double imax)
        {
            return new Complex(NextDouble(random, min, max), NextDouble(random, imin, imax));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex NextComplex(this IRandom random, Double min, Double max, Double imin, Double imax)
        {
            return new Complex(NextDouble(random, min, max), NextDouble(random, imin, imax));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan()
        {
            return NextTimeSpan(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this System.Random random)
        {
            return NextTimeSpan(random, TimeSpan.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this IRandom random)
        {
            return NextTimeSpan(random, TimeSpan.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(TimeSpan max)
        {
            return NextTimeSpan(Generator, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this System.Random random, TimeSpan max)
        {
            return NextTimeSpan(random, TimeSpan.MinValue, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(this IRandom random, TimeSpan max)
        {
            return NextTimeSpan(random, TimeSpan.MinValue, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan NextTimeSpan(TimeSpan min, TimeSpan max)
        {
            return NextTimeSpan(Generator, min, max);
        }

        public static TimeSpan NextTimeSpan(this System.Random random, TimeSpan min, TimeSpan max)
        {
            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }
            
            return min.Add(TimeSpan.FromTicks(NextInt64(random, min.Ticks, max.Ticks)));
        }
        
        public static TimeSpan NextTimeSpan(this IRandom random, TimeSpan min, TimeSpan max)
        {
            if (max == min)
            {
                return min;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }
            
            return min.Add(TimeSpan.FromTicks(NextInt64(random, min.Ticks, max.Ticks)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime()
        {
            return NextDateTime(Generator);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this System.Random random)
        {
            return NextDateTime(random, DateTime.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this IRandom random)
        {
            return NextDateTime(random, DateTime.MaxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(DateTime max)
        {
            return NextDateTime(Generator, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this System.Random random, DateTime max)
        {
            return NextDateTime(random, DateTime.MinValue, max);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(this IRandom random, DateTime max)
        {
            return NextDateTime(random, DateTime.MinValue, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextDateTime(DateTime min, DateTime max)
        {
            return NextDateTime(Generator, min, max);
        }

        public static DateTime NextDateTime(this System.Random random, DateTime min, DateTime max)
        {
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
        
        public static DateTime NextDateTime(this IRandom random, DateTime min, DateTime max)
        {
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
        
        public static void Reset(IRandom random)
        {
            Generator = random ?? throw new ArgumentNullException(nameof(random));
        }
        
        public static void Reset(System.Random random)
        {
            Generator = random as IRandom ?? new RandomAdapter(random ?? throw new ArgumentNullException(nameof(random)));
        }
    }
}