// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Drawing.Colors;
using NetExtender.Types.Geometry;
using NetExtender.Utilities.Cryptography;
using NetExtender.Utilities.IO;
using NetExtender.Windows.Types;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectBitmap.Enumerator GetEnumerator(this DirectBitmap bitmap)
        {
            return GetEnumerator(bitmap, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectBitmap.Enumerator GetEnumerator(this DirectBitmap bitmap, GeometryRotationType rotation)
        {
            return new DirectBitmap.Enumerator(bitmap, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectBitmap.Enumerator GetEnumerator(this DirectBitmap bitmap, Rectangle rectangle)
        {
            return GetEnumerator(bitmap, rectangle, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectBitmap.Enumerator GetEnumerator(this DirectBitmap bitmap, Rectangle rectangle, GeometryRotationType rotation)
        {
            return new DirectBitmap.Enumerator(bitmap, rectangle, rotation);
        }

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

            Size size = new Size(image.Width, image.Height).AspectRatioBounds(bounds);

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeContrast(this Bitmap bitmap, Double contrast)
        {
            return MakeContrast(bitmap, contrast, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeContrast(this Bitmap bitmap, Double contrast, Boolean transparency)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeContrast(direct, contrast, transparency);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeContrast(this DirectBitmap bitmap, Double contrast)
        {
            return MakeContrast(bitmap, contrast, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean MakeContrast(this DirectBitmap bitmap, Double contrast, Boolean transparency)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            unchecked
            {
                Span<Byte> lookup = stackalloc Byte[Byte.MaxValue + 1];

                const Double percent = 100;
                Double c = (percent + contrast) * (1 / percent);
                c *= c;

                for (Int32 i = 0; i < Byte.MaxValue + 1; i++)
                {
                    Double value = i;
                    value *= 1.0 / Byte.MaxValue;
                    value -= 0.5;
                    value *= c;
                    value += 0.5;
                    value *= Byte.MaxValue;

                    value = value switch
                    {
                        < Byte.MinValue => Byte.MinValue,
                        > Byte.MaxValue => Byte.MaxValue,
                        _ => value
                    };

                    lookup[i] = (Byte) value;
                }

                Span<Byte> integer = stackalloc Byte[4];

                if (transparency)
                {
                    foreach (DirectPoint point in bitmap)
                    {
                        Int32 color = point.Color.ToArgb();
                        MemoryMarshal.Write(integer, ref color);

                        integer[0] = lookup[integer[0]]; // B
                        integer[1] = lookup[integer[1]]; // G
                        integer[2] = lookup[integer[2]]; // R
                        integer[3] = integer[3]; // A

                        point.Color = Color.FromArgb(MemoryMarshal.Read<Int32>(integer));
                    }
                }
                else
                {
                    foreach (DirectPoint point in bitmap)
                    {
                        Int32 color = point.Color.ToArgb();
                        MemoryMarshal.Write(integer, ref color);

                        integer[0] = lookup[integer[0]]; // B
                        integer[1] = lookup[integer[1]]; // G
                        integer[2] = lookup[integer[2]]; // R
                        integer[3] = Byte.MaxValue; // A

                        point.Color = Color.FromArgb(MemoryMarshal.Read<Int32>(integer));
                    }
                }

                return true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeBlackWhite(this Bitmap bitmap)
        {
            return MakeBlackWhite(bitmap, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeBlackWhite(this Bitmap bitmap, Boolean transparency)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeBlackWhite(direct, transparency);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeBlackWhite(this DirectBitmap bitmap)
        {
            return MakeBlackWhite(bitmap, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean MakeBlackWhite(this DirectBitmap bitmap, Boolean transparency)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            Span<Double> lookup = stackalloc Double[Byte.MaxValue + 1];

            for (Int32 i = 0; i < lookup.Length; i++)
            {
                lookup[i] = ColorUtilities.RGBToSrgbItur(i);
            }

            Span<Byte> integer = stackalloc Byte[4];

            if (transparency)
            {
                foreach (DirectPoint point in bitmap)
                {
                    Int32 color = point.Color.ToArgb();
                    MemoryMarshal.Write(integer, ref color);

                    Double b = lookup[integer[0]]; // B
                    Double g = lookup[integer[1]]; // G
                    Double r = lookup[integer[2]]; // R
                    Byte a = integer[3]; // A
                    Double l = 0.2126 * r + 0.7152 * g + 0.0722 * b;

                    point.Color = l > 0.179 ? Color.FromArgb(a, Color.Black) : Color.FromArgb(a, Color.White);
                }
            }
            else
            {
                foreach (DirectPoint point in bitmap)
                {
                    Int32 color = point.Color.ToArgb();
                    MemoryMarshal.Write(integer, ref color);

                    Double b = lookup[integer[0]]; // B
                    Double g = lookup[integer[1]]; // G
                    Double r = lookup[integer[2]]; // R
                    Double l = 0.2126 * r + 0.7152 * g + 0.0722 * b;

                    point.Color = l > 0.179 ? Color.Black : Color.White;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeTransparent(direct);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this Bitmap bitmap, params Color[]? transparent)
        {
            return MakeTransparent(bitmap, (IEnumerable<Color>?) transparent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this Bitmap bitmap, IEnumerable<Color>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeTransparent(direct, transparent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this Bitmap bitmap, IEnumerable<Int32>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeTransparent(direct, transparent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this Bitmap bitmap, IEnumerable<UInt32>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            using DirectBitmap direct = bitmap.Direct();
            return MakeTransparent(direct, transparent);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean MakeTransparent(this DirectBitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            const UInt32 alpha = Byte.MaxValue;
            
            foreach (DirectPoint point in bitmap)
            {
                UInt32 color = unchecked((UInt32) point.Color.ToArgb()) | alpha;

                if (color == (UInt32.MinValue | alpha) || color == UInt32.MaxValue)
                {
                    point.Color = Color.Transparent;
                }
            }

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this DirectBitmap bitmap, params Color[]? transparent)
        {
            return MakeTransparent(bitmap, (IEnumerable<Color>?) transparent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this DirectBitmap bitmap, IEnumerable<Color>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            return MakeTransparent(bitmap, transparent?.Select(item => unchecked((UInt32) item.ToArgb())));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean MakeTransparent(this DirectBitmap bitmap, IEnumerable<Int32>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            return MakeTransparent(bitmap, transparent?.Select(item => unchecked((UInt32) item)));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean MakeTransparent(this DirectBitmap bitmap, IEnumerable<UInt32>? transparent)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            const UInt32 alpha = Byte.MaxValue;
            ImmutableHashSet<UInt32> set = transparent?.Select(value => value | alpha).ToImmutableHashSet() ?? ImmutableHashSet<UInt32>.Empty;

            if (set.Count <= 0)
            {
                set = set.Add(UInt32.MinValue | alpha).Add(UInt32.MaxValue);
            }

            foreach (DirectPoint point in bitmap)
            {
                UInt32 color = unchecked((UInt32) point.Color.ToArgb()) | alpha;

                if (set.Contains(color))
                {
                    point.Color = Color.Transparent;
                }
            }

            return true;
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

            BitmapData data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

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

        public static unsafe Boolean Copy(this Bitmap bitmap, Bitmap other)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (bitmap == other)
            {
                return false;
            }

            if (bitmap.Width != other.Width || bitmap.Height != other.Height || bitmap.PixelFormat != other.PixelFormat)
            {
                return false;
            }

            using DirectBitmap source = bitmap.Direct();
            using DirectBitmap target = other.Direct();
            
            UInt64 count = (UInt64) (source.Height * source.Stride * (Int64) DirectBitmap.BytesPerPixel);
            Buffer.MemoryCopy(source.Scan0.ToPointer(), target.Scan0.ToPointer(), count, count);
            return true;
        }
        
        public static Boolean Copy(this Bitmap bitmap, Bitmap other, Rectangle source, Rectangle target)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Rectangle r1 = new Rectangle(Point.Empty, bitmap.Size);
            Rectangle r2 = new Rectangle(Point.Empty, other.Size);

            if (r1 == r2 && r1 == source && r2 == target)
            {
                return Copy(bitmap, other);
            }

            using DirectBitmap direct = other.Direct();
            direct.Copy(bitmap, source, target);
            return true;
        }

        public static Bitmap? Slice(this Bitmap bitmap, Rectangle region)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (region.Width <= 0 || region.Height <= 0)
            {
                return null;
            }

            Rectangle intersect = Rectangle.Intersect(new Rectangle(Point.Empty, bitmap.Size), region);

            if (intersect.IsEmpty)
            {
                return null;
            }

            Bitmap slice = new Bitmap(intersect.Width, intersect.Height);
            Copy(bitmap, slice, intersect, new Rectangle(Point.Empty, intersect.Size));
            return slice;
        }

        public static Boolean Clear(this Bitmap bitmap, Color color)
        {
            return Clear(bitmap, color.ToArgb());
        }

        public static Boolean Clear(this Bitmap bitmap, Int32 color)
        {
            using DirectBitmap direct = bitmap.Direct();
            return direct.Clear(color);
        }

        public static DirectBitmap Direct(this Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            DirectBitmap direct = new DirectBitmap(bitmap);
            direct.Lock();
            return direct;
        }
	
        public static DirectBitmap Direct(this Bitmap bitmap, DirectBitmapLockFormat format)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            DirectBitmap direct = new DirectBitmap(bitmap);
            direct.Lock(format);
            return direct;
        }

        public static Bitmap DeepClone(this Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            return bitmap.Clone(new Rectangle(default, bitmap.Size), bitmap.PixelFormat);
        }

        [DllImport("msvcrt.dll")]
        private static extern Int32 memcmp(IntPtr first, IntPtr second, Int64 count);

        public static Boolean Equals(this Bitmap? bitmap, Bitmap? other)
        {
            if (bitmap == other)
            {
                return true;
            }
            
            if (bitmap is null || other is null)
            {
                return false;
            }

            if (bitmap.Size != other.Size)
            {
                return false;
            }

            BitmapData first = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData second = other.LockBits(new Rectangle(Point.Empty, other.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                return memcmp(first.Scan0, second.Scan0, first.Stride * bitmap.Height) == 0;
            }
            finally
            {
                bitmap.UnlockBits(first);
                other.UnlockBits(second);
            }
        }
    }
}