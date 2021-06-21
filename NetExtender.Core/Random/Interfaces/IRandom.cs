// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Random.Interfaces
{
    public interface IRandom
    {
        public Int32 Next();

        public Int32 Next(Int32 max);

        public Int32 Next(Int32 min, Int32 max);

        public void NextBytes(Byte[] buffer);

        public void NextBytes(Span<Byte> buffer);

        public Double NextDouble();
    }
}