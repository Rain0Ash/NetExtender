// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Drawing.Colors.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Windows.Types
{
    public enum DirectBitmapLockFormat
    {
        /// <summary>
        /// Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components. The remaining 8 bits are not used.
        /// </summary>
        Format32bppRgb = 139273,

        /// <summary>
        /// Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.
        /// The red, green, and blue components are premultiplied, according to the alpha component.
        /// </summary>
        Format32bppPArgb = 925707,

        /// <summary>
        /// Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.
        /// </summary>
        Format32bppArgb = 2498570,
    }

    public sealed unsafe class DirectBitmap : IDisposable
    {
        public const Int32 BytesPerPixel = 4;

        private Bitmap Bitmap { get; }
        private BitmapData? Data { get; set; }

        private Int32* LockScan0
        {
            get
            {
                return Data is not null ? (Int32*) Data.Scan0 : (Int32*) IntPtr.Zero;
            }
        }

        public Size Size { get; }

        public Int32 Stride
        {
            get
            {
                return Data?.Stride / BytesPerPixel ?? 0;
            }
        }

        public Int32 Width
        {
            get
            {
                return Size.Width;
            }
        }

        public Int32 Height
        {
            get
            {
                return Size.Height;
            }
        }

        public IntPtr Scan0
        {
            get
            {
                return Data?.Scan0 ?? IntPtr.Zero;
            }
        }

        public Boolean IsUnlock
        {
            get
            {
                return Data is null;
            }
        }

        public DirectBitmap(Bitmap bitmap)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            Int32 depth = Image.GetPixelFormatSize(bitmap.PixelFormat);
            if (depth != BitUtilities.BitInByte * BytesPerPixel)
            {
                throw new ArgumentException($@"Bitmap must have a 32bpp depth instead of '{depth}'.", nameof(bitmap));
            }

            Bitmap = bitmap;
            Size = bitmap.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 index, out Color color)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }
            
            if (!TryGetPixel(index, out Int32 pixel))
            {
                color = default;
                return false;
            }
            
            color = Color.FromArgb(pixel);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel<TColor>(Int32 index, [MaybeNullWhen(false)] out TColor color) where TColor : IColor
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (!TryGetPixel(index, out Color pixel))
            {
                color = default;
                return false;
            }

            color = pixel.ToColor<TColor>();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 index, out Int32 pixel)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (index < 0 || index >= Height * Stride)
            {
                pixel = default;
                return false;
            }

            pixel = *(LockScan0 + index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 index, out UInt32 pixel)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (index < 0 || index >= Height * Stride)
            {
                pixel = default;
                return false;
            }

            pixel = *((UInt32*) LockScan0 + index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 x, Int32 y, out Color color)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }
            
            if (!TryGetPixel(x, y, out Int32 pixel))
            {
                color = default;
                return false;
            }
            
            color = Color.FromArgb(pixel);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel<TColor>(Int32 x, Int32 y, [MaybeNullWhen(false)] out TColor color) where TColor : IColor
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (!TryGetPixel(x, y, out Color pixel))
            {
                color = default;
                return false;
            }

            color = pixel.ToColor<TColor>();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 x, Int32 y, out Int32 pixel)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                pixel = default;
                return false;
            }

            pixel = *(LockScan0 + x + y * Stride);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryGetPixel(Int32 x, Int32 y, out UInt32 pixel)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                pixel = default;
                return false;
            }

            pixel = *((UInt32*) LockScan0 + x + y * Stride);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel(Int32 index, Color color)
        {
            return TrySetPixel(index, color.ToArgb());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel<TColor>(Int32 index, TColor color) where TColor : IColor
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }
            
            if (color is null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            return TrySetPixel(index, color.ToColor());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel(Int32 index, Int32 color)
        {
            return TrySetPixel(index, unchecked((UInt32) color));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TrySetPixel(Int32 index, UInt32 color)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (index < 0 || index >= Height * Stride)
            {
                return false;
            }

            *(UInt32*) (LockScan0 + index) = color;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel(Int32 x, Int32 y, Color color)
        {
            return TrySetPixel(x, y, color.ToArgb());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel<TColor>(Int32 x, Int32 y, TColor color) where TColor : IColor
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }
            
            if (color is null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            return TrySetPixel(x, y, color.ToColor());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TrySetPixel(Int32 x, Int32 y, Int32 color)
        {
            return TrySetPixel(x, y, unchecked((UInt32) color));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TrySetPixel(Int32 x, Int32 y, UInt32 color)
        {
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }

            *(UInt32*) (LockScan0 + x + y * Stride) = color;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Clear(Color color)
        {
            return Clear(color.ToArgb());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Clear(Int32 pixel)
        {
            Boolean unlock = false;
            if (Data is null)
            {
                Lock();
                unlock = true;
            }
            
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            Int32 count = Width * Height;
            Int32* scan = LockScan0;

            Int32 component = pixel & 0xFF;
            if (component == ((pixel >> 8) & 0xFF) && component == ((pixel >> 16) & 0xFF) && component == ((pixel >> 24) & 0xFF))
            {
                Unsafe.InitBlock(scan, unchecked((Byte) component), (UInt32) (Height * Stride * BytesPerPixel));
                return true;
            }

            const Int32 block = 8;
            count = Math.DivRem(count, block, out Int32 remainder);

            while (count-- > 0)
            {
                *scan++ = pixel;
                *scan++ = pixel;
                *scan++ = pixel;
                *scan++ = pixel;

                *scan++ = pixel;
                *scan++ = pixel;
                *scan++ = pixel;
                *scan++ = pixel;
            }

            while (remainder-- > 0)
            {
                *scan++ = pixel;
            }

            if (unlock)
            {
                Unlock();
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Clear(Rectangle region, Color color)
        {
            return Clear(region, color.ToArgb());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Clear(Rectangle region, Int32 pixel)
        {
            Rectangle bitmap = new Rectangle(Point.Empty, Size);
            
            if (!region.IntersectsWith(bitmap))
            {
                return false;
            }

            if (region == bitmap)
            {
                return Clear(pixel);
            }

            const Int32 bound = 16;
            if (region.Bottom - region.Top < bound)
            {
                for (Int32 y = region.Top; y < region.Bottom; y++)
                {
                    for (Int32 x = region.Left; x < region.Right; x++)
                    {
                        *(LockScan0 + x + y * Stride) = pixel;
                    }
                }

                return true;
            }

            UInt64 stride = (UInt64) region.Width * BytesPerPixel;

            Int32 component = pixel & 0xFF;
            if (component == ((pixel >> 8) & 0xFF) && component == ((pixel >> 16) & 0xFF) && component == ((pixel >> 24) & 0xFF))
            {
                for (Int32 y = region.Top; y < region.Bottom; y++)
                {
                    Unsafe.InitBlock(LockScan0 + region.Left + y * Stride, unchecked((Byte) component), (UInt32) stride);
                }
                
                return true;
            }

            Int32[] row = new Int32[region.Width];
            
            const Int32 block = 8;
            Int32 count = Math.DivRem(row.Length, block, out Int32 remainder);

            fixed (Int32* pointer = row)
            {
                Int32 index = count;
                
                Int32* source = pointer;
                while (index-- > 0)
                {
                    *source++ = pixel;
                    *source++ = pixel;
                    *source++ = pixel;
                    *source++ = pixel;

                    *source++ = pixel;
                    *source++ = pixel;
                    *source++ = pixel;
                    *source++ = pixel;
                }

                while (remainder-- > 0)
                {
                    *source++ = pixel;
                }

                Int32* scan = LockScan0 + region.Left;
                for (Int32 y = region.Top; y < region.Bottom; y++)
                {
                    Buffer.MemoryCopy(pointer, scan + y * Stride, stride, stride);
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Copy(ReadOnlySpan<Int32> values)
        {
            return Copy(values, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean Copy(ReadOnlySpan<Int32> values, Boolean without)
        {
            Int32 count = Width * Height;

            if (values.Length != count)
            {
                return false;
            }

            Int32* target = LockScan0;

            fixed (Int32* source = values)
            {
                Int32* scan = source;

                if (without)
                {
                    while (count-- > 0)
                    {
                        if (*scan == 0)
                        {
                            target++;
                            scan++;
                            continue;
                        }

                        *target++ = *scan++;
                    }
                    
                    return true;
                }

                const Int32 block = 8;
                count = Math.DivRem(count, block, out Int32 remainder);

                while (count-- > 0)
                {
                    *target++ = *scan++;
                    *target++ = *scan++;
                    *target++ = *scan++;
                    *target++ = *scan++;

                    *target++ = *scan++;
                    *target++ = *scan++;
                    *target++ = *scan++;
                    *target++ = *scan++;
                }

                while (remainder-- > 0)
                {
                    *target++ = *scan++;
                }
            }

            return true;
        }

        public Boolean Copy(Bitmap bitmap, Rectangle source, Rectangle target)
        {
            if (bitmap is null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (bitmap == Bitmap)
            {
                return false;
            }

            Rectangle first = new Rectangle(Point.Empty, bitmap.Size);
            Rectangle second = new Rectangle(0, 0, Width, Height);

            if (source.Width <= 0 || source.Height <= 0 || target.Width <= 0 || target.Height <= 0 || !first.IntersectsWith(source) || !target.IntersectsWith(second))
            {
                return false;
            }

            first = Rectangle.Intersect(source, first);
            first = Rectangle.Intersect(first, new Rectangle(source.X, source.Y, target.Width, target.Height));
            second = Rectangle.Intersect(target, second);
            first = Rectangle.Intersect(first, new Rectangle(-target.X + source.X, -target.Y + source.Y, Width, Height));

            Int32 width = Math.Min(first.Width, second.Width);
            Int32 height = Math.Min(first.Height, second.Height);

            if (width <= 0 || height <= 0)
            {
                return false;
            }

            using DirectBitmap direct = bitmap.Direct();
            UInt64 stride = (UInt64) width * BytesPerPixel;

            for (Int32 y = 0; y < height; y++)
            {
                void* spointer = direct.LockScan0 + first.Left + (first.Top + y) * direct.Stride;
                void* dpointer = LockScan0 + second.Left + (second.Top + y) * Stride;
                Buffer.MemoryCopy(spointer, dpointer, stride, stride);
            }

            return true;
        }

        public Int32[] ToArray()
        {
            Boolean unlock = false;
            if (Data is null)
            {
                Lock();
                unlock = true;
            }
            
            if (Data is null)
            {
                throw new InvalidOperationException("Must be locked before any pixel operations.");
            }

            Int32 bytes = Math.Abs(Data.Stride) * Bitmap.Height;
            Int32[] color = new Int32[bytes / BytesPerPixel];

            Marshal.Copy(Data.Scan0, color, 0, bytes / BytesPerPixel);

            if (unlock)
            {
                Unlock();
            }

            return color;
        }

        public Locker Lock()
        {
            return Lock((DirectBitmapLockFormat) Bitmap.PixelFormat);
        }

        public Locker Lock(DirectBitmapLockFormat format)
        {
            if (Data is not null)
            {
                throw new InvalidOperationException("Unlock must be called before a Lock operation");
            }

            return Lock(ImageLockMode.ReadWrite, (PixelFormat) format);
        }

        private Locker Lock(ImageLockMode mode, PixelFormat format)
        {
            Rectangle rectangle = new Rectangle(Point.Empty, Bitmap.Size);
            return Lock(mode, rectangle, format);
        }

        private Locker Lock(ImageLockMode mode, Rectangle rectangle, PixelFormat pixelFormat)
        {
            Data = Bitmap.LockBits(rectangle, mode, pixelFormat);
            return new Locker(this);
        }

        public Boolean Unlock()
        {
            if (Data is null)
            {
                return false;
            }

            Bitmap.UnlockBits(Data);
            return true;
        }

        public void Dispose()
        {
            Unlock();
        }

        public Color this[Int32 index]
        {
            get
            {
                if (Data is null)
                {
                    throw new InvalidOperationException("Must be locked before any pixel operations.");
                }

                return TryGetPixel(index, out Color color) ? color : throw new InvalidOperationException($"Invalid pixel at '{index}'");
            }
            set
            {
                if (Data is null)
                {
                    throw new InvalidOperationException("Must be locked before any pixel operations.");
                }

                if (!TrySetPixel(index, value))
                {
                    throw new InvalidOperationException($"Invalid pixel at '{index}'");
                }
            }
        }

        public Color this[Int32 x, Int32 y]
        {
            get
            {
                if (Data is null)
                {
                    throw new InvalidOperationException("Must be locked before any pixel operations.");
                }

                return TryGetPixel(x, y, out Color color) ? color : throw new InvalidOperationException($"Invalid pixel at '{x},{y}'");
            }
            set
            {
                if (Data is null)
                {
                    throw new InvalidOperationException("Must be locked before any pixel operations.");
                }

                if (!TrySetPixel(x, y, value))
                {
                    throw new InvalidOperationException($"Invalid pixel at '{x},{y}'");
                }
            }
        }

        public readonly struct Locker : IDisposable
        {
            public DirectBitmap Bitmap { get; }

            public Locker(DirectBitmap bitmap)
            {
                Bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            }

            public void Dispose()
            {
                Bitmap.Unlock();
            }
        }
    }
}