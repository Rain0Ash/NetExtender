using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Text;
using NetExtender.Types.Console.Interfaces;

namespace NetExtender.Types.Console
{
    [SuppressMessage("Interoperability", "CA1416")]
    public class ConsoleHandler : IConsole
    {
        private static IConsole? @default;
        protected internal static IConsole Default
        {
            get
            {
                return @default ?? Seal.Instance;
            }
            protected set
            {
                @default = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        protected PlatformNotSupportedHandler? PlatformNotSupportedEvent;
        public event PlatformNotSupportedHandler? PlatformNotSupported
        {
            add
            {
                PlatformNotSupportedEvent += value;
            }
            remove
            {
                PlatformNotSupportedEvent -= value;
            }
        }

        /// <inheritdoc cref="System.Console.CancelKeyPress"/>
        public virtual event ConsoleCancelEventHandler? CancelKeyPress
        {
            add
            {
                System.Console.CancelKeyPress += value;
            }
            remove
            {
                System.Console.CancelKeyPress -= value;
            }
        }

        /// <inheritdoc cref="System.Console.Title"/>
        public virtual String? Title
        {
            get
            {
                try
                {
                    return System.Console.Title;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Title), typeof(String), exception, null) as String;
                }
            }
            set
            {
                try
                {
                    System.Console.Title = value ?? String.Empty;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Title), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.In"/>
        public virtual TextReader In
        {
            get
            {
                try
                {
                    return System.Console.In;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(In), typeof(TextReader), exception, null) as TextReader ?? TextReader.Null;
                }
            }
            set
            {
                SetIn(value);
            }
        }

        /// <inheritdoc cref="System.Console.Out"/>
        public virtual TextWriter Out
        {
            get
            {
                try
                {
                    return System.Console.Out;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Out), typeof(TextWriter), exception, null) as TextWriter ?? TextWriter.Null;
                }
            }
            set
            {
                SetOut(value);
            }
        }

