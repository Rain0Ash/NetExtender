// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DynamicData.Annotations;

namespace NetExtender.Utils.Types
{
    public static class NullableUtils
    {
        /// <summary>
        /// Returns nullable of specified value.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">THe value</param>
        /// <returns><paramref name="value"/> wrapped in nullable.</returns>
        [Pure]
        public static T? AsNullable<T>(this T value) where T : struct
        {
            return value;
        }
    }
}