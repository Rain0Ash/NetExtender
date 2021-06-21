// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Numerics
{
    public readonly struct CharPoint
    {
        public static CharPoint operator +(CharPoint first, CharPoint second)
        {
            Int32 x = first.X + second.X;
            Int32 y = first.Y + second.Y;
            return new CharPoint(Unsafe.As<Int32, Char>(ref x), Unsafe.As<Int32, Char>(ref y));
        }

        public static CharPoint operator -(CharPoint first, CharPoint second)
        {
            Int32 x = first.X - second.X;
            Int32 y = first.Y - second.Y;
            return new CharPoint(Unsafe.As<Int32, Char>(ref x), Unsafe.As<Int32, Char>(ref y));
        }

        public static CharPoint operator *(CharPoint first, CharPoint second)
        {
            Int32 x = first.X * second.X;
            Int32 y = first.Y * second.Y;
            return new CharPoint(Unsafe.As<Int32, Char>(ref x), Unsafe.As<Int32, Char>(ref y));
        }

        public static CharPoint operator /(CharPoint first, CharPoint second)
        {
            Int32 x = first.X / second.X;
            Int32 y = first.Y / second.Y;
            return new CharPoint(Unsafe.As<Int32, Char>(ref x), Unsafe.As<Int32, Char>(ref y));
        }

        public static CharPoint operator %(CharPoint first, CharPoint second)
        {
            Int32 x = first.X % second.X;
            Int32 y = first.Y % second.Y;
            return new CharPoint(Unsafe.As<Int32, Char>(ref x), Unsafe.As<Int32, Char>(ref y));
        }

        public Char X { get; }
        public Char Y { get; }

        public CharPoint(Char x, Char y)
        {
            X = x;
            Y = y;
        }

        public static CharPoint Zero { get; } = new CharPoint((Char)0, (Char)0);
        public static CharPoint One { get; } = new CharPoint((Char)1, (Char)1);
        public static CharPoint Minimum { get; } = new CharPoint(Char.MinValue, Char.MinValue);
        public static CharPoint Maximum { get; } = new CharPoint(Char.MaxValue, Char.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CharPoint Offset(CharPoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CharPoint Offset(Char x, Char y)
        {
            return this + new CharPoint(x, y);
        }

        public CharPoint Offset(PointOffset offset, Char count = (Char)1)
        {
            if (count == (Char)0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new CharPoint((Char)0, count),
                PointOffset.Down => this + new CharPoint((Char)0, count),
                PointOffset.Left => this - new CharPoint(count, (Char)0),
                PointOffset.Right => this + new CharPoint(count, (Char)0),
                PointOffset.UpLeft => this - new CharPoint(count, count),
                PointOffset.DownLeft => this + new CharPoint((Char)0, count) - new CharPoint(count, (Char)0),
                PointOffset.UpRight => this - new CharPoint((Char)0, count) + new CharPoint(count, (Char)0),
                PointOffset.DownRight => this + new CharPoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CharPoint Delta(CharPoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CharPoint Delta(Char count)
        {
            return this - new CharPoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CharPoint Delta(Char x, Char y)
        {
            return this - new CharPoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive()
        {
            return true;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct SBytePoint
    {
        public static SBytePoint operator +(SBytePoint first, SBytePoint second)
        {
            Int32 x = first.X + second.X;
            Int32 y = first.Y + second.Y;
            return new SBytePoint(Unsafe.As<Int32, SByte>(ref x), Unsafe.As<Int32, SByte>(ref y));
        }

        public static SBytePoint operator -(SBytePoint first, SBytePoint second)
        {
            Int32 x = first.X - second.X;
            Int32 y = first.Y - second.Y;
            return new SBytePoint(Unsafe.As<Int32, SByte>(ref x), Unsafe.As<Int32, SByte>(ref y));
        }

        public static SBytePoint operator *(SBytePoint first, SBytePoint second)
        {
            Int32 x = first.X * second.X;
            Int32 y = first.Y * second.Y;
            return new SBytePoint(Unsafe.As<Int32, SByte>(ref x), Unsafe.As<Int32, SByte>(ref y));
        }

        public static SBytePoint operator /(SBytePoint first, SBytePoint second)
        {
            Int32 x = first.X / second.X;
            Int32 y = first.Y / second.Y;
            return new SBytePoint(Unsafe.As<Int32, SByte>(ref x), Unsafe.As<Int32, SByte>(ref y));
        }

        public static SBytePoint operator %(SBytePoint first, SBytePoint second)
        {
            Int32 x = first.X % second.X;
            Int32 y = first.Y % second.Y;
            return new SBytePoint(Unsafe.As<Int32, SByte>(ref x), Unsafe.As<Int32, SByte>(ref y));
        }

        public SByte X { get; }
        public SByte Y { get; }

        public SBytePoint(SByte x, SByte y)
        {
            X = x;
            Y = y;
        }

        public static SBytePoint Zero { get; } = new SBytePoint(0, 0);
        public static SBytePoint One { get; } = new SBytePoint(1, 1);
        public static SBytePoint Minimum { get; } = new SBytePoint(SByte.MinValue, SByte.MinValue);
        public static SBytePoint Maximum { get; } = new SBytePoint(SByte.MaxValue, SByte.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SBytePoint Offset(SBytePoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SBytePoint Offset(SByte x, SByte y)
        {
            return this + new SBytePoint(x, y);
        }

        public SBytePoint Offset(PointOffset offset, SByte count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new SBytePoint(0, count),
                PointOffset.Down => this + new SBytePoint(0, count),
                PointOffset.Left => this - new SBytePoint(count, 0),
                PointOffset.Right => this + new SBytePoint(count, 0),
                PointOffset.UpLeft => this - new SBytePoint(count, count),
                PointOffset.DownLeft => this + new SBytePoint(0, count) - new SBytePoint(count, 0),
                PointOffset.UpRight => this - new SBytePoint(0, count) + new SBytePoint(count, 0),
                PointOffset.DownRight => this + new SBytePoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SBytePoint Delta(SBytePoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SBytePoint Delta(SByte count)
        {
            return this - new SBytePoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SBytePoint Delta(SByte x, SByte y)
        {
            return this - new SBytePoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct BytePoint
    {
        public static BytePoint operator +(BytePoint first, BytePoint second)
        {
            Int32 x = first.X + second.X;
            Int32 y = first.Y + second.Y;
            return new BytePoint(Unsafe.As<Int32, Byte>(ref x), Unsafe.As<Int32, Byte>(ref y));
        }

        public static BytePoint operator -(BytePoint first, BytePoint second)
        {
            Int32 x = first.X - second.X;
            Int32 y = first.Y - second.Y;
            return new BytePoint(Unsafe.As<Int32, Byte>(ref x), Unsafe.As<Int32, Byte>(ref y));
        }

        public static BytePoint operator *(BytePoint first, BytePoint second)
        {
            Int32 x = first.X * second.X;
            Int32 y = first.Y * second.Y;
            return new BytePoint(Unsafe.As<Int32, Byte>(ref x), Unsafe.As<Int32, Byte>(ref y));
        }

        public static BytePoint operator /(BytePoint first, BytePoint second)
        {
            Int32 x = first.X / second.X;
            Int32 y = first.Y / second.Y;
            return new BytePoint(Unsafe.As<Int32, Byte>(ref x), Unsafe.As<Int32, Byte>(ref y));
        }

        public static BytePoint operator %(BytePoint first, BytePoint second)
        {
            Int32 x = first.X % second.X;
            Int32 y = first.Y % second.Y;
            return new BytePoint(Unsafe.As<Int32, Byte>(ref x), Unsafe.As<Int32, Byte>(ref y));
        }

        public Byte X { get; }
        public Byte Y { get; }

        public BytePoint(Byte x, Byte y)
        {
            X = x;
            Y = y;
        }

        public static BytePoint Zero { get; } = new BytePoint(0, 0);
        public static BytePoint One { get; } = new BytePoint(1, 1);
        public static BytePoint Minimum { get; } = new BytePoint(Byte.MinValue, Byte.MinValue);
        public static BytePoint Maximum { get; } = new BytePoint(Byte.MaxValue, Byte.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BytePoint Offset(BytePoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BytePoint Offset(Byte x, Byte y)
        {
            return this + new BytePoint(x, y);
        }

        public BytePoint Offset(PointOffset offset, Byte count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new BytePoint(0, count),
                PointOffset.Down => this + new BytePoint(0, count),
                PointOffset.Left => this - new BytePoint(count, 0),
                PointOffset.Right => this + new BytePoint(count, 0),
                PointOffset.UpLeft => this - new BytePoint(count, count),
                PointOffset.DownLeft => this + new BytePoint(0, count) - new BytePoint(count, 0),
                PointOffset.UpRight => this - new BytePoint(0, count) + new BytePoint(count, 0),
                PointOffset.DownRight => this + new BytePoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BytePoint Delta(BytePoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BytePoint Delta(Byte count)
        {
            return this - new BytePoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BytePoint Delta(Byte x, Byte y)
        {
            return this - new BytePoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive()
        {
            return true;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct Int16Point
    {
        public static Int16Point operator +(Int16Point first, Int16Point second)
        {
            Int32 x = first.X + second.X;
            Int32 y = first.Y + second.Y;
            return new Int16Point(Unsafe.As<Int32, Int16>(ref x), Unsafe.As<Int32, Int16>(ref y));
        }

        public static Int16Point operator -(Int16Point first, Int16Point second)
        {
            Int32 x = first.X - second.X;
            Int32 y = first.Y - second.Y;
            return new Int16Point(Unsafe.As<Int32, Int16>(ref x), Unsafe.As<Int32, Int16>(ref y));
        }

        public static Int16Point operator *(Int16Point first, Int16Point second)
        {
            Int32 x = first.X * second.X;
            Int32 y = first.Y * second.Y;
            return new Int16Point(Unsafe.As<Int32, Int16>(ref x), Unsafe.As<Int32, Int16>(ref y));
        }

        public static Int16Point operator /(Int16Point first, Int16Point second)
        {
            Int32 x = first.X / second.X;
            Int32 y = first.Y / second.Y;
            return new Int16Point(Unsafe.As<Int32, Int16>(ref x), Unsafe.As<Int32, Int16>(ref y));
        }

        public static Int16Point operator %(Int16Point first, Int16Point second)
        {
            Int32 x = first.X % second.X;
            Int32 y = first.Y % second.Y;
            return new Int16Point(Unsafe.As<Int32, Int16>(ref x), Unsafe.As<Int32, Int16>(ref y));
        }

        public Int16 X { get; }
        public Int16 Y { get; }

        public Int16Point(Int16 x, Int16 y)
        {
            X = x;
            Y = y;
        }

        public static Int16Point Zero { get; } = new Int16Point(0, 0);
        public static Int16Point One { get; } = new Int16Point(1, 1);
        public static Int16Point Minimum { get; } = new Int16Point(Int16.MinValue, Int16.MinValue);
        public static Int16Point Maximum { get; } = new Int16Point(Int16.MaxValue, Int16.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16Point Offset(Int16Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16Point Offset(Int16 x, Int16 y)
        {
            return this + new Int16Point(x, y);
        }

        public Int16Point Offset(PointOffset offset, Int16 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Int16Point(0, count),
                PointOffset.Down => this + new Int16Point(0, count),
                PointOffset.Left => this - new Int16Point(count, 0),
                PointOffset.Right => this + new Int16Point(count, 0),
                PointOffset.UpLeft => this - new Int16Point(count, count),
                PointOffset.DownLeft => this + new Int16Point(0, count) - new Int16Point(count, 0),
                PointOffset.UpRight => this - new Int16Point(0, count) + new Int16Point(count, 0),
                PointOffset.DownRight => this + new Int16Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16Point Delta(Int16Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16Point Delta(Int16 count)
        {
            return this - new Int16Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16Point Delta(Int16 x, Int16 y)
        {
            return this - new Int16Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct UInt16Point
    {
        public static UInt16Point operator +(UInt16Point first, UInt16Point second)
        {
            Int32 x = first.X + second.X;
            Int32 y = first.Y + second.Y;
            return new UInt16Point(Unsafe.As<Int32, UInt16>(ref x), Unsafe.As<Int32, UInt16>(ref y));
        }

        public static UInt16Point operator -(UInt16Point first, UInt16Point second)
        {
            Int32 x = first.X - second.X;
            Int32 y = first.Y - second.Y;
            return new UInt16Point(Unsafe.As<Int32, UInt16>(ref x), Unsafe.As<Int32, UInt16>(ref y));
        }

        public static UInt16Point operator *(UInt16Point first, UInt16Point second)
        {
            Int32 x = first.X * second.X;
            Int32 y = first.Y * second.Y;
            return new UInt16Point(Unsafe.As<Int32, UInt16>(ref x), Unsafe.As<Int32, UInt16>(ref y));
        }

        public static UInt16Point operator /(UInt16Point first, UInt16Point second)
        {
            Int32 x = first.X / second.X;
            Int32 y = first.Y / second.Y;
            return new UInt16Point(Unsafe.As<Int32, UInt16>(ref x), Unsafe.As<Int32, UInt16>(ref y));
        }

        public static UInt16Point operator %(UInt16Point first, UInt16Point second)
        {
            Int32 x = first.X % second.X;
            Int32 y = first.Y % second.Y;
            return new UInt16Point(Unsafe.As<Int32, UInt16>(ref x), Unsafe.As<Int32, UInt16>(ref y));
        }

        public UInt16 X { get; }
        public UInt16 Y { get; }

        public UInt16Point(UInt16 x, UInt16 y)
        {
            X = x;
            Y = y;
        }

        public static UInt16Point Zero { get; } = new UInt16Point(0, 0);
        public static UInt16Point One { get; } = new UInt16Point(1, 1);
        public static UInt16Point Minimum { get; } = new UInt16Point(UInt16.MinValue, UInt16.MinValue);
        public static UInt16Point Maximum { get; } = new UInt16Point(UInt16.MaxValue, UInt16.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16Point Offset(UInt16Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16Point Offset(UInt16 x, UInt16 y)
        {
            return this + new UInt16Point(x, y);
        }

        public UInt16Point Offset(PointOffset offset, UInt16 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new UInt16Point(0, count),
                PointOffset.Down => this + new UInt16Point(0, count),
                PointOffset.Left => this - new UInt16Point(count, 0),
                PointOffset.Right => this + new UInt16Point(count, 0),
                PointOffset.UpLeft => this - new UInt16Point(count, count),
                PointOffset.DownLeft => this + new UInt16Point(0, count) - new UInt16Point(count, 0),
                PointOffset.UpRight => this - new UInt16Point(0, count) + new UInt16Point(count, 0),
                PointOffset.DownRight => this + new UInt16Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16Point Delta(UInt16Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16Point Delta(UInt16 count)
        {
            return this - new UInt16Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16Point Delta(UInt16 x, UInt16 y)
        {
            return this - new UInt16Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive()
        {
            return true;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct Int32Point
    {
        public static implicit operator Int32Point(System.Drawing.Point point)
        {
            return new Int32Point(point.X, point.Y);
        }

        public static implicit operator System.Drawing.Point(Int32Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static Int32Point operator +(Int32Point first, Int32Point second)
        {
            return new Int32Point(first.X + second.X, first.Y + second.Y);
        }

        public static Int32Point operator -(Int32Point first, Int32Point second)
        {
            return new Int32Point(first.X - second.X, first.Y - second.Y);
        }

        public static Int32Point operator *(Int32Point first, Int32Point second)
        {
            return new Int32Point(first.X * second.X, first.Y * second.Y);
        }

        public static Int32Point operator /(Int32Point first, Int32Point second)
        {
            return new Int32Point(first.X / second.X, first.Y / second.Y);
        }

        public static Int32Point operator %(Int32Point first, Int32Point second)
        {
            return new Int32Point(first.X % second.X, first.Y % second.Y);
        }

        public Int32 X { get; }
        public Int32 Y { get; }

        public Int32Point(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public static Int32Point Zero { get; } = new Int32Point(0, 0);
        public static Int32Point One { get; } = new Int32Point(1, 1);
        public static Int32Point Minimum { get; } = new Int32Point(Int32.MinValue, Int32.MinValue);
        public static Int32Point Maximum { get; } = new Int32Point(Int32.MaxValue, Int32.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32Point Offset(Int32Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32Point Offset(Int32 x, Int32 y)
        {
            return this + new Int32Point(x, y);
        }

        public Int32Point Offset(PointOffset offset, Int32 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Int32Point(0, count),
                PointOffset.Down => this + new Int32Point(0, count),
                PointOffset.Left => this - new Int32Point(count, 0),
                PointOffset.Right => this + new Int32Point(count, 0),
                PointOffset.UpLeft => this - new Int32Point(count, count),
                PointOffset.DownLeft => this + new Int32Point(0, count) - new Int32Point(count, 0),
                PointOffset.UpRight => this - new Int32Point(0, count) + new Int32Point(count, 0),
                PointOffset.DownRight => this + new Int32Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32Point Delta(Int32Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32Point Delta(Int32 count)
        {
            return this - new Int32Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32Point Delta(Int32 x, Int32 y)
        {
            return this - new Int32Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct UInt32Point
    {
        public static UInt32Point operator +(UInt32Point first, UInt32Point second)
        {
            return new UInt32Point(first.X + second.X, first.Y + second.Y);
        }

        public static UInt32Point operator -(UInt32Point first, UInt32Point second)
        {
            return new UInt32Point(first.X - second.X, first.Y - second.Y);
        }

        public static UInt32Point operator *(UInt32Point first, UInt32Point second)
        {
            return new UInt32Point(first.X * second.X, first.Y * second.Y);
        }

        public static UInt32Point operator /(UInt32Point first, UInt32Point second)
        {
            return new UInt32Point(first.X / second.X, first.Y / second.Y);
        }

        public static UInt32Point operator %(UInt32Point first, UInt32Point second)
        {
            return new UInt32Point(first.X % second.X, first.Y % second.Y);
        }

        public UInt32 X { get; }
        public UInt32 Y { get; }

        public UInt32Point(UInt32 x, UInt32 y)
        {
            X = x;
            Y = y;
        }

        public static UInt32Point Zero { get; } = new UInt32Point(0, 0);
        public static UInt32Point One { get; } = new UInt32Point(1, 1);
        public static UInt32Point Minimum { get; } = new UInt32Point(UInt32.MinValue, UInt32.MinValue);
        public static UInt32Point Maximum { get; } = new UInt32Point(UInt32.MaxValue, UInt32.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32Point Offset(UInt32Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32Point Offset(UInt32 x, UInt32 y)
        {
            return this + new UInt32Point(x, y);
        }

        public UInt32Point Offset(PointOffset offset, UInt32 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new UInt32Point(0, count),
                PointOffset.Down => this + new UInt32Point(0, count),
                PointOffset.Left => this - new UInt32Point(count, 0),
                PointOffset.Right => this + new UInt32Point(count, 0),
                PointOffset.UpLeft => this - new UInt32Point(count, count),
                PointOffset.DownLeft => this + new UInt32Point(0, count) - new UInt32Point(count, 0),
                PointOffset.UpRight => this - new UInt32Point(0, count) + new UInt32Point(count, 0),
                PointOffset.DownRight => this + new UInt32Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32Point Delta(UInt32Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32Point Delta(UInt32 count)
        {
            return this - new UInt32Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32Point Delta(UInt32 x, UInt32 y)
        {
            return this - new UInt32Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive()
        {
            return true;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct Int64Point
    {
        public static Int64Point operator +(Int64Point first, Int64Point second)
        {
            return new Int64Point(first.X + second.X, first.Y + second.Y);
        }

        public static Int64Point operator -(Int64Point first, Int64Point second)
        {
            return new Int64Point(first.X - second.X, first.Y - second.Y);
        }

        public static Int64Point operator *(Int64Point first, Int64Point second)
        {
            return new Int64Point(first.X * second.X, first.Y * second.Y);
        }

        public static Int64Point operator /(Int64Point first, Int64Point second)
        {
            return new Int64Point(first.X / second.X, first.Y / second.Y);
        }

        public static Int64Point operator %(Int64Point first, Int64Point second)
        {
            return new Int64Point(first.X % second.X, first.Y % second.Y);
        }

        public Int64 X { get; }
        public Int64 Y { get; }

        public Int64Point(Int64 x, Int64 y)
        {
            X = x;
            Y = y;
        }

        public static Int64Point Zero { get; } = new Int64Point(0, 0);
        public static Int64Point One { get; } = new Int64Point(1, 1);
        public static Int64Point Minimum { get; } = new Int64Point(Int64.MinValue, Int64.MinValue);
        public static Int64Point Maximum { get; } = new Int64Point(Int64.MaxValue, Int64.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64Point Offset(Int64Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64Point Offset(Int64 x, Int64 y)
        {
            return this + new Int64Point(x, y);
        }

        public Int64Point Offset(PointOffset offset, Int64 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Int64Point(0, count),
                PointOffset.Down => this + new Int64Point(0, count),
                PointOffset.Left => this - new Int64Point(count, 0),
                PointOffset.Right => this + new Int64Point(count, 0),
                PointOffset.UpLeft => this - new Int64Point(count, count),
                PointOffset.DownLeft => this + new Int64Point(0, count) - new Int64Point(count, 0),
                PointOffset.UpRight => this - new Int64Point(0, count) + new Int64Point(count, 0),
                PointOffset.DownRight => this + new Int64Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64Point Delta(Int64Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64Point Delta(Int64 count)
        {
            return this - new Int64Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64Point Delta(Int64 x, Int64 y)
        {
            return this - new Int64Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct UInt64Point
    {
        public static UInt64Point operator +(UInt64Point first, UInt64Point second)
        {
            return new UInt64Point(first.X + second.X, first.Y + second.Y);
        }

        public static UInt64Point operator -(UInt64Point first, UInt64Point second)
        {
            return new UInt64Point(first.X - second.X, first.Y - second.Y);
        }

        public static UInt64Point operator *(UInt64Point first, UInt64Point second)
        {
            return new UInt64Point(first.X * second.X, first.Y * second.Y);
        }

        public static UInt64Point operator /(UInt64Point first, UInt64Point second)
        {
            return new UInt64Point(first.X / second.X, first.Y / second.Y);
        }

        public static UInt64Point operator %(UInt64Point first, UInt64Point second)
        {
            return new UInt64Point(first.X % second.X, first.Y % second.Y);
        }

        public UInt64 X { get; }
        public UInt64 Y { get; }

        public UInt64Point(UInt64 x, UInt64 y)
        {
            X = x;
            Y = y;
        }

        public static UInt64Point Zero { get; } = new UInt64Point(0, 0);
        public static UInt64Point One { get; } = new UInt64Point(1, 1);
        public static UInt64Point Minimum { get; } = new UInt64Point(UInt64.MinValue, UInt64.MinValue);
        public static UInt64Point Maximum { get; } = new UInt64Point(UInt64.MaxValue, UInt64.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64Point Offset(UInt64Point point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64Point Offset(UInt64 x, UInt64 y)
        {
            return this + new UInt64Point(x, y);
        }

        public UInt64Point Offset(PointOffset offset, UInt64 count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new UInt64Point(0, count),
                PointOffset.Down => this + new UInt64Point(0, count),
                PointOffset.Left => this - new UInt64Point(count, 0),
                PointOffset.Right => this + new UInt64Point(count, 0),
                PointOffset.UpLeft => this - new UInt64Point(count, count),
                PointOffset.DownLeft => this + new UInt64Point(0, count) - new UInt64Point(count, 0),
                PointOffset.UpRight => this - new UInt64Point(0, count) + new UInt64Point(count, 0),
                PointOffset.DownRight => this + new UInt64Point(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64Point Delta(UInt64Point point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64Point Delta(UInt64 count)
        {
            return this - new UInt64Point(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64Point Delta(UInt64 x, UInt64 y)
        {
            return this - new UInt64Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive()
        {
            return true;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct SinglePoint
    {
        public static implicit operator SinglePoint(System.Numerics.Vector2 point)
        {
            return new SinglePoint(point.X, point.Y);
        }

        public static implicit operator System.Numerics.Vector2(SinglePoint point)
        {
            return new System.Numerics.Vector2(point.X, point.Y);
        }

        public static SinglePoint operator +(SinglePoint first, SinglePoint second)
        {
            return new SinglePoint(first.X + second.X, first.Y + second.Y);
        }

        public static SinglePoint operator -(SinglePoint first, SinglePoint second)
        {
            return new SinglePoint(first.X - second.X, first.Y - second.Y);
        }

        public static SinglePoint operator *(SinglePoint first, SinglePoint second)
        {
            return new SinglePoint(first.X * second.X, first.Y * second.Y);
        }

        public static SinglePoint operator /(SinglePoint first, SinglePoint second)
        {
            return new SinglePoint(first.X / second.X, first.Y / second.Y);
        }

        public static SinglePoint operator %(SinglePoint first, SinglePoint second)
        {
            return new SinglePoint(first.X % second.X, first.Y % second.Y);
        }

        public Single X { get; }
        public Single Y { get; }

        public SinglePoint(Single x, Single y)
        {
            X = x;
            Y = y;
        }

        public static SinglePoint Zero { get; } = new SinglePoint(0, 0);
        public static SinglePoint One { get; } = new SinglePoint(1, 1);
        public static SinglePoint Minimum { get; } = new SinglePoint(Single.MinValue, Single.MinValue);
        public static SinglePoint Maximum { get; } = new SinglePoint(Single.MaxValue, Single.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SinglePoint Offset(SinglePoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SinglePoint Offset(Single x, Single y)
        {
            return this + new SinglePoint(x, y);
        }

        public SinglePoint Offset(PointOffset offset, Single count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new SinglePoint(0, count),
                PointOffset.Down => this + new SinglePoint(0, count),
                PointOffset.Left => this - new SinglePoint(count, 0),
                PointOffset.Right => this + new SinglePoint(count, 0),
                PointOffset.UpLeft => this - new SinglePoint(count, count),
                PointOffset.DownLeft => this + new SinglePoint(0, count) - new SinglePoint(count, 0),
                PointOffset.UpRight => this - new SinglePoint(0, count) + new SinglePoint(count, 0),
                PointOffset.DownRight => this + new SinglePoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SinglePoint Delta(SinglePoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SinglePoint Delta(Single count)
        {
            return this - new SinglePoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SinglePoint Delta(Single x, Single y)
        {
            return this - new SinglePoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct DoublePoint
    {
        public static DoublePoint operator +(DoublePoint first, DoublePoint second)
        {
            return new DoublePoint(first.X + second.X, first.Y + second.Y);
        }

        public static DoublePoint operator -(DoublePoint first, DoublePoint second)
        {
            return new DoublePoint(first.X - second.X, first.Y - second.Y);
        }

        public static DoublePoint operator *(DoublePoint first, DoublePoint second)
        {
            return new DoublePoint(first.X * second.X, first.Y * second.Y);
        }

        public static DoublePoint operator /(DoublePoint first, DoublePoint second)
        {
            return new DoublePoint(first.X / second.X, first.Y / second.Y);
        }

        public static DoublePoint operator %(DoublePoint first, DoublePoint second)
        {
            return new DoublePoint(first.X % second.X, first.Y % second.Y);
        }

        public Double X { get; }
        public Double Y { get; }

        public DoublePoint(Double x, Double y)
        {
            X = x;
            Y = y;
        }

        public static DoublePoint Zero { get; } = new DoublePoint(0, 0);
        public static DoublePoint One { get; } = new DoublePoint(1, 1);
        public static DoublePoint Minimum { get; } = new DoublePoint(Double.MinValue, Double.MinValue);
        public static DoublePoint Maximum { get; } = new DoublePoint(Double.MaxValue, Double.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DoublePoint Offset(DoublePoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DoublePoint Offset(Double x, Double y)
        {
            return this + new DoublePoint(x, y);
        }

        public DoublePoint Offset(PointOffset offset, Double count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new DoublePoint(0, count),
                PointOffset.Down => this + new DoublePoint(0, count),
                PointOffset.Left => this - new DoublePoint(count, 0),
                PointOffset.Right => this + new DoublePoint(count, 0),
                PointOffset.UpLeft => this - new DoublePoint(count, count),
                PointOffset.DownLeft => this + new DoublePoint(0, count) - new DoublePoint(count, 0),
                PointOffset.UpRight => this - new DoublePoint(0, count) + new DoublePoint(count, 0),
                PointOffset.DownRight => this + new DoublePoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DoublePoint Delta(DoublePoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DoublePoint Delta(Double count)
        {
            return this - new DoublePoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DoublePoint Delta(Double x, Double y)
        {
            return this - new DoublePoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }

    public readonly struct DecimalPoint
    {
        public static DecimalPoint operator +(DecimalPoint first, DecimalPoint second)
        {
            return new DecimalPoint(first.X + second.X, first.Y + second.Y);
        }

        public static DecimalPoint operator -(DecimalPoint first, DecimalPoint second)
        {
            return new DecimalPoint(first.X - second.X, first.Y - second.Y);
        }

        public static DecimalPoint operator *(DecimalPoint first, DecimalPoint second)
        {
            return new DecimalPoint(first.X * second.X, first.Y * second.Y);
        }

        public static DecimalPoint operator /(DecimalPoint first, DecimalPoint second)
        {
            return new DecimalPoint(first.X / second.X, first.Y / second.Y);
        }

        public static DecimalPoint operator %(DecimalPoint first, DecimalPoint second)
        {
            return new DecimalPoint(first.X % second.X, first.Y % second.Y);
        }

        public Decimal X { get; }
        public Decimal Y { get; }

        public DecimalPoint(Decimal x, Decimal y)
        {
            X = x;
            Y = y;
        }

        public static DecimalPoint Zero { get; } = new DecimalPoint(0, 0);
        public static DecimalPoint One { get; } = new DecimalPoint(1, 1);
        public static DecimalPoint Minimum { get; } = new DecimalPoint(Decimal.MinValue, Decimal.MinValue);
        public static DecimalPoint Maximum { get; } = new DecimalPoint(Decimal.MaxValue, Decimal.MaxValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DecimalPoint Offset(DecimalPoint point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DecimalPoint Offset(Decimal x, Decimal y)
        {
            return this + new DecimalPoint(x, y);
        }

        public DecimalPoint Offset(PointOffset offset, Decimal count = 1)
        {
            if (count == 0)
            {
                return this;
            }

            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new DecimalPoint(0, count),
                PointOffset.Down => this + new DecimalPoint(0, count),
                PointOffset.Left => this - new DecimalPoint(count, 0),
                PointOffset.Right => this + new DecimalPoint(count, 0),
                PointOffset.UpLeft => this - new DecimalPoint(count, count),
                PointOffset.DownLeft => this + new DecimalPoint(0, count) - new DecimalPoint(count, 0),
                PointOffset.UpRight => this - new DecimalPoint(0, count) + new DecimalPoint(count, 0),
                PointOffset.DownRight => this + new DecimalPoint(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DecimalPoint Delta(DecimalPoint point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DecimalPoint Delta(Decimal count)
        {
            return this - new DecimalPoint(count, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DecimalPoint Delta(Decimal x, Decimal y)
        {
            return this - new DecimalPoint(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return X >= 0 && Y >= 0;
        }

        public override String ToString()
        {
            return $"{{X={X}, Y={Y}}}";
        }
    }
}