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
    public unsafe struct RectangleFEnumerator : IEnumerable<PointF>
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

        private readonly delegate*<ref RectangleFEnumerator, Boolean> _next;
        private readonly delegate*<ref RectangleFEnumerator, void> _reset;

        public RectangleFEnumerator(RectangleF rectangle, GeometryBoundsType bounds, GeometryRotationType rotation)
            : this(rectangle, new SizeF(1, 1), bounds, rotation)
        {
        }

        public RectangleFEnumerator(RectangleF rectangle, SizeF step, GeometryBoundsType bounds, GeometryRotationType rotation)
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
                    _current = new PointF(_rectangle.Left, _rectangle.Top);
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
                    _current = new PointF(_rectangle.Left, _rectangle.Top);
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
        public readonly RectangleFEnumerator GetEnumerator()
        {
            RectangleFEnumerator enumerator = this;
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
        private static Boolean HorizontalBoundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single x = enumerator._rectangle.Left + enumerator._step.Width * enumerator._iterator.X++;
            if (x <= enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._rectangle.Top + enumerator._step.Height * ++enumerator._iterator.Y;
            if (y <= enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalInboundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single x = enumerator._rectangle.Left + enumerator._step.Width * enumerator._iterator.X++;
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

            Single y = enumerator._rectangle.Top + enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            if (y >= enumerator._rectangle.Bottom && enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = enumerator._rectangle.Bottom;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalOutboundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single x = enumerator._rectangle.Left + enumerator._step.Width * enumerator._iterator.X++;
            if (x < enumerator._rectangle.Right || enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._rectangle.Top + enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._rectangle.Bottom || enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HorizontalPointMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single x = enumerator._rectangle.Left + enumerator._step.Width * enumerator._iterator.X++;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.X = x;
                return true;
            }
            
            Single y = enumerator._rectangle.Top + enumerator._step.Height * ++enumerator._iterator.Y;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.X = enumerator._rectangle.Left;
                enumerator._current.Y = y;
                enumerator._iterator.X = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalBoundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single y = enumerator._rectangle.Top + enumerator._step.Height * enumerator._iterator.Y++;
            if (y <= enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._rectangle.Left + enumerator._step.Width * ++enumerator._iterator.X;
            if (x <= enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalInboundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single y = enumerator._rectangle.Top + enumerator._step.Height * enumerator._iterator.Y++;
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

            Single x = enumerator._rectangle.Left + enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }
            
            if (x >= enumerator._rectangle.Right && enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = enumerator._rectangle.Right;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalOutboundMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single y = enumerator._rectangle.Top + enumerator._step.Height * enumerator._iterator.Y++;
            if (y < enumerator._rectangle.Bottom || enumerator._current.Y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._rectangle.Left + enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._rectangle.Right || enumerator._current.X < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean VerticalPointMoveNext(ref RectangleFEnumerator enumerator)
        {
            Single y = enumerator._rectangle.Top + enumerator._step.Height * enumerator._iterator.Y++;
            if (y < enumerator._rectangle.Bottom)
            {
                enumerator._current.Y = y;
                return true;
            }
            
            Single x = enumerator._rectangle.Left + enumerator._step.Width * ++enumerator._iterator.X;
            if (x < enumerator._rectangle.Right)
            {
                enumerator._current.Y = enumerator._rectangle.Top;
                enumerator._current.X = x;
                enumerator._iterator.Y = 1;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void HorizontalReset(ref RectangleFEnumerator enumerator)
        {
            enumerator._iterator = Point.Empty;
            enumerator._current.X = enumerator._rectangle.Left;
            enumerator._current.Y = enumerator._rectangle.Top;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void VerticalReset(ref RectangleFEnumerator enumerator)
        {
            enumerator._iterator = Point.Empty;
            enumerator._current.Y = enumerator._rectangle.Top;
            enumerator._current.X = enumerator._rectangle.Left;
        }
    }
}