// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Times
{
    public struct DateTimeProvider : IUnsafeSize, IStruct<DateTimeProvider>
    {
        public static class Utc
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static DateTimeProvider Provider
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return new DateTimeProvider(DateTimeKind.Utc);
                }
            }
        }

        public static implicit operator Boolean(DateTimeProvider value)
        {
            return !value.IsEmpty;
        }

        public static DateTimeProvider Provider
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new DateTimeProvider(DateTimeKind.Local);
            }
        }

        public Type Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return typeof(DateTimeProvider);
            }
        }

        unsafe Int32 IUnsafeSize.Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return sizeof(DateTimeProvider);
            }
        }

        unsafe Int32 IUnsafeSize.Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return sizeof(DateTimeProvider);
            }
        }

        public readonly Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _unsafe.IsEmpty;
            }
        }

        private Unsafe _unsafe;

        public DateTimeKind Kind
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return _unsafe.Kind;
            }
            set
            {
                _unsafe = value switch
                {
                    DateTimeKind.Unspecified => Unsafe.Now,
                    DateTimeKind.Utc => Unsafe.UtcNow,
                    DateTimeKind.Local => Unsafe.Now,
                    _ => throw new EnumUndefinedOrNotSupportedException<DateTimeKind>(value, nameof(value), null)
                };
            }
        }

        public readonly DateTime Now
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _unsafe.Get();
            }
        }

        public readonly DateTimeOffset Offset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _unsafe.Offset();
            }
        }

        public DateTimeProvider(DateTimeKind kind)
        {
            _unsafe = kind switch
            {
                DateTimeKind.Unspecified => Unsafe.Now,
                DateTimeKind.Utc => Unsafe.UtcNow,
                DateTimeKind.Local => Unsafe.Now,
                _ => throw new EnumUndefinedOrNotSupportedException<DateTimeKind>(kind, nameof(kind), null)
            };
        }
        
        public unsafe ref Byte GetPinnableReference()
        {
            fixed (Byte* pointer = this)
            {
                return ref *pointer;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        private readonly unsafe struct Unsafe : IUnsafeSize, IStruct<Unsafe>
        {
            public static Unsafe Now { get; } = new Unsafe(DateTimeKind.Local, &GetNow, &GetOffsetNow);
            public static Unsafe UtcNow { get; } = new Unsafe(DateTimeKind.Utc, &GetUtcNow, &GetOffsetUtcNow);
            
            public Type Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return typeof(Unsafe);
                }
            }

            public Int32 Length
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return sizeof(Unsafe);
                }
            }

            public Int32 Size
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return sizeof(Unsafe);
                }
            }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _now == null || _offset == null;
                }
            }

            public DateTimeKind Kind { get; }
            private readonly delegate*<DateTime> _now;
            private readonly delegate*<DateTimeOffset> _offset;

            private Unsafe(DateTimeKind kind, delegate*<DateTime> now, delegate*<DateTimeOffset> offset)
            {
                Kind = kind;
                _now = now;
                _offset = offset;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public DateTime Get()
            {
                return _now();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public DateTimeOffset Offset()
            {
                return _offset();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            private static DateTime GetNow()
            {
                return DateTime.Now;
            }
        
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            private static DateTime GetUtcNow()
            {
                return DateTime.UtcNow;
            }
        
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            private static DateTimeOffset GetOffsetNow()
            {
                return DateTimeOffset.Now;
            }
        
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            private static DateTimeOffset GetOffsetUtcNow()
            {
                return DateTimeOffset.UtcNow;
            }
            
            public ref Byte GetPinnableReference()
            {
                fixed (Byte* pointer = this)
                {
                    return ref *pointer;
                }
            }
        }
    }
}