using System;

namespace NetExtender.Crypto.Hashes.XXHash
{
    public static partial class XXHash32
    {
        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static UInt32 ComputeHash(Byte[] data, UInt32 seed = 0)
        {
            return ComputeHash(data, data.Length, seed);
        }
        
        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe UInt32 ComputeHash(Byte[] data, Int32 length, UInt32 seed = 0)
        {
            fixed (Byte* pData = &data[0])
            {
                return UnsafeComputeHash(pData, length, seed);
            }
        }

        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="offset">The offset of the data for hashing</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe UInt32 ComputeHash(Byte[] data, Int32 offset, Int32 length, UInt32 seed = 0)
        {
            fixed (Byte* pData = &data[0 + offset])
            {
                return UnsafeComputeHash(pData, length, seed);
            }
        }

        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static UInt64 ComputeHash(ArraySegment<Byte> data, UInt32 seed = 0)
        {
            return ComputeHash(data.Array, data.Offset, data.Count, seed);
        }
    }
}