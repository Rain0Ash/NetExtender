// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Types.Drawing.Colors;
using NetExtender.Types.Drawing.Colors.Interfaces;
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
        private static Object Synchronization { get; } = ConcurrentUtilities.Synchronization;
        
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
        public static Int32 Read()
        {
            return Console.Read();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo ReadKeyIntercept()
        {
            return Console.ReadKey(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo Getch()
        {
            return ReadKeyIntercept();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? ReadLine()
        {
            return Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? CastReadAsEnumerable<T>()
        {
            return CastReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? CastReadAsEnumerable<T>(CultureInfo info)
        {
            return CastReadAsEnumerable<T>(StringUtilities.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? CastReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? CastReadAsEnumerable<T>(String[] separators, CultureInfo info)
        {
            return Console.ReadLine()?.CastConvert<T>(separators, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? ReadAsEnumerable<T>()
        {
            return ReadAsEnumerable<T>(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? ReadAsEnumerable<T>(CultureInfo info)
        {
            return ReadAsEnumerable<T>(StringUtilities.DefaultSeparator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? ReadAsEnumerable<T>(String separator, CultureInfo info)
        {
            return Console.ReadLine()?.Convert<T>(separator, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T?>? ReadAsEnumerable<T>(String[] separators, CultureInfo info)
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
            lock (Synchronization)
            {
                try
                {
                    Console.SetCursorPosition(0, line);
                    Console.Write(new String(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, line);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
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

        public static Boolean AnsiColorSequence { get; set; } = true;
        
        //TODO: Add async versions
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value)
        {
            return Write(value, (IFormatProvider?) null);
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
        public static T Write<T>(T value, ConsoleColor? color)
        {
            return Write(value, color, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return Write(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, EscapeType escape)
        {
            return Write(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, color, escape, false, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color color)
        {
            return Write(value, (ANSIColor) color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TColor>(T value, TColor? color) where TColor : IColor
        {
            return Write(value, color, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color color, IFormatProvider? provider)
        {
            return Write(value, (ANSIColor) color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TColor>(T value, TColor? color, IFormatProvider? provider) where TColor : IColor
        {
            return Write(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color color, EscapeType escape)
        {
            return Write(value, (ANSIColor) color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TColor>(T value, TColor? color, EscapeType escape) where TColor : IColor
        {
            return Write(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return Write(value, (ANSIColor) color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, ConsoleColor? background)
        {
            return Write(value, color, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, ConsoleColor? background, IFormatProvider? provider)
        {
            return Write(value, color, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, ConsoleColor? background, EscapeType escape)
        {
            return Write(value, color, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, color, background, escape, false, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            Console.WriteLine(String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value)
        {
            return WriteLine(value, (IFormatProvider?) null);
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
        public static T WriteLine<T>(T value, ConsoleColor? color)
        {
            return WriteLine(value, color, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return WriteLine(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, EscapeType escape)
        {
            return WriteLine(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, color, escape, true, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color color)
        {
            return WriteLine(value, (ANSIColor) color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TColor>(T value, TColor? color) where TColor : IColor
        {
            return WriteLine(value, color, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color color, IFormatProvider? provider)
        {
            return WriteLine(value, (ANSIColor) color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TColor>(T value, TColor? color, IFormatProvider? provider) where TColor : IColor
        {
            return WriteLine(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color color, EscapeType escape)
        {
            return WriteLine(value, (ANSIColor) color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TColor>(T value, TColor? color, EscapeType escape) where TColor : IColor
        {
            return WriteLine(value, color, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, (ANSIColor) color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, ConsoleColor? background)
        {
            return WriteLine(value, color, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, ConsoleColor? background, IFormatProvider? provider)
        {
            return WriteLine(value, color, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, ConsoleColor? background, EscapeType escape)
        {
            return WriteLine(value, color, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, color, background, escape, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value)
        {
            return ToConsole(value, (IFormatProvider?) null);
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
            lock (Synchronization)
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
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color)
        {
            return ToConsole(value, color, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape)
        {
            return ToConsole(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return ToConsole(value, color, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, color, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine)
        {
            return ToConsole(value, color, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, color, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, color, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, color, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, color, newLine, escape, provider);
        }

        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            if (!color.HasValue)
            {
                return ToConsole(value, newLine, escape, provider);
            }

            lock (Synchronization)
            {
                ConsoleColor console = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;

                ToConsole(value, newLine, escape, provider);

                Console.ForegroundColor = console;

                return value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background)
        {
            return ToConsole(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape)
        {
            return ToConsole(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, true, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine)
        {
            return ToConsole(value, foreground, background, newLine, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, foreground, background, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, foreground, background, newLine, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, foreground, background, newLine, escape, provider);
        }

        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            if (!background.HasValue)
            {
                return ToConsole(value, foreground, newLine, escape, provider);
            }

            lock (Synchronization)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background.Value;

                ToConsole(value, foreground, newLine, escape, provider);

                Console.BackgroundColor = color;

                return value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color)
        {
            return ToConsole(value, (ANSIColor) color);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color) where TColor : IColor
        {
            return ToConsole(value, color, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape)
        {
            return ToConsole(value, (ANSIColor) color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape) where TColor : IColor
        {
            return ToConsole(value, color, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, true, escape, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine)
        {
            return ToConsole(value, (ANSIColor) color,  newLine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine) where TColor : IColor
        {
            return ToConsole(value, color, newLine, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, (ANSIColor) color, escape, newLine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape, Boolean newLine) where TColor : IColor
        {
            return ToConsole(value, color, newLine, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, (ANSIColor) color, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, EscapeType escape) where TColor : IColor
        {
            return ToConsole(value, color, newLine, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) color, newLine, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) color, escape, newLine, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape, Boolean newLine, IFormatProvider? provider) where TColor : IColor
        {
            return ToConsole(value, color, newLine, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) color, newLine, escape, provider);
        }

        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            if (color is null)
            {
                return ToConsole(value, newLine, escape, provider);
            }

            if (typeof(TColor) == typeof(ANSIDifferentColor))
            {
                ANSIDifferentColor different = Unsafe.As<TColor, ANSIDifferentColor>(ref color);
                return ToConsole(value, new ANSIColor(different.ForegroundR, different.ForegroundG, different.ForegroundB), 
                    new ANSIColor(different.BackgroundR, different.BackgroundG, different.BackgroundB), newLine, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                return color.NearestConsoleColor(out ConsoleColor console)
                    ? ToConsole(value, console, newLine, escape, provider)
                    : ToConsole(value, newLine, escape, provider);
            }
            
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            
            if (typeof(TColor) == typeof(ANSIColor))
            {
                ToConsole(color.ToString(text, provider), newLine, escape, provider);
                return value;
            }

            ANSIColor ansi = color.ToColor();
            ToConsole(ansi.ToString(text, provider), newLine, escape, provider);
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, true, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, true, escape, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background,  newLine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, newLine, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape, Boolean newLine)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, escape, newLine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape, Boolean newLine) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, newLine, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, EscapeType escape)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, newLine, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, newLine, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, newLine, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, newLine, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape, Boolean newLine, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, escape, newLine, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape, Boolean newLine, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return ToConsole(value, foreground, background, newLine, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return ToConsole(value, (ANSIColor) foreground, (ANSIColor) background, newLine, escape, provider);
        }

        // ReSharper disable once CognitiveComplexity
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            if (background is null)
            {
                return ToConsole(value, foreground, newLine, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                Boolean backsuccessful = background.NearestConsoleColor(out ConsoleColor backcolor);

                if (foreground is not null && foreground.NearestConsoleColor(out ConsoleColor forecolor))
                {
                    return backsuccessful ? ToConsole(value, forecolor, backcolor, newLine, escape, provider) : ToConsole(value, forecolor, newLine, escape, provider);
                }

                if (!backsuccessful)
                {
                    return ToConsole(value, newLine, escape, provider);
                }

                if (typeof(TBackground) != typeof(ANSIColor))
                {
                    return ToConsole(value, null, backcolor, newLine, escape, provider);
                }

                ANSIColor color = Unsafe.As<TBackground, ANSIColor>(ref background);

                return color.Mode switch
                {
                    AnsiColorSequenceMode.None => ToConsole(value, newLine, escape, provider),
                    AnsiColorSequenceMode.Foreground => ToConsole(value, backcolor, newLine, escape, provider),
                    AnsiColorSequenceMode.Background => ToConsole(value, null, backcolor, newLine, escape, provider),
                    AnsiColorSequenceMode.Fill => ToConsole(value, backcolor, backcolor, newLine, escape, provider),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;

            if (foreground is null)
            {
                if (typeof(TBackground) == typeof(ANSIColor))
                {
                    ToConsole(background.ToString(text, provider), newLine, escape, provider);
                    return value;
                }
                
                ANSIColor ansi = background.ToColor();
                ansi = ansi.Clone(AnsiColorSequenceMode.Background);
                ToConsole(ansi.ToString(text, provider), newLine, escape, provider);
                return value;
            }

            ANSIDifferentColor different = new ANSIDifferentColor(foreground.ToColor(), background.ToColor());
            ToConsole(different.ToString(text, provider), newLine, escape, provider);
            return value;
        }
    }
}