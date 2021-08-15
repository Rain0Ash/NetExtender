// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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