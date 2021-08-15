// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetExtender.Utilities.Types
{
    public static class ImageSourceUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern Boolean DeleteObject(IntPtr hObject);
        
        public static ImageSource ToImageSource(this Icon icon)
        {
            if (icon is null)
            {
                throw new ArgumentNullException(nameof(icon));
            }

            return ToImageSource(icon.ToBitmap());
        }
        
        public static ImageSource ToImageSource(this Bitmap image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            IntPtr handle = image.GetHbitmap();

            ImageSource source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return source;
        }
    }
}