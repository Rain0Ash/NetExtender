// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace NetExtender.ImageSharp.Utilities
{
    public static class ImageSharpDrawingUtilities
    {
        public static Bitmap ToBitmap<TPixel>(this Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using MemoryStream stream = new MemoryStream();

            IImageEncoder encoder = image.Configuration.ImageFormatsManager.GetEncoder(PngFormat.Instance);
            image.Save(stream, encoder);

            stream.Seek(0, SeekOrigin.Begin);

            return new Bitmap(stream);
        }

        public static Image<TPixel> ToSharpImage<TPixel>(this Bitmap bitmap) where TPixel : unmanaged, IPixel<TPixel>
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using MemoryStream stream = new MemoryStream();

            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);

            return SixLabors.ImageSharp.Image.Load<TPixel>(stream);
        }
    }
}