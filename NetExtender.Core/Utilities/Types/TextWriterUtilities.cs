// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Utilities.Types
{
    public static class TextWriterUtilities
    {
        private static Type NullWriterType { get; } = TextWriter.Null.GetType();
        private static Type SynchronizedWriterType { get; } = TextWriter.Synchronized(TextWriter.Null).GetType();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this TextWriter? writer)
        {
            return writer is null || writer.GetType() == NullWriterType;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSynchronized(this TextWriter? writer)
        {
            return writer is not null && writer.GetType() == SynchronizedWriterType;
        }

        public static void WriteLine(this TextWriter writer, params String?[]? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            writer.WriteLine(String.Join(writer.NewLine, values));
        }

        public static void WriteLine(this TextWriter writer, params Object?[]? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            writer.WriteLine(String.Join(writer.NewLine, values));
        }

        public static void WriteLine(this TextWriter writer, IEnumerable<String?>? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            foreach (String? value in values)
            {
                writer.WriteLine(value);
            }
        }

        public static void WriteLine(this TextWriter writer, IEnumerable<Object?>? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            foreach (Object? value in values)
            {
                writer.WriteLine(value);
            }
        }

        public static async Task WriteLineAsync(this TextWriter writer, IEnumerable<String?>? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            foreach (String? value in values)
            {
                await writer.WriteLineAsync(value).ConfigureAwait(false);
            }
        }

        public static async ValueTask WriteLineAsync(this TextWriter writer, IAsyncEnumerable<String?>? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            await foreach (String? value in values)
            {
                await writer.WriteLineAsync(value).ConfigureAwait(false);
            }
        }

        public static void WriteLineTokens(this TextWriter writer, params String?[]? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            writer.WriteLine(String.Join(' ', values));
        }

        public static void WriteLineTokens(this TextWriter writer, params Object?[]? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            writer.WriteLine(String.Join(' ', values));
        }

        public static async Task WriteLineTokensAsync(this TextWriter writer, params String?[]? values)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (values is null)
            {
                return;
            }

            // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
            await writer.WriteLineAsync(String.Join(' ', values)).ConfigureAwait(false);
        }
    }
}