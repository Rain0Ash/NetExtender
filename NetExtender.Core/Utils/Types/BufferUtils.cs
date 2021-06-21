// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace NetExtender.Utils.Types
{
    public static class BufferUtils
    {
        public const Int32 DefaultBuffer = 4096;
        
        public static Byte[] Combine(this Byte[] first, Byte[] second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            Byte[] result = new Byte[first.Length + second.Length];
            
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            
            return result;
        }

        public static Byte[] Combine(this Byte[] first, Byte[] second, Byte[] third)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            Byte[] result = new Byte[first.Length + second.Length + third.Length];
            
            Int32 offset = 0;
            Buffer.BlockCopy(first, 0, result, offset, first.Length);
            Buffer.BlockCopy(second, 0, result, offset += first.Length, second.Length);
            // ReSharper disable once RedundantAssignment
            Buffer.BlockCopy(third, 0, result, offset += second.Length, third.Length);
            
            return result;
        }
        
        public static Byte[] Combine(this Byte[] first, Byte[] second, Byte[] third, params Byte[][] arrays)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            if (arrays is null)
            {
                throw new ArgumentNullException(nameof(arrays));
            }

            Byte[] result = new Byte[first.Length + second.Length + third.Length + arrays.Sum(array => array.Length)];

            Int32 offset = 0;
            Buffer.BlockCopy(first, 0, result, offset, first.Length);
            Buffer.BlockCopy(second, 0, result, offset += first.Length, second.Length);
            Buffer.BlockCopy(third, 0, result, offset += second.Length, third.Length);

            offset += third.Length;
            
            foreach (Byte[] block in arrays)
            {
                Buffer.BlockCopy(block, 0, result, offset, block.Length);
                offset += block.Length;
            }
            
            return result;
        }
    }
}