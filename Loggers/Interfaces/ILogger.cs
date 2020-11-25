// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Loggers.Messages;

namespace NetExtender.Loggers.Interfaces
{
    public interface ILogger
    {
        public event TypeHandler<LogMessage> Logged;

        public Int32 SavedMessageCount { get; }

        public void Log(LogMessage message);
        public void Clear();
    }
}