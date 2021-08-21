// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Static
{
    public static class UnsafeUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T Read<T>(void* source)
        {
            return Unsafe.Read<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T ReadUnaligned<T>(void* source)
        {
            return Unsafe.ReadUnaligned<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(ref Byte source)
        {
            return Unsafe.ReadUnaligned<T>(ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Int32 length)
        {
            Byte* pointer = (Byte*) destination;
            for (Int32 i = 0; i < length; i++)
            {
                pointer[i] = Byte.MinValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, Int32 count)
        {
            Fill<T>(destination, default, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, T? value, Int32 count)
        {
            for (Int32 i = 0; i < count; i++)
            {
                Write(destination, value);
                destination = Add<T>(destination);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write<T>(void* destination, T value)
        {
            Unsafe.Write(destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteUnaligned<T>(void* destination, T value)
        {
            Unsafe.WriteUnaligned(destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(ref Byte destination, T value)
        {
            Unsafe.WriteUnaligned(ref destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(void* destination, ref T source)
        {
            Unsafe.Copy(destination, ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(ref T destination, void* source)
        {
            Unsafe.Copy(ref destination, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* AsPointer<T>(ref T value)
        {
            return Unsafe.AsPointer(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SizeOf<T>()
        {
            return Unsafe.SizeOf<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlock(void* destination, void* source, UInt32 byteCount)
        {
            Unsafe.CopyBlock(destination, source, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlock(ref Byte destination, ref Byte source, UInt32 byteCount)
        {
            Unsafe.CopyBlock(ref destination, ref source, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlockUnaligned(void* destination, void* source, UInt32 byteCount)
        {
            Unsafe.CopyBlockUnaligned(destination, source, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlockUnaligned(ref Byte destination, ref Byte source, UInt32 byteCount)
        {
            Unsafe.CopyBlockUnaligned(ref destination, ref source, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlock(void* startAddress, Byte value, UInt32 byteCount)
        {
            Unsafe.InitBlock(startAddress, value, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(ref Byte startAddress, Byte value, UInt32 byteCount)
        {
            Unsafe.InitBlock(ref startAddress, value, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlockUnaligned(void* startAddress, Byte value, UInt32 byteCount)
        {
            Unsafe.InitBlockUnaligned(startAddress, value, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlockUnaligned(ref Byte startAddress, Byte value, UInt32 byteCount)
        {
            Unsafe.InitBlockUnaligned(ref startAddress, value, byteCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T As<T>(Object value) where T : class
        {
            return Unsafe.As<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T AsRef<T>(void* source)
        {
            return ref Unsafe.AsRef<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(in T source)
        {
            return ref Unsafe.AsRef(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref Unsafe.As<TFrom, TTo>(ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Unbox<T>(Object box) where T : struct
        {
            return ref Unsafe.Unbox<T>(box);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, Int32 elementOffset)
        {
            return ref Unsafe.Add(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add(void* source, Int32 byteOffset)
        {
            return (void*) ((IntPtr) source + byteOffset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add<T>(void* source)
        {
            return Unsafe.Add<T>(source, 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add<T>(void* source, Int32 elementOffset)
        {
            return Unsafe.Add<T>(source, elementOffset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source)
        {
            return ref Unsafe.Add(ref source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, IntPtr elementOffset)
        {
            return ref Unsafe.Add(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            return ref Unsafe.AddByteOffset(ref source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, Int32 elementOffset)
        {
            return ref Unsafe.Subtract(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract(void* source, Int32 byteOffset)
        {
            return (void*) ((IntPtr) source - byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract<T>(void* source)
        {
            return Unsafe.Subtract<T>(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract<T>(void* source, Int32 elementOffset)
        {
            return Unsafe.Subtract<T>(source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source)
        {
            return ref Unsafe.Subtract(ref source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
        {
            return ref Unsafe.Subtract(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            return ref Unsafe.SubtractByteOffset(ref source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr ByteOffset<T>(ref T origin, ref T target)
        {
            return Unsafe.ByteOffset(ref origin, ref target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AreSame<T>(ref T left, ref T right)
        {
            return Unsafe.AreSame(ref left, ref right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAddressGreaterThan<T>(ref T left, ref T right)
        {
            return Unsafe.IsAddressGreaterThan(ref left, ref right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAddressLessThan<T>(ref T left, ref T right)
        {
            return Unsafe.IsAddressLessThan(ref left, ref right);
        }

        public static class Segfault
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate void UnmanagedCallback();

            private static UnmanagedCallback SegfaultDelegate { get; } = (UnmanagedCallback) Marshal.GetDelegateForFunctionPointer((IntPtr) 1, typeof(UnmanagedCallback));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Crash()
            {
                SegfaultDelegate?.Invoke();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(Int32 milliseconds)
            {
                return CrashAsync(milliseconds, CancellationToken.None);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(TimeSpan wait)
            {
                return CrashAsync(wait, CancellationToken.None);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(Int32 milliseconds, CancellationToken token)
            {
                return CrashAsync(TimeSpan.FromMilliseconds(milliseconds), token);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task CrashAsync(TimeSpan wait, CancellationToken token)
            {
                try
                {
                    await Task.Delay(wait, token).ContinueWith(Crash, token).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    //ignored
                }
            }
        }
    }
}