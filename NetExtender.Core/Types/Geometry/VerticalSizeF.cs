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
    [TypeConverter("System.Drawing.SizeFConverter, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public struct VerticalSizeF : IReadOnlyCollection<PointF>, IEquatable<SizeF>, IEquatable<VerticalSizeF>, IEquatable<HorizontalSizeF>
    {
        public static implicit operator VerticalSizeF(Size value)
        {
            return new VerticalSizeF(value);
        }

        public static implicit operator VerticalSizeF(SizeF value)
        {
            return new VerticalSizeF(value);
        }

        public static implicit operator VerticalSizeF(VerticalSize value)
        {
            return new VerticalSizeF(value);
        }

        public static explicit operator Vector2(VerticalSizeF value)
        {
            return value.Size.ToVector2();
        }

        public static explicit operator PointF(VerticalSizeF value)
        {
            return (PointF) value.Size;
        }

        public static implicit operator SizeF(VerticalSizeF value)
        {
            return value.Size;
        }

        public static Boolean operator ==(VerticalSizeF first, VerticalSizeF second)
        {
            return first.Size == second.Size;
        }

        public static Boolean operator !=(VerticalSizeF first, VerticalSizeF second)
        {
            return first.Size != second.Size;
        }

        public static VerticalSizeF operator +(VerticalSizeF first, VerticalSizeF second)
        {
            return first.Size + second.Size;
        }

        public static VerticalSizeF operator -(VerticalSizeF first, VerticalSizeF second)
        {
            return first.Size - second.Size;
        }

        public static VerticalSizeF operator *(VerticalSizeF first, Single second)
        {
            return first.Size * second;
        }

        public static VerticalSizeF operator *(Single first, VerticalSizeF second)
        {
            return first * second.Size;
        }

        public static VerticalSizeF operator /(VerticalSizeF first, Single second)
        {
            return first.Size / second;
        }

        /// <inheritdoc cref="System.Drawing.SizeF.Empty"/>
        public static SizeF Empty
        {
            get
            {
                return default;
            }
        }

        private SizeF _size;
        public readonly SizeF Size
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

        /// <inheritdoc cref="System.Drawing.SizeF.Width"/>
        public Single Width
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

        /// <inheritdoc cref="System.Drawing.SizeF.Height"/>
        public Single Height
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

        /// <inheritdoc cref="System.Drawing.SizeF.IsEmpty"/>
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
                return (Int32) Width * (Int32) Height;
            }
        }

        public VerticalSizeF(SizeF size)
        {
            _size = size;
        }

        /// <inheritdoc cref="SizeF(PointF)"/>
        public VerticalSizeF(PointF point)
        {
            _size = new SizeF(point);
        }

        /// <inheritdoc cref="SizeF(Single,Single)"/>
        public VerticalSizeF(Single width, Single height)
        {
            _size = new SizeF(width, height);
        }

        /// <inheritdoc cref="SizeF(Vector2)"/>
        public VerticalSizeF(Vector2 vector)
        {
            _size = new SizeF(vector);
        }
        
        /// <inheritdoc cref="System.Drawing.SizeF.ToVector2"/>
        public Vector2 ToVector2()
        {
            return _size.ToVector2();
        }

        /// <inheritdoc cref="SizeF.GetHashCode"/>
        public override readonly Int32 GetHashCode()
        {
            return _size.GetHashCode();
        }

        /// <inheritdoc cref="SizeF.Equals(Object)"/>
        public override readonly Boolean Equals(Object? obj)
        {
            return obj switch
            {
                SizeF size => Equals(size),
                VerticalSizeF size => Equals(size),
                HorizontalSizeF size => Equals(size),
                _ => false
            };
        }

        /// <inheritdoc cref="SizeF.Equals(System.Drawing.SizeF)"/>
        public readonly Boolean Equals(SizeF other)
        {
            return _size == other;
        }

        /// <inheritdoc cref="SizeF.Equals(System.Drawing.SizeF)"/>
        public readonly Boolean Equals(VerticalSizeF other)
        {
            return Equals((SizeF) other);
        }

        /// <inheritdoc cref="SizeF.Equals(System.Drawing.SizeF)"/>
        public readonly Boolean Equals(HorizontalSizeF other)
        {
            return Equals((SizeF) other);
        }

        /// <inheritdoc cref="SizeF.ToString"/>
        public override readonly String ToString()
        {
            return _size.ToString();
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
            
            private PointF _current = PointF.Empty;
            public readonly PointF Current
            {
                get
                {
                    return _current;
                }
            }

            private readonly SizeF _size;
            public readonly SizeF Size
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

            public Enumerator(SizeF size)
            {
                _size = size;
                _step = new SizeF(1, 1);
            }

            public Enumerator(SizeF size, SizeF step)
            {
                if (Math.Abs(step.Height) < Single.Epsilon)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Height), step.Height, null);
                }

                if (Math.Abs(step.Width) < Single.Epsilon)
                {
                    throw new ArgumentOutOfRangeException(nameof(step.Width), step.Width, null);
                }
                
                _size = size;
                _step = step;
            }

            [SuppressMessage("ReSharper", "InvertIf")]
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public Boolean MoveNext()
            {
                Single y = Step.Height * _iterator.Y;
                if (y <= Size.Height)
                {
                    _current.Y = y;
                    _iterator.Y++;
                    return true;
                }
                
                Single x = Step.Width * _iterator.X;
                if (x <= Size.Width)
                {
                    _current.Y = 0;
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
                _current = PointF.Empty;
            }
        }
    }
}