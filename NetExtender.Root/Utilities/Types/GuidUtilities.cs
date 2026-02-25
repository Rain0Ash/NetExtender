// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class GuidUtilities
    {
        private const Byte Variant10xxMask = 0xC0;
        private const Byte Variant10xxValue = 0x80;
        private const UInt16 VersionMask = 0xF000;
        private const UInt16 Version7Value = 0x7000;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid NewGuid()
        {
            return NewGuid(DateTimeOffset.UtcNow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid NewGuid(this DateTime timestamp)
        {
            return NewGuid((DateTimeOffset) timestamp);
        }

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
#endif
        public static unsafe Guid NewGuid(this DateTimeOffset timestamp)
        {
#if NET9_0_OR_GREATER
            return Guid.CreateVersion7(timestamp);
#else
            Int64 milliseconds = timestamp.ToUnixTimeMilliseconds();

            if (milliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp), timestamp, null);
            }

            unchecked
            {
                Guid guid = Guid.NewGuid();
                Span<Byte> buffer = stackalloc Byte[16];
                guid.TryWriteBytes(buffer);

                Int32 a = (Int32) (milliseconds >> 16);
                Int16 b = (Int16) milliseconds;
                Int16 c = (Int16) ((((Int16) ((buffer[4] << 8) + buffer[5])) & ~VersionMask) | Version7Value);
                Byte d = (Byte) ((buffer[8] & ~Variant10xxMask) | Variant10xxValue);

                return new Guid(a, b, c, d, buffer[9], buffer[10], buffer[11], buffer[12], buffer[13], buffer[14], buffer[15]);
            }
#endif
        }
    }
}