// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Cryptography.Hash.XXHash
{
    public static partial class XXHash64
    {
        /// <summary>
        /// Compute xxHash for the data byte span
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe UInt64 ComputeHash(ReadOnlySpan<Byte> data, UInt64 seed = 0)
        {
            fixed (Byte* pointer = &MemoryMarshal.GetReference(data))
            {
                return UnsafeComputeHash(pointer, data.Length, seed);
            }
        }
    }
}
