using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace NetExtender.Types.Console.Interfaces
{
    public interface IConsole : IConsoleInfo
    {
        public static IConsole Default
        {
            get
            {
                return ConsoleHandler.Default;
            }
        }

        public event PlatformNotSupportedHandler PlatformNotSupported;

        /// <inheritdoc cref="System.Console.CancelKeyPress"/>
        public event ConsoleCancelEventHandler? CancelKeyPress;

        /// <inheritdoc cref="System.Console.Title"/>
        public new String? Title { get; set; }

        /// <inheritdoc cref="System.Console.In"/>
        public new TextReader In { get; set; }

        /// <inheritdoc cref="System.Console.Out"/>
        public new TextWriter Out { get; set; }

        /// <inheritdoc cref="System.Console.Error"/>
        public new TextWriter Error { get; set; }

        /// <inheritdoc cref="System.Console.IsInputRedirected"/>
        public new Boolean IsInputRedirected { get; }

        /// <inheritdoc cref="System.Console.IsOutputRedirected"/>
        public new Boolean IsOutputRedirected { get; }

        /// <inheritdoc cref="System.Console.IsErrorRedirected"/>
        public new Boolean IsErrorRedirected { get; }

        public new Encoding? Encoding { get; set; }

        /// <inheritdoc cref="System.Console.InputEncoding"/>
        public new Encoding? InputEncoding { get; set; }

        /// <inheritdoc cref="System.Console.OutputEncoding"/>
        public new Encoding? OutputEncoding { get; set; }
        
        public new Boolean? IsVisible { get; set; }
        
        public new IConsoleFontInfo? Font { get; set; }
        public new UInt32 FontIndex { get; set; }
        public new Int16 FontSize { get; set; }

        /// <inheritdoc cref="System.Console.ForegroundColor"/>
        public new ConsoleColor ForegroundColor { get; set; }

        /// <inheritdoc cref="System.Console.BackgroundColor"/>
        public new ConsoleColor BackgroundColor { get; set; }

        public new Point Cursor { get; set; }

        /// <inheritdoc cref="System.Console.CursorSize"/>
        public new Int32 CursorSize { get; set; }

        /// <inheritdoc cref="System.Console.CursorVisible"/>
        public new Boolean CursorVisible { get; set; }

        /// <inheritdoc cref="System.Console.CursorLeft"/>
        public new Int32 CursorLeft { get; set; }

        /// <inheritdoc cref="System.Console.CursorTop"/>
        public new Int32 CursorTop { get; set; }

        public new Rectangle Window { get; set; }
        public new Point WindowPosition { get; set; }
        public new Size WindowSize { get; set; }

        /// <inheritdoc cref="System.Console.WindowLeft"/>
        public new Int32 WindowLeft { get; set; }

        /// <inheritdoc cref="System.Console.WindowTop"/>
        public new Int32 WindowTop { get; set; }

        /// <inheritdoc cref="System.Console.WindowWidth"/>
        public new Int32 WindowWidth { get; set; }

        /// <inheritdoc cref="System.Console.WindowHeight"/>
        public new Int32 WindowHeight { get; set; }

        public new Size LargestWindowSize { get; }

        /// <inheritdoc cref="System.Console.LargestWindowWidth"/>
        public new Int32 LargestWindowWidth { get; }

        /// <inheritdoc cref="System.Console.LargestWindowHeight"/>
        public new Int32 LargestWindowHeight { get; }

        public new Size BufferSize { get; set; }

        /// <inheritdoc cref="System.Console.BufferWidth"/>
        public new Int32 BufferWidth { get; set; }

        /// <inheritdoc cref="System.Console.BufferHeight"/>
        public new Int32 BufferHeight { get; set; }

        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        public new Boolean KeyAvailable { get; }

        /// <inheritdoc cref="System.Console.NumberLock"/>
        public new Boolean NumberLock { get; }

        /// <inheritdoc cref="System.Console.CapsLock"/>
        public new Boolean CapsLock { get; }
        
        public new Boolean? IsVTCode { get; set; }
        public new Boolean? IsQuickEditEnabled { get; set; }
        
        public new Boolean? IsEchoInputEnabled { get; set; }
        public new Boolean? IsWindowInputEnabled { get; set; }
        public new Boolean? IsMouseInputEnabled { get; set; }

        /// <inheritdoc cref="System.Console.TreatControlCAsInput"/>
        public new Boolean TreatControlCAsInput { get; set; }

        /// <inheritdoc cref="System.Console.Beep()"/>
        public Boolean Beep();

        /// <inheritdoc cref="System.Console.Beep(System.Int32,System.Int32)"/>
        public Boolean Beep(Int32 frequency, Int32 duration);

        /// <inheritdoc cref="System.Console.SetIn(System.IO.TextReader)"/>
        public Boolean SetIn(TextReader? reader);

        /// <inheritdoc cref="System.Console.SetOut(System.IO.TextWriter)"/>
        public Boolean SetOut(TextWriter? writer);

        /// <inheritdoc cref="System.Console.SetError(System.IO.TextWriter)"/>
        public Boolean SetError(TextWriter? writer);

        /// <inheritdoc cref="System.Console.OpenStandardInput()"/>
        public Stream OpenStandardInput();

        /// <inheritdoc cref="System.Console.OpenStandardInput(System.Int32)"/>
        public Stream OpenStandardInput(Int32 buffer);

        /// <inheritdoc cref="System.Console.OpenStandardOutput()"/>
        public Stream OpenStandardOutput();

        /// <inheritdoc cref="System.Console.OpenStandardOutput(System.Int32)"/>
        public Stream OpenStandardOutput(Int32 buffer);

        /// <inheritdoc cref="System.Console.OpenStandardError()"/>
        public Stream OpenStandardError();

        /// <inheritdoc cref="System.Console.OpenStandardError(System.Int32)"/>
        public Stream OpenStandardError(Int32 buffer);

        public Boolean SetFont();
        public Boolean SetFont(Int16 size);
        public Boolean SetFont(String? font);
        public Boolean SetFont(String? font, Int16 size);

        /// <inheritdoc cref="System.Console.GetCursorPosition()"/>
        public Point GetCursorPosition();

        /// <inheritdoc cref="System.Console.SetCursorPosition(System.Int32,System.Int32)"/>
        public Boolean SetCursorPosition(Point position);

        /// <inheritdoc cref="System.Console.SetCursorPosition(System.Int32,System.Int32)"/>
        public Boolean SetCursorPosition(Int32 left, Int32 top);

        public Boolean ResetCursorPosition();

        /// <inheritdoc cref="System.Console.SetWindowPosition(System.Int32,System.Int32)"/>
        public Boolean SetWindowPosition(Point position);

        /// <inheritdoc cref="System.Console.SetWindowPosition(System.Int32,System.Int32)"/>
        public Boolean SetWindowPosition(Int32 left, Int32 top);

        /// <inheritdoc cref="System.Console.SetWindowSize(System.Int32,System.Int32)"/>
        public Boolean SetWindowSize(Size size);

        /// <inheritdoc cref="System.Console.SetWindowSize(System.Int32,System.Int32)"/>
        public Boolean SetWindowSize(Int32 width, Int32 height);

        /// <inheritdoc cref="System.Console.SetBufferSize(System.Int32,System.Int32)"/>
        public Boolean SetBufferSize(Size size);

        /// <inheritdoc cref="System.Console.SetBufferSize(System.Int32,System.Int32)"/>
        public Boolean SetBufferSize(Int32 width, Int32 height);

        /// <inheritdoc cref="System.Console.MoveBufferArea(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)"/>
        public Boolean MoveBufferArea(Rectangle source, Point target);

        /// <inheritdoc cref="System.Console.MoveBufferArea(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Char,System.ConsoleColor,System.ConsoleColor)"/>
        public Boolean MoveBufferArea(Rectangle source, Point target, Char @char, ConsoleColor foreground, ConsoleColor background);

        /// <inheritdoc cref="System.Console.Read()"/>
        public Int32 Read();

        /// <inheritdoc cref="System.Console.ReadKey()"/>
        public ConsoleKeyInfo ReadKey();

        /// <inheritdoc cref="System.Console.ReadKey(System.Boolean)"/>
        public ConsoleKeyInfo ReadKey(Boolean intercept);

        /// <inheritdoc cref="System.Console.ReadLine()"/>
        public String? ReadLine();

        /// <inheritdoc cref="System.Console.Write(System.Char)"/>
        public Boolean Write(Char value);

        /// <inheritdoc cref="System.Console.Write(System.Char[])"/>
        public Boolean Write(Char[]? buffer);

        /// <inheritdoc cref="System.Console.Write(System.Char[],System.Int32,System.Int32)"/>
        public Boolean Write(Char[] buffer, Int32 index, Int32 count);

        /// <inheritdoc cref="System.Console.Write(System.String?)"/>
        public Boolean Write(String? value);

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?)"/>
        public Boolean Write(String format, Object? arg0);

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?,System.Object?)"/>
        public Boolean Write(String format, Object? arg0, Object? arg1);

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?,System.Object?,System.Object?)"/>
        public Boolean Write(String format, Object? arg0, Object? arg1, Object? arg2);

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?[])"/>
        public Boolean Write(String format, params Object?[]? args);

        /// <inheritdoc cref="System.Console.Write(System.Boolean)"/>
        public Boolean Write(Boolean value);

        /// <inheritdoc cref="System.Console.Write(System.Int32)"/>
        public Boolean Write(Int32 value);

        /// <inheritdoc cref="System.Console.Write(System.UInt32)"/>
        public Boolean Write(UInt32 value);

        /// <inheritdoc cref="System.Console.Write(System.Int64)"/>
        public Boolean Write(Int64 value);

        /// <inheritdoc cref="System.Console.Write(System.UInt64)"/>
        public Boolean Write(UInt64 value);

        /// <inheritdoc cref="System.Console.Write(System.Single)"/>
        public Boolean Write(Single value);

        /// <inheritdoc cref="System.Console.Write(System.Double)"/>
        public Boolean Write(Double value);

        /// <inheritdoc cref="System.Console.Write(System.Decimal)"/>
        public Boolean Write(Decimal value);

        /// <inheritdoc cref="System.Console.Write(System.Object?)"/>
        public Boolean Write(Object? value);

        /// <inheritdoc cref="System.Console.WriteLine()"/>
        public Boolean WriteLine();

        /// <inheritdoc cref="System.Console.WriteLine(System.Char)"/>
        public Boolean WriteLine(Char value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Char[])"/>
        public Boolean WriteLine(Char[]? buffer);

        /// <inheritdoc cref="System.Console.WriteLine(System.Char[],System.Int32,System.Int32)"/>
        public Boolean WriteLine(Char[] buffer, Int32 index, Int32 count);

        /// <inheritdoc cref="System.Console.WriteLine(System.String?)"/>
        public Boolean WriteLine(String? value);

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?)"/>
        public Boolean WriteLine(String format, Object? arg0);

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?,System.Object?)"/>
        public Boolean WriteLine(String format, Object? arg0, Object? arg1);

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?,System.Object?,System.Object?)"/>
        public Boolean WriteLine(String format, Object? arg0, Object? arg1, Object? arg2);

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?[])"/>
        public Boolean WriteLine(String format, params Object?[]? args);

        /// <inheritdoc cref="System.Console.WriteLine(System.Boolean)"/>
        public Boolean WriteLine(Boolean value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Int32)"/>
        public Boolean WriteLine(Int32 value);

        /// <inheritdoc cref="System.Console.WriteLine(System.UInt32)"/>
        public Boolean WriteLine(UInt32 value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Int64)"/>
        public Boolean WriteLine(Int64 value);

        /// <inheritdoc cref="System.Console.WriteLine(System.UInt64)"/>
        public Boolean WriteLine(UInt64 value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Single)"/>
        public Boolean WriteLine(Single value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Double)"/>
        public Boolean WriteLine(Double value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Decimal)"/>
        public Boolean WriteLine(Decimal value);

        /// <inheritdoc cref="System.Console.WriteLine(System.Object?)"/>
        public Boolean WriteLine(Object? value);

        /// <inheritdoc cref="System.Console.Clear()"/>
        public Boolean Clear();

        /// <inheritdoc cref="System.Console.ResetColor()"/>
        public Boolean ResetColor();
    }

    public interface IConsoleInfo
    {
        /// <inheritdoc cref="System.Console.Title"/>
        public String? Title { get; }

        /// <inheritdoc cref="System.Console.In"/>
        public TextReader In { get; }

        /// <inheritdoc cref="System.Console.Out"/>
        public TextWriter Out { get; }

        /// <inheritdoc cref="System.Console.Error"/>
        public TextWriter Error { get; }

        /// <inheritdoc cref="System.Console.IsInputRedirected"/>
        public Boolean IsInputRedirected { get; }

        /// <inheritdoc cref="System.Console.IsOutputRedirected"/>
        public Boolean IsOutputRedirected { get; }

        /// <inheritdoc cref="System.Console.IsErrorRedirected"/>
        public Boolean IsErrorRedirected { get; }

        public Encoding? Encoding { get; }

        /// <inheritdoc cref="System.Console.InputEncoding"/>
        public Encoding? InputEncoding { get; }

        /// <inheritdoc cref="System.Console.OutputEncoding"/>
        public Encoding? OutputEncoding { get; }
        
        public Boolean? IsVisible { get; }
        
        public IConsoleFontInfo? Font { get; }
        public UInt32 FontIndex { get; }
        public Int16 FontSize { get; }

        /// <inheritdoc cref="System.Console.ForegroundColor"/>
        public ConsoleColor ForegroundColor { get; }

        /// <inheritdoc cref="System.Console.BackgroundColor"/>
        public ConsoleColor BackgroundColor { get; }

        public Point Cursor { get; }

        /// <inheritdoc cref="System.Console.CursorSize"/>
        public Int32 CursorSize { get; }

        /// <inheritdoc cref="System.Console.CursorVisible"/>
        public Boolean CursorVisible { get; }

        /// <inheritdoc cref="System.Console.CursorLeft"/>
        public Int32 CursorLeft { get; }

        /// <inheritdoc cref="System.Console.CursorTop"/>
        public Int32 CursorTop { get; }

        public Rectangle Window { get; }
        public Point WindowPosition { get; }
        public Size WindowSize { get; }

        /// <inheritdoc cref="System.Console.WindowLeft"/>
        public Int32 WindowLeft { get; }

        /// <inheritdoc cref="System.Console.WindowTop"/>
        public Int32 WindowTop { get; }

        /// <inheritdoc cref="System.Console.WindowWidth"/>
        public Int32 WindowWidth { get; }

        /// <inheritdoc cref="System.Console.WindowHeight"/>
        public Int32 WindowHeight { get; }

        public Size LargestWindowSize { get; }

        /// <inheritdoc cref="System.Console.LargestWindowWidth"/>
        public Int32 LargestWindowWidth { get; }

        /// <inheritdoc cref="System.Console.LargestWindowHeight"/>
        public Int32 LargestWindowHeight { get; }

        public Size BufferSize { get; }

        /// <inheritdoc cref="System.Console.BufferWidth"/>
        public Int32 BufferWidth { get; }

        /// <inheritdoc cref="System.Console.BufferHeight"/>
        public Int32 BufferHeight { get; }

        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        public Boolean KeyAvailable { get; }

        /// <inheritdoc cref="System.Console.NumberLock"/>
        public Boolean NumberLock { get; }

        /// <inheritdoc cref="System.Console.CapsLock"/>
        public Boolean CapsLock { get; }
        
        public Boolean? IsVTCode { get; }
        public Boolean? IsQuickEditEnabled { get; }
        
        public Boolean? IsEchoInputEnabled { get; }
        public Boolean? IsWindowInputEnabled { get; }
        public Boolean? IsMouseInputEnabled { get; }

        /// <inheritdoc cref="System.Console.TreatControlCAsInput"/>
        public Boolean TreatControlCAsInput { get; }
    }
}