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
    [TypeConverter("System.Drawing.RectangleConverter, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public struct HorizontalRectangle : IReadOnlyCollection<Point>, IEquatable<Rectangle>, IEquatable<HorizontalRectangle>, IEquatable<VerticalRectangle>
    {
        public static implicit operator HorizontalRectangle(Rectangle value)
        {
            return new HorizontalRectangle(value);
        }
        
        public static implicit operator Rectangle(HorizontalRectangle value)
        {
            return value.Rectangle;
        }
        
        public static Boolean operator ==(HorizontalRectangle first, HorizontalRectangle second)
        {
            return first.Rectangle == second.Rectangle;
        }

        public static Boolean operator !=(HorizontalRectangle first, HorizontalRectangle second)
        {
            return first.Rectangle != second.Rectangle;
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Empty"/>>
        public static Rectangle Empty
        {
            get
            {
                return default;
            }
        }
        
        private Rectangle _rectangle;
        public readonly Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            private init
            {
                _rectangle = value;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Location"/>>
        public Point Location
        {
            readonly get
            {
                return _rectangle.Location;
            }
            set
            {
                _rectangle.Location = value;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Size"/>>
        public Size Size
        {
            readonly get
            {
                return _rectangle.Size;
            }
            set
            {
                _rectangle.Size = value;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Size"/>>
        public readonly HorizontalSize HorizontalSize
        {
            get
            {
                return _rectangle.Size;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Size"/>>
        public readonly VerticalSize VerticalSize
        {
            get
            {
                return _rectangle.Size;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.X"/>>
        public Int32 X
        {
            readonly get
            {
                return _rectangle.X;
            }
            set
            {
                _rectangle.X = value;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Y"/>>
        public Int32 Y
        {
            readonly get
            {
                return _rectangle.Y;
            }
            set
            {
                _rectangle.Y = value;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Width"/>>
        public Int32 Width
        {
            readonly get
            {
                return _rectangle.Width;
            }
            set
            {
                _rectangle.Width = value;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Height"/>>
        public Int32 Height
        {
            readonly get
            {
                return _rectangle.Height;
            }
            set
            {
                _rectangle.Height = value;
            }
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Left"/>>
        public readonly Int32 Left
        {
            get
            {
                return _rectangle.Left;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Top"/>>
        public readonly Int32 Top
        {
            get
            {
                return _rectangle.Top;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Right"/>>
        public readonly Int32 Right
        {
            get
            {
                return _rectangle.Right;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Bottom"/>>
        public readonly Int32 Bottom
        {
            get
            {
                return _rectangle.Bottom;
            }
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.IsEmpty"/>>
        public readonly Boolean IsEmpty
        {
            get
            {
                return _rectangle.IsEmpty;
            }
        }

        public Int32 Count
        {
            get
            {
                return Width * Height;
            }
        }

        public HorizontalRectangle(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle(Int32,Int32,Int32,Int32)"/>>
        public HorizontalRectangle(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            _rectangle = new Rectangle(x, y, width, height);
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle(Point,System.Drawing.Size)"/>>
        public HorizontalRectangle(Point location, Size size)
        {
            _rectangle = new Rectangle(location, size);
        }
        
        /// <inheritdoc cref="System.Drawing.Rectangle.Contains(Int32,Int32)"/>>
        public readonly Boolean Contains(Int32 x, Int32 y)
        {
            return _rectangle.Contains(x, y);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Contains(Point)"/>>
        public readonly Boolean Contains(Point point)
        {
            return _rectangle.Contains(point);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Contains(System.Drawing.Rectangle)"/>>
        public readonly Boolean Contains(Rectangle rectangle)
        {
            return _rectangle.Contains(rectangle);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.IntersectsWith"/>>
        public readonly Boolean IntersectsWith(Rectangle rectangle)
        {
            return _rectangle.IntersectsWith(rectangle);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Offset(Int32,Int32)"/>>
        public void Offset(Int32 x, Int32 y)
        {
            _rectangle.Offset(x, y);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Offset(Point)"/>>
        public void Offset(Point point)
        {
            _rectangle.Offset(point);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Inflate(Int32,Int32)"/>>
        public void Inflate(Int32 width, Int32 height)
        {
            _rectangle.Inflate(width, height);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Inflate(System.Drawing.Size)"/>>
        public void Inflate(Size size)
        {
            _rectangle.Inflate(size);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Intersect(System.Drawing.Rectangle)"/>>
        public void Intersect(Rectangle rectangle)
        {
            _rectangle.Intersect(rectangle);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.GetHashCode"/>>
        public override readonly Int32 GetHashCode()
        {
            return _rectangle.GetHashCode();
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Equals(Object)"/>>
        public override readonly Boolean Equals(Object? obj)
        {
            return obj switch
            {
                Rectangle rectangle => Equals(rectangle),
                HorizontalRectangle rectangle => Equals(rectangle),
                VerticalRectangle rectangle => Equals(rectangle),
                _ => false
            };
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Equals(System.Drawing.Rectangle)"/>>
        public readonly Boolean Equals(Rectangle other)
        {
            return _rectangle == other;
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Equals(System.Drawing.Rectangle)"/>>
        public readonly Boolean Equals(HorizontalRectangle other)
        {
            return Equals((Rectangle) other);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.Equals(System.Drawing.Rectangle)"/>>
        public readonly Boolean Equals(VerticalRectangle other)
        {
            return Equals((Rectangle) other);
        }

        /// <inheritdoc cref="System.Drawing.Rectangle.ToString"/>>
        public override readonly String ToString()
        {
            return _rectangle.ToString();
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

            private readonly Rectangle _rectangle;
            public readonly Rectangle Rectangle
            {
                get
                {
                    return _rectangle;
                }
                private init
                {
                    _rectangle = value;
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

            public Enumerator(Rectangle rectangle)
            {
                _rectangle = rectangle;
                _step = new Size(1, 1);
                _current = new Point(_rectangle.Left - _step.Width, _rectangle.Top);
            }

            public Enumerator(Rectangle rectangle, Size step)
            {
                if (step.Width == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Width), step.Width, null);
                }

                if (step.Height == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Height), step.Height, null);
                }

                _rectangle = rectangle;
                _step = step;
                _current = new Point(_rectangle.Left - _step.Width, _rectangle.Top);
            }

            [SuppressMessage("ReSharper", "InvertIf")]
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public Boolean MoveNext()
            {
                Int32 x = _rectangle.Left + _current.X + Step.Width;
                if (x <= Rectangle.Right)
                {
                    _current.X = x;
                    return true;
                }
                
                Int32 y = _rectangle.Top + _current.Y + Step.Height;
                if (y <= Rectangle.Bottom)
                {
                    _current.X = _rectangle.Left;
                    _current.Y = y;
                    return true;
                }

                return false;
            }
            
            public void Reset()
            {
                _current = new Point(_rectangle.Left - _step.Width, _rectangle.Top);
            }
        }
    }
}