using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Win32.SafeHandles;
using NetExtender.Types.Streams;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [ReflectionSignature]
        private static Object? ChooseStrategy(FileStream fileStream, SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
        {
            return null;
        }

        [ReflectionSignature]
        private static Object? ChooseStrategy(FileStream fileStream, String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int64 preallocationSize)
        {
            return null;
        }

        [ReflectionSignature]
        private static Object? ChooseStrategy(FileStream fileStream, String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int64 preallocationSize, Int32? unixCreateMode)
        {
            return null;
        }
        
        [SuppressMessage("Design", "CA1041")]
        public class InterceptHarmonyFileStream : InterceptFileStream
        {
            public InterceptHarmonyFileStream(String path, FileMode mode)
                : base(path, mode)
            {
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access)
                : base(path, mode, access)
            {
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share)
                : base(path, mode, access, share)
            {
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
                : base(path, mode, access, share, bufferSize)
            {
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Boolean useAsync)
                : base(path, mode, access, share, bufferSize, useAsync)
            {
            }

            public InterceptHarmonyFileStream(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
                : base(path, mode, access, share, bufferSize, options)
            {
            }

            public InterceptHarmonyFileStream(String path, FileStreamOptions options)
                : base(path, options)
            {
            }
            
            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access)
                : base(handle, access)
            {
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle)
                : base(handle, access, ownsHandle)
            {
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize)
                : base(handle, access, ownsHandle, bufferSize)
            {
            }

            [Obsolete]
            public InterceptHarmonyFileStream(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize, Boolean isAsync)
                : base(handle, access, ownsHandle, bufferSize, isAsync)
            {
            }
            
            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access)
                : base(handle, access)
            {
            }

            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
                : base(handle, access, bufferSize)
            {
            }

            public InterceptHarmonyFileStream(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
                : base(handle, access, bufferSize, isAsync)
            {
            }
        }
    }
}