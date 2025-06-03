// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.ImageSharp.Hashing.Interfaces;
using NetExtender.Utilities.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace NetExtender.ImageSharp.Utilities
{
    public static class SharpImageHashingUtilities
    {
        private static Byte[] BitCounts { get; } =
        {
            0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7,
            3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 4, 5, 5, 6, 5, 6, 6, 7, 5, 6, 6, 7, 6, 7, 7, 8
        };

        public static UInt64 Hashing(this ISharpImageHashEvaluator evaluator, Stream stream)
        {
            if (evaluator is null)
            {
                throw new ArgumentNullException(nameof(evaluator));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using Image<Rgba32> image = Image.Load<Rgba32>(stream);
            return evaluator.Hash(image);
        }

        public static UInt64 AverageHashing(Image<Rgba32> image)
        {
            const Int32 Width = 8;
            const Int32 Height = 8;
            const Int32 Size = Width * Height;
            const UInt64 MostSignificantBitMask = 1UL << (Size - 1);

            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            image.Mutate(context => context.Resize(Width, Height).Grayscale(GrayscaleMode.Bt601).AutoOrient());

            UInt32 average = 0;

            for (Int32 y = 0; y < Height; y++)
            {
                Int32 index = y;
                UInt32 sum = 0;
                
                void ProcessPixels(PixelAccessor<Rgba32> accessor)
                {
                    Span<Rgba32> row = accessor.GetRowSpan(index);
                    for (Int32 x = 0; x < Width; x++)
                    {
                        sum += row[x].R;
                    }
                }

                image.ProcessPixelRows(ProcessPixels);
                average += sum;
            }

            average /= Size;

            UInt64 hash = 0UL;
            UInt64 mask = MostSignificantBitMask;

            for (Int32 y = 0; y < Height; y++)
            {
                Int32 index = y;

                void ProcessPixels(PixelAccessor<Rgba32> accessor)
                {
                    Span<Rgba32> row = accessor.GetRowSpan(index);

                    for (Int32 x = 0; x < Width; x++)
                    {
                        if (row[x].R >= average)
                        {
                            hash |= mask;
                        }

                        mask >>= 1;
                    }
                }

                image.ProcessPixelRows(ProcessPixels);
            }

            return hash;
        }

        public static UInt64 DifferenceHashing(Image<Rgba32> image)
        {
            const Int32 Width = 9;
            const Int32 Height = 8;

            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            image.Mutate(context => context.AutoOrient().Resize(Width, Height).Grayscale(GrayscaleMode.Bt601));

            UInt64 mask = 1UL << (Height * (Width - 1) - 1);
            UInt64 hash = 0UL;

            for (Int32 y = 0; y < Height; y++)
            {
                Int32 index = y;
                
                void ProcessPixels(PixelAccessor<Rgba32> accessor)
                {
                    Span<Rgba32> row = accessor.GetRowSpan(index);
                    Rgba32 left = row[0];

                    for (Int32 x = 1; x < Width; x++)
                    {
                        Rgba32 right = row[x];

                        if (left.R < right.R)
                        {
                            hash |= mask;
                        }

                        left = right;
                        mask >>= 1;
                    }
                }

                image.ProcessPixelRows(ProcessPixels);
            }

            return hash;
        }

        // ReSharper disable once CognitiveComplexity
        public static UInt64 PerceptualHashing(Image<Rgba32> image)
        {
            const Int32 Size = 64;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Double[] Dct1D(IReadOnlyList<Double> values)
            {
                Double[] array = new Double[Size];

                for (Int32 coefficient = 0; coefficient < Size; coefficient++)
                {
                    for (Int32 i = 0; i < Size; i++)
                    {
                        array[coefficient] += values[i] * Math.Cos((2.0 * i + 1.0) * coefficient * Math.PI / (2.0 * Size));
                    }

                    array[coefficient] *= MathUtilities.Constants.Double.Sqrt2 / 8;

                    if (coefficient == 0)
                    {
                        array[coefficient] *= MathUtilities.Constants.Double.ISqrt2;
                    }
                }

                return array;
            }

            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Double[][] rows = new Double[Size][];
            Double[] sequence = new Double[Size];
            Double[][] matrix = new Double[Size][];

            image.Mutate(context => context.Resize(Size, Size).Grayscale(GrayscaleMode.Bt601).AutoOrient());

            for (Int32 y = 0; y < Size; y++)
            {
                for (Int32 x = 0; x < Size; x++)
                {
                    sequence[x] = image[x, y].R;
                }

                rows[y] = Dct1D(sequence);
            }

            for (Int32 x = 0; x < Size; x++)
            {
                for (Int32 y = 0; y < Size; y++)
                {
                    sequence[y] = rows[y][x];
                }

                matrix[x] = Dct1D(sequence);
            }

            List<Double> top8X8 = new List<Double>(Size);
            for (Int32 y = 0; y < 8; y++)
            {
                for (Int32 x = 0; x < 8; x++)
                {
                    top8X8.Add(matrix[y][x]);
                }
            }

            Double[] right = top8X8.ToArray();

            Double median = right.OrderBy(value => value).Skip(31).Take(2).Average();

            UInt64 mask = 1UL << (Size - 1);
            UInt64 hash = 0UL;

            for (Int32 i = 0; i < Size; i++)
            {
                if (right[i] > median)
                {
                    hash |= mask;
                }

                mask >>= 1;
            }

            return hash;
        }

        public static Double Similarity(UInt64 first, UInt64 second)
        {
            return (64 - BitCount(first ^ second)) * 100 / 64.0;
        }

        public static Double Similarity(Byte[] first, Byte[] second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (first.Length != 8)
            {
                throw new ArgumentOutOfRangeException(nameof(first), first.Length, null);
            }

            if (second.Length != 8)
            {
                throw new ArgumentOutOfRangeException(nameof(second), second.Length, null);
            }

            UInt64 fhash = BitConverter.ToUInt64(first, 0);
            UInt64 shash = BitConverter.ToUInt64(second, 0);

            return Similarity(fhash, shash);
        }

        private static UInt32 BitCount(UInt64 value)
        {
            UInt32 count = 0;
            for (; value > 0; value >>= 8)
            {
                count += BitCounts[value & 0xFF];
            }

            return count;
        }
    }
}