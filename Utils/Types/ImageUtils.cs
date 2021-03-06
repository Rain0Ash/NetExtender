// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using JetBrains.Annotations;
using NetExtender.Crypto;
using NetExtender.Utils.GUI.Drawing;

namespace NetExtender.Utils.Types
{
    public enum ImageFormatType
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

    public static class ImageUtils
    {
        public static ImageFormatType GetImageFormatType(String path)
        {
            try
            {
                return GetImageFormatType(Image.FromFile(path));
            }
            catch
            {
                return ImageFormatType.None;
            }
        }

        public static ImageFormatType GetImageFormatType(Image image)
        {
            return image?.RawFormat?.ToString() switch
            {
                null => ImageFormatType.None,
                "MemoryBMP" => ImageFormatType.MemoryBmp,
                "Bmp" => ImageFormatType.Bmp,
                "Emf" => ImageFormatType.Emf,
                "Wmf" => ImageFormatType.Wmf,
                "Gif" => ImageFormatType.Gif,
                "Jpeg" => ImageFormatType.Jpeg,
                "Png" => ImageFormatType.Png,
                "Tiff" => ImageFormatType.Tiff,
                "Exif" => ImageFormatType.Exif,
                "Icon" => ImageFormatType.Icon,
                _ => ImageFormatType.Unknown
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

        public static Bitmap GetResizedImage(String path, Size bounds)
        {
            using Bitmap bitmap = new Bitmap(path);
            return GetResizedImage(bitmap, bounds);
        }

        public static Bitmap GetResizedImage(Bitmap image, Size bounds)
        {
            Size boundSize = new Size(image.Width, image.Height).AspectRatioBoundSize(bounds);

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
            Rectangle rectangle = GetRectangleForText(message, font);
            Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height);

            using Graphics gfx = Graphics.FromImage(bmp);
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            gfx.FillRectangle(new SolidBrush(background), 0, 0, bmp.Width, bmp.Height);
            gfx.DrawString(message, font, new SolidBrush(foreground), 20, 5);

            return bmp;
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
            return DrawingUtils.GetAspectRatio(image.Width, image.Height);
        }

        public static Icon IconFromImage(Image image)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            // Header
            bw.Write((Int16) 0); // 0 : reserved
            bw.Write((Int16) 1); // 2 : 1=ico, 2=cur
            bw.Write((Int16) 1); // 4 : number of images
            // Image directory
            Int32 w = image.Width;
            if (w >= 256)
            {
                w = 0;
            }

            bw.Write((Byte) w); // 0 : width of image
            Int32 h = image.Height;
            if (h > 255)
            {
                h = 0;
            }

            bw.Write((Byte) h); // 1 : height of image
            bw.Write((Byte) 0); // 2 : number of colors in palette
            bw.Write((Byte) 0); // 3 : reserved
            bw.Write((Int16) 0); // 4 : number of color planes
            bw.Write((Int16) 0); // 6 : bits per pixel
            Int64 sizeHere = ms.Position;
            bw.Write(0); // 8 : image size
            Int32 start = (Int32) ms.Position + 4;
            bw.Write(start); // 12: offset of image data
            // Image data
            image.Save(ms, ImageFormat.Png);
            Int32 imageSize = (Int32) ms.Position - start;
            ms.Seek(sizeHere, SeekOrigin.Begin);
            bw.Write(imageSize);
            ms.Seek(0, SeekOrigin.Begin);

            // And load it
            return new Icon(ms);
        }

        public static Byte[] ToBytes(this Image image)
        {
            return ToBytes(image, image.RawFormat);
        }

        public static Byte[] ToBytes(this Image image, ImageFormat format)
        {
            using MemoryStream stream = new MemoryStream();
            image.Save(stream, format);
            return stream.ToArray();
        }

        public static Image FromBytes(Byte[] image)
        {
            using MemoryStream stream = new MemoryStream(image);
            return Image.FromStream(stream);
        }

        public static T RotateImage<T>([NotNull] this T image, RotateFlipType rotate) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            image.RotateFlip(rotate);
            return image;
        }

        public static T RotateImageCopy<T>([NotNull] this T image, RotateFlipType rotate) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return RotateImage((T) (Object) image.Clone(), rotate);
        }

        public static T RotateImage<T>([NotNull] this T image, RotateFlipType rotate, Boolean copy) where T : Image
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return copy ? RotateImageCopy(image, rotate) : RotateImage(image, rotate);
        }

        public static Bitmap RotateImage([NotNull] this Image image, Single angle)
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

        public static Bitmap Resize([NotNull] this Image image, Size size)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return new Bitmap(image, size);
        }

        public static Byte[] GetHash(this Image image)
        {
            return image.ToBytes().GetHash();
        }

        public static Byte[] GetHash(this Image image, HashType type)
        {
            return image.ToBytes().Hashing(type);
        }

        /// <summary>
        /// Takes a screenshot of the screen as a whole
        /// </summary>
        /// <param name="screen">Screen to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static Bitmap TakeScreenshot(this Screen screen)
        {
            if (screen is null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            Bitmap bitmap = new Bitmap(screen.Bounds.Width > 1 ? screen.Bounds.Width : 1, screen.Bounds.Height > 1 ? screen.Bounds.Height : 1, PixelFormat.Format32bppArgb);

            try
            {
                if (screen.Bounds.Width > 1 && screen.Bounds.Height > 1)
                {
                    using Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshot of the screen as a whole (if multiple screens are attached, it takes an image containing them all)
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static Bitmap TakeScreenshot(this IEnumerable<Screen> screens)
        {
            if (screens is null)
            {
                throw new ArgumentNullException(nameof(screens));
            }

            Rectangle rectangle = screens.Aggregate(Rectangle.Empty, (current, screen) => Rectangle.Union(current, screen.Bounds));

            Bitmap bitmap = new Bitmap(rectangle.Width > 1 ? rectangle.Width : 1, rectangle.Height > 1 ? rectangle.Width : 1, PixelFormat.Format32bppArgb);

            try
            {
                if (rectangle.Width > 1 && rectangle.Height > 1)
                {
                    using Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return bitmap;
        }

        /// <summary>
        /// Takes a screenshots of the screens
        /// </summary>
        /// <param name="screens">Screens to get the screenshot from</param>
        /// <returns>Returns a Bitmap containing the screen shot</returns>
        public static IList<Bitmap> TakeScreenshots(this IEnumerable<Screen> screens)
        {
            if (screens is null)
            {
                throw new ArgumentNullException(nameof(screens));
            }

            return screens.Select(TakeScreenshot).ToList();
        }

        [DllImport("msvcrt.dll")]
        private static extern Int32 memcmp(IntPtr first, IntPtr second, Int64 count);

        public static Boolean CompareBitmap(Bitmap first, Bitmap second)
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