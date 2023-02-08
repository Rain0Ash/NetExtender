// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public class ConsoleLoggerBehavior : ConsoleLoggerBehavior<LoggingMessageLevel>
    {
        protected override void Color(LoggingMessageLevel level, out Color? foreground, out Color? background)
        {
            (foreground, background) = level switch
            {
                LoggingMessageLevel.Log => (default(Color?), default(Color?)),
                LoggingMessageLevel.Debug => (ConsoleColor.Cyan.ToColor(), default),
                LoggingMessageLevel.Info => (ConsoleColor.Blue.ToColor(), default),
                LoggingMessageLevel.Action => (ConsoleColor.DarkBlue.ToColor(), default),
                LoggingMessageLevel.Good => (ConsoleColor.Green.ToColor(), default),
                LoggingMessageLevel.Attention => (ConsoleColor.DarkYellow.ToColor(), default),
                LoggingMessageLevel.Warning => (ConsoleColor.Yellow.ToColor(), default),
                LoggingMessageLevel.Error => (ConsoleColor.Red.ToColor(), default),
                LoggingMessageLevel.Critical => (ConsoleColor.DarkRed.ToColor(), default),
                LoggingMessageLevel.Fatal => (ConsoleColor.Magenta.ToColor(), default),
                LoggingMessageLevel.Unknown => (ConsoleColor.Gray.ToColor(), default),
                _ => throw new EnumUndefinedOrNotSupportedException<LoggingMessageLevel>(level, nameof(level), null)
            };
        }
    }

    public abstract class ConsoleLoggerBehavior<TLevel> : ColorLoggerBehavior<TLevel> where TLevel : unmanaged, Enum
    {
        public override Boolean IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        protected ConsoleLoggerBehavior()
        {
        }

        protected ConsoleLoggerBehavior(ILoggerFormatProvider<TLevel> formatter)
            : base(formatter)
        {
        }

        protected override Boolean Log(String? message, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            message = Formatter.Format(message, level, options, offset, provider);

            if (message is null)
            {
                return false;
            }

            if (!options.HasFlag(LoggingMessageOptions.Color))
            {
                message.ToConsole(escape, provider);
                return true;
            }

            Color(level, out Color? foreground, out Color? background);

            if (background is null)
            {
                _ = foreground is not null ? message.ToConsole(foreground.Value, escape, provider) : message.ToConsole(escape, provider);
                return true;
            }

            _ = foreground is not null ? message.ToConsole(foreground.Value, background.Value, escape, provider) : message.ToConsole(default, background.Value, escape, provider);
            return true;
        }
    }
}