        /// <inheritdoc cref="System.Console.Error"/>
        public virtual TextWriter Error
        {
            get
            {
                try
                {
                    return System.Console.Error;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Error), typeof(TextWriter), exception, null) as TextWriter ?? TextWriter.Null;
                }
            }
            set
            {
                SetError(value);
            }
        }

        /// <inheritdoc cref="System.Console.IsInputRedirected"/>
        public virtual Boolean IsInputRedirected
        {
            get
            {
                try
                {
                    return System.Console.IsInputRedirected;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(IsInputRedirected), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        /// <inheritdoc cref="System.Console.IsOutputRedirected"/>
        public virtual Boolean IsOutputRedirected
        {
            get
            {
                try
                {
                    return System.Console.IsOutputRedirected;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(IsOutputRedirected), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        /// <inheritdoc cref="System.Console.IsErrorRedirected"/>
        public virtual Boolean IsErrorRedirected
        {
            get
            {
                try
                {
                    return System.Console.IsErrorRedirected;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(IsErrorRedirected), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        public virtual Encoding? Encoding
        {
            get
            {
                try
                {
                    Encoding input = System.Console.InputEncoding;
                    Encoding output = System.Console.OutputEncoding;
                    return Equals(input, output) ? output : null;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Encoding), typeof(Encoding), exception, null) as Encoding;
                }
            }
            set
            {
                if (value is null)
                {
                    return;
                }
                
                try
                {
                    System.Console.InputEncoding = value;
                    System.Console.OutputEncoding = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Encoding), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.InputEncoding"/>
        public virtual Encoding? InputEncoding
        {
            get
            {
                try
                {
                    return System.Console.InputEncoding;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(InputEncoding), typeof(Encoding), exception, null) as Encoding;
                }
            }
            set
            {
                if (value is null)
                {
                    return;
                }
                
                try
                {
                    System.Console.InputEncoding = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(InputEncoding), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.OutputEncoding"/>
        public virtual Encoding? OutputEncoding
        {
            get
            {
                try
                {
                    return System.Console.OutputEncoding;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(OutputEncoding), typeof(Encoding), exception, null) as Encoding;
                }
            }
            set
            {
                if (value is null)
                {
                    return;
                }

                try
                {
                    System.Console.InputEncoding = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(InputEncoding), null, exception, value);
                }
            }
        }

        public virtual Boolean? IsVisible
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsVisible), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsVisible), null, exception, value);
                }
            }
        }

        public virtual IConsoleFontInfo? Font
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Font), typeof(IConsoleFontInfo), exception, null) as IConsoleFontInfo;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Font), null, exception, value);
                }
            }
        }

        public virtual UInt32 FontIndex
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(FontIndex), typeof(UInt32), exception, null) is not UInt32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(FontIndex), null, exception, value);
                }
            }
        }

        public virtual Int16 FontSize
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(FontSize), typeof(Int16), exception, null) is not Int16 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(FontSize), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.ForegroundColor"/>
        public virtual ConsoleColor ForegroundColor
        {
            get
            {
                try
                {
                    return System.Console.ForegroundColor;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(ForegroundColor), typeof(ConsoleColor), exception, null) is not ConsoleColor result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.ForegroundColor = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(ForegroundColor), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.BackgroundColor"/>
        public virtual ConsoleColor BackgroundColor
        {
            get
            {
                try
                {
                    return System.Console.BackgroundColor;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(BackgroundColor), typeof(ConsoleColor), exception, null) is not ConsoleColor result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.BackgroundColor = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(BackgroundColor), null, exception, value);
                }
            }
        }

        public virtual Point Cursor
        {
            get
            {
                try
                {
                    (Int32 Left, Int32 Top) position = System.Console.GetCursorPosition();
                    return new Point(position.Left, position.Top);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(Cursor), typeof(Point), exception, null) is not Point result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.SetCursorPosition(value.X, value.Y);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Cursor), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.CursorSize"/>
        public virtual Int32 CursorSize
        {
            get
            {
                try
                {
                    return System.Console.CursorSize;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(CursorSize), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.CursorSize = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(CursorSize), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.CursorVisible"/>
        public virtual Boolean CursorVisible
        {
            get
            {
                try
                {
                    return System.Console.CursorVisible;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(CursorVisible), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.CursorVisible = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(CursorVisible), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.CursorLeft"/>
        public virtual Int32 CursorLeft
        {
            get
            {
                try
                {
                    return System.Console.CursorLeft;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(CursorLeft), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.CursorLeft = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(CursorLeft), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.CursorTop"/>
        public virtual Int32 CursorTop
        {
            
            get
            {
                try
                {
                    return System.Console.CursorTop;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(CursorTop), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.CursorTop = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(CursorTop), null, exception, value);
                }
            }
        }

        public virtual Rectangle Window
        {
            get
            {
                try
                {
                    return new Rectangle(System.Console.WindowLeft, System.Console.WindowTop, System.Console.WindowWidth, System.Console.WindowHeight);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(Window), typeof(Rectangle), exception, null) is not Rectangle result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    Rectangle rectangle = Window;
                    
                    try
                    {
                        System.Console.SetWindowPosition(value.Left, value.Top);
                        System.Console.SetWindowSize(value.Width, value.Height);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            System.Console.SetWindowSize(rectangle.Width, rectangle.Height);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            System.Console.SetWindowPosition(rectangle.Left, rectangle.Top);
                        }
                        catch (Exception)
                        {
                        }

                        throw;
                    }
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Window), null, exception, value);
                }
            }
        }

        public virtual Point WindowPosition
        {
            get
            {
                try
                {
                    return new Point(System.Console.WindowLeft, System.Console.WindowTop);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowPosition), typeof(Point), exception, null) is not Point result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.SetWindowPosition(value.X, value.Y);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowPosition), null, exception, value);
                }
            }
        }

        public virtual Size WindowSize
        {
            get
            {
                try
                {
                    return new Size(System.Console.WindowWidth, System.Console.WindowHeight);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowSize), typeof(Size), exception, null) is not Size result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.SetWindowSize(value.Width, value.Height);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowSize), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.WindowLeft"/>
        public virtual Int32 WindowLeft
        {
            get
            {
                try
                {
                    return System.Console.WindowLeft;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowLeft), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.WindowLeft = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowLeft), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.WindowTop"/>
        public virtual Int32 WindowTop
        {
            get
            {
                try
                {
                    return System.Console.WindowTop;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowTop), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.WindowTop = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowTop), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.WindowWidth"/>
        public virtual Int32 WindowWidth
        {
            get
            {
                try
                {
                    return System.Console.WindowWidth;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowWidth), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.WindowWidth = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowWidth), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.WindowHeight"/>
        public virtual Int32 WindowHeight
        {
            get
            {
                try
                {
                    return System.Console.WindowHeight;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(WindowHeight), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.WindowHeight = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(WindowHeight), null, exception, value);
                }
            }
        }

        public virtual Size LargestWindowSize
        {
            get
            {
                try
                {
                    return new Size(System.Console.LargestWindowWidth, System.Console.LargestWindowHeight);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(LargestWindowSize), typeof(Size), exception, null) is not Size result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        /// <inheritdoc cref="System.Console.LargestWindowWidth"/>
        public virtual Int32 LargestWindowWidth
        {
            get
            {
                try
                {
                    return System.Console.LargestWindowWidth;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(LargestWindowWidth), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        /// <inheritdoc cref="System.Console.LargestWindowHeight"/>
        public virtual Int32 LargestWindowHeight
        {
            get
            {
                try
                {
                    return System.Console.LargestWindowHeight;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(LargestWindowHeight), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        public virtual Size BufferSize
        {
            get
            {
                try
                {
                    return new Size(System.Console.BufferWidth, System.Console.BufferHeight);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(BufferSize), typeof(Size), exception, null) is not Size result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.SetBufferSize(value.Width, value.Height);
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(BufferSize), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.BufferWidth"/>
        public virtual Int32 BufferWidth
        {
            get
            {
                try
                {
                    return System.Console.BufferWidth;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(BufferWidth), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.BufferWidth = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(BufferWidth), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.BufferHeight"/>
        public virtual Int32 BufferHeight
        {
            get
            {
                try
                {
                    return System.Console.BufferHeight;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(BufferHeight), typeof(Int32), exception, null) is not Int32 result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.BufferHeight = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(BufferHeight), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        public virtual Boolean KeyAvailable
        {
            get
            {
                try
                {
                    return System.Console.KeyAvailable;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(KeyAvailable), typeof(Boolean), exception, null) is true;
                }
            }
        }

        /// <inheritdoc cref="System.Console.NumberLock"/>
        public virtual Boolean NumberLock
        {
            get
            {
                try
                {
                    return System.Console.NumberLock;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(NumberLock), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        /// <inheritdoc cref="System.Console.CapsLock"/>
        public virtual Boolean CapsLock
        {
            get
            {
                try
                {
                    return System.Console.CapsLock;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(CapsLock), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
        }

        public virtual Boolean? IsVTCode
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsVTCode), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsVTCode), null, exception, value);
                }
            }
        }

        public virtual Boolean? IsQuickEditEnabled
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsQuickEditEnabled), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsQuickEditEnabled), null, exception, value);
                }
            }
        }

        public virtual Boolean? IsEchoInputEnabled
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsEchoInputEnabled), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsEchoInputEnabled), null, exception, value);
                }
            }
        }

        public virtual Boolean? IsWindowInputEnabled
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsWindowInputEnabled), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsWindowInputEnabled), null, exception, value);
                }
            }
        }

        public virtual Boolean? IsMouseInputEnabled
        {
            get
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(IsMouseInputEnabled), typeof(Boolean?), exception, null) switch
                    {
                        Boolean result => result,
                        _ => null
                    };
                }
                catch (PlatformNotSupportedException)
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    throw new PlatformNotSupportedException();
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsMouseInputEnabled), null, exception, value);
                }
            }
        }

        /// <inheritdoc cref="System.Console.TreatControlCAsInput"/>
        public virtual Boolean TreatControlCAsInput
        {
            get
            {
                try
                {
                    return System.Console.TreatControlCAsInput;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    if (@event.Invoke(nameof(TreatControlCAsInput), typeof(Boolean), exception, null) is not Boolean result)
                    {
                        throw;
                    }

                    return result;
                }
            }
            set
            {
                try
                {
                    System.Console.TreatControlCAsInput = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(TreatControlCAsInput), null, exception, value);
                }
            }
        }

        protected ConsoleHandler()
        {
        }

        /// <inheritdoc cref="System.Console.Beep()"/>
        public virtual Boolean Beep()
        {
            try
            {
                System.Console.Beep();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Beep), typeof(Boolean), exception) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <inheritdoc cref="System.Console.Beep(System.Int32,System.Int32)"/>
        public virtual Boolean Beep(Int32 frequency, Int32 duration)
        {
            try
            {
                System.Console.Beep(frequency, duration);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Beep), typeof(Boolean), exception, frequency, duration) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetIn(System.IO.TextReader)"/>
        public virtual Boolean SetIn(TextReader? reader)
        {
            try
            {
                System.Console.SetIn(reader ?? TextReader.Null);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetIn), typeof(Boolean), exception, reader) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetOut(System.IO.TextWriter)"/>
        public virtual Boolean SetOut(TextWriter? writer)
        {
            try
            {
                System.Console.SetOut(writer ?? TextWriter.Null);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetOut), typeof(Boolean), exception, writer) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetError(System.IO.TextWriter)"/>
        public virtual Boolean SetError(TextWriter? writer)
        {
            try
            {
                System.Console.SetError(writer ?? TextWriter.Null);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetError), typeof(Boolean), exception, writer) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardInput()"/>
        public virtual Stream OpenStandardInput()
        {
            try
            {
                return System.Console.OpenStandardInput();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardInput), typeof(Stream), exception) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardInput(System.Int32)"/>
        public virtual Stream OpenStandardInput(Int32 buffer)
        {
            try
            {
                return System.Console.OpenStandardInput(buffer);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardInput), typeof(Stream), exception, buffer) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardOutput()"/>
        public virtual Stream OpenStandardOutput()
        {
            try
            {
                return System.Console.OpenStandardOutput();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardOutput), typeof(Stream), exception) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardOutput(System.Int32)"/>
        public virtual Stream OpenStandardOutput(Int32 buffer)
        {
            try
            {
                return System.Console.OpenStandardOutput(buffer);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardOutput), typeof(Stream), exception, buffer) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardError()"/>
        public virtual Stream OpenStandardError()
        {
            try
            {
                return System.Console.OpenStandardError();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardError), typeof(Stream), exception) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.OpenStandardError(System.Int32)"/>
        public virtual Stream OpenStandardError(Int32 buffer)
        {
            try
            {
                return System.Console.OpenStandardError(buffer);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardError), typeof(Stream), exception, buffer) is not Stream result)
                {
                    throw;
                }

                return result;
            }
        }

        public virtual Boolean SetFont()
        {
            return SetFont(0);
        }

        public virtual Boolean SetFont(Int16 size)
        {
            return SetFont(null, size);
        }

        public virtual Boolean SetFont(String? font)
        {
            return SetFont(font, 0);
        }

        public virtual Boolean SetFont(String? font, Int16 size)
        {
            try
            {
                throw new PlatformNotSupportedException();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetFont), typeof(Boolean), exception, font, size) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.GetCursorPosition()"/>
        public virtual Point GetCursorPosition()
        {
            try
            {
                (Int32 Left, Int32 Top) position = System.Console.GetCursorPosition();
                return new Point(position.Left, position.Top);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(GetCursorPosition), typeof(Point), exception, null) is not Point result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.SetCursorPosition(System.Int32,System.Int32)"/>
        public virtual Boolean SetCursorPosition(Point position)
        {
            try
            {
                System.Console.SetCursorPosition(position.X, position.Y);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetCursorPosition), typeof(Boolean), exception, position) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetCursorPosition(System.Int32,System.Int32)"/>
        public virtual Boolean SetCursorPosition(Int32 left, Int32 top)
        {
            try
            {
                System.Console.SetCursorPosition(left, top);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetCursorPosition), typeof(Boolean), exception, left, top) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean ResetCursorPosition()
        {
            return SetCursorPosition(0, 0);
        }

        /// <inheritdoc cref="System.Console.SetWindowPosition(System.Int32,System.Int32)"/>
        public virtual Boolean SetWindowPosition(Point position)
        {
            try
            {
                System.Console.SetWindowPosition(position.X, position.Y);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetWindowPosition), typeof(Boolean), exception, position) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetWindowPosition(System.Int32,System.Int32)"/>
        public virtual Boolean SetWindowPosition(Int32 left, Int32 top)
        {
            try
            {
                System.Console.SetWindowPosition(left, top);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetWindowPosition), typeof(Boolean), exception, left, top) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetWindowSize(System.Int32,System.Int32)"/>
        public virtual Boolean SetWindowSize(Size size)
        {
            try
            {
                System.Console.SetWindowSize(size.Width, size.Height);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetWindowSize), typeof(Boolean), exception, size) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetWindowSize(System.Int32,System.Int32)"/>
        public virtual Boolean SetWindowSize(Int32 width, Int32 height)
        {
            try
            {
                System.Console.SetWindowSize(width, height);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetWindowSize), typeof(Boolean), exception, width, height) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetBufferSize(System.Int32,System.Int32)"/>
        public virtual Boolean SetBufferSize(Size size)
        {
            try
            {
                System.Console.SetBufferSize(size.Width, size.Height);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetBufferSize), typeof(Boolean), exception, size) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.SetBufferSize(System.Int32,System.Int32)"/>
        public virtual Boolean SetBufferSize(Int32 width, Int32 height)
        {
            try
            {
                System.Console.SetBufferSize(width, height);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetBufferSize), typeof(Boolean), exception, width, height) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.MoveBufferArea(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)"/>
        public virtual Boolean MoveBufferArea(Rectangle source, Point target)
        {
            try
            {
                System.Console.MoveBufferArea(source.Left, source.Top, source.Width, source.Height, target.X, target.Y);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(MoveBufferArea), typeof(Boolean), exception, source, target) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.MoveBufferArea(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32,System.Char,System.ConsoleColor,System.ConsoleColor)"/>
        public virtual Boolean MoveBufferArea(Rectangle source, Point target, Char @char, ConsoleColor foreground, ConsoleColor background)
        {
            try
            {
                System.Console.MoveBufferArea(source.Left, source.Top, source.Width, source.Height, target.X, target.Y, @char, foreground, background);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(MoveBufferArea), typeof(Boolean), exception, source, target, @char, foreground, background) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Read()"/>
        public virtual Int32 Read()
        {
            try
            {
                return System.Console.Read();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(Read), typeof(Int32), exception) is not Int32 result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.ReadKey()"/>
        public virtual ConsoleKeyInfo ReadKey()
        {
            try
            {
                return System.Console.ReadKey();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(ReadKey), typeof(ConsoleKeyInfo), exception) is not ConsoleKeyInfo result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.ReadKey(System.Boolean)"/>
        public virtual ConsoleKeyInfo ReadKey(Boolean intercept)
        {
            try
            {
                return System.Console.ReadKey(intercept);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(ReadKey), typeof(ConsoleKeyInfo), exception, intercept) is not ConsoleKeyInfo result)
                {
                    throw;
                }

                return result;
            }
        }

        /// <inheritdoc cref="System.Console.ReadLine()"/>
        public virtual String? ReadLine()
        {
            try
            {
                return System.Console.ReadLine();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(ReadLine), typeof(String), exception) as String;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Char)"/>
        public virtual Boolean Write(Char value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Char[])"/>
        public virtual Boolean Write(Char[]? buffer)
        {
            try
            {
                System.Console.Write(buffer);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, buffer) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Char[],System.Int32,System.Int32)"/>
        public virtual Boolean Write(Char[] buffer, Int32 index, Int32 count)
        {
            try
            {
                System.Console.Write(buffer, index, count);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, buffer, index, count) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.String?)"/>
        public virtual Boolean Write(String? value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?)"/>
        public virtual Boolean Write(String format, Object? arg0)
        {
            try
            {
                System.Console.Write(format, arg0);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, format, arg0) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?,System.Object?)"/>
        public virtual Boolean Write(String format, Object? arg0, Object? arg1)
        {
            try
            {
                System.Console.Write(format, arg0, arg1);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, format, arg0, arg1) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?,System.Object?,System.Object?)"/>
        public virtual Boolean Write(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            try
            {
                System.Console.Write(format, arg0, arg1, arg2);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, format, arg0, arg1, arg2) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.String,System.Object?[])"/>
        public virtual Boolean Write(String format, params Object?[]? args)
        {
            try
            {
                System.Console.Write(format, args);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, format, args) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Boolean)"/>
        public virtual Boolean Write(Boolean value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Int32)"/>
        public virtual Boolean Write(Int32 value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.UInt32)"/>
        public virtual Boolean Write(UInt32 value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Int64)"/>
        public virtual Boolean Write(Int64 value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.UInt64)"/>
        public virtual Boolean Write(UInt64 value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Single)"/>
        public virtual Boolean Write(Single value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Double)"/>
        public virtual Boolean Write(Double value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Decimal)"/>
        public virtual Boolean Write(Decimal value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Write(System.Object?)"/>
        public virtual Boolean Write(Object? value)
        {
            try
            {
                System.Console.Write(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Write), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine()"/>
        public virtual Boolean WriteLine()
        {
            try
            {
                System.Console.WriteLine();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Char)"/>
        public virtual Boolean WriteLine(Char value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Char[])"/>
        public virtual Boolean WriteLine(Char[]? buffer)
        {
            try
            {
                System.Console.WriteLine(buffer);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, buffer) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Char[],System.Int32,System.Int32)"/>
        public virtual Boolean WriteLine(Char[] buffer, Int32 index, Int32 count)
        {
            try
            {
                System.Console.WriteLine(buffer, index, count);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, buffer, index, count) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.String?)"/>
        public virtual Boolean WriteLine(String? value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?)"/>
        public virtual Boolean WriteLine(String format, Object? arg0)
        {
            try
            {
                System.Console.WriteLine(format, arg0);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, format, arg0) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?,System.Object?)"/>
        public virtual Boolean WriteLine(String format, Object? arg0, Object? arg1)
        {
            try
            {
                System.Console.WriteLine(format, arg0, arg1);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, format, arg0, arg1) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?,System.Object?,System.Object?)"/>
        public virtual Boolean WriteLine(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            try
            {
                System.Console.WriteLine(format, arg0, arg1, arg2);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, format, arg0, arg1, arg2) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.String?,System.Object?[])"/>
        public virtual Boolean WriteLine(String format, params Object?[]? args)
        {
            try
            {
                System.Console.WriteLine(format, args);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, format, args) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Boolean)"/>
        public virtual Boolean WriteLine(Boolean value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Int32)"/>
        public virtual Boolean WriteLine(Int32 value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.UInt32)"/>
        public virtual Boolean WriteLine(UInt32 value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Int64)"/>
        public virtual Boolean WriteLine(Int64 value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.UInt64)"/>
        public virtual Boolean WriteLine(UInt64 value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Single)"/>
        public virtual Boolean WriteLine(Single value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Double)"/>
        public virtual Boolean WriteLine(Double value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Decimal)"/>
        public virtual Boolean WriteLine(Decimal value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.WriteLine(System.Object?)"/>
        public virtual Boolean WriteLine(Object? value)
        {
            try
            {
                System.Console.WriteLine(value);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(WriteLine), typeof(Boolean), exception, value) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.Clear()"/>
        public virtual Boolean Clear()
        {
            try
            {
                System.Console.Clear();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(Clear), typeof(Boolean), exception) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="System.Console.ResetColor()"/>
        public virtual Boolean ResetColor()
        {
            try
            {
                System.Console.ResetColor();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(ResetColor), typeof(Boolean), exception) is true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private sealed class Seal : ConsoleHandler
        {
            public static Seal Instance { get; } = new Seal();
            
            private Seal()
            {
            }
        }
    }
}