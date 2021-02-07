// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Common;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Messages.Rules
{
    public abstract class ConsoleRule<T> : IConsoleRule<T>
    {
        public ConsoleRuleOptions Options { get; set; }
        
        public ReaderHandler<T> Handler { get; }

        protected ConsoleRule()
            : this(null)
        {
        }

        protected ConsoleRule(ReaderHandler<T> handler)
        {
            Handler = handler ?? RunAsync;
        }

        protected virtual Boolean Check(IReaderMessage<T> message)
        {
            return true;
        }

        public async Task<Boolean> ExecuteAsync(IReaderMessage<T> message)
        {
            return Check(message) && await Handler.Invoke(this, message.Next).ConfigureAwait(false);
        }

        protected virtual Task<Boolean> RunAsync(IConsoleRule<T> rule, IReaderMessage<T> message)
        {
            return TaskUtils.False;
        }
    }
}