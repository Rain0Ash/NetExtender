// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Geometry
{
    [Serializable]
    [TypeConverter("System.Drawing.SizeConverter, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public struct HorizontalSize : IReadOnlyCollection<Point>, IEquatable<Size>, IEquatable<HorizontalSize>, IEquatable<VerticalSize>
    {
        public static implicit operator HorizontalSize(Size value)
        {
            return new HorizontalSize(value);
        }

        public static explicit operator Point(HorizontalSize value)
        {
            return (Point) value.Size;
        }

        public static implicit operator Size(HorizontalSize value)
        {
            return value.Size;
        }

        public static implicit operator SizeF(HorizontalSize value)
        {
            return new SizeF(value.Size);
        }

        public static implicit operator HorizontalSizeF(HorizontalSize value)
        {
            return new HorizontalSizeF(value);
        }

        public static Boolean operator ==(HorizontalSize first, HorizontalSize second)
        {
            return first.Size == second.Size;
        }

        public static Boolean operator !=(HorizontalSize first, HorizontalSize second)
        {
            return first.Size != second.Size;
        }

        public static HorizontalSize operator +(HorizontalSize first, HorizontalSize second)
        {
            return first.Size + second.Size;
        }

        public static HorizontalSize operator -(HorizontalSize first, HorizontalSize second)
        {
            return first.Size - second.Size;
        }

        public static HorizontalSize operator *(HorizontalSize first, Int32 second)
        {
            return first.Size * second;
        }

        public static HorizontalSize operator *(Int32 first, HorizontalSize second)
        {
            return first * second.Size;
        }

        public static HorizontalSize operator /(HorizontalSize first, Int32 second)
        {
            return first.Size / second;
        }

        public static HorizontalSizeF operator *(HorizontalSize first, Single second)
        {
            return first.Size * second;
        }

        public static HorizontalSizeF operator *(Single first, HorizontalSize second)
        {
            return first * second.Size;
        }

        public static HorizontalSizeF operator /(HorizontalSize first, Single second)
        {
            return first.Size / second;
        }

        /// <inheritdoc cref="System.Drawing.Size.Empty"/>
        public static Size Empty
        {
            get
            {
                return default;
            }
        }
        
        private Size _size;
        public readonly Size Size
        {
            get
            {
                return _size;
            }
            private init
            {
                _size = value;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Size.Width"/>
        public Int32 Width
        {
            readonly get
            {
                return _size.Width;
            }
            set
            {
                _size.Width = value;
            }
        }

        /// <inheritdoc cref="System.Drawing.Size.Height"/>
        public Int32 Height
        {
            readonly get
            {
                return _size.Height;
            }
            set
            {
                _size.Height = value;
            }
        }

        /// <inheritdoc cref="System.Drawing.Size.IsEmpty"/>
        public readonly Boolean IsEmpty
        {
            get
            {
                return _size.IsEmpty;
            }
        }

        public Int32 Count
        {
            get
            {
                return Width * Height;
            }
        }

        public HorizontalSize(Size size)
        {
            _size = size;
        }
        
        /// <inheritdoc cref="System.Drawing.Size(Point)"/>
        public HorizontalSize(Point point)
        {
            _size = new Size(point);
        }
        
        /// <inheritdoc cref="System.Drawing.Size(Int32,Int32)"/>
        public HorizontalSize(Int32 width, Int32 height)
        {
            _size = new Size(width, height);
        }

        /// <inheritdoc cref="System.Drawing.Size.GetHashCode"/>
        public override readonly Int32 GetHashCode()
        {
            return _size.GetHashCode();
        }

        /// <inheritdoc cref="System.Drawing.Size.Equals(Object)"/>
        public override readonly Boolean Equals(Object? obj)
        {
            return obj switch
            {
                Size size => Equals(size),
                HorizontalSize size => Equals(size),
                VerticalSize size => Equals(size),
                _ => false
            };
        }

        /// <inheritdoc cref="System.Drawing.Size.Equals(System.Drawing.Size)"/>
        public readonly Boolean Equals(Size other)
        {
            return _size == other;
        }

        /// <inheritdoc cref="System.Drawing.Size.Equals(System.Drawing.Size)"/>
        public readonly Boolean Equals(HorizontalSize other)
        {
            return Equals((Size) other);
        }

        /// <inheritdoc cref="System.Drawing.Size.Equals(System.Drawing.Size)"/>
        public readonly Boolean Equals(VerticalSize other)
        {
            return Equals((Size) other);
        }

        /// <inheritdoc cref="System.Drawing.Size.ToString"/>
        public override readonly String ToString()
        {
            return _size.ToString();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (Point point in this)
            {
                yield return point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Point>) this).GetEnumerator();
        }

        public struct Enumerator
        {
            private Point _current;
            public readonly Point Current
            {
                get
                {
                    return _current;
                }
            }

            private readonly Size _size;
            public readonly Size Size
            {
                get
                {
                    return _size;
                }
                private init
                {
                    _size = value;
                }
            }
            
            private readonly Size _step;
            public readonly Size Step
            {
                get
                {
                    return _step;
                }
                private init
                {
                    _step = value;
                }
            }

            public Enumerator(Size size)
            {
                _size = size;
                _step = new Size(1, 1);
                _current = new Point(-_step.Width, 0);
            }

            public Enumerator(Size size, Size step)
            {
                if (step.Width == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Width), step.Width, null);
                }

                if (step.Height == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Height), step.Height, null);
                }

                _size = size;
                _step = step;
                _current = new Point(-_step.Width, 0);
            }

            [SuppressMessage("ReSharper", "InvertIf")]
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public Boolean MoveNext()
            {
                Int32 x = _current.X + Step.Width;
                if (x <= Size.Width)
                {
                    _current.X = x;
                    return true;
                }
                
                Int32 y = _current.Y + Step.Height;
                if (y <= Size.Height)
                {
                    _current.X = 0;
                    _current.Y = y;
                    return true;
                }

                return false;
            }
            
            public void Reset()
            {
                _current = new Point(-_step.Width, 0);
            }
        }
    }
}