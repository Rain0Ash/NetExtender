// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utils.Numerics;
using NetExtender.Loggers.Collections;
using NetExtender.Loggers.Interfaces;
using NetExtender.Loggers.Messages;

// ReSharper disable MemberCanBePrivate.Global

namespace NetExtender.Loggers
{
    public class Logger : ILogger
    {
        public static Logger Default { get; } = new Logger();
        
        public event TypeHandler<LogMessage> Logged;

        public Int32 SavedMessageCount
        {
            get
            {
                return _messages.MaximumLength;
            }
            set
            {
                _messages.MaximumLength = value;
            }
        }

        private readonly LoggerCollection _messages;

        public Logger(Int32 count = 255)
        {
            count = count.ToRange(16);
            
            _messages = new LoggerCollection(count);
            SavedMessageCount = _messages.Capacity;
        }

        public void Log(LogMessage message)
        {
            _messages.Add(message);
            Logged?.Invoke(message);
        }

        public void Clear()
        {
            _messages.Clear();
        }
    }
}