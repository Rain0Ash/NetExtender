// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Utils.Types;
using NetExtender.Localizations;
using NetExtender.Loggers.Common;
using NetExtender.Loggers.Messages.Interfaces;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.Loggers.Messages
{
    public class LogMessage : StringBase, ILogMessage
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

        public const ConsoleColor DefaultColor = ConsoleColor.White;
        
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
        };

        public override Boolean Immutable
        {
            get
            {
                return Message.Immutable;
            }
        }

        public override Boolean Constant
        {
            get
            {
                return Message.Constant && Format.Length <= 0;
            }
        }

        public override Int32 Length
        {
            get
            {
                return Message.Length;
            }
        }

        public override String Text
        {
            get
            {
                return Message.ToString();
            }
            protected set
            {
                Message = new StringAdapter(value);
            }
        }

        private IString _message;
        [NotNull]
        public IString Message
        {
            get
            {
                return _message;
            }
            protected set
            {
                _message = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Object[] Format { protected get; init; }

        private readonly MessageType _type;
        public MessageType Type
        {
            get
            {
                return _type;
            }
            init
            {
                _type = value;

                if (_color is null)
                {
                    Color = MessageColors.TryGetValue(Type, out ConsoleColor color) ? color : DefaultColor;
                }
            }
        }

        private readonly ConsoleColor? _color;
        public ConsoleColor Color
        {
            get
            {
                return _color ?? ConsoleColor.White;
            }
            init
            {
                _color = value;
            }
        }

        public Int32 Priority { get; init; }
        public MessageAdditions Additions { get; init; } = MessageAdditions.CurrentTime;
        public Boolean NewLine { get; init; }
        public DateTime DateTime { get; }

        public static implicit operator String(LogMessage obj)
        {
            return obj.ToString();
        }
        
        public LogMessage(String message)
            : this(message.ToIString())
        {
        }

        public LogMessage(String message, MessageType type)
            : this(message.ToIString(), type)
        {
        }

        public LogMessage([NotNull] IString message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            DateTime = DateTime.Now;
        }

        public LogMessage([NotNull] IString message, MessageType type)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            DateTime = DateTime.Now;
        }

        public void ToConsole()
        {
            ToConsole(NewLine);
        }

        public void ToConsole(Boolean newLine)
        {
            ToConsole(newLine, Color);
        }

        public void ToConsole(Boolean newLine, ConsoleColor color)
        {
            this.ToConsole(color, newLine);
        }

        public Color GetColor()
        {
            return ConsoleUtils.GetColor(Color);
        }

        public override String ToString()
        {
            CultureInfo info = Localization.Culture;

            String dateTime = Additions switch
            {
                MessageAdditions.CurrentDate => DateTime.Date.ToString(info),
                MessageAdditions.CurrentTime => $"{DateTime.Hour}:{DateTime.Minute}:{DateTime.Second}",
                MessageAdditions.CurrentDateTime => DateTime.ToString(info),
                _ => String.Empty
            };

            return $"{dateTime}{(dateTime.Length > 0 ? " " : String.Empty)}{(Message.Format(Format))}";
        }
    }
}