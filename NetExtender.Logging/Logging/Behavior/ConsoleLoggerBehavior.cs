// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Logging.Common;
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

        protected override void Log(String value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            value = Format(type, options, offset, provider) + value;
            
            if (!options.HasFlag(LoggingMessageOptions.Color))
            {
                value.ToConsole(escape, provider);
                return;
            }

            Color(type, out Color? foreground, out Color? background);

            if (background is null)
            {
                _ = foreground is not null ? value.ToConsole(foreground.Value, escape, provider) : value.ToConsole(escape, provider);
                return;
            }

            _ = foreground is not null ? value.ToConsole(foreground.Value, background.Value, escape, provider) : value.ToConsole(default, background.Value, escape, provider);
        }
    }
}