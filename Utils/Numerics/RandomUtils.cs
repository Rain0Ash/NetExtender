// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Random;

namespace NetExtender.Utils.Numerics
{
    public static partial class RandomUtils
    {
        private static readonly Object Lock = new Object();
        private static MersenneTwister Random { get; } = new MersenneTwister(DateTime.UtcNow.Millisecond);

        public static Int32 Next(Int32 max = Int32.MaxValue)
        {
            return NextInt32(0, max);
        }

        public static Int32 Next(Int32 min, Int32 max)
        {
            return NextInt32(min, max);
        }

        public static Single NextSingle()
        {
            lock (Lock)
            {
                return Random.NextSingleWithOne();
            }
        }
        
        public static Single NextSingle(Single max)
        {
            return NextSingle() * max;
        }

        public static Single NextSingle(Single min, Single max)
        {
            Single value = NextSingle();
            return value * max - value * min + min;
        }

        public static Double NextDouble()
        {
            lock (Lock)
            {
                return Random.NextDoubleWithOne();
            }
        }

        public static Double NextDouble(Double max)
        {
            return NextDouble() * max;
        }
        
        public static Double NextDouble(Double min, Double max)
        {
            Double value = NextDouble();
            return value * max - value * min + min;
        }
        
        public static Decimal NextDecimal(Decimal max = Decimal.MaxValue)
        {
            return new Decimal(NextDouble()) * max;
        }
        
        public static Decimal NextDecimal(Decimal min, Decimal max)
        {
            Decimal value = new Decimal(NextDouble());
            return value * max - value * min + min;
        }

        /// <summary>
        /// Randomize boolean
        /// </summary>
        /// <param name="chance">Abs chance to true. Between [0;1]</param>
        /// <returns></returns>
        public static Boolean NextBoolean(Double chance = 0.5)
        {
            if (chance >= 1)
            {
                return true;
            }

            if (chance <= 0)
            {
                return false;
            }
            
            return NextDouble() <= chance;
        }
        
        private static IEnumerable<T> Range<T>(T min, T max, Int32 count, Func<T, T, T> generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            switch (count)
            {
                case < 0:
                    throw new ArgumentException("Count can't be less than -1");
                case 0:
                    yield break;
            }

            for (Int32 i = 0; i < count; i++)
            {
                yield return generator(min, max);
            }
        }
    }
}