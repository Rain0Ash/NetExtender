// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Types
{
    public static class ProgressUtilities
    {
        /// <inheritdoc cref="IProgress{T}.Report"/>
        public static void Report<T>(this IProgress<T> progress, T value)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            progress.Report(value);
        }
    }
}