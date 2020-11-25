// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utils.Types
{
    public static class LazyUtils
    {
        public static T Initialize<T>(this Lazy<T> source)
        {
            return source.Value;
        }

        public static T ValueOr<T>(this Lazy<T> source, T @default)
        {
            return source.IsValueCreated ? source.Value : @default;
        }
        
        public static T ValueOr<T>(this Lazy<T> source, Func<T> @default)
        {
            return source.IsValueCreated ? source.Value : @default.Invoke();
        }
        
        public static T ValueOrDefault<T>(this Lazy<T> source)
        {
            return source.IsValueCreated ? source.Value : default;
        }
    }
}