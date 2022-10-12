// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public class LoggerStreamBehavior : LoggerBehavior
    {
        protected Stream Stream { get; }
        protected StreamWriter Writer { get; }

        public LoggerStreamBehavior(Stream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            Writer = new StreamWriter(Stream);
        }

        protected override void Log(String value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            value = Format(type, options, offset, provider) + value;
            Writer.WriteLine(value);
        }

        protected override void Dispose(Boolean disposing)
        {
            Writer.Dispose();
        }

        protected override async ValueTask DisposeAsync(Boolean disposing)
        {
            await Writer.DisposeAsync();
        }
    }
}