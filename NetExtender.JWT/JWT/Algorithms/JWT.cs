// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Utilities.Core;

namespace NetExtender.JWT.Algorithms
{
    [ReflectionNaming]
    public static partial class JWT
    {
        internal const UInt16 StackAlloc = 16384;
        internal static UTF8Encoding Encoding { get; } = new UTF8Encoding(false);
    }
}