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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Write(String value)
        {
            Console.Write(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void Write(String value, ConsoleColor? color)
        {
            if (color is null)
            {
                Write(value);
                return;
            }
            
            lock (Synchronization)
            {
                ConsoleColor console = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                Write(value);
                Console.ForegroundColor = console;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void Write(String value, ConsoleColor? foreground, ConsoleColor? background)
        {
            if (background is null)
            {
                Write(value, foreground);
                return;
            }
            
            lock (Synchronization)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background.Value;
                Write(value, foreground);
                Console.BackgroundColor = color;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value)
        {
            return Write(value, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, EscapeType escape)
        {
            return Write(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, IFormatProvider? provider)
        {
            return Write(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T Write<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            Write(text);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color)
        {
            return Write(value, color, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, EscapeType escape)
        {
            return Write(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return Write(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T Write<T>(T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            Write(text, color);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? foreground, ConsoleColor? background)
        {
            return Write(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape)
        {
            return Write(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, ConsoleColor? foreground, ConsoleColor? background, IFormatProvider? provider)
        {
            return Write(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T Write<T>(T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            Write(text, foreground, background);
            return value;
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
        public static T Write<T>(T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return Write(value, (ANSIColor) color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteWithoutAnsi<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            if (color is null)
            {
                return Write(value, escape, provider);
            }
            
            return color.NearestConsoleColor(out ConsoleColor console) ? Write(value, console, escape, provider) : Write(value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T Write<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            if (color is null)
            {
                return Write(value, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                return WriteWithoutAnsi(value, color, escape, provider);
            }

            if (typeof(TColor) == typeof(ANSIDifferentColor))
            {
                ANSIDifferentColor different = Unsafe.As<TColor, ANSIDifferentColor>(ref color);
                return Write(value, different.ToColor(AnsiColorSequenceMode.Foreground), different.ToColor(AnsiColorSequenceMode.Background), escape, provider);
            }

            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            
            if (typeof(TColor) == typeof(ANSIColor))
            {
                Write(color.ToString(text, provider), escape, provider);
                return value;
            }

            Write(color.ToColor<TColor, ANSIColor>().ToString(text, provider), escape, provider);
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color foreground, Color background)
        {
            return Write(value, (ANSIColor) foreground, (ANSIColor) background);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background) where TForeground : IColor where TBackground : IColor
        {
            return Write(value, foreground, background, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color foreground, Color background, EscapeType escape)
        {
            return Write(value, (ANSIColor) foreground, (ANSIColor) background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return Write(value, foreground, background, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color foreground, Color background, IFormatProvider? provider)
        {
            return Write(value, (ANSIColor) foreground, (ANSIColor) background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return Write(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Write<T>(T value, Color foreground, Color background, EscapeType escape, IFormatProvider? provider)
        {
            return Write(value, (ANSIColor) foreground, (ANSIColor) background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static T WriteWithoutAnsi<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            if (background is null)
            {
                return Write(value, foreground, escape, provider);
            }
            
            Boolean backsuccessful = background.NearestConsoleColor(out ConsoleColor backcolor);

            if (foreground is not null && foreground.NearestConsoleColor(out ConsoleColor forecolor))
            {
                return backsuccessful ? Write(value, forecolor, backcolor, escape, provider) : Write(value, forecolor, escape, provider);
            }

            if (!backsuccessful)
            {
                return Write(value, escape, provider);
            }

            if (typeof(TBackground) != typeof(ANSIColor))
            {
                return Write(value, null, backcolor, escape, provider);
            }

            ANSIColor color = Unsafe.As<TBackground, ANSIColor>(ref background);

            return color.Mode switch
            {
                AnsiColorSequenceMode.None => Write(value, escape, provider),
                AnsiColorSequenceMode.Foreground => Write(value, backcolor, escape, provider),
                AnsiColorSequenceMode.Background => Write(value, null, backcolor, escape, provider),
                AnsiColorSequenceMode.Fill => Write(value, backcolor, backcolor, escape, provider),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T Write<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            if (background is null)
            {
                return Write(value, foreground, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                return WriteWithoutAnsi(value, foreground, background, escape, provider);
            }
            
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;

            if (foreground is null)
            {
                if (typeof(TBackground) == typeof(ANSIColor))
                {
                    Write(background.ToString(text, provider), escape, provider);
                    return value;
                }
                
                Write(background.ToColor<TBackground, ANSIColor>().Clone(AnsiColorSequenceMode.Background).ToString(text, provider), escape, provider);
                return value;
            }

            ANSIDifferentColor different = new ANSIDifferentColor(foreground.ToColor(), background.ToColor());
            Write(different.ToString(text, provider), escape, provider);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteLine(String value)
        {
            Console.WriteLine(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WriteLine(String value, ConsoleColor? color)
        {
            if (color is null)
            {
                WriteLine(value);
                return;
            }
            
            lock (Synchronization)
            {
                ConsoleColor console = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                WriteLine(value);
                Console.ForegroundColor = console;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WriteLine(String value, ConsoleColor? foreground, ConsoleColor? background)
        {
            if (background is null)
            {
                WriteLine(value, foreground);
                return;
            }
            
            lock (Synchronization)
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = background.Value;
                WriteLine(value, foreground);
                Console.BackgroundColor = color;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            Console.WriteLine();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value)
        {
            return WriteLine(value, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, EscapeType escape)
        {
            return WriteLine(value, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, IFormatProvider? provider)
        {
            return WriteLine(value, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLine<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            WriteLine(text);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color)
        {
            return WriteLine(value, color, (IFormatProvider?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, EscapeType escape)
        {
            return WriteLine(value, color, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return WriteLine(value, color, ConvertUtilities.DefaultEscapeType, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLine<T>(T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            WriteLine(text, color);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? foreground, ConsoleColor? background)
        {
            return WriteLine(value, foreground, background, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape)
        {
            return WriteLine(value, foreground, background, escape, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, ConsoleColor? foreground, ConsoleColor? background, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLine<T>(T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            WriteLine(text, foreground, background);
            return value;
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
        public static T WriteLine<T>(T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, (ANSIColor) color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLineWithoutAnsi<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            if (color is null)
            {
                return WriteLine(value, escape, provider);
            }
            
            return color.NearestConsoleColor(out ConsoleColor console) ? WriteLine(value, console, escape, provider) : WriteLine(value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLine<T, TColor>(T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            if (color is null)
            {
                return WriteLine(value, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                return WriteLineWithoutAnsi(value, color, escape, provider);
            }

            if (typeof(TColor) == typeof(ANSIDifferentColor))
            {
                ANSIDifferentColor different = Unsafe.As<TColor, ANSIDifferentColor>(ref color);
                return WriteLine(value, different.ToColor(AnsiColorSequenceMode.Foreground), different.ToColor(AnsiColorSequenceMode.Background), escape, provider);
            }

            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            
            if (typeof(TColor) == typeof(ANSIColor))
            {
                WriteLine(color.ToString(text, provider), escape, provider);
                return value;
            }

            WriteLine(color.ToColor<TColor, ANSIColor>().ToString(text, provider), escape, provider);
            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color foreground, Color background)
        {
            return WriteLine(value, (ANSIColor) foreground, (ANSIColor) background);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color foreground, Color background, EscapeType escape)
        {
            return WriteLine(value, (ANSIColor) foreground, (ANSIColor) background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, escape, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color foreground, Color background, IFormatProvider? provider)
        {
            return WriteLine(value, (ANSIColor) foreground, (ANSIColor) background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, ConvertUtilities.DefaultEscapeType, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T WriteLine<T>(T value, Color foreground, Color background, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, (ANSIColor) foreground, (ANSIColor) background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static T WriteLineWithoutAnsi<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            if (background is null)
            {
                return WriteLine(value, foreground, escape, provider);
            }
            
            Boolean backsuccessful = background.NearestConsoleColor(out ConsoleColor backcolor);

            if (foreground is not null && foreground.NearestConsoleColor(out ConsoleColor forecolor))
            {
                return backsuccessful ? WriteLine(value, forecolor, backcolor, escape, provider) : WriteLine(value, forecolor, escape, provider);
            }

            if (!backsuccessful)
            {
                return WriteLine(value, escape, provider);
            }

            if (typeof(TBackground) != typeof(ANSIColor))
            {
                return WriteLine(value, null, backcolor, escape, provider);
            }

            ANSIColor color = Unsafe.As<TBackground, ANSIColor>(ref background);

            return color.Mode switch
            {
                AnsiColorSequenceMode.None => WriteLine(value, escape, provider),
                AnsiColorSequenceMode.Foreground => WriteLine(value, backcolor, escape, provider),
                AnsiColorSequenceMode.Background => WriteLine(value, null, backcolor, escape, provider),
                AnsiColorSequenceMode.Fill => WriteLine(value, backcolor, backcolor, escape, provider),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T WriteLine<T, TForeground, TBackground>(T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            if (background is null)
            {
                return WriteLine(value, foreground, escape, provider);
            }

            if (!AnsiColorSequence)
            {
                return WriteLineWithoutAnsi(value, foreground, background, escape, provider);
            }
            
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;

            if (foreground is null)
            {
                if (typeof(TBackground) == typeof(ANSIColor))
                {
                    WriteLine(background.ToString(text, provider), escape, provider);
                    return value;
                }
                
                WriteLine(background.ToColor<TBackground, ANSIColor>().Clone(AnsiColorSequenceMode.Background).ToString(text, provider), escape, provider);
                return value;
            }

            ANSIDifferentColor different = new ANSIDifferentColor(foreground.ToColor(), background.ToColor());
            WriteLine(different.ToString(text, provider), escape, provider);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value)
        {
            return WriteLine(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape)
        {
            return WriteLine(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, IFormatProvider? provider)
        {
            return WriteLine(value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine)
        {
            return newLine ? WriteLine(value) : Write(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine, EscapeType escape)
        {
            return newLine ? WriteLine(value, escape) : Write(value, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, provider) : Write(value, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, escape, provider) : Write(value, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color)
        {
            return WriteLine(value, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape)
        {
            return WriteLine(value, color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, IFormatProvider? provider)
        {
            return WriteLine(value, color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine)
        {
            return newLine ? WriteLine(value, color) : Write(value, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, EscapeType escape)
        {
            return newLine ? WriteLine(value, color, escape) : Write(value, color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, color, provider) : Write(value, color, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? color, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, color, escape, provider) : Write(value, color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background)
        {
            return WriteLine(value, foreground, background);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape)
        {
            return WriteLine(value, foreground, background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine)
        {
            return newLine ? WriteLine(value, foreground, background) : Write(value, foreground, background);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, EscapeType escape)
        {
            return newLine ? WriteLine(value, foreground, background, escape) : Write(value, foreground, background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, foreground, background, provider) : Write(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, ConsoleColor? foreground, ConsoleColor? background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, foreground, background, escape, provider) : Write(value, foreground, background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color)
        {
            return WriteLine(value, color);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color) where TColor : IColor
        {
            return WriteLine(value, color);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape)
        {
            return WriteLine(value, color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape) where TColor : IColor
        {
            return WriteLine(value, color, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, IFormatProvider? provider)
        {
            return WriteLine(value, color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, IFormatProvider? provider) where TColor : IColor
        {
            return WriteLine(value, color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            return WriteLine(value, color, escape, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine)
        {
            return newLine ? WriteLine(value, color) : Write(value, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine) where TColor : IColor
        {
            return newLine ? WriteLine(value, color) : Write(value, color);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, EscapeType escape)
        {
            return newLine ? WriteLine(value, color, escape) : Write(value, color, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, EscapeType escape) where TColor : IColor
        {
            return newLine ? WriteLine(value, color, escape) : Write(value, color, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, color, provider) : Write(value, color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, IFormatProvider? provider) where TColor : IColor
        {
            return newLine ? WriteLine(value, color, provider) : Write(value, color, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color color, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, color, escape, provider) : Write(value, color, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TColor>(this T value, TColor? color, Boolean newLine, EscapeType escape, IFormatProvider? provider) where TColor : IColor
        {
            return newLine ? WriteLine(value, color, escape, provider) : Write(value, color, escape, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background)
        {
            return WriteLine(value, foreground, background);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape)
        {
            return WriteLine(value, foreground, background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, EscapeType escape, IFormatProvider? provider)
        {
            return WriteLine(value, foreground, background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return WriteLine(value, foreground, background, escape, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine)
        {
            return newLine ? WriteLine(value, foreground, background) : Write(value, foreground, background);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine) where TForeground : IColor where TBackground : IColor
        {
            return newLine ? WriteLine(value, foreground, background) : Write(value, foreground, background);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, EscapeType escape)
        {
            return newLine ? WriteLine(value, foreground, background, escape) : Write(value, foreground, background, escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, EscapeType escape) where TForeground : IColor where TBackground : IColor
        {
            return newLine ? WriteLine(value, foreground, background, escape) : Write(value, foreground, background, escape);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, foreground, background, provider) : Write(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return newLine ? WriteLine(value, foreground, background, provider) : Write(value, foreground, background, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T>(this T value, Color foreground, Color background, Boolean newLine, EscapeType escape, IFormatProvider? provider)
        {
            return newLine ? WriteLine(value, foreground, background, escape, provider) : Write(value, foreground, background, escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ToConsole<T, TForeground, TBackground>(this T value, TForeground? foreground, TBackground? background, Boolean newLine, EscapeType escape, IFormatProvider? provider) where TForeground : IColor where TBackground : IColor
        {
            return newLine ? WriteLine(value, foreground, background, escape, provider) : Write(value, foreground, background, escape, provider);
        }
    }
}