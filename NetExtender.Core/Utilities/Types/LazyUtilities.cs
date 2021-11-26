// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Types
{
    public static class LazyUtilities
    {
        public static T Initialize<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static T ValueOr<T>(this Lazy<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : alternate;
        }
        
        public static T ValueOr<T>(this Lazy<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.IsValueCreated ? source.Value : alternate.Invoke();
        }
        
        public static T? ValueOrDefault<T>(this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : default;
        }
    }
}