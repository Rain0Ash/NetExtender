// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Geometry
{
    [Serializable]
    [TypeConverter("System.Drawing.RectangleConverter, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public struct VerticalRectangleF : IReadOnlyCollection<PointF>, IEquatable<RectangleF>, IEquatable<VerticalRectangleF>, IEquatable<HorizontalRectangleF>
    {
        public static implicit operator VerticalRectangleF(Rectangle value)
        {
            return new VerticalRectangleF(value);
        }
        public static implicit operator VerticalRectangleF(RectangleF value)
        {
            return new VerticalRectangleF(value);
        }
        
        public static implicit operator RectangleF(VerticalRectangleF value)
        {
            return value.Rectangle;
        }
        
        public static explicit operator Vector4(VerticalRectangleF value)
        {
            return value.Rectangle.ToVector4();
        }

        public static explicit operator VerticalRectangleF(Vector4 value)
        {
            return new VerticalRectangleF(value);
        }

        public static Boolean operator ==(VerticalRectangleF first, VerticalRectangleF second)
        {
            return first.Rectangle == second.Rectangle;
        }

        public static Boolean operator !=(VerticalRectangleF first, VerticalRectangleF second)
        {
            return first.Rectangle != second.Rectangle;
        }
        
        /// <inheritdoc cref="RectangleF.Empty"/>>
        public static RectangleF Empty
        {
            get
            {
                return default;
            }
        }
        
        private RectangleF _rectangle;
        public readonly RectangleF Rectangle
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
        
        /// <inheritdoc cref="RectangleF.Location"/>>
        public PointF Location
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
        
        /// <inheritdoc cref="RectangleF.Size"/>>
        public SizeF Size
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

        /// <inheritdoc cref="RectangleF.Size"/>>
        public readonly VerticalSizeF VerticalSize
        {
            get
            {
                return _rectangle.Size;
            }
        }

        /// <inheritdoc cref="RectangleF.Size"/>>
        public readonly HorizontalSizeF HorizontalSize
        {
            get
            {
                return _rectangle.Size;
            }
        }

        /// <inheritdoc cref="RectangleF.X"/>>
        public Single X
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

        /// <inheritdoc cref="RectangleF.Y"/>>
        public Single Y
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

        /// <inheritdoc cref="RectangleF.Width"/>>
        public Single Width
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

        /// <inheritdoc cref="RectangleF.Height"/>>
        public Single Height
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
        
        /// <inheritdoc cref="RectangleF.Left"/>>
        public readonly Single Left
        {
            get
            {
                return _rectangle.Left;
            }
        }

        /// <inheritdoc cref="RectangleF.Top"/>>
        public readonly Single Top
        {
            get
            {
                return _rectangle.Top;
            }
        }

        /// <inheritdoc cref="RectangleF.Right"/>>
        public readonly Single Right
        {
            get
            {
                return _rectangle.Right;
            }
        }

        /// <inheritdoc cref="RectangleF.Bottom"/>>
        public readonly Single Bottom
        {
            get
            {
                return _rectangle.Bottom;
            }
        }

        /// <inheritdoc cref="RectangleF.IsEmpty"/>>
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
                return (Int32) Width * (Int32) Height;
            }
        }

        public VerticalRectangleF(RectangleF rectangle)
        {
            _rectangle = rectangle;
        }
        
        /// <inheritdoc cref="RectangleF(Single,Single,Single,Single)"/>>
        public VerticalRectangleF(Single x, Single y, Single width, Single height)
        {
            _rectangle = new RectangleF(x, y, width, height);
        }
        
        /// <inheritdoc cref="RectangleF(PointF,SizeF)"/>>
        public VerticalRectangleF(PointF location, SizeF size)
        {
            _rectangle = new RectangleF(location, size);
        }
        
        /// <inheritdoc cref="RectangleF(Vector4)"/>>
        public VerticalRectangleF(Vector4 vector)
        {
            _rectangle = new RectangleF(vector);
        }
        
        /// <inheritdoc cref="RectangleF.Contains(Single,Single)"/>>
        public readonly Boolean Contains(Single x, Single y)
        {
            return _rectangle.Contains(x, y);
        }

        /// <inheritdoc cref="RectangleF.Contains(PointF)"/>>
        public readonly Boolean Contains(PointF point)
        {
            return _rectangle.Contains(point);
        }

        /// <inheritdoc cref="RectangleF.Contains(RectangleF)"/>>
        public readonly Boolean Contains(RectangleF rectangle)
        {
            return _rectangle.Contains(rectangle);
        }

        /// <inheritdoc cref="RectangleF.IntersectsWith"/>>
        public readonly Boolean IntersectsWith(RectangleF rectangle)
        {
            return _rectangle.IntersectsWith(rectangle);
        }

        /// <inheritdoc cref="RectangleF.Offset(Single,Single)"/>>
        public void Offset(Single x, Single y)
        {
            _rectangle.Offset(x, y);
        }

        /// <inheritdoc cref="RectangleF.Offset(PointF)"/>>
        public void Offset(PointF point)
        {
            _rectangle.Offset(point);
        }

        /// <inheritdoc cref="RectangleF.Inflate(Single,Single)"/>>
        public void Inflate(Single width, Single height)
        {
            _rectangle.Inflate(width, height);
        }

        /// <inheritdoc cref="RectangleF.Inflate(SizeF)"/>>
        public void Inflate(SizeF size)
        {
            _rectangle.Inflate(size);
        }

        /// <inheritdoc cref="RectangleF.Intersect(RectangleF)"/>>
        public void Intersect(RectangleF rectangle)
        {
            _rectangle.Intersect(rectangle);
        }

        /// <inheritdoc cref="RectangleF.GetHashCode"/>>
        public override readonly Int32 GetHashCode()
        {
            return _rectangle.GetHashCode();
        }

        /// <inheritdoc cref="RectangleF.Equals(Object)"/>>
        public override readonly Boolean Equals(Object? obj)
        {
            return obj switch
            {
                RectangleF rectangle => Equals(rectangle),
                VerticalRectangleF rectangle => Equals(rectangle),
                HorizontalRectangleF rectangle => Equals(rectangle),
                _ => false
            };
        }

        /// <inheritdoc cref="RectangleF.Equals(RectangleF)"/>>
        public readonly Boolean Equals(RectangleF other)
        {
            return _rectangle == other;
        }

        /// <inheritdoc cref="RectangleF.Equals(RectangleF)"/>>
        public readonly Boolean Equals(VerticalRectangleF other)
        {
            return Equals((RectangleF) other);
        }

        /// <inheritdoc cref="RectangleF.Equals(RectangleF)"/>>
        public readonly Boolean Equals(HorizontalRectangleF other)
        {
            return Equals((RectangleF) other);
        }

        /// <inheritdoc cref="RectangleF.ToString"/>>
        public override readonly String ToString()
        {
            return _rectangle.ToString();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<PointF> IEnumerable<PointF>.GetEnumerator()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (PointF point in this)
            {
                yield return point;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PointF>) this).GetEnumerator();
        }

        public struct Enumerator
        {
            private Point _iterator = Point.Empty;
            
            private PointF _current;
            public readonly PointF Current
            {
                get
                {
                    return _current;
                }
            }

            private readonly RectangleF _rectangle;
            public readonly RectangleF Rectangle
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
            
            private readonly SizeF _step;
            public readonly SizeF Step
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

            public Enumerator(RectangleF rectangle)
            {
                _rectangle = rectangle;
                _step = new SizeF(1, 1);
                _current = new PointF(_rectangle.Left, _rectangle.Top);
            }

            public Enumerator(RectangleF rectangle, SizeF step)
            {
                if (Math.Abs(step.Width) < Single.Epsilon)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Width), step.Width, null);
                }

                if (Math.Abs(step.Height) < Single.Epsilon)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Height), step.Height, null);
                }

                _rectangle = rectangle;
                _step = step;
                _current = new PointF(_rectangle.Left, _rectangle.Top);
            }

            [SuppressMessage("ReSharper", "InvertIf")]
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public Boolean MoveNext()
            {
                Single y = _rectangle.Top + Step.Height * _iterator.Y;
                if (y < Rectangle.Bottom)
                {
                    _current.Y = y;
                    _iterator.Y++;
                    return true;
                }
                
                Single x = _rectangle.Left + Step.Width * _iterator.X;
                if (x < Rectangle.Right)
                {
                    _current.Y = _rectangle.Top;
                    _current.X = x;
                    _iterator.Y = 0;
                    _iterator.X++;
                    return true;
                }

                return false;
            }
            
            public void Reset()
            {
                _iterator = Point.Empty;
                _current = new PointF(_rectangle.Left, _rectangle.Top);
            }
        }
    }
}