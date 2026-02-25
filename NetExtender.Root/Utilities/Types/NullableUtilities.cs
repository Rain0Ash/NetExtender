// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class NullableUtilities
    {
        /// <summary>
        /// Returns nullable of specified value.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">The value</param>
        /// <returns><paramref name="value"/> wrapped in nullable.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? AsNullable<T>(this T value) where T : struct
        {
            return value;
        }

        /// <summary>
        /// Return value or default
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">The value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToValue<T>(this T? value) where T : struct
        {
            return value ?? default;
        }
    }
}