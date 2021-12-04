// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DrawingPoint = System.Drawing.Point;
using DrawingPixelFormat = System.Drawing.Imaging.PixelFormat;

namespace NetExtender.Utilities.Types
{
    public static class ImageSourceUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern Boolean DeleteObject(IntPtr handle);
        
        private static BitmapSizeOptions Options { get; } = BitmapSizeOptions.FromEmptyOptions();

        public static ImageSource ToImageSource(this Icon icon)
        {
            return ToBitmapSource(icon);
        }
        
        public static BitmapSource ToBitmapSource(this Icon icon)
        {
            if (icon is null)
            {
                throw new ArgumentNullException(nameof(icon));
            }

            return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, Options);
        }

        public static ImageSource ToImageSource(this Bitmap image)
        {
            return ToBitmapSource(image);
        }

        public static BitmapSource ToBitmapSource(this Bitmap image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            IntPtr handle = image.GetHbitmap();

            try
            {
                BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, Options);
                return source;
            }
            finally
            {
                DeleteObject(handle);
            }
        }

        public static Icon ToIcon(this BitmapSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ToBitmap(source).ToIcon();
        }

        public static Image ToImage(this BitmapSource source)
        {
            return ToBitmap(source);
        }

        public static Bitmap ToBitmap(this BitmapSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Bitmap bitmap = new Bitmap(source.PixelWidth, source.PixelHeight, DrawingPixelFormat.Format32bppPArgb);
            BitmapData bits = bitmap.LockBits(new Rectangle(DrawingPoint.Empty, bitmap.Size), ImageLockMode.WriteOnly, DrawingPixelFormat.Format32bppPArgb);
	
            source.CopyPixels(Int32Rect.Empty, bits.Scan0, bits.Height * bits.Stride, bits.Stride);
            bitmap.UnlockBits(bits);
	
            return bitmap;
        }
    }
}