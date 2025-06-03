// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class GuidUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid NewGuid()
        {
            return NewGuid(DateTimeOffset.UtcNow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid NewGuid(this DateTime timestamp)
        {
            return Create(Guid.NewGuid(), timestamp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid NewGuid(this DateTimeOffset timestamp)
        {
            return Create(Guid.NewGuid(), timestamp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid Create(this Guid value, DateTime timestamp)
        {
            return Create(value, (DateTimeOffset) timestamp);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Guid Create(this Guid value, DateTimeOffset timestamp)
        {
            Span<Byte> bytes = stackalloc Byte[16];
            value.TryWriteBytes(bytes);

            WriteDateTime(bytes, timestamp);
            return new Guid(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static DateTimeOffset GetTimestamp(this Guid value)
        {
            Span<Byte> bytes = stackalloc Byte[16];
            value.TryWriteBytes(bytes);
            return ToDateTime(bytes);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WriteDateTime(Span<Byte> destination, DateTimeOffset timestamp)
        {
            Int64 unixtime = timestamp.ToUnixTimeMilliseconds();
            Span<Byte> bytes = stackalloc Byte[8];
            BinaryPrimitives.WriteInt64LittleEndian(bytes, unixtime);

            bytes.Slice(2, 4).CopyTo(destination);
            bytes.Slice(0, 2).CopyTo(destination.Slice(4));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static DateTimeOffset ToDateTime(ReadOnlySpan<Byte> value)
        {
            Span<Byte> bytes = stackalloc Byte[8];
            value.Slice(4, 2).CopyTo(bytes);
            value.Slice(0, 4).CopyTo(bytes.Slice(2));
            bytes.Slice(6).Clear();
            Int64 unixtime = BinaryPrimitives.ReadInt64LittleEndian(bytes);

            return DateTimeOffset.FromUnixTimeMilliseconds(unixtime);
        }
    }
}