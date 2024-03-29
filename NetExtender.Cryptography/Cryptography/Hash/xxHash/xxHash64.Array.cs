﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Cryptography.Hash.XXHash
{
    public static partial class XXHash64
    {
        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static UInt64 ComputeHash(Byte[] data, UInt32 seed = 0)
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
        public static unsafe UInt64 ComputeHash(Byte[] data, Int32 length, UInt64 seed = 0)
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
        /// <param name="offset">Offset of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe UInt64 ComputeHash(Byte[] data, Int32 offset, Int32 length, UInt64 seed = 0)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            fixed (Byte* pointer = &data[0 + offset])
            {
                return UnsafeComputeHash(pointer, length, seed);
            }
        }   
        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static UInt64 ComputeHash(ArraySegment<Byte> data, UInt64 seed = 0)
        {
            return ComputeHash(data.Array ?? throw new ArgumentNullException(nameof(data)), data.Offset, data.Count, seed);
        }   

    }
}