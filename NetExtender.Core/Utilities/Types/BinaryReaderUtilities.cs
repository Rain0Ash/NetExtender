// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static partial class BinaryReaderUtilities
    {
        public static Task<Byte[]> ReadBytesAsync(this BinaryReader reader, Int32 count)
        {
            return ReadBytesAsync(reader, count, CancellationToken.None);
        }

        public static Task<Byte[]> ReadBytesAsync(this BinaryReader reader, Int32 count, CancellationToken token)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return Task.Factory.StartNew(() => reader.ReadBytes(count), token);
        }
    }
}