// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using Core.Types.Handle;

namespace NetExtender.Utils.Types
{
    public static class PointerUtils
    {
        public static GCHandle ToGCHandle(this Object obj)
        {
            if (obj is GCHandle handle)
            {
                return ToGCHandle(handle);
            }
            
            return GCHandle.Alloc(obj);
        }
        
        public static GCHandle ToGCHandle(this GCHandle handle)
        {
            return handle;
        }
        
        public static IntPtr ToIntPtr(this Object obj)
        {
            return ToGCHandle(obj).ToIntPtr();
        }

        public static IntPtr ToIntPtr(this GCHandle obj)
        {
            return GCHandle.ToIntPtr(obj);
        }

        public static GCHandleProvider ToGCHandleProvider(this Object obj)
        {
            if (obj is GCHandle handle)
            {
                return ToGCHandleProvider(handle);
            }
            
            return new GCHandleProvider(obj);
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