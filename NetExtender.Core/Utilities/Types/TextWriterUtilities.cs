// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class TextWriterUtilities
    {
        private static Type SynchronizedWriterType { get; } = TextWriter.Synchronized(TextWriter.Null).GetType();

        internal static Boolean IsSynchronized(this TextWriter writer)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            return writer.GetType() == SynchronizedWriterType;
        }

        public static void WriteLine(this TextWriter writer, params String?[] values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            writer.WriteLine(String.Join(writer.NewLine, values));
        }

        public static void WriteLine(this TextWriter writer, params Object?[] values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            writer.WriteLine(String.Join(writer.NewLine, values));
        }

        public static void WriteLine(this TextWriter writer, IEnumerable<String?> values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (String? value in values)
            {
                writer.WriteLine(value);
            }
        }

        public static void WriteLine(this TextWriter writer, IEnumerable<Object?> values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (Object? value in values)
            {
                writer.WriteLine(value);
            }
        }

        public static async Task WriteLineAsync(this TextWriter writer, IEnumerable<String?> values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (String? value in values)
            {
                await writer.WriteLineAsync(value);
            }
        }

        public static async ValueTask WriteLineAsync(this TextWriter writer, IAsyncEnumerable<String?> values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            await foreach (String? value in values)
            {
                await writer.WriteLineAsync(value);
            }
        }

        public static void WriteLineTokens(this TextWriter writer, params String?[] values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            writer.WriteLine(String.Join(" ", values));
        }

        public static void WriteLineTokens(this TextWriter writer, params Object?[] values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            writer.WriteLine(String.Join(" ", values));
        }

        public static async Task WriteLineTokensAsync(this TextWriter writer, params String?[] values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            await writer.WriteLineAsync(String.Join(" ", values));
        }
    }
}