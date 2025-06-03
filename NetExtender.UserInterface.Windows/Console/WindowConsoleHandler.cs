using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using NetExtender.Types.Attributes;
using NetExtender.Types.Console;
using NetExtender.Types.Console.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface.Console.Interfaces;
using NetExtender.Utilities.UserInterface;
using NetExtender.Utilities.Windows;
using NetExtender.Workstation.Interfaces;

namespace NetExtender.UserInterface.Console
{
    [StaticInitializerRequired]
    public class WindowConsoleHandler : ConsoleHandler, IWindowConsole
    {
        public new static IWindowConsole Default
        {
            get
            {
                return ConsoleHandler.Default as IWindowConsole ?? throw new NeverOperationException($"{nameof(ConsoleHandler)}.{nameof(ConsoleHandler.Default)} is overwritten.");
            }
        }

        static WindowConsoleHandler()
        {
            ConsoleHandler.Default = Seal.Instance;
        }

        public virtual Icon? Icon
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.ConsoleIcon;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    return @event.Invoke(nameof(Icon), typeof(Icon), exception, null) as Icon;
                }
            }
            set
            {
                try
                {
                    ConsoleWindowUtilities.ConsoleIcon = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Icon), null, exception, value);
                }
            }
        }

        public override Boolean? IsVisible
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsConsoleVisible;
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
                    ConsoleWindowUtilities.IsConsoleVisible = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsVisible), null, exception, value);
                }
            }
        }

        public override IConsoleFontInfo? Font
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.Font;
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
                    ConsoleWindowUtilities.Font = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(Font), null, exception, value);
                }
            }
        }

        public override UInt32 FontIndex
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.ConsoleFontIndex;
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
                    ConsoleWindowUtilities.ConsoleFontIndex = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(FontIndex), null, exception, value);
                }
            }
        }

        public override Int16 FontSize
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.FontSize;
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
                    ConsoleWindowUtilities.FontSize = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(FontSize), null, exception, value);
                }
            }
        }

        public override Boolean? IsVTCode
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsVTCode;
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
                    ConsoleWindowUtilities.IsVTCode = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsVTCode), null, exception, value);
                }
            }
        }

        public override Boolean? IsQuickEditEnabled
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsQuickEditEnabled;
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
                    ConsoleWindowUtilities.IsQuickEditEnabled = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsQuickEditEnabled), null, exception, value);
                }
            }
        }

        public override Boolean? IsEchoInputEnabled
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsEchoInputEnabled;
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
                    ConsoleWindowUtilities.IsEchoInputEnabled = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsEchoInputEnabled), null, exception, value);
                }
            }
        }

        public override Boolean? IsWindowInputEnabled
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsWindowInputEnabled;
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
                    ConsoleWindowUtilities.IsWindowInputEnabled = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsWindowInputEnabled), null, exception, value);
                }
            }
        }

        public override Boolean? IsMouseInputEnabled
        {
            get
            {
                try
                {
                    return ConsoleWindowUtilities.IsMouseInputEnabled;
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
                    ConsoleWindowUtilities.IsMouseInputEnabled = value;
                }
                catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
                {
                    @event.Invoke(nameof(IsMouseInputEnabled), null, exception, value);
                }
            }
        }

        protected WindowConsoleHandler()
        {
        }

        public virtual Graphics CreateGraphics()
        {
            try
            {
                return ConsoleWindowUtilities.CreateConsoleGraphics();
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                if (@event.Invoke(nameof(OpenStandardInput), typeof(Graphics), exception) is not Graphics result)
                {
                    throw;
                }

                return result;
            }
        }

        public virtual Boolean TryGetTitle([MaybeNullWhen(false)] String result)
        {
            try
            {
                return ConsoleWindowUtilities.TryGetTitle(out result);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                result = @event.Invoke(nameof(TryGetTitle), typeof(String), exception) as String;
                return result is not null;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean TrySetTitle(String? result)
        {
            try
            {
                return ConsoleWindowUtilities.TrySetTitle(result);
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(TrySetTitle), typeof(Boolean), exception, result) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public override Boolean SetFont()
        {
            try
            {
                ConsoleWindowUtilities.SetFont();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetFont), typeof(Boolean), exception) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public override Boolean SetFont(Int16 size)
        {
            try
            {
                ConsoleWindowUtilities.SetFont(size);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetFont), typeof(Boolean), exception, size) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public override Boolean SetFont(String? font)
        {
            try
            {
                ConsoleWindowUtilities.SetFont(font);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(SetFont), typeof(Boolean), exception, font) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public override Boolean SetFont(String? font, Int16 size)
        {
            try
            {
                ConsoleWindowUtilities.SetFont(font, size);
                return true;
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

        public virtual Boolean CenterToScreen()
        {
            try
            {
                ConsoleWindowUtilities.CenterToScreen();
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(CenterToScreen), typeof(Boolean), exception) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean CenterToScreen(IScreen screen)
        {
            try
            {
                ConsoleWindowUtilities.CenterToScreen(screen);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(CenterToScreen), typeof(Boolean), exception, screen) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean CenterToScreen(DefaultMonitorType type)
        {
            try
            {
                ConsoleWindowUtilities.CenterToScreen(type);
                return true;
            }
            catch (PlatformNotSupportedException exception) when (PlatformNotSupportedEvent is { } @event)
            {
                return @event.Invoke(nameof(CenterToScreen), typeof(Boolean), exception, type) is true;
            }
            catch (PlatformNotSupportedException)
            {
                return false;
            }
        }

        private sealed class Seal : WindowConsoleHandler
        {
            public static Seal Instance { get; } = new Seal();
            
            private Seal()
            {
            }
        }
    }
}