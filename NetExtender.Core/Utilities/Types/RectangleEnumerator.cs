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
    public unsafe struct RectangleEnumerator : IEnumerable<Point>
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

        private readonly delegate*<ref RectangleEnumerator, Boolean> _next;
        private readonly delegate*<ref RectangleEnumerator, void> _reset;

        public RectangleEnumerator(Rectangle rectangle, GeometryBoundsType bounds, GeometryRotationType rotation)
            : this(rectangle, new Size(1, 1), bounds, rotation)
        {
        }

        public RectangleEnumerator(Rectangle rectangle, Size step, GeometryBoundsType bounds, GeometryRotationType rotation)
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
                    _current = new Point(_rectangle.Left - _step.Width, _rectangle.Top);
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
                    _current = new Point(_rectangle.Left, _rectangle.Top - _step.Height);
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
        public readonly RectangleEnumerator GetEnumerator()
        {
            RectangleEnumerator enumerator = this;
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
        private static Boolean HorizontalBoundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x <= enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y <= enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalInboundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }
            
            if (x >= enumerator._rectangle.Right && enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.X = enumerator._rectangle.Right;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                return true;
            }
            
            if (y >= enumerator._rectangle.Bottom && enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = enumerator._rectangle.Bottom;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalOutboundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right || enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom || enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalPointMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }

            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalBoundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y <= enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x <= enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalInboundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            if (y >= enumerator._rectangle.Bottom && enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = enumerator._rectangle.Bottom;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                return true;
            }
            
            if (x >= enumerator._rectangle.Right && enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = enumerator._rectangle.Right;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalOutboundMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom || enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right || enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalPointMoveNext(ref RectangleEnumerator enumerator)
        {
            Int32 y = enumerator._current.Y + enumerator._step.Height;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }

            Int32 x = enumerator._current.X + enumerator._step.Width;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void HorizontalReset(ref RectangleEnumerator enumerator)
        {
            enumerator._current.X = enumerator._rectangle.Left - enumerator._step.Width;
            enumerator._current.Y = enumerator._rectangle.Top;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void VerticalReset(ref RectangleEnumerator enumerator)
        {
            enumerator._current.Y = enumerator._rectangle.Top - enumerator._step.Height;
            enumerator._current.X = enumerator._rectangle.Left;
        }
    }
}