// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.TextReaders;

namespace NetExtender.Utilities.Types
{
    public static class TextReaderUtilities
    {
        private static Type NullReaderType { get; } = TextReader.Null.GetType();
        private static Type SynchronizedReaderType { get; } = TextReader.Synchronized(TextReader.Null).GetType();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this TextWriter? reader)
        {
            return reader is null || reader.GetType() == NullReaderType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSynchronized(this TextReader? reader)
        {
            return reader is not null && reader.GetType() == SynchronizedReaderType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> ReadLines(this TextReader reader)
        {
            return ReadLines(reader, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> ReadLines(this TextReader reader, Boolean disposing)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return new TextLineEnumerator(reader, disposing);
        }

        public static IAsyncEnumerable<String> ReadLineAsync(this TextReader reader)
        {
            return ReadLineAsync(reader, false);
        }

        public static IAsyncEnumerable<String> ReadLineAsync(this TextReader reader, Boolean disposing)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return new TextLineEnumerator(reader, disposing);
        }

        public static String[] ReadLinesToEnd(this TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return reader.ReadToEnd().SplitBy(SplitType.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        private static String? ReadTokenCore(TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            Int32 value;
            while ((value = reader.Read()) != -1)
            {
                if (!Char.IsWhiteSpace((Char) value))
                {
                    break;
                }
            }

            StringBuilder result = new StringBuilder();
            result.Append((Char) value);

            while ((value = reader.Read()) != -1)
            {
                if (Char.IsWhiteSpace((Char) value))
                {
                    break;
                }

                result.Append((Char) value);
            }

            return result.Length > 0 ? result.ToString() : null;
        }

        public static String? ReadToken(this TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (!reader.IsSynchronized())
            {
                return ReadTokenCore(reader);
            }

            lock (reader)
            {
                return ReadTokenCore(reader);
            }
        }

        public static String[]? ReadTokensInLine(this TextReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return reader.ReadLine()?.SplitBy(SplitType.NewLineAndSpace, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}