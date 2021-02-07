// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Loggers.Common;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.Loggers.Messages.Interfaces
{
    public interface ILogMessage : IString, IConsoleMessage
    {
        public IString Message { get; }
        public MessageType Type { get; }
        public Int32 Priority { get; }
        public DateTime DateTime { get; }
    }
}