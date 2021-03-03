using System;
using System.Runtime.InteropServices;

namespace NetExtender.Crypto.Hashes.XXHash
{
    public static partial class XXHash32
    {
        /// <summary>
        /// Compute xxHash for the data byte span 
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe UInt32 ComputeHash(ReadOnlySpan<Byte> data, Int32 length, UInt32 seed = 0)
        {
            fixed (Byte* pointer = &MemoryMarshal.GetReference(data))
            {
                return UnsafeComputeHash(pointer, length, seed);
            }
        }
    }
}
