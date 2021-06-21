// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NetExtender.ImageSharp.Hashing.Interfaces
{
    public interface ISharpImageHashEvaluator
    {
        UInt64 Hash(Image<Rgba32> image);
    }
}