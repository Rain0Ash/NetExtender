// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Numerics;

namespace NetExtender.Utils.Types
{
    public static class VectorUtils
    {
        public static Vector<T> Create<T>(params T[] items) where T : struct
        {
            return new Vector<T>(items);
        }
    }
}