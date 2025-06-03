// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.Win32.SafeHandles;
using NetExtender.Utilities.Core;

#pragma warning disable CA1041

namespace NetExtender.Types.Streams
{
    public class CustomFileStreamWrapper : FileStream
    {
        [ReflectionNaming]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class FileStreamHelpers
        {
            public static class Signature
            {
                public delegate void ValidateArguments(String path, FileMode mode, FileAccess? access, FileShare? share, Int32? bufferSize, FileOptions options, Int64? preallocationSize);
                public delegate void ValidateHandle(SafeFileHandle handle, FileAccess access, Int32 bufferSize);
                public delegate void ValidateHandleAsync(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync);
            }
            
            [ReflectionSignature]
            public static Signature.ValidateArguments? ValidateArguments { get; }
            public static Signature.ValidateHandle? ValidateHandle { get; }
            public static Signature.ValidateHandleAsync? ValidateHandleAsync { get; }
            
            static FileStreamHelpers()
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                Type? helper = Type.GetType($"{nameof(System)}.{nameof(System.IO)}.Strategies.{nameof(FileStreamHelpers)}");
                
                ParameterInfo[]? parameters = typeof(Signature.ValidateArguments).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                ValidateArguments = parameters is not null ? helper?.GetMethod(nameof(Signature.ValidateArguments), binding, parameters)?.CreateDelegate<Signature.ValidateArguments>() : null;
                
                parameters = typeof(Signature.ValidateHandle).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                ValidateHandle = parameters is not null ? typeof(FileStream).GetMethod(nameof(Signature.ValidateHandle), binding, parameters)?.CreateDelegate<Signature.ValidateHandle>() : null;
                
                parameters = typeof(Signature.ValidateHandleAsync).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                ValidateHandleAsync = parameters is not null ? typeof(FileStream).GetMethod(nameof(Signature.ValidateHandle), binding, parameters)?.CreateDelegate<Signature.ValidateHandleAsync>() : null;
            }
        }
        
        internal const Int32 DefaultBufferSize = 4096;
        internal const FileShare DefaultShare = FileShare.Read;
        private const Boolean DefaultIsAsync = false;
        
        public CustomFileStreamWrapper(String path, FileMode mode)
            : base(path, mode)
        {
        }

        public CustomFileStreamWrapper(String path, FileMode mode, FileAccess access)
            : base(path, mode, access)
        {
        }

        public CustomFileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share)
            : base(path, mode, access, share)
        {
        }

        public CustomFileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 buffer)
            : base(path, mode, access, share, buffer)
        {
        }

        public CustomFileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 buffer, Boolean @async)
            : base(path, mode, access, share, buffer, async)
        {
        }

        public CustomFileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 buffer, FileOptions options)
            : base(path, mode, access, share, buffer, options)
        {
        }

        public CustomFileStreamWrapper(String path, FileStreamOptions options)
            : base(path, options)
        {
        }
        
        [Obsolete]
        public CustomFileStreamWrapper(IntPtr handle, FileAccess access)
            : base(handle, access)
        {
        }

        [Obsolete]
        public CustomFileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle)
            : base(handle, access, ownsHandle)
        {
        }

        [Obsolete]
        public CustomFileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 buffer)
            : base(handle, access, ownsHandle, buffer)
        {
        }

        [Obsolete]
        public CustomFileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 buffer, Boolean async)
            : base(handle, access, ownsHandle, buffer, async)
        {
        }
        public CustomFileStreamWrapper(SafeFileHandle handle, FileAccess access)
            : base(handle, access)
        {
        }

        public CustomFileStreamWrapper(SafeFileHandle handle, FileAccess access, Int32 buffer)
            : base(handle, access, buffer)
        {
        }

        public CustomFileStreamWrapper(SafeFileHandle handle, FileAccess access, Int32 buffer, Boolean async)
            : base(handle, access, buffer, async)
        {
        }
            
        protected static Object ValidateHandle(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
        {
            FileStreamHelpers.ValidateHandle?.Invoke(handle, access, bufferSize);
            return handle;
        }

        protected static Object ValidateHandle(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
        {
            FileStreamHelpers.ValidateHandleAsync?.Invoke(handle, access, bufferSize, isAsync);
            return handle;
        }

        protected static Object ValidateArguments(String path, FileMode mode, FileAccess? access, FileShare? share, Int32? bufferSize, Boolean? useAsync, Int64? preallocationSize)
        {
            FileOptions options = useAsync ?? DefaultIsAsync ? FileOptions.Asynchronous : FileOptions.None;
            return ValidateArguments(path, mode, access, share, bufferSize, options, preallocationSize);
        }

        protected static Object ValidateArguments(String path, FileMode mode, FileAccess? access, FileShare? share, Int32? bufferSize, FileOptions options, Int64? preallocationSize)
        {
            access ??= mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite;
            FileStreamHelpers.ValidateArguments?.Invoke(path, mode, access.Value, share ?? DefaultShare, bufferSize ?? DefaultBufferSize, options, preallocationSize ?? 0);
            return path;
        }
    }
}