// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public class LoggerStreamBehavior<TLevel> : LoggerBehavior<TLevel> where TLevel : unmanaged, Enum
    {
        protected Stream Stream { get; }
        protected StreamWriter? Writer { get; private set; }

        public LoggerStreamBehavior(Stream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            Writer = new StreamWriter(Stream);
        }

        public LoggerStreamBehavior(Stream stream, ILoggerFormatProvider<TLevel> formatter)
            : base(formatter)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            Writer = new StreamWriter(Stream);
        }

        protected override Boolean Log(String? message, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            if (Writer is null)
            {
                throw new ObjectDisposedException(nameof(StreamWriter));
            }

            message = Formatter.Format(message, level, options, offset, provider);

            if (message is null)
            {
                return false;
            }

            Writer.WriteLine(message);
            Writer.Flush();
            return true;
        }

        protected override void Dispose(Boolean disposing)
        {
            Writer?.Dispose();
            Writer = null;
        }

        protected override async ValueTask DisposeAsync(Boolean disposing)
        {
            if (Writer is not null)
            {
                await Writer.DisposeAsync();
                Writer = null;
            }
        }
    }
}