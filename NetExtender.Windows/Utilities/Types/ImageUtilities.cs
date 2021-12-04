// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Crypto;

namespace NetExtender.Utilities.Types
{
    public enum ImageType
    {
        None,
        MemoryBmp,
        Bmp,
        Emf,
        Wmf,
        Gif,
        Jpeg,
        Png,
        Tiff,
        Exif,
        Icon,
        Unknown
    }

    public static class ImageUtilities
    {
        public static ImageType GetImageFormatType(String path)
        {
            try
            {
                return GetImageFormatType(Image.FromFile(path));
            }
            catch
            {
                return ImageType.None;
            }
        }

        public static ImageType GetImageFormatType(Image image)
        {
            return image?.RawFormat?.ToString() switch
            {
                null => ImageType.None,
                "MemoryBMP" => ImageType.MemoryBmp,
                "Bmp" => ImageType.Bmp,
                "Emf" => ImageType.Emf,
                "Wmf" => ImageType.Wmf,
                "Gif" => ImageType.Gif,
                "Jpeg" => ImageType.Jpeg,
                "Png" => ImageType.Png,
                "Tiff" => ImageType.Tiff,
                "Exif" => ImageType.Exif,
                "Icon" => ImageType.Icon,
                _ => ImageType.Unknown
            };
        }

        public static MemoryStream ToStream(this Image image, ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        public static Image FromStream(Stream stream)
        {
            return Image.FromStream(stream, true, true);
        }
        
        public static Task<Image> FromStreamAsync(Stream stream)
        {
            return FromStreamAsync(stream, CancellationToken.None);
        }
        
        public static Task<Image> FromStreamAsync(Stream stream, CancellationToken token)
        {
            try
            {
                return Task.Run(() => FromStream(stream), token);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled<Image>(token);
            }
        }

        public static Bitmap GetResizedImage(String path, Size bounds)
        {
            using Bitmap bitmap = new Bitmap(path);
            return GetResizedImage(bitmap, bounds);
        }

        public static Bitmap GetResizedImage(Bitmap image, Size bounds)
        {
            Size boundSize = new Size(image.Width, image.Height).AspectRatioBoundsSize(bounds);

            Bitmap thumbnail = new Bitmap(boundSize.Width, boundSize.Height, image.PixelFormat);
            using Graphics gfx = Graphics.FromImage(thumbnail);
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rectangle = new Rectangle(0, 0, boundSize.Width, boundSize.Height);
            gfx.DrawImage(image, rectangle);
            return thumbnail;
        }

        public static Bitmap GetTextImage(String message)
        {
            return GetTextImage(message, Color.White, Color.Black);
        }

        public static Bitmap GetTextImage(String message, Font font)
        {
            return GetTextImage(message, font, Color.White, Color.Black);
        }

        public static Bitmap GetTextImage(String message, Color background, Color foreground)
        {
            return GetTextImage(message, new Font("Arial", 12), background, foreground);
        }

        public static Bitmap GetTextImage(String message, Font font, Color background, Color foreground)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (font is null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            Rectangle rectangle = GetRectangleForText(message, font);
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);

            using Graphics gfx = Graphics.FromImage(bitmap);
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            gfx.FillRectangle(new SolidBrush(background), 0, 0, bitmap.Width, bitmap.Height);
            gfx.DrawString(message, font, new SolidBrush(foreground), 20, 5);

            return bitmap;
        }

        public static Rectangle GetRectangleForText(String text, String fontName, Single fontSize)
        {
            return GetRectangleForText(text, new Font(fontName, fontSize));
        }

        public static Rectangle GetRectangleForText(String text, Font font)
        {
            using Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using Graphics gfx = Graphics.FromImage(bmp);
            SizeF stringSize = gfx.MeasureString(text, font);

            return new Rectangle {Width = (Int32) stringSize.Width + 20, Height = (Int32) stringSize.Height + 10};
        }

        public static Double GetAspectRatio(this Image image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return DrawingUtilities.GetAspectRatio(image.Width, image.Height);
        }

