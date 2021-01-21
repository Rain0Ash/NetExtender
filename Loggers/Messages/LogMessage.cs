// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Globalization;
using System.Linq;
using NetExtender.Utils.Types;
using NetExtender.Localizations;
using NetExtender.Loggers.Common;
using NetExtender.Types.Strings;
using NetExtender.Utils.IO;

namespace NetExtender.Loggers.Messages
{
    public readonly struct LogMessage : IConsoleMessage
    {
        public static LogMessage NullError(String param)
        {
            return new LogMessage($"{param} is null", MessageType.CriticalError);
        }

        public static LogMessage LogException(Exception e, MessageType type)
        {
            return new LogMessage(e.ToString(), type);
        }

        public static LogMessage LogGeneric(String msg, MessageType type)
        {
            return new LogMessage(msg, type);
        }
        
        public static IDictionary<MessageType, ConsoleColor> MessageColors { get; } = new Dictionary<MessageType, ConsoleColor>
        {
            [MessageType.Default] = ConsoleColor.White,
            [MessageType.Debug] = ConsoleColor.Cyan,
            [MessageType.Info] = ConsoleColor.Blue,
            [MessageType.Action] = ConsoleColor.DarkBlue,
            [MessageType.Good] = ConsoleColor.Green,
            [MessageType.Warning] = ConsoleColor.DarkYellow,
            [MessageType.CriticalWarning] = ConsoleColor.Red,
            [MessageType.Error] = ConsoleColor.DarkRed,
            [MessageType.CriticalError] = ConsoleColor.Magenta,
            [MessageType.FatalError] = ConsoleColor.DarkMagenta,
            [MessageType.UnknownError] = ConsoleColor.Gray
        }.ToImmutableDictionary();

        public MultiString Message { get; }
        private String[] Format { get; }
        public MessageType Type { get; }
        public ConsoleColor MessageColor { get; }
        public Int32 Priority { get; }
        public MessageAdditions Additions { get; }
        public Boolean NewLine { get; }
        public DateTime DateTime { get; }

        public static implicit operator String(LogMessage obj)
        {
            return obj.ToString();
        }

        public LogMessage(String message, MessageType type = MessageType.Default, IEnumerable<String> format = null,
            ConsoleColor? color = null, Int32 priority = 0,
            MessageAdditions additions = MessageAdditions.CurrentTime, Boolean newLine = true)
            : this(new LocaleMultiString(message), type, format, color, priority, additions, newLine)
        {
        }

        public LogMessage(MultiString message, MessageType type = MessageType.Default, IEnumerable<String> format = null,
            ConsoleColor? color = null, Int32 priority = 0,
            MessageAdditions additions = MessageAdditions.CurrentTime, Boolean newLine = true)
        {
            Message = message;
            Format = format?.ToArray();
            Type = type;
            MessageColor = color ?? MessageColors[Type];
            Priority = priority;
            Additions = additions;
            NewLine = newLine;

            DateTime = DateTime.Now;
        }

        public void ToConsole()
        {
            ToConsole(NewLine);
        }

        public void ToConsole(Boolean newLine)
        {
            ToConsole(newLine, MessageColor);
        }

        public void ToConsole(Boolean newLine, ConsoleColor color)
        {
            this.ToConsole(color, newLine);
        }

        public Color GetColor()
        {
            return ConsoleUtils.GetColor(MessageColor);
        }

        public override String ToString()
        {
            CultureInfo info = Localization.CurrentCulture;

            String dateTime = Additions switch
            {
                MessageAdditions.CurrentDate => DateTime.Date.ToString(info),
                MessageAdditions.CurrentTime => $"{DateTime.Hour}:{DateTime.Minute}:{DateTime.Second}",
                MessageAdditions.CurrentDateTime => DateTime.ToString(info),
                _ => String.Empty
            };

            return $"{dateTime}{(dateTime.Length > 0 ? " " : String.Empty)}{(Format.IsNotEmpty() ? String.Format(Message, Format) : Message)}";
        }
    }
}