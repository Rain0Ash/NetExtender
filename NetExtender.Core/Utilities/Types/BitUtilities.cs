// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Utilities.Static;

namespace NetExtender.Utilities.Types
{
    public static class BitUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte RotateLeft(this SByte value, Int32 offset)
        {
            const Int32 size = sizeof(SByte) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (SByte) ((value >> offset) | (value << (size - offset)));
                    default:
                        offset %= size;
                        return (SByte) ((value << offset) | (value >> (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RotateLeft(this Byte value, Int32 offset)
        {
            const Int32 size = sizeof(Byte) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (Byte) ((value >> offset) | (value << (size - offset)));
                    default:
                        offset %= size;
                        return (Byte) ((value << offset) | (value >> (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateLeft(this Int16 value, Int32 offset)
        {
            const Int32 size = sizeof(Int16) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (Int16) ((value >> offset) | (value << (size - offset)));
                    default:
                        offset %= size;
                        return (Int16) ((value << offset) | (value >> (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateLeft(this UInt16 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt16) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (UInt16) ((value >> offset) | (value << (size - offset)));
                    default:
                        offset %= size;
                        return (UInt16) ((value << offset) | (value >> (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateLeft(this Int32 value, Int32 offset)
        {
            const Int32 size = sizeof(Int32) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value >> offset) | (value << (size - offset));
                    default:
                        offset %= size;
                        return (value << offset) | (value >> (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateLeft(this UInt32 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt32) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value >> offset) | (value << (size - offset));
                    default:
                        offset %= size;
                        return (value << offset) | (value >> (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateLeft(this Int64 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt64) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value >> offset) | (value << (size - offset));
                    default:
                        offset %= size;
                        return (value << offset) | (value >> (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateLeft(this UInt64 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt64) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value >> offset) | (value << (size - offset));
                    default:
                        offset %= size;
                        return (value << offset) | (value >> (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte RotateRight(this SByte value, Int32 offset)
        {
            const Int32 size = sizeof(SByte) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (SByte) ((value << offset) | (value >> (size - offset)));
                    default:
                        offset %= size;
                        return (SByte) ((value >> offset) | (value << (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RotateRight(this Byte value, Int32 offset)
        {
            const Int32 size = sizeof(Byte) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (Byte) ((value << offset) | (value >> (size - offset)));
                    default:
                        offset %= size;
                        return (Byte) ((value >> offset) | (value << (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RotateRight(this Int16 value, Int32 offset)
        {
            const Int32 size = sizeof(Int16) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (Int16) ((value << offset) | (value >> (size - offset)));
                    default:
                        offset %= size;
                        return (Int16) ((value >> offset) | (value << (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RotateRight(this UInt16 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt16) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (UInt16) ((value << offset) | (value >> (size - offset)));
                    default:
                        offset %= size;
                        return (UInt16) ((value >> offset) | (value << (size - offset)));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RotateRight(this Int32 value, Int32 offset)
        {
            const Int32 size = sizeof(Int32) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value << offset) | (value >> (size - offset));
                    default:
                        offset %= size;
                        return (value >> offset) | (value << (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RotateRight(this UInt32 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt32) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value << offset) | (value >> (size - offset));
                    default:
                        offset %= size;
                        return (value >> offset) | (value << (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RotateRight(this Int64 value, Int32 offset)
        {
            const Int32 size = sizeof(Int64) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value << offset) | (value >> (size - offset));
                    default:
                        offset %= size;
                        return (value >> offset) | (value << (size - offset));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RotateRight(this UInt64 value, Int32 offset)
        {
            const Int32 size = sizeof(UInt64) * 8;

            unchecked
            {
                switch (offset)
                {
                    case 0:
                        return value;
                    case < 0:
                        offset = -offset % size;
                        return (value << offset) | (value >> (size - offset));
                    default:
                        offset %= size;
                        return (value >> offset) | (value << (size - offset));
                }
            }
        }

        public static Int16 ToInt16(SByte high, SByte low)
        {
            unchecked
            {
                return ToInt16(high, (Byte) low);
            }
        }

        public static Int16 ToInt16(SByte high, Byte low)
        {
            unchecked
            {
                return (Int16) (high << (sizeof(SByte) * 8) | low);
            }
        }

        public static UInt16 ToUInt16(Byte high, Byte low)
        {
            return (UInt16) ((high << (sizeof(Byte) * 8)) | low);
        }

        public static Int32 ToInt32(Int16 high, Int16 low)
        {
            unchecked
            {
                return ToInt32(high, (UInt16) low);
            }
        }

        public static Int32 ToInt32(Int16 high, UInt16 low)
        {
            return high << (sizeof(Int16) * 8) | low;
        }

        public static UInt32 ToUInt32(UInt16 high, UInt16 low)
        {
            return ((UInt32) high << (sizeof(UInt16) * 8)) | low;
        }

        public static Int64 ToInt64(Int32 high, Int32 low)
        {
            unchecked
            {
                return ToInt64(high, (UInt32) low);
            }
        }

        public static Int64 ToInt64(Int32 high, UInt32 low)
        {
            return (Int64) high << (sizeof(Int32) * 8) | low;
        }

        public static UInt64 ToUInt64(UInt32 high, UInt32 low)
        {
            return ((UInt64) high << (sizeof(UInt32) * 8)) | low;
        }

        public static SByte High(this Int16 value)
        {
            unchecked
            {
                return (SByte) (value >> sizeof(SByte) * 8);
            }
        }

        public static SByte Low(this Int16 value)
        {
            unchecked
            {
                return (SByte) (value & SByte.MinValue);
            }
        }

        public static Byte High(this UInt16 value)
        {
            unchecked
            {
                return (Byte) (value >> sizeof(Byte) * 8);
            }
        }

        public static Byte Low(this UInt16 value)
        {
            unchecked
            {
                return (Byte) (value & Byte.MaxValue);
            }
        }

        public static Int16 High(this Int32 value)
        {
            unchecked
            {
                return (Int16) (value >> sizeof(Int16) * 8);
            }
        }

        public static Int16 Low(this Int32 value)
        {
            unchecked
            {
                return (Int16) (value & Int16.MinValue);
            }
        }

        public static UInt16 High(this UInt32 value)
        {
            unchecked
            {
                return (UInt16) (value >> sizeof(UInt16) * 8);
            }
        }

        public static UInt16 Low(this UInt32 value)
        {
            unchecked
            {
                return (UInt16) (value & UInt16.MaxValue);
            }
        }

        public static Int32 High(this Int64 value)
        {
            unchecked
            {
                return (Int32) (value >> sizeof(Int32) * 8);
            }
        }

        public static Int32 Low(this Int64 value)
        {
            unchecked
            {
                return (Int32) (value & Int32.MinValue);
            }
        }

        public static UInt32 High(this UInt64 value)
        {
            unchecked
            {
                return (UInt32) (value >> sizeof(UInt32) * 8);
            }
        }

        public static UInt32 Low(this UInt64 value)
        {
            unchecked
            {
                return (UInt32) (value & UInt32.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this SByte value)
        {
            unchecked
            {
                return PopCount((UInt32) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this Byte value)
        {
            return BitOperations.PopCount(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this Int16 value)
        {
            unchecked
            {
                return PopCount((UInt32) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this UInt16 value)
        {
            return BitOperations.PopCount(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this Int32 value)
        {
            unchecked
            {
                return PopCount((UInt32) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this UInt32 value)
        {
            return BitOperations.PopCount(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this Int64 value)
        {
            unchecked
            {
                return PopCount((UInt64) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 PopCount(this UInt64 value)
        {
            return BitOperations.PopCount(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 PopCount(this Single value)
        {
            return PopCount(Unsafe.Read<UInt32>(&value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 PopCount(this Double value)
        {
            return BitOperations.PopCount(Unsafe.Read<UInt64>(&value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe Int32 PopCount(this Decimal value)
        {
            void* pointer = &value;
            return BitOperations.PopCount(Unsafe.Read<UInt64>(pointer)) + BitOperations.PopCount(Unsafe.Read<UInt64>(UnsafeUtilities.Add(pointer, sizeof(UInt64))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 PopCount<T>(this T value) where T : unmanaged
        {
            return PopCount(&value, sizeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 PopCount<T>(in T value) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return PopCount(pointer, sizeof(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Int32 PopCount(void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return 0;
            }

            unchecked
            {
                Int32 count = 0;
                Int32 position = 0;

                while (position < length)
                {
                    switch (length - position)
                    {
                        case >= sizeof(UInt64):
                            count += BitOperations.PopCount(Unsafe.Read<UInt64>((void*) ((UIntPtr) pointer + position)));
                            position += sizeof(UInt64);

                            break;
                        case >= sizeof(UInt32):
                            count += BitOperations.PopCount(Unsafe.Read<UInt32>((void*) ((UIntPtr) pointer + position)));
                            position += sizeof(UInt32);

                            break;
                        case >= sizeof(UInt16):
                            count += BitOperations.PopCount(Unsafe.Read<UInt16>((void*) ((UIntPtr) pointer + position)));
                            position += sizeof(UInt16);

                            break;
                        case >= sizeof(Byte):
                            count += BitOperations.PopCount(Unsafe.Read<Byte>((void*) ((UIntPtr) pointer + position)));
                            position += sizeof(Byte);

                            break;
                    }
                }

                return count;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BitwiseEquals<T>(this T[] source, ReadOnlySpan<T> sequence) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsSpan().BitwiseEquals(sequence);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean BitwiseEquals<T>(this Span<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            fixed (void* pf = first)
            fixed (void* ps = second)
            {
                return BitwiseEquals(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean BitwiseEquals<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            unchecked
            {
                for (Int32 i = 0; i < sizeof(T); i++)
                {
                    if (pf[i] != ps[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean BitwiseEquals(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return true;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (Int32 i = 0; i < length; i++)
                {
                    if (ps[i] != pp[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean BitwiseEquals<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseEquals<T>((Byte*) &first, (Byte*) &second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean BitwiseEquals<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseEquals<T>((Byte*) pf, (Byte*) ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseNot<T>(this T[] source) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseNot();
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseNot<T>(this Span<T> source) where T : unmanaged
        {
            fixed (void* pointer = source)
            {
                BitwiseNot(pointer, source.Length * sizeof(T));
                return source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseNot<T>(Byte* source) where T : unmanaged
        {
            Byte* pointer = (Byte*) &source;

            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (~pointer[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseNot(void* source, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* pointer = (Byte*) source;

            unchecked
            {
                for (Int32 i = 0; i < length; i++)
                {
                    pointer[i] = (Byte) (~pointer[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseNot<T>(this T value) where T : unmanaged
        {
            return BitwiseNot<T>((Byte*) &value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseNot<T>(in T value) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return BitwiseNot<T>((Byte*) pointer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseAnd<T>(this T[] source, ReadOnlySpan<T> sequence) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseAnd(sequence);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseAnd<T>(this Span<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            fixed (void* pf = first)
            fixed (void* ps = second)
            {
                BitwiseAnd(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseAnd<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (pf[i] & ps[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseAnd(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (Int32 i = 0; i < length; i++)
                {
                    ps[i] = (Byte) (ps[i] & pp[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseAnd<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseAnd<T>((Byte*) &first, (Byte*) &second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseAnd<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseAnd<T>((Byte*) pf, (Byte*) ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseOr<T>(this T[] source, ReadOnlySpan<T> sequence) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseOr(sequence);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseOr<T>(this Span<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            fixed (void* pf = first)
            fixed (void* ps = second)
            {
                BitwiseOr(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseOr<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (pf[i] | ps[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseOr(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (Int32 i = 0; i < length; i++)
                {
                    ps[i] = (Byte) (ps[i] | pp[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseOr<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseOr<T>((Byte*) &first, (Byte*) &second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseOr<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseOr<T>((Byte*) pf, (Byte*) ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseXor<T>(this T[] source, ReadOnlySpan<T> sequence) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseXor(sequence);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseXor<T>(this Span<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            fixed (void* pf = first)
            fixed (void* ps = second)
            {
                BitwiseXor(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseXor<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (pf[i] ^ ps[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseXor(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (Int32 i = 0; i < length; i++)
                {
                    ps[i] = (Byte) (ps[i] ^ pp[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseXor<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseXor<T>((Byte*) &first, (Byte*) &second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseXor<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseXor<T>((Byte*) pf, (Byte*) ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseShiftLeft<T>(this T[] source, Int32 shift) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseShiftLeft(shift);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseShiftLeft<T>(this Span<T> source, Int32 shift) where T : unmanaged
        {
            fixed (void* pointer = source)
            {
                BitwiseShiftLeft(pointer, source.Length * sizeof(T), shift);
                return source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseShiftLeft<T>(Byte* source, Int32 shift) where T : unmanaged
        {
            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            fixed (void* pointer = alloc)
            {
                Unsafe.CopyBlock(pointer, source, (UInt32) alloc.Length);
                BitwiseShiftLeft(pointer, alloc.Length, shift);
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseShiftLeft(void* source, Int32 length, Int32 shift)
        {
            if (length <= 0 || shift == 0)
            {
                return;
            }

            if (shift < 0)
            {
                BitwiseShiftRight(source, length, -shift);
                return;
            }

            if (shift >= length * 8)
            {
                UnsafeUtilities.Fill(source, length);
                return;
            }

            unchecked
            {
                Byte* pointer = (Byte*) source;

                Int32 offset = Math.DivRem(shift, 8, out shift);
                Byte carry = (Byte) ((1 << shift) - 1);

                for (Int32 i = 0; i < length; i++)
                {
                    Int32 position = i + offset;
                    if (position >= length)
                    {
                        pointer[i] = Byte.MinValue;
                        continue;
                    }

                    pointer[i] = position + 1 < length
                        ? (Byte) ((Byte) (pointer[position] << shift) | (Byte) (pointer[position + 1] >> (8 - shift) & carry))
                        : (Byte) (pointer[position] << shift);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseShiftLeft<T>(this T value, Int32 shift) where T : unmanaged
        {
            return BitwiseShiftLeft<T>((Byte*) &value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseShiftLeft<T>(in T value, Int32 shift) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return BitwiseShiftLeft<T>((Byte*) pointer, shift);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] BitwiseShiftRight<T>(this T[] source, Int32 shift) where T : unmanaged
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AsSpan().BitwiseShiftRight(shift);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> BitwiseShiftRight<T>(this Span<T> source, Int32 shift) where T : unmanaged
        {
            fixed (void* pointer = source)
            {
                BitwiseShiftRight(pointer, source.Length * sizeof(T), shift);
                return source;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseShiftRight<T>(Byte* source, Int32 shift) where T : unmanaged
        {
            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            fixed (void* pointer = alloc)
            {
                Unsafe.CopyBlock(pointer, source, (UInt32) alloc.Length);
                BitwiseShiftRight(pointer, alloc.Length, shift);
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseShiftRight(void* source, Int32 length, Int32 shift)
        {
            if (length <= 0 || shift == 0)
            {
                return;
            }

            if (shift < 0)
            {
                BitwiseShiftLeft(source, length, -shift);
                return;
            }

            if (shift >= length * 8)
            {
                UnsafeUtilities.Fill(source, length);
                return;
            }

            unchecked
            {
                Byte* pointer = (Byte*) source;

                Int32 offset = Math.DivRem(shift, 8, out shift);
                Byte carry = (Byte) (0xFF << (8 - shift));

                for (Int32 i = length - 1; i >= 0; i--)
                {
                    Int32 position = i - offset;
                    if (position < 0)
                    {
                        pointer[i] = Byte.MinValue;
                        continue;
                    }

                    pointer[i] = position - 1 >= 0
                        ? (Byte) ((Byte) ((0xff & pointer[position]) >> shift) | (Byte) (pointer[position - 1] << (8 - shift) & carry))
                        : (Byte) ((0xff & pointer[position]) >> shift);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseShiftRight<T>(this T value, Int32 shift) where T : unmanaged
        {
            return BitwiseShiftRight<T>((Byte*) &value, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T BitwiseShiftRight<T>(in T value, Int32 shift) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return BitwiseShiftRight<T>((Byte*) pointer, shift);
            }
        }
    }
}