        public static Icon ToIcon(this Image image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            
            // Header
            writer.Write((Int16) 0); // 0 : reserved
            writer.Write((Int16) 1); // 2 : 1=ico, 2=cur
            writer.Write((Int16) 1); // 4 : number of images
            
            // Image directory
            Int32 width = image.Width;
            if (width >= 256)
            {
                width = 0;
            }

            writer.Write((Byte) width); // 0 : width of image
            
            Int32 h = image.Height;
            if (h > 255)
            {
                h = 0;
            }

            writer.Write((Byte) h); // 1 : height of image
            writer.Write((Byte) 0); // 2 : number of colors in palette
            writer.Write((Byte) 0); // 3 : reserved
            writer.Write((Int16) 0); // 4 : number of color planes
            writer.Write((Int16) 0); // 6 : bits per pixel
            Int64 here = stream.Position;
            writer.Write(0); // 8 : image size
            Int32 start = (Int32) stream.Position + 4;
            writer.Write(start); // 12: offset of image data
            
            // Image data
            image.Save(stream, ImageFormat.Png);
            Int32 size = (Int32) stream.Position - start;
            stream.Seek(here, SeekOrigin.Begin);
            writer.Write(size);
            stream.Seek(0, SeekOrigin.Begin);

            return new Icon(stream);
        }

        public static Byte[] ToBytes(this Image image)
        {
            return ToBytes(image, image.RawFormat);
        }

        public static Byte[] ToBytes(this Image image, ImageFormat? format)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using MemoryStream stream = new MemoryStream();
            image.Save(stream, format ?? image.RawFormat);
            return stream.ToArray();
        }

        public static Image FromBytes(Byte[] image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using MemoryStream stream = new MemoryStream(image);
            return Image.FromStream(stream);
        }

        public static T RotateImage<T>(this T image, RotateFlipType rotate) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            image.RotateFlip(rotate);
            return image;
        }

        public static T RotateImageCopy<T>(this T image, RotateFlipType rotate) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return RotateImage((T) image.Clone(), rotate);
        }

        public static T RotateImage<T>(this T image, RotateFlipType rotate, Boolean copy) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return copy ? RotateImageCopy(image, rotate) : RotateImage(image, rotate);
        }

        public static Bitmap RotateImage(this Image image, Single angle)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Single alpha = angle;

            while (alpha < 0)
            {
                alpha += 360;
            }

            const Single gamma = 90;
            Single beta = 180 - angle - gamma;

            Single c1 = image.Height;
            Single a1 = Math.Abs((Single) (c1 * Math.Sin(alpha * Math.PI / 180)));
            Single b1 = Math.Abs((Single) (c1 * Math.Sin(beta * Math.PI / 180)));

            Single c2 = image.Width;
            Single a2 = Math.Abs((Single) (c2 * Math.Sin(alpha * Math.PI / 180)));
            Single b2 = Math.Abs((Single) (c2 * Math.Sin(beta * Math.PI / 180)));

            Int32 width = Convert.ToInt32(b2 + a1);
            Int32 height = Convert.ToInt32(b1 + a2);

            Bitmap rotated = new Bitmap(width, height);

            using Graphics graphics = Graphics.FromImage(rotated);

            graphics.TranslateTransform(rotated.Width / 2f, rotated.Height / 2f);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-rotated.Width / 2f, -rotated.Height / 2f);
            graphics.DrawImage(image, new Point((width - image.Width) / 2, (height - image.Height) / 2));

            return rotated;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="size">The new size to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(this Image image, Size size)
        {
            return Resize(image, size.Width, size.Height);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(this Image image, Int32 width, Int32 height)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destination = new Bitmap(width, height);

            destination.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using Graphics graphics = Graphics.FromImage(destination);
            
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes attributes = new ImageAttributes();
            
            attributes.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel, attributes);

            return destination;
        }

        public static Byte[] GetHash(this Image image)
        {
            return image.ToBytes().Hashing();
        }

        public static Byte[] GetHash(this Image image, HashType type)
        {
            return image.ToBytes().Hashing(type);
        }

        [DllImport("msvcrt.dll")]
        private static extern Int32 memcmp(IntPtr first, IntPtr second, Int64 count);

        public static Boolean CompareBitmap(Bitmap? first, Bitmap? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Size != second.Size)
            {
                return false;
            }

            BitmapData bd1 = first.LockBits(new Rectangle(new Point(0, 0), first.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bd2 = second.LockBits(new Rectangle(new Point(0, 0), second.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1Scan0 = bd1.Scan0;
                IntPtr bd2Scan0 = bd2.Scan0;

                Int32 stride = bd1.Stride;
                Int32 len = stride * first.Height;

                return memcmp(bd1Scan0, bd2Scan0, len) == 0;
            }
            finally
            {
                first.UnlockBits(bd1);
                second.UnlockBits(bd2);
            }
        }
    }
}