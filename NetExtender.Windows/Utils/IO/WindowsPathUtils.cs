// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using NetExtender.Windows.IO;

namespace NetExtender.Utils.Windows.IO
{
    public static class WindowsPathUtils
    {
        public const Int32 MaxPathLength = Byte.MaxValue;
        public const Int32 MaxLongPathLength = UInt16.MaxValue;

        public const String LongPathPrefix = @"\\?\";
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal static class Native
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern SafeFileHandle CreateFile(String name, NativeFileAccess access, FileShare share, IntPtr security, FileMode mode, NativeFileFlags flags,
                IntPtr template);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern Int32 GetFileAttributes(String path);

            [DllImport("kernel32.dll")]
            public static extern Int32 GetFileType(SafeFileHandle handle);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean DeleteFile(String name);
        }

        internal static class Safe
        {
            private const Int32 ErrorFileNotFound = 2;
            private const Int32 ErrorPathNotFound = 3;

            public static Int32 GetFileAttributes(String name)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                Int32 result = Native.GetFileAttributes(name);
                if (result != -1)
                {
                    return result;
                }

                Int32 code = Marshal.GetLastWin32Error();
                switch (code)
                {
                    case ErrorFileNotFound:
                    case ErrorPathNotFound:
                    {
                        break;
                    }
                    default:
                    {
                        Exception? exception = GetLastIOException(name);

                        if (exception is not null)
                        {
                            throw exception;
                        }

                        break;
                    }
                }

                return result;
            }

            public static Boolean FileExists(String name)
            {
                return GetFileAttributes(name) != -1;
            }

            public static Boolean DeleteFile(String name)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (Native.DeleteFile(name))
                {
                    return true;
                }

                Int32 code = Marshal.GetLastWin32Error();
                switch (code)
                {
                    case ErrorFileNotFound:
                    case ErrorPathNotFound:
                    {
                        break;
                    }
                    default:
                    {
                        Exception? exception = GetLastIOException(name);

                        if (exception is not null)
                        {
                            throw exception;
                        }

                        break;
                    }
                }

                return true;
            }

            public static SafeFileHandle CreateFile(String path, NativeFileAccess access, FileShare share, IntPtr security, FileMode mode, NativeFileFlags flags, IntPtr template)
            {
                SafeFileHandle result = Native.CreateFile(path, access, share, security, mode, flags, template);
                if (result.IsInvalid || Native.GetFileType(result) == 1)
                {
                    return result;
                }

                result.Dispose();
                throw new NotSupportedException($"The specified file name '{path}' is not a disk-based file.");
            }

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern Boolean GetFileSizeEx(SafeFileHandle handle, out UInt64 size);

            public static UInt64 GetFileSize(String path, SafeFileHandle? handle)
            {
                if (handle is null || handle.IsInvalid)
                {
                    return 0;
                }

                if (GetFileSizeEx(handle, out UInt64 value))
                {
                    return value;
                }

                Exception? exception = GetLastIOException(path);

                if (exception is not null)
                {
                    throw exception;
                }

                return 0;
            }
            
            private const Int32 NativeErrorCode = -2147024896;

            public static Int32 MakeHRFromErrorCode(Int32 code)
            {
                return NativeErrorCode | code;
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            private static extern Int32 FormatMessage(Int32 dwFlags, IntPtr lpSource, Int32 dwMessageId, Int32 dwLanguageId, StringBuilder lpBuffer, Int32 nSize, IntPtr vaListArguments);

            public static String GetErrorMessage(Int32 code)
            {
                StringBuilder lpBuffer = new StringBuilder(0x200);
                return 0 != FormatMessage(0x3200, IntPtr.Zero, code, 0, lpBuffer, lpBuffer.Capacity, IntPtr.Zero) ? lpBuffer.ToString() : $"Unknown error: {code}";
            }

            public static Exception? GetLastIOException(String path)
            {
                Int32 code = Marshal.GetLastWin32Error();
                if (code == 0)
                {
                    return null;
                }

                Int32 hresult = Marshal.GetHRForLastWin32Error();
                return hresult >= 0 ? new Win32Exception(code) : GetIOException(code, path);
            }

            public static Exception? GetIOException(Int32 code, String? path)
            {
                switch (code)
                {
                    case 0:
                    {
                        break;
                    }
                    case 2: // File not found
                    {
                        return String.IsNullOrEmpty(path) ? new FileNotFoundException() : new FileNotFoundException(null, path);
                    }
                    case 3: // Directory not found
                    {
                        return String.IsNullOrEmpty(path) ? new DirectoryNotFoundException() : new DirectoryNotFoundException($"Could not find a part of the path '{path}'.");
                    }
                    case 5: // Access denied
                    {
                        return String.IsNullOrEmpty(path) ? new UnauthorizedAccessException() : new UnauthorizedAccessException($"Access to the path '{path}' was denied.");
                    }
                    case 15: // Drive not found
                    {
                        return String.IsNullOrEmpty(path)
                            ? new DriveNotFoundException()
                            : new DriveNotFoundException($"Could not find the drive '{path}'. The drive might not be ready or might not be mapped.");
                    }
                    case 32: // Sharing violation
                    {
                        return String.IsNullOrEmpty(path)
                            ? new IOException(GetErrorMessage(code), MakeHRFromErrorCode(code))
                            : new IOException($"The process cannot access the file '{path}' because it is being used by another process.", MakeHRFromErrorCode(code));
                    }
                    case 80: // File already exists
                    {
                        if (!String.IsNullOrEmpty(path))
                        {
                            return new IOException($"The file '{path}' already exists.", MakeHRFromErrorCode(code));
                        }

                        break;
                    }
                    case 87: // Invalid parameter
                    {
                        return new IOException(GetErrorMessage(code), MakeHRFromErrorCode(code));
                    }
                    case 183: // File or directory already exists
                    {
                        if (!String.IsNullOrEmpty(path))
                        {
                            return new IOException($"Cannot create '{path}' because a file or directory with the same name already exists.", MakeHRFromErrorCode(code));
                        }

                        break;
                    }
                    case 206: // Path too long
                    {
                        return new PathTooLongException();
                    }
                    case 995: // Operation cancelled
                    {
                        return new OperationCanceledException();
                    }
                    default:
                    {
                        return Marshal.GetExceptionForHR(MakeHRFromErrorCode(code));
                    }
                }

                return null;
            }
        }

        public static NativeFileAccess ToNative(this FileAccess access)
        {
            NativeFileAccess result = 0;
            if ((access & FileAccess.Read) == FileAccess.Read)
            {
                result |= NativeFileAccess.GenericRead;
            }

            if ((access & FileAccess.Write) == FileAccess.Write)
            {
                result |= NativeFileAccess.GenericWrite;
            }

            return result;
        }
    }
}