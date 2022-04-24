// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.Types
{
    public static class BinaryWriterUtilities
    {
        public static void Write<T>(this BinaryWriter writer, T value) where T : struct
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            Int32 size = Unsafe.SizeOf<T>();
            Span<Byte> buffer = stackalloc Byte[size];
            MemoryMarshal.Write(buffer, ref value);
            writer.Write(buffer);
        }
    }
}