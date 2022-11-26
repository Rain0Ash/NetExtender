// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Cryptography;

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
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return GetImageFormatType(Image.FromFile(path));
            }
            catch (Exception)
            {
                return ImageType.None;
            }
        }

        public static ImageType GetImageFormatType(Image? image)
        {
            return image?.RawFormat.ToString() switch
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

        public static Bitmap? ToBitmap(this Image? image)
        {
            if (image is null)
            {
                return null;
            }

            try
            {
                return new Bitmap(image);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MemoryStream ToStream(this Image image, ImageFormat format)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            MemoryStream stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }

        public static Image FromStream(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return Image.FromStream(stream, true, true);
        }

        public static Task<Image> FromStreamAsync(Stream stream)
        {
            return FromStreamAsync(stream, CancellationToken.None);
        }

        public static Task<Image> FromStreamAsync(Stream stream, CancellationToken token)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                return Task.Run(() => FromStream(stream), token);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled<Image>(token);
            }
        }

        public static Bitmap ResizeImage(String path, Size bounds)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using Bitmap bitmap = new Bitmap(path);
            return ResizeImage(bitmap, bounds);
        }

        public static Bitmap ResizeImage(this Bitmap image, Size bounds)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Size size = new Size(image.Width, image.Height).AspectRatioBoundsSize(bounds);

            Bitmap thumbnail = new Bitmap(size.Width, size.Height, image.PixelFormat);
            using Graphics gfx = Graphics.FromImage(thumbnail);
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rectangle = new Rectangle(0, 0, size.Width, size.Height);
            gfx.DrawImage(image, rectangle);

            return thumbnail;
        }

        public static Bitmap GetTextImage(String message)
        {
            return GetTextImage(message, Color.White, Color.Black);
        }

        public static Bitmap GetTextImage(String message, Font? font)
        {
            return GetTextImage(message, font, Color.White, Color.Black);
        }

        public static Bitmap GetTextImage(String message, Color background, Color foreground)
        {
            return GetTextImage(message, null, background, foreground);
        }

        public static Bitmap GetTextImage(String message, Font? font, Color background, Color foreground)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            font ??= new Font("Arial", 12);

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

        public static Rectangle GetRectangleForText(String text, String font, Single size)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return GetRectangleForText(text, new Font(font, size));
        }

        public static Rectangle GetRectangleForText(String text, Font font)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (font is null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            using Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using Graphics gfx = Graphics.FromImage(bmp);
            SizeF stringSize = gfx.MeasureString(text, font);

            return new Rectangle { Width = (Int32) stringSize.Width + 20, Height = (Int32) stringSize.Height + 10 };
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
            return ToBytes(image, null);
        }

        public static Byte[] ToBytes(this Image image, ImageFormat? format)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using MemoryStream stream = new MemoryStream(image.Width * image.Height * 4);
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

        public static Boolean TrySave(this Image image, Stream stream, ImageFormat format)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                image.Save(stream, format);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySave(this Image image, Stream stream, ImageCodecInfo encoder, EncoderParameters? parameters)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                image.Save(stream, encoder, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySave(this Image image, String filename)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                image.Save(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySave(this Image image, String filename, ImageFormat format)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                image.Save(filename, format);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TrySave(this Image image, String filename, ImageCodecInfo encoder, EncoderParameters? parameters)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            try
            {
                image.Save(filename, encoder, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

            Bitmap bitmap = new Bitmap(width, height);

            using Graphics graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(bitmap.Width / 2F, bitmap.Height / 2F);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-bitmap.Width / 2F, -bitmap.Height / 2F);
            graphics.DrawImage(image, new Point((width - image.Width) / 2, (height - image.Height) / 2));

            return bitmap;
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

            Rectangle rectangle = new Rectangle(0, 0, width, height);
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
            graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Bitmap SetOpacity(this Image image, Double opacity)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (image.PixelFormat.HasFlag(PixelFormat.Indexed))
            {
                throw new ArgumentException("Cannot change the opacity of an indexed image.");
            }

            Bitmap bitmap = (Bitmap) image.Clone();

            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            try
            {
                const Int32 BytePerPixel = 4;
                Byte[] pixels = new Byte[bitmap.Width * bitmap.Height * BytePerPixel];

                IntPtr pointer = data.Scan0;
                Marshal.Copy(pointer, pixels, 0, pixels.Length);

                opacity = Math.Clamp(opacity, 0, 1);
                for (Int32 counter = 0; counter < pixels.Length; counter += BytePerPixel)
                {
                    Int32 position = counter + BytePerPixel - 1;

                    if (pixels[position] == 0)
                    {
                        continue;
                    }

                    pixels[position] = unchecked((Byte) (pixels[position] * opacity));
                }

                Marshal.Copy(pixels, 0, pointer, pixels.Length);
                return bitmap;
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        private static class ColorMatrixCache
        {
            private static Single[][] Matrix { get; } =
            {
                new[] { 1.0F, 0.0F, 0.0F, 0.0F, 0.0F },
                new[] { 0.0F, 1.0F, 0.0F, 0.0F, 0.0F },
                new[] { 0.0F, 0.0F, 1.0F, 0.0F, 0.0F },
                new[] { 0.0F, 0.0F, 0.0F, 1.0F, 0.0F },
                new[] { 0.0F, 0.0F, 0.0F, 0.0F, 1.0F }
            };

            public static ColorMatrix CreateAlphaBlending(Single opacity)
            {
                return new ColorMatrix(Matrix)
                {
                    [3, 3] = Math.Clamp(opacity, 0, 1)
                };
            }

            public static ColorMatrix CreateAlphaBlending(Double opacity)
            {
                return CreateAlphaBlending((Single) opacity);
            }
        }

        public static Bitmap AlphaBlending(this Image first, Image second, Double opacity)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            Bitmap bitmap = new Bitmap(Math.Max(first.Width, second.Width), Math.Max(first.Height, second.Height));

            using ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(ColorMatrixCache.CreateAlphaBlending(opacity), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(first, 0, 0, first.Width, first.Height);
            graphics.DrawImage(second, new Rectangle(0, 0, second.Width, second.Height), 0, 0, second.Width, second.Height, GraphicsUnit.Pixel, attributes);

            return bitmap;
        }

        public static Byte[] Hashing(this Image image)
        {
            return Hashing(image, CryptographyUtilities.DefaultHashType);
        }

        public static Byte[] Hashing(this Image image, HashType type)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Byte[] destination = new Byte[type.Size()];
            if (!Hashing(image, destination, type))
            {
                throw new InvalidOperationException("Failed to hash image.");
            }

            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Image image, Span<Byte> destination)
        {
            return Hashing(image, destination, CryptographyUtilities.DefaultHashType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Image image, Span<Byte> destination, HashType type)
        {
            return Hashing(image, destination, type, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Hashing(this Image image, Span<Byte> destination, out Int32 written)
        {
            return Hashing(image, destination, CryptographyUtilities.DefaultHashType, out written);
        }

        public static unsafe Boolean Hashing(this Image image, Span<Byte> destination, HashType type, out Int32 written)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (destination.Length < type.Size())
            {
                throw new ArgumentException("The destination buffer is too small.", nameof(destination));
            }

            if (image is not Bitmap bitmap)
            {
                return image.ToBytes().Hashing(destination, type, out written);
            }

            BitmapData data = bitmap.LockBits(new Rectangle(new Point(0, 0), bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr firstscan = data.Scan0;
                Int32 stride = data.Stride;

                ReadOnlySpan<Byte> info = new ReadOnlySpan<Byte>(firstscan.ToPointer(), stride * bitmap.Height);
                return info.Hashing(destination, type, out written);
            }
            finally
            {
                bitmap.UnlockBits(data);
            }
        }

        [DllImport("msvcrt.dll")]
        private static extern Int32 memcmp(IntPtr first, IntPtr second, Int64 count);

        public static Boolean CompareBitmap(Bitmap? first, Bitmap? second)
        {
            if (first is null || second is null)
            {
                return first == second;
            }

            if (first.Size != second.Size)
            {
                return false;
            }

            BitmapData firstdata = first.LockBits(new Rectangle(new Point(0, 0), first.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData seconddata = second.LockBits(new Rectangle(new Point(0, 0), second.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr firstscan = firstdata.Scan0;
                IntPtr secondscan = seconddata.Scan0;

                Int32 stride = firstdata.Stride;

                return memcmp(firstscan, secondscan, stride * first.Height) == 0;
            }
            finally
            {
                first.UnlockBits(firstdata);
                second.UnlockBits(seconddata);
            }
        }
    }
}