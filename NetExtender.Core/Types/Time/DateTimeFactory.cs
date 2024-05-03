using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Sizes.Interfaces;

namespace NetExtender.Types.Times
{
    public struct DateTimeFactory : IUnsafeSize
    {
        public static class Utc
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static DateTimeFactory Factory
            {
                get
                {
                    return new DateTimeFactory(DateTimeKind.Utc);
                }
            }
        }

        public static DateTimeFactory Factory
        {
            get
            {
                return new DateTimeFactory(DateTimeKind.Local);
            }
        }

        public Type Type
        {
            get
            {
                return typeof(DateTimeFactory);
            }
        }

        unsafe Int32 IUnsafeSize.Length
        {
            get
            {
                return sizeof(DateTimeFactory);
            }
        }

        unsafe Int32 IUnsafeSize.Size
        {
            get
            {
                return sizeof(DateTimeFactory);
            }
        }

        public readonly Boolean IsEmpty
        {
            get
            {
                return _unsafe.IsEmpty;
            }
        }

        private Unsafe _unsafe;

        public DateTimeKind Kind
        {
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
            get
            {
                return _unsafe.Get();
            }
        }

        public readonly DateTimeOffset Offset
        {
            get
            {
                return _unsafe.Offset();
            }
        }

        public DateTimeFactory(DateTimeKind kind)
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
        
        private readonly unsafe struct Unsafe : IUnsafeSize
        {
            public static Unsafe Now { get; } = new Unsafe(DateTimeKind.Local, &GetNow, &GetOffsetNow);
            public static Unsafe UtcNow { get; } = new Unsafe(DateTimeKind.Utc, &GetUtcNow, &GetOffsetUtcNow);
            
            public Type Type
            {
                get
                {
                    return typeof(Unsafe);
                }
            }

            public Int32 Length
            {
                get
                {
                    return sizeof(Unsafe);
                }
            }

            public Int32 Size
            {
                get
                {
                    return sizeof(Unsafe);
                }
            }

            public Boolean IsEmpty
            {
                get
                {
                    return _now == default || _offset == default;
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