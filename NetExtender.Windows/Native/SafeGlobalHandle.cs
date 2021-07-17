// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Windows
{
    /// <summary>
    /// A <see cref="SafeHandle"/> for a global memory allocation.
    /// </summary>
    internal sealed class SafeGlobalHandle : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeGlobalHandle"/> class.
        /// </summary>
        /// <param name="toManage">
        /// The initial handle value.
        /// </param>
        /// <param name="size">
        /// The size of this memory block, in bytes.
        /// </param>
        private SafeGlobalHandle(IntPtr toManage, Int32 size)
            : base(IntPtr.Zero, true)
        {
            Size = size;
            SetHandle(toManage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeGlobalHandle"/> class.
        /// </summary>
        private SafeGlobalHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the handle value is invalid;
        /// otherwise, <see langword="false"/>.
        /// </value>
        public override Boolean IsInvalid
        {
            get
            {
                return IntPtr.Zero == handle;
            }
        }

        /// <summary>
        /// Returns the size of this memory block.
        /// </summary>
        /// <value>
        /// The size of this memory block, in bytes.
        /// </value>
        public Int32 Size { get; }

        /// <summary>
        /// Allocates memory from the unmanaged memory of the process using GlobalAlloc.
        /// </summary>
        /// <param name="bytes">
        /// The number of bytes in memory required.
        /// </param>
        /// <returns>
        /// A <see cref="SafeGlobalHandle"/> representing the memory.
        /// </returns>
        /// <exception cref="OutOfMemoryException">
        /// There is insufficient memory to satisfy the request.
        /// </exception>
        public static SafeGlobalHandle Allocate(Int32 bytes)
        {
            return new SafeGlobalHandle(Marshal.AllocHGlobal(bytes), bytes);
        }

        /// <summary>
        /// Returns an invalid handle.
        /// </summary>
        /// <returns>
        /// An invalid <see cref="SafeGlobalHandle"/>.
        /// </returns>
        public static SafeGlobalHandle Invalid()
        {
            return new SafeGlobalHandle();
        }

        /// <summary>
        /// Executes the code required to free the handle.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the handle is released successfully;
        /// otherwise, in the event of a catastrophic failure, <see langword="false"/>.
        /// In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        protected override Boolean ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            return true;
        }
    }
}