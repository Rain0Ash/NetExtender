// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Random.Interfaces
{
    public interface IRandom : IRandomComparableGenerator<Int32>, IDisposable
    {
        public void NextBytes(Byte[] buffer);
        public void NextBytes(Span<Byte> buffer);
        public Double NextDouble();
    }
}