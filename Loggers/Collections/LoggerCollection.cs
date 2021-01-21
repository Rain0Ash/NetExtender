// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NetExtender.Loggers.Messages;
using NetExtender.Types.Lists;

namespace NetExtender.Loggers.Collections
{
    public class LoggerCollection : EventQueueList<LogMessage>
    {
        public LoggerCollection()
        {
        }

        public LoggerCollection([NotNull] IEnumerable<LogMessage> collection)
            : base(collection)
        {
        }

        public LoggerCollection(Int32 capacity)
            : base(capacity)
        {
        }
    }
}