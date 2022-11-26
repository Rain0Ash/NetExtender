// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetExtender.Utilities.UserInterface
{
    public static class WindowsPresentationRenderUtilities
    {
        private const Int32 DefaultDpiX = 96;
        private const Int32 DefaultDpiY = 96;

        public static RenderTargetBitmap Render(this UIElement element)
        {
            return Render(element, element.RenderSize);
        }

        public static RenderTargetBitmap Render(this UIElement element, Size size)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return Render(element, new Rect(new Point(0, 0), size));
        }

        public static RenderTargetBitmap Render(this UIElement element, Rect rectangle)
        {
            return Render(element, rectangle, new Size(DefaultDpiX, DefaultDpiY));
        }

        public static RenderTargetBitmap Render(this UIElement element, Size size, Size dpi)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return Render(element, new Rect(new Point(0, 0), size), dpi);
        }

        public static RenderTargetBitmap Render(this UIElement element, Rect rectangle, Size dpi)
        {
            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            element.Measure(rectangle.Size);
            element.Arrange(new Rect(rectangle.Size));
            element.UpdateLayout();

            RenderTargetBitmap render = new RenderTargetBitmap((Int32) rectangle.Size.Width, (Int32) rectangle.Size.Height, dpi.Width, dpi.Height, PixelFormats.Default);

            DrawingVisual visual = new DrawingVisual();
            VisualBrush brush = new VisualBrush(element);

            using DrawingContext content = visual.RenderOpen();
            content.DrawRectangle(brush, null, rectangle);
            content.Close();

            render.Render(visual);
            return render;
        }
    }
}