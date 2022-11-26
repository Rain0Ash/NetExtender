// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public class ConsoleLoggerBehavior : ColorLoggerBehavior
    {
        public override Boolean IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        public ConsoleLoggerBehavior()
        {
        }

        public ConsoleLoggerBehavior(ILoggerFormatProvider formatter)
            : base(formatter)
        {
        }

        protected override Boolean Log(String? message, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            message = Formatter.Format(message, type, options, offset, provider);

            if (message is null)
            {
                return false;
            }

            if (!options.HasFlag(LoggingMessageOptions.Color))
            {
                message.ToConsole(escape, provider);
                return true;
            }

            Color(type, out Color? foreground, out Color? background);

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