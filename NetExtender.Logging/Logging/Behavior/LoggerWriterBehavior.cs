// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public class LoggerWriterBehavior : LoggerBehavior
    {
        protected TextWriter? Writer { get; private set; }

        public LoggerWriterBehavior(TextWriter writer)
        {
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        protected override Boolean Log(String? message, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            if (Writer is null)
            {
                throw new ObjectDisposedException(nameof(TextWriter));
            }

            message = Formatter.Format(message, type, options, offset, provider);
            
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
            if (!disposing)
            {
                return;
            }

            Writer?.Dispose();
            Writer = null;
        }

        protected override async ValueTask DisposeAsync(Boolean disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (Writer is not null)
            {
                await Writer.DisposeAsync();
                Writer = null;
            }
        }
    }
}