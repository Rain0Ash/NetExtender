// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Numerics
{
    public static partial class BitUtilities
    {
        public const Int32 BitInByte = 8;
        private static ImmutableArray<ImmutableArray<Byte>> SetBitTable { get; }

        static BitUtilities()
        {
            const Byte range = BitInByte;
            SetBitTable = MathUtilities.Range(Byte.MaxValue + 1).Select(static i => MathUtilities.Range(range).Where(bit => (i & (1 << bit)) != 0).ToImmutableArray()).ToImmutableArray();
        }

        public static void Deconstruct(this Char value, out Byte high, out Byte low)
        {
            unchecked
            {
                high = (Byte) (value >> (sizeof(Byte) * BitInByte));
                low = (Byte) (value & Byte.MaxValue);
            }
        }

        public static void Deconstruct(this Int16 value, out Byte high, out Byte low)
        {
            unchecked
            {
                high = (Byte) (value >> (sizeof(Byte) * BitInByte));
                low = (Byte) (value & Byte.MaxValue);
            }
        }

        public static void Deconstruct(this UInt16 value, out Byte high, out Byte low)
        {
            unchecked
            {
                high = (Byte) (value >> (sizeof(Byte) * BitInByte));
                low = (Byte) (value & Byte.MaxValue);
            }
        }

        public static void Deconstruct(this Int32 value, out UInt16 high, out UInt16 low)
        {
            unchecked
            {
                high = (UInt16) (value >> (sizeof(UInt16) * BitInByte));
                low = (UInt16) (value & UInt16.MaxValue);
            }
        }

        public static void Deconstruct(this UInt32 value, out UInt16 high, out UInt16 low)
        {
            unchecked
            {
                high = (UInt16) (value >> (sizeof(UInt16) * BitInByte));
                low = (UInt16) (value & UInt16.MaxValue);
            }
        }

        public static void Deconstruct(this Int64 value, out UInt32 high, out UInt32 low)
        {
            unchecked
            {
                high = (UInt32) (value >> (sizeof(UInt32) * BitInByte));
                low = (UInt32) (value & UInt32.MaxValue);
            }
        }

        public static void Deconstruct(this UInt64 value, out UInt32 high, out UInt32 low)
        {
            unchecked
            {
                high = (UInt32) (value >> (sizeof(UInt32) * BitInByte));
                low = (UInt32) (value & UInt32.MaxValue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16(SByte high, SByte low)
        {
            unchecked
            {
                return ToInt16(high, (Byte) low);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16(SByte high, Byte low)
        {
            unchecked
            {
                return (Int16) (high << (sizeof(SByte) * BitInByte) | low);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16(Byte high, Byte low)
        {
            return (UInt16) ((high << (sizeof(Byte) * BitInByte)) | low);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32(Int16 high, Int16 low)
        {
            unchecked
            {
                return ToInt32(high, (UInt16) low);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32(Int16 high, UInt16 low)
        {
            return high << (sizeof(Int16) * BitInByte) | low;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32(UInt16 high, UInt16 low)
        {
            return ((UInt32) high << (sizeof(UInt16) * BitInByte)) | low;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64(Int32 high, Int32 low)
        {
            unchecked
            {
                return ToInt64(high, (UInt32) low);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64(Int32 high, UInt32 low)
        {
            return (Int64) high << (sizeof(Int32) * BitInByte) | low;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64(UInt32 high, UInt32 low)
        {
            return ((UInt64) high << (sizeof(UInt32) * BitInByte)) | low;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetSetBits(Byte value, Span<Byte> destination, out Int32 written)
        {
            written = 0;
            ImmutableArray<Byte> position = SetBitTable[value];

            if (destination.Length < position.Length)
            {
                return false;
            }

            for (Int32 i = 0; i < position.Length; i++)
            {
                destination[i] = position[i];
                written++;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetSetBits(Byte value, Span<Int32> destination, out Int32 written)
        {
            written = 0;
            ImmutableArray<Byte> position = SetBitTable[value];

            if (destination.Length < position.Length)
            {
                return false;
            }

            for (Int32 i = 0; i < position.Length; i++)
            {
                destination[i] = position[i];
                written++;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetSetBits(Byte value, Span<UInt32> destination, out Int32 written)
        {
            written = 0;
            ImmutableArray<Byte> position = SetBitTable[value];

            if (destination.Length < position.Length)
            {
                return false;
            }

            for (Int32 i = 0; i < position.Length; i++)
            {
                destination[i] = position[i];
                written++;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits<T>(this T value, Span<Int32> destination, out Int32 written) where T : unmanaged
        {
            return TryGetSetBits(&value, sizeof(T), destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits<T>(in T value, Span<Int32> destination, out Int32 written) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return TryGetSetBits(pointer, sizeof(T), destination, out written);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits(void* pointer, Int32 length, Span<Int32> destination, out Int32 written)
        {
            if (length > 0)
            {
                return TryGetSetBits(pointer, (UInt32) length, destination, out written);
            }

            written = 0;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean TryGetSetBits(void* pointer, UInt32 length, Span<Int32> destination, out Int32 written)
        {
            written = 0;
            if (length <= 0)
            {
                return true;
            }

            Span<Int32> position = stackalloc Int32[BitInByte];
            for (Int32 i = 0; i < length; i++)
            {
                Byte value = ((Byte*) pointer)[i];
                if (!TryGetSetBits(value, position, out Int32 count))
                {
                    return false;
                }

                if (destination.Length < written + count)
                {
                    return false;
                }

                for (Int32 j = 0; j < count; j++)
                {
                    destination[written++] = position[j] + BitInByte * i;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits<T>(this T value, Span<UInt32> destination, out Int32 written) where T : unmanaged
        {
            return TryGetSetBits(&value, sizeof(T), destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits<T>(in T value, Span<UInt32> destination, out Int32 written) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return TryGetSetBits(pointer, sizeof(T), destination, out written);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean TryGetSetBits(void* pointer, Int32 length, Span<UInt32> destination, out Int32 written)
        {
            if (length > 0)
            {
                return TryGetSetBits(pointer, (UInt32) length, destination, out written);
            }

            written = 0;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean TryGetSetBits(void* pointer, UInt32 length, Span<UInt32> destination, out Int32 written)
        {
            written = 0;
            if (length <= 0)
            {
                return true;
            }

            Span<UInt32> position = stackalloc UInt32[BitInByte];
            for (UInt32 i = 0; i < length; i++)
            {
                Byte value = ((Byte*) pointer)[i];
                if (!TryGetSetBits(value, position, out Int32 count))
                {
                    return false;
                }

                if (destination.Length < written + count)
                {
                    return false;
                }

                for (Int32 j = 0; j < count; j++)
                {
                    destination[written++] = position[j] + BitInByte * i;
                }
            }

            return true;
        }

        //TODO: BitUtilities Trailing/leading zeros for void*

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 BitwisePopCount(this Single value)
        {
            return BitOperations.PopCount(Unsafe.Read<UInt32>(&value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 BitwisePopCount(this Double value)
        {
            return BitOperations.PopCount(Unsafe.Read<UInt64>(&value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe Int32 BitwisePopCount(this Decimal value)
        {
            void* pointer = &value;
            return BitOperations.PopCount(Unsafe.Read<UInt64>(pointer)) + BitOperations.PopCount(Unsafe.Read<UInt64>(UnsafeUtilities.Add(pointer, sizeof(UInt64))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 BitwisePopCount<T>(this T value) where T : unmanaged
        {
            return BitwisePopCount(&value, sizeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 BitwisePopCount<T>(in T value) where T : unmanaged
        {
            fixed (T* pointer = &value)
            {
                return BitwisePopCount(pointer, sizeof(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Int32 BitwisePopCount(void* pointer, Int32 length)
        {
            return length > 0 ? (Int32) BitwisePopCount(pointer, (UInt32) length) : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe UInt32 BitwisePopCount(void* pointer, UInt32 length)
        {
            if (length <= 0)
            {
                return 0;
            }

            unchecked
            {
                UInt32 count = 0;
                UInt32 position = 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static T Read<T>(void* pointer, UInt32 position) where T : unmanaged
                {
                    return Unsafe.Read<T>(UnsafeUtilities.Add(pointer, position));
                }

                while (position < length)
                {
                    switch (length - position)
                    {
                        case > sizeof(UInt64):
                            count += (UInt32) BitOperations.PopCount(Read<UInt64>(pointer, position));
                            position += sizeof(UInt64);

                            break;

                        case sizeof(UInt64):
                            return count + (UInt32) BitOperations.PopCount(Read<UInt64>(pointer, position));

                        case sizeof(UInt32) + sizeof(UInt16) + sizeof(Byte):
                            count += (UInt32) BitOperations.PopCount(Read<UInt32>(pointer, position));
                            position += sizeof(UInt32);
                            goto case sizeof(UInt16) + sizeof(Byte);

                        case sizeof(UInt32) + sizeof(UInt16):
                            count += (UInt32) BitOperations.PopCount(Read<UInt32>(pointer, position));
                            position += sizeof(UInt32);
                            goto case sizeof(UInt16);

                        case sizeof(UInt32) + sizeof(Byte):
                            count += (UInt32) BitOperations.PopCount(Read<UInt32>(pointer, position));
                            position += sizeof(UInt32);
                            goto case sizeof(Byte);

                        case sizeof(UInt32):
                            return count + (UInt32) BitOperations.PopCount(Read<UInt32>(pointer, position));

                        case sizeof(UInt16) + sizeof(Byte):
                            count += (UInt32) BitOperations.PopCount(Read<UInt16>(pointer, position));
                            position += sizeof(UInt16);
                            goto case sizeof(Byte);

                        case sizeof(UInt16):
                            return count + (UInt32) BitOperations.PopCount(Read<UInt16>(pointer, position));

                        case sizeof(Byte):
                            return count + (UInt32) BitOperations.PopCount(Read<Byte>(pointer, position));

                        default:
                            return count;
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
        public static Boolean BitwiseEquals<T>(this Span<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            return BitwiseEquals((ReadOnlySpan<T>) first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean BitwiseEquals<T>(this ReadOnlySpan<T> first, ReadOnlySpan<T> second) where T : unmanaged
        {
            fixed (void* pf = first, ps = second)
            {
                return BitwiseEquals(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean BitwiseEquals<T>(Byte* first, Byte* second) where T : unmanaged
        {
            unchecked
            {
                for (UInt32 i = 0; i < sizeof(T); i++)
                {
                    if (first[i] != second[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Boolean BitwiseEquals(void* source, void* pointer, Int32 length)
        {
            return length <= 0 || BitwiseEquals(source, pointer, (UInt32) length);
        }

        //TODO: optimize
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe Boolean BitwiseEquals(void* source, void* pointer, UInt32 length)
        {
            if (length <= 0)
            {
                return true;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (UInt32 i = 0; i < length; i++)
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
            fixed (T* pf = &first, ps = &second)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseNot(void* source, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseNot(source, (UInt32) length);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseNot(void* source, UInt32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* pointer = (Byte*) source;

            unchecked
            {
                for (UInt32 i = 0; i < length; i++)
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
            fixed (void* pf = first, ps = second)
            {
                BitwiseAnd(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseAnd<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (first[i] & second[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseAnd(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseAnd(source, pointer, (UInt32) length);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseAnd(void* source, void* pointer, UInt32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (UInt32 i = 0; i < length; i++)
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
            fixed (void* pf = first, ps = second)
            {
                BitwiseOr(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseOr<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (first[i] | second[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseOr(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseOr(source, pointer, (UInt32) length);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseOr(void* source, void* pointer, UInt32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (UInt32 i = 0; i < length; i++)
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
            fixed (void* pf = first, ps = second)
            {
                BitwiseXor(pf, ps, Math.Min(first.Length, second.Length) * sizeof(T));
                return first;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe T BitwiseXor<T>(Byte* first, Byte* second) where T : unmanaged
        {
            Span<Byte> alloc = stackalloc Byte[sizeof(T)];

            unchecked
            {
                for (Int32 i = 0; i < alloc.Length; i++)
                {
                    alloc[i] = (Byte) (first[i] ^ second[i]);
                }
            }

            return MemoryMarshal.Read<T>(alloc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseXor(void* source, void* pointer, Int32 length)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseXor(source, pointer, (UInt32) length);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseXor(void* source, void* pointer, UInt32 length)
        {
            if (length <= 0)
            {
                return;
            }

            Byte* ps = (Byte*) source;
            Byte* pp = (Byte*) pointer;

            unchecked
            {
                for (UInt32 i = 0; i < length; i++)
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
            fixed (T* pf = &first, ps = &second)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseShiftLeft(void* source, Int32 length, Int32 shift)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseShiftLeft(source, (UInt32) length, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseShiftLeft(void* source, UInt32 length, Int32 shift)
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

            if ((UInt64) shift >= (UInt64) length * BitInByte)
            {
                UnsafeUtilities.Fill(source, length);
                return;
            }

            unchecked
            {
                Byte* pointer = (Byte*) source;
                Int32 offset = Math.DivRem(shift, BitInByte, out shift);

                if (shift <= 0)
                {
                    for (Int64 i = length - 1; i >= offset; --i)
                    {
                        pointer[i] = pointer[i - offset];
                    }

                    UnsafeUtilities.Fill(pointer, offset);
                    return;
                }

                Int32 suboffset = BitInByte - shift;

                for (Int64 i = length - 1; i > offset; --i)
                {
                    pointer[i] = (Byte) ((pointer[i - offset] << shift) | (pointer[i - offset - 1] >> suboffset));
                }

                pointer[offset] = (Byte) (pointer[0] << shift);
                UnsafeUtilities.Fill(pointer, offset);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void BitwiseShiftRight(void* source, Int32 length, Int32 shift)
        {
            if (length <= 0)
            {
                return;
            }

            BitwiseShiftRight(source, (UInt32) length, shift);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void BitwiseShiftRight(void* source, UInt32 length, Int32 shift)
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

            if ((UInt64) shift >= (UInt64) length * BitInByte)
            {
                UnsafeUtilities.Fill(source, length);
                return;
            }

            unchecked
            {
                Byte* pointer = (Byte*) source;
                Int32 offset = Math.DivRem(shift, BitInByte, out shift);
                UInt32 limit = length - (UInt32) offset - 1;

                if (shift <= 0)
                {
                    for (Int64 i = 0; i <= limit; i++)
                    {
                        pointer[i] = pointer[i + offset];
                    }

                    UnsafeUtilities.Fill(UnsafeUtilities.Add(pointer, limit + 1), length);
                    return;
                }

                Int32 suboffset = BitInByte - shift;

                for (Int64 i = 0; i < limit; i++)
                {
                    pointer[i] = (Byte) ((pointer[i + offset] >> shift) | pointer[i + offset + 1] << suboffset);
                }

                pointer[limit] = (Byte) (pointer[length - 1] >> shift);
                UnsafeUtilities.Fill(UnsafeUtilities.Add(pointer, limit + 1), length);
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

        //TODO: bitwise rotate
    }
}