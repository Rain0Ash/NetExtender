// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;
using NetExtender.Utilities.Numerics;
using NetExtender.Windows.Types;

namespace NetExtender.Utilities.Types
{
    public static class GraphicsUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCircle(this Graphics graphics, Pen pen, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawCircle(this Graphics graphics, Pen pen, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillCircle(this Graphics graphics, Brush brush, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.FillEllipse(brush, x, y, d, d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillCircle(this Graphics graphics, Brush brush, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.FillEllipse(brush, x, y, d, d);
        }

        public static PointF Alignment(this ContentAlignment alignment, PointF point, SizeF size)
        {
            return alignment switch
            {
                ContentAlignment.TopLeft => point.WithOffset(PointOffset.UpLeft, size),
                ContentAlignment.TopCenter => point.WithOffset(PointOffset.UpLeft, new SizeF(size.Width / 2, size.Height)),
                ContentAlignment.TopRight => point.WithOffset(PointOffset.Up, size),
                ContentAlignment.MiddleLeft => point.WithOffset(PointOffset.UpLeft, new SizeF(size.Width, size.Height / 2)),
                ContentAlignment.MiddleCenter => point.WithOffset(PointOffset.UpLeft, new SizeF(size.Width / 2, size.Height / 2)),
                ContentAlignment.MiddleRight => point.WithOffset(PointOffset.Up, new SizeF(size.Width, size.Height / 2)),
                ContentAlignment.BottomLeft => point.WithOffset(PointOffset.Left, size),
                ContentAlignment.BottomCenter => point.WithOffset(PointOffset.Left, new SizeF(size.Width / 2, size.Height)),
                ContentAlignment.BottomRight => point.WithOffset(PointOffset.None, size),
                _ => throw new EnumUndefinedOrNotSupportedException<ContentAlignment>(alignment, nameof(alignment), null)
            };
        }

        public static void DrawString(this Graphics graphics, String? value, Font font, Brush brush, PointF point, ContentAlignment alignment)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            SizeF length = graphics.MeasureString(value, font);
            point = alignment.Alignment(point, length);
            graphics.DrawString(value, font, brush, point);
        }

        public static void DrawString(this Graphics graphics, String? value, Font font, Brush brush, PointF point, ContentAlignment alignment, StringFormat? format)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
            
            SizeF length = graphics.MeasureString(value, font);
            point = alignment.Alignment(point, length);
            graphics.DrawString(value, font, brush, point, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawString(this Graphics graphics, String? value, Font font, Brush brush, Single x, Single y, ContentAlignment alignment)
        {
            DrawString(graphics, value, font, brush, new PointF(x, y), alignment);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawString(this Graphics graphics, String? value, Font font, Brush brush, Single x, Single y, ContentAlignment alignment, StringFormat? format)
        {
            DrawString(graphics, value, font, brush, new PointF(x, y), alignment, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawString(this Graphics graphics, PointF point, params DrawableString[] values)
        {
            DrawString(graphics, point, ContentAlignment.BottomRight, values);
        }

        public static void DrawString(this Graphics graphics, PointF point, ContentAlignment alignment, params DrawableString[] values)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length <= 0)
            {
                return;
            }

            SizeF length = new SizeF(-values[^1].Distance, 0);

            foreach (DrawableString value in values)
            {
                SizeF size = graphics.MeasureString(value, value.Font);
                length.Width += size.Width + value.Distance;
                length.Height = Math.Max(length.Height, size.Height);
            }

            point = alignment.Alignment(point, length);

            foreach (DrawableString value in values)
            {
                graphics.DrawString(value, value.Font, value.Brush, point, value.Format);
                point.X += graphics.MeasureString(value, value.Font).Width + value.Distance;
            }
        }
    }
}