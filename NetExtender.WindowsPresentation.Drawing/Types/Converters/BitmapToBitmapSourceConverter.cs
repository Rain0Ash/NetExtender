// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentationForms.Types.Converters
{
    public class BitmapToBitmapSourceConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is Bitmap bitmap && typeof(ImageSource).IsAssignableFrom(targetType))
            {
                return bitmap.ToBitmapSource();
            }

            return value;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is BitmapSource source && typeof(Bitmap) == targetType)
            {
                return source.ToBitmap();
            }

            return value;
        }
    }
}