// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Geometry;

// ReSharper disable InvertIf
// ReSharper disable ConvertToAutoProperty

namespace NetExtender.Utilities.Types
{
    public unsafe struct SizeFEnumerator : IEnumerable<PointF>
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

        private readonly GeometryBoundsType _bounds;
        public readonly GeometryBoundsType Bounds
        {
            get
            {
                return _bounds;
            }
        }

        private readonly GeometryRotationType _rotation;
        public readonly GeometryRotationType Rotation
        {
            get
            {
                return _rotation;
            }
        }

        private readonly delegate*<ref SizeFEnumerator, Boolean> _next;
        private readonly delegate*<ref SizeFEnumerator, void> _reset;

        public SizeFEnumerator(SizeF size, GeometryBoundsType bounds, GeometryRotationType rotation)
            : this(size, new SizeF(1, 1), bounds, rotation)
        {
        }

        public SizeFEnumerator(SizeF size, SizeF step, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            if (Math.Abs(step.Width) < Single.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(step.Width), step.Width, null);
            }

            if (Math.Abs(step.Height) < Single.Epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(step.Height), step.Height, null);
            }

            _size = size;
            _step = step;

            switch (rotation)
            {
                case GeometryRotationType.Default:
                case GeometryRotationType.Horizontal:
                    _next = bounds switch
                    {
                        GeometryBoundsType.Bound => &HorizontalBoundMoveNext,
                        GeometryBoundsType.Inbound => &HorizontalInboundMoveNext,
                        GeometryBoundsType.Outbound => &HorizontalOutboundMoveNext,
                        GeometryBoundsType.Point => &HorizontalPointMoveNext,
                        _ => throw new EnumUndefinedOrNotSupportedException<GeometryBoundsType>(bounds, nameof(bounds), null)
                    };

                    _bounds = bounds;
                    _rotation = rotation;
                    _current = PointF.Empty;
                    _reset = &HorizontalReset;

                    return;
                case GeometryRotationType.Vertical:
                case GeometryRotationType.Other:
                    _next = bounds switch
                    {
                        GeometryBoundsType.Bound => &VerticalBoundMoveNext,
                        GeometryBoundsType.Inbound => &VerticalInboundMoveNext,
                        GeometryBoundsType.Outbound => &VerticalOutboundMoveNext,
                        GeometryBoundsType.Point => &VerticalPointMoveNext,
                        _ => throw new EnumUndefinedOrNotSupportedException<GeometryBoundsType>(bounds, nameof(bounds), null)
                    };

                    _bounds = bounds;
                    _rotation = rotation;
                    _current = PointF.Empty;
                    _reset = &VerticalReset;

                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<GeometryRotationType>(rotation, nameof(rotation), null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext()
        {
            return _next(ref this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _reset(ref this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SizeFEnumerator GetEnumerator()
        {
            SizeFEnumerator enumerator = this;
            enumerator.Reset();
            return enumerator;
        }
        
        readonly IEnumerator<PointF> IEnumerable<PointF>.GetEnumerator()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (PointF point in this)
            {
                yield return point;
            }
        }

        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PointF>) this).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalBoundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single x = enumerator._step.Width * enumerator._iterator.X++;
            if (x <= enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._step.Height * ++enumerator._iterator.Y;
            if (y <= enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalInboundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single x = enumerator._step.Width * enumerator._iterator.X++;
            if (x < enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }

            if (x >= enumerator._size.Width && enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.X = enumerator._size.Width;
                return true;
            }

            Single y = enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            if (y >= enumerator._size.Height && enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = enumerator._size.Height;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalOutboundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single x = enumerator._step.Width * enumerator._iterator.X++;
            if (x < enumerator._size.Width || enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._size.Height || enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalPointMoveNext(ref SizeFEnumerator enumerator)
        {
            Single x = enumerator._step.Width * enumerator._iterator.X++;
            if (x < enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalBoundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single y = enumerator._step.Height * enumerator._iterator.Y++;
            if (y <= enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._step.Width * ++enumerator._iterator.X;
            if (x <= enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalInboundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single y = enumerator._step.Height * enumerator._iterator.Y++;
            if (y < enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            if (y >= enumerator._size.Height && enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.Y = enumerator._size.Height;
                return true;
            }

            Single x = enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }
            
            if (x >= enumerator._size.Width && enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = enumerator._size.Width;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalOutboundMoveNext(ref SizeFEnumerator enumerator)
        {
            Single y = enumerator._step.Height * enumerator._iterator.Y++;
            if (y < enumerator._size.Height || enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._size.Width || enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalPointMoveNext(ref SizeFEnumerator enumerator)
        {
            Single y = enumerator._step.Height * enumerator._iterator.Y++;
            if (y < enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void HorizontalReset(ref SizeFEnumerator enumerator)
        {
            enumerator._iterator = Point.Empty;
            enumerator._current.X = 0;
            enumerator._current.Y = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void VerticalReset(ref SizeFEnumerator enumerator)
        {
            enumerator._iterator = Point.Empty;
            enumerator._current.Y = 0;
            enumerator._current.X = 0;
        }
    }
}