// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public abstract class ColorLoggerBehavior : LoggerBehavior
    {
        protected virtual void Color(LoggingMessageType type, out Color? foreground, out Color? background)
        {
            (foreground, background) = type switch
            {
                LoggingMessageType.Log => (default(Color?), default(Color?)),
                LoggingMessageType.Debug => (ConsoleColor.Cyan.ToColor(), default),
                LoggingMessageType.Info => (ConsoleColor.Blue.ToColor(), default),
                LoggingMessageType.Action => (ConsoleColor.DarkBlue.ToColor(), default),
                LoggingMessageType.Good => (ConsoleColor.Green.ToColor(), default),
                LoggingMessageType.Attention => (ConsoleColor.DarkYellow.ToColor(), default),
                LoggingMessageType.Warning => (ConsoleColor.Yellow.ToColor(), default),
                LoggingMessageType.Error => (ConsoleColor.Red.ToColor(), default),
                LoggingMessageType.Critical => (ConsoleColor.DarkRed.ToColor(), default),
                LoggingMessageType.Fatal => (ConsoleColor.Magenta.ToColor(), default),
                LoggingMessageType.Unknown => (ConsoleColor.Gray.ToColor(), default),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}