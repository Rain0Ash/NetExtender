// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Core.Types.Multithreading;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.IO
{
    public enum ConsoleInputType
    {
        None,
        Line,
        KeyInfo,
        KeyInfoIntercept,
        KeyCode
    }
    
    public static class ConsoleUtilities
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
        public static ConsoleKeyInfo ReadKeyIntercept()
        {
            return Console.ReadKey(true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>()
        {
            return CastReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T>? CastReadAsEnumerable<T>(CultureInfo info)
        {
            return CastReadAsEnumerable<T>(StringUtilities.DefaultSeparator, info);
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
            return ReadAsEnumerable<T>(StringUtilities.DefaultSeparator, info);
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
            return TryReadAsEnumerable<T>(StringUtilities.DefaultSeparator, info);
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
        public static Boolean ClearLine()
        {
            return ClearLine(Console.CursorTop);
        }

        public static Boolean ClearLine(Int32 line)
        {
            try
            {
                Synchronization.Wait();

                Console.SetCursorPosition(0, line);
                Console.Write(new String(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, line);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Synchronization.Release();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Clear()
        {
            try
            {
                Console.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //TODO: Add async versions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value)
        {
            return Write(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, IFormatProvider? provider)
        {
            return Write(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, EscapeType escape)
        {
            return Write(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground)
        {
            return Write(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            return Write(value, foreground, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            return Write(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            return Write(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            return Write(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            return Write(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            Console.WriteLine(String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value)
        {
            return WriteLine(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, IFormatProvider? provider)
        {
            return WriteLine(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, EscapeType escape)
        {
            return WriteLine(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground)
        {
            return WriteLine(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape)
        {
            return WriteLine(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background)
        {
            return WriteLine(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            return WriteLine(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, escape, true, provider);
        }

        private static MutexSlim Synchronization { get; } = new MutexSlim();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value)
        {
            return ToConsole(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape)
        {
            return ToConsole(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, IFormatProvider? provider)
        {
            return ToConsole(value, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine)
        {
            return ToConsole(value, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, newLine, escape, provider);
        }

        public static T ToConsole<T>(this T value, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;

            if (newLine)
            {
                Console.WriteLine(text);
                return value;
            }

            Console.Write(text);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground)
        {
            return ToConsole(value, foreground, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape)
        {
            return ToConsole(value, foreground, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine)
        {
            return ToConsole(value, foreground, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, foreground, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, foreground, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, newLine, escape, provider);
        }

        public static T ToConsole<T>(this T value, ConsoleColor foreground, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            try
            {
                Synchronization.Wait();
                
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = foreground;

                ToConsole(value, newLine, escape, provider);

                Console.ForegroundColor = color;

                return value;
            }
            finally
            {
                Synchronization.Release();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background)
        {
            return ToConsole(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape)
        {
            return ToConsole(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine)
        {
            return ToConsole(value, foreground, background, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, foreground, background, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, foreground, background, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, newLine, escape, provider);
        }

        public static T ToConsole<T>(this T value, ConsoleColor foreground, ConsoleColor background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            try
            {
                Synchronization.Wait();
                
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background;

                ToConsole(value, foreground, newLine, escape, provider);

                Console.BackgroundColor = color;
                
                return value;
            }
            finally
            {
                Synchronization.Release();
            }
        }
    }
}