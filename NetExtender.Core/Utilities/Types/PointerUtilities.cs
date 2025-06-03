// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using Core.Types.Handle;

namespace NetExtender.Utilities.Types
{
    public static class PointerUtilities
    {
        public static GCHandle ToGCHandle(this Object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                GCHandle handle => ToGCHandle(handle),
                _ => GCHandle.Alloc(value)
            };
        }

        public static GCHandle ToGCHandle(this GCHandle handle)
        {
            return handle;
        }

        public static IntPtr ToIntPtr(this Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return ToGCHandle(value).ToIntPtr();
        }

        public static IntPtr ToIntPtr(this GCHandle value)
        {
            return GCHandle.ToIntPtr(value);
        }

        public static GCHandleProvider ToGCHandleProvider(this Object value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                GCHandle handle => ToGCHandleProvider(handle),
                _ => new GCHandleProvider(value)
            };
        }

        public static GCHandleProvider ToGCHandleProvider(this GCHandle handle)
        {
            return new GCHandleProvider(handle);
        }

        public static GCHandle GetHandle(this IntPtr ptr)
        {
            return GCHandle.FromIntPtr(ptr);
        }

        public static Object? GetObject(this IntPtr ptr)
        {
            return GetHandle(ptr).Target;
        }
    }
}