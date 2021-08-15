// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using NetExtender.Windows;
using NetExtender.Utilities.Windows.IO.NTFS;

namespace NetExtender.IO.FileSystem.NTFS.DataStreams
{
    internal sealed class StreamName : IDisposable
    {
        private static readonly SafeGlobalHandle InvalidBlock = SafeGlobalHandle.Invalid();

        /// <summary>
        /// Returns the handle to the block of memory.
        /// </summary>
        /// <value>
        /// The <see cref="SafeGlobalHandle"/> representing the block of memory.
        /// </value>
        public SafeGlobalHandle MemoryBlock { get; private set; } = InvalidBlock;

        /// <summary>
        /// Ensures that there is sufficient memory allocated.
        /// </summary>
        /// <param name="capacity">
        /// The required capacity of the block, in bytes.
        /// </param>
        /// <exception cref="OutOfMemoryException">
        /// There is insufficient memory to satisfy the request.
        /// </exception>
        public void EnsureCapacity(Int32 capacity)
        {
            Int32 currentSize = MemoryBlock.IsInvalid ? 0 : MemoryBlock.Size;
            if (currentSize >= capacity)
            {
                return;
            }

            if (currentSize != 0)
            {
                currentSize <<= 1;
            }

            if (currentSize < capacity)
            {
                currentSize = capacity;
            }

            if (!MemoryBlock.IsInvalid)
            {
                MemoryBlock.Dispose();
            }

            MemoryBlock = SafeGlobalHandle.Allocate(currentSize);
        }

        /// <summary>
        /// Reads the Unicode string from the memory block.
        /// </summary>
        /// <param name="length">
        /// The length of the string to read, in characters.
        /// </param>
        /// <returns>
        /// The string read from the memory block.
        /// </returns>
        public String? ReadString(Int32 length)
        {
            if (length <= 0 || MemoryBlock.IsInvalid)
            {
                return null;
            }

            if (length > MemoryBlock.Size)
            {
                length = MemoryBlock.Size;
            }

            return Marshal.PtrToStringUni(MemoryBlock.DangerousGetHandle(), length);
        }

        /// <summary>
        /// Reads the string, and extracts the stream name.
        /// </summary>
        /// <param name="length">
        /// The length of the string to read, in characters.
        /// </param>
        /// <returns>
        /// The stream name.
        /// </returns>
        public String? ReadStreamName(Int32 length)
        {
            String? name = ReadString(length);
            if (String.IsNullOrEmpty(name))
            {
                return name;
            }

            // Name is of the format ":NAME:$DATA\0"
            Int32 sepindex = name.IndexOf(NTFSAlternateStreamUtilities.StreamSeparator, 1);
            if (sepindex != -1)
            {
                return name.Substring(1, sepindex - 1);
            }

            sepindex = name.IndexOf('\0');
            
            return sepindex > 1 ? name.Substring(1, sepindex - 1) : null;
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (MemoryBlock.IsInvalid)
            {
                return;
            }

            MemoryBlock.Dispose();
            MemoryBlock = InvalidBlock;
        }
    }
}