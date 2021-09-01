// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using NetExtender.Random.Interfaces;

namespace NetExtender.Crypto.Random
{
    public sealed unsafe class CryptoRandom : IRandom
    {
        private RandomNumberGenerator Generator { get; }

        public CryptoRandom()
        {
            Generator = RandomNumberGenerator.Create();
        }

        public CryptoRandom(String generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            Generator = RandomNumberGenerator.Create(generator) ?? throw new InvalidOperationException("Invalid random generator name.");
        }

        public CryptoRandom(CspParameters? parameters)
        {
            Generator = new RNGCryptoServiceProvider(parameters);
        }

        public Int32 Next()
        {
            Span<Byte> stack = stackalloc Byte[sizeof(Int32)];
            NextBytes(stack);
            return MemoryMarshal.Read<Int32>(stack);
        }

        public Int32 Next(Int32 max)
        {
            return Next(Int32.MinValue, max);
        }

        public Int32 Next(Int32 min, Int32 max)
        {
            if (min >= max)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            unchecked
            {
                Int64 difference = (Int64) max - min;
                Int64 upperbound = UInt32.MaxValue / difference * difference;

                UInt32 value;
                do
                {
                    value = (UInt32) Next();
                } while (value >= upperbound);

                return (Int32) (min + value % difference);
            }
        }

        public void NextBytes(Byte[] buffer)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            Generator.GetBytes(buffer);
        }

        public void NextBytes(Span<Byte> buffer)
        {
            Generator.GetBytes(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double NextDouble()
        {
            return NextDoubleWithoutOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Double NextDouble(Boolean include)
        {
            return include ? NextDoubleWithOne() : NextDoubleWithoutOne();
        }

        public Double NextDoubleWithOne()
        {
            Span<Byte> stack = stackalloc Byte[sizeof(UInt32) * 2];

            NextBytes(stack);

            UInt32 first = MemoryMarshal.Read<UInt32>(stack.Slice(0, sizeof(UInt32)));
            UInt32 second = MemoryMarshal.Read<UInt32>(stack.Slice(sizeof(UInt32), sizeof(UInt32)));

            return first >= second ? (Double) second / first : first / (Double) second;
        }

        public Double NextDoubleWithoutOne()
        {
            Double value;
            do
            {
                value = NextDoubleWithOne();
            } while (Math.Abs(value - 1) < Double.Epsilon);

            return value;
        }

        public void Dispose()
        {
            Generator.Dispose();
        }
    }
}