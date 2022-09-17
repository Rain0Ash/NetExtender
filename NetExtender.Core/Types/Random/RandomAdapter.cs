// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Random.Interfaces;

namespace NetExtender.Types.Random
{
    public sealed class RandomAdapter : IRandom
    {
        [return: NotNullIfNotNull("random")]
        public static implicit operator RandomAdapter?(System.Random? random)
        {
            return random is not null ? new RandomAdapter(random) : null;
        }
        
        [return: NotNullIfNotNull("random")]
        public static explicit operator System.Random?(RandomAdapter? random)
        {
            return random?.Random;
        }

        private System.Random Random { get; }

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
            Random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public Int32 Next()
        {
            return Random.Next();
        }

        public Int32 Next(Int32 max)
        {
            return Random.Next(max);
        }

        public Int32 Next(Int32 min, Int32 max)
        {
            return Random.Next(min, max);
        }

        public void NextBytes(Byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

        public void NextBytes(Span<Byte> buffer)
        {
            Random.NextBytes(buffer);
        }

        public Double NextDouble()
        {
            return Random.NextDouble();
        }

        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) || Random.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return Random.GetHashCode();
        }

        public override String? ToString()
        {
            return Random.ToString();
        }

        void IDisposable.Dispose()
        {
        }
    }
}