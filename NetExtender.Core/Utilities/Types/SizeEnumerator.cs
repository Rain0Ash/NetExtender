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
    public unsafe struct SizeEnumerator : IEnumerable<Point>
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

        private readonly delegate*<ref SizeEnumerator, Boolean> _next;
        private readonly delegate*<ref SizeEnumerator, void> _reset;

        public SizeEnumerator(Size size, GeometryBoundsType bounds, GeometryRotationType rotation)
            : this(size, new Size(1, 1), bounds, rotation)
        {
        }

        public SizeEnumerator(Size size, Size step, GeometryBoundsType bounds, GeometryRotationType rotation)
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
                    _current = new Point(-_step.Width, 0);
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
                    _current = new Point(0, -_step.Height);
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
        public readonly SizeEnumerator GetEnumerator()
        {
            SizeEnumerator enumerator = this;
            enumerator.Reset();
            return enumerator;
        }

        readonly IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (Point point in this)
            {
                yield return point;
            }
        }

        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Point>) this).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalBoundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x <= enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y <= enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalInboundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
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

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._size.Height)
            {
                enumerator._current.X = enumerator._size.Height;
                enumerator._current.Y = y;
                return true;
            }
            
            if (y >= enumerator._size.Height && enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = enumerator._size.Height;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalOutboundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._size.Width || enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._size.Height || enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalPointMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._size.Width)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._size.Height)
            {
                enumerator._current.X = 0;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalBoundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y <= enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x <= enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalInboundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
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

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                return true;
            }
            
            if (x >= enumerator._size.Width && enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = enumerator._size.Width;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalOutboundMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._size.Height || enumerator._current.Y < enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._size.Width || enumerator._current.X < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalPointMoveNext(ref SizeEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._size.Height)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._size.Width)
            {
                enumerator._current.Y = 0;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void HorizontalReset(ref SizeEnumerator enumerator)
        {
            enumerator._current.X = -enumerator._step.Width;
            enumerator._current.Y = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void VerticalReset(ref SizeEnumerator enumerator)
        {
            enumerator._current.Y = -enumerator._step.Height;
            enumerator._current.X = 0;
        }
    }
}