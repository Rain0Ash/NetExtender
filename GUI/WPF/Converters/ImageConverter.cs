// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetExtender.GUI.WPF.Converters
{
    /// <summary>
    /// One-way converter from System.Drawing.Image to System.Windows.Media.ImageSource
    /// </summary>
    [ValueConversion(typeof(Image), typeof(ImageSource))]
    [ValueConversion(typeof(Icon), typeof(ImageSource))]
    public class ImageConverter : MarkupExtension, IValueConverter
    {
        private static ImageConverter converter;
        public Object Convert(Object value, Type targetType,
            Object parameter, CultureInfo culture)
        {
            // empty images are empty...
            if (value is null) { return null; }

            Image image;
            if (value is Icon icon)
            {
                image = icon.ToBitmap();
            }
            else
            {
                image = (Image) value;
            }
            
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            
            image.Save(memoryStream, ImageFormat.Png);
            
            memoryStream.Seek(0, SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }

        public Object ConvertBack(Object value, Type targetType,
            Object parameter, CultureInfo culture)
        {
            return null;
        }

        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ??= new ImageConverter();
        }
    }
}