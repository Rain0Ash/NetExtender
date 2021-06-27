// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public enum ConsoleInputType
    {
        None,
        Line,
        KeyInfo,
        KeyInfoIntercept,
        KeyCode
    }
    
    public static class ConsoleUtils
    {
        public static event ConsoleCancelEventHandler CancelKeyPress
        {
            add
            {
                Console.CancelKeyPress += value;
            }
            remove
            {
                Console.CancelKeyPress -= value;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>()
        {
            return CastReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>(CultureInfo info)
        {
            return CastReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? ReadAsEnumerable<T>()
        {
            return ReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? ReadAsEnumerable<T>(CultureInfo info)
        {
            return ReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? ReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? ReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.Convert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? TryReadAsEnumerable<T>()
        {
            return TryReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? TryReadAsEnumerable<T>(CultureInfo info)
        {
            return TryReadAsEnumerable<T>(StringUtils.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? TryReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.TryConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? TryReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.TryConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearLine()
        {
            ClearLine(Console.CursorTop);
        }

        public static void ClearLine(Int32 line)
        {
            Console.SetCursorPosition(0, line);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear()
        {
            Console.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value)
        {
            Write(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, IFormatProvider? provider)
        {
            Write(value, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, EscapeType escape)
        {
            Write(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground)
        {
            Write(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            Write(value, foreground, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            Write(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            Write(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            Write(value, foreground, background, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            Write(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value)
        {
            WriteLine(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, IFormatProvider? provider)
        {
            WriteLine(value, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, EscapeType escape)
        {
            WriteLine(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground)
        {
            WriteLine(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            WriteLine(value, foreground, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            WriteLine(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            WriteLine(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            WriteLine(value, foreground, background, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            WriteLine(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, escape, true, provider);
        }

        private static readonly Object ConsoleLock = new Object();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value)
        {
            ToConsole(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape)
        {
            ToConsole(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, IFormatProvider? provider)
        {
            ToConsole(value, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine)
        {
            ToConsole(value, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            String str = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtils.NullString;

            if (newLine)
            {
                Console.WriteLine(str);
            }
            else
            {
                Console.Write(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground)
        {
            ToConsole(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape)
        {
            ToConsole(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            ToConsole(value, foreground, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine)
        {
            ToConsole(value, foreground, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, foreground, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, foreground, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, foreground, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, foreground, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = foreground;

                ToConsole(value, newLine, escape, provider);

                Console.ForegroundColor = color;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background)
        {
            ToConsole(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            ToConsole(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine)
        {
            ToConsole(value, foreground, background, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine)
        {
            ToConsole(value, foreground, background, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape)
        {
            ToConsole(value, foreground, background, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, newLine, ConvertUtils.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            ToConsole(value, foreground, background, newLine, escape, provider);
        }

        public static void ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            lock (ConsoleLock)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background;

                ToConsole(value, foreground, newLine, escape, provider);

                Console.BackgroundColor = color;
            }
        }
    }
}