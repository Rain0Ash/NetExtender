// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Utils.Types
{
    public static class BitUtils
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

        public static Int16 ToInt16(SByte left, SByte right)
        {
            unchecked
            {
                return ToInt16(left, (Byte) right);
            }
        }

        public static Int16 ToInt16(SByte left, Byte right)
        {
            unchecked
            {
                return (Int16) (left << 8 | right);
            }
        }

        public static UInt16 ToUInt16(Byte left, Byte right)
        {
            return (UInt16) ((left << 8) | right);
        }

        public static Int32 ToInt32(Int16 left, Int16 right)
        {
            unchecked
            {
                return ToInt32(left, (UInt16) right);
            }
        }

        public static Int32 ToInt32(Int16 left, UInt16 right)
        {
            return left << 16 | right;
        }

        public static UInt32 ToUInt32(UInt16 left, UInt16 right)
        {
            return ((UInt32) left << 16) | right;
        }

        public static Int64 ToInt64(Int32 left, Int32 right)
        {
            unchecked
            {
                return ToInt64(left, (UInt32) right);
            }
        }

        public static Int64 ToInt64(Int32 left, UInt32 right)
        {
            return (Int64) left << 32 | right;
        }

        public static UInt64 ToUInt64(UInt32 left, UInt32 right)
        {
            return ((UInt64) left << 32) | right;
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
        private static unsafe Boolean BitwiseEquals<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            for (Int32 i = 0; i < sizeof(T); i++)
            {
                if (pf[i] != ps[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static unsafe Boolean BitwiseEquals<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseEquals<T>((Byte*) &first, (Byte*) &second);
        }

        public static unsafe Boolean BitwiseEquals<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseEquals<T>((Byte*) pf, (Byte*) ps);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe T BitwiseAnd<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Int32 size = sizeof(T);
            Span<Byte> bytes = stackalloc Byte[size];

            for (Int32 i = 0; i < size; i++)
            {
                bytes[i] = (Byte) (pf[i] & ps[i]);
            }

            return MemoryMarshal.Read<T>(bytes);
        }

        public static unsafe T BitwiseAnd<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseAnd<T>((Byte*) &first, (Byte*) &second);
        }

        public static unsafe T BitwiseAnd<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseAnd<T>((Byte*) pf, (Byte*) ps);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe T BitwiseOr<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Int32 size = sizeof(T);
            Span<Byte> bytes = stackalloc Byte[size];

            for (Int32 i = 0; i < size; i++)
            {
                bytes[i] = (Byte) (pf[i] | ps[i]);
            }

            return MemoryMarshal.Read<T>(bytes);
        }

        public static unsafe T BitwiseOr<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseOr<T>((Byte*) &first, (Byte*) &second);
        }

        public static unsafe T BitwiseOr<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseOr<T>((Byte*) pf, (Byte*) ps);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe T BitwiseXor<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Byte* pf = (Byte*) &first;
            Byte* ps = (Byte*) &second;

            Int32 size = sizeof(T);
            Span<Byte> bytes = stackalloc Byte[size];

            for (Int32 i = 0; i < size; i++)
            {
                bytes[i] = (Byte) (pf[i] ^ ps[i]);
            }

            return MemoryMarshal.Read<T>(bytes);
        }

        public static unsafe T BitwiseXor<T>(this T first, T second) where T : unmanaged
        {
            return BitwiseXor<T>((Byte*) &first, (Byte*) &second);
        }

        public static unsafe T BitwiseXor<T>(in T first, in T second) where T : unmanaged
        {
            fixed (T* pf = &first)
            fixed (T* ps = &second)
            {
                return BitwiseXor<T>((Byte*) pf, (Byte*) ps);
            }
        }
    }
}