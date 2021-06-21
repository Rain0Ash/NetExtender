// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Random.Interfaces;

namespace NetExtender.Random
{
    public sealed class RandomAdapter : IRandom
    {
        public static implicit operator RandomAdapter(System.Random random)
        {
            return new RandomAdapter(random);
        }
        
        public static implicit operator System.Random(RandomAdapter random)
        {
            return random._random;
        }
        
        private readonly System.Random _random;

        public RandomAdapter()
            : this(new System.Random())
        {
        }

        public RandomAdapter(Int32 seed)
            : this(new System.Random(seed))
        {
        }

        public RandomAdapter(System.Random random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public Int32 Next()
        {
            return _random.Next();
        }

        public Int32 Next(Int32 max)
        {
            return _random.Next(max);
        }

        public Int32 Next(Int32 min, Int32 max)
        {
            return _random.Next(min, max);
        }

        public void NextBytes(Byte[] buffer)
        {
            _random.NextBytes(buffer);
        }

        public void NextBytes(Span<Byte> buffer)
        {
            _random.NextBytes(buffer);
        }

        public Double NextDouble()
        {
            return _random.NextDouble();
        }

        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) || _random.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return _random.GetHashCode();
        }

        public override String? ToString()
        {
            return _random.ToString();
        }
    }
}