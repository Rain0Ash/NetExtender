// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Events.Args;
using NetExtender.Messages;
using NetExtender.Messages.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.Consoles
{
    public class ConsoleReader : MessageReader<String>
    {
        public ConsoleReader()
        {
        }

        protected override Boolean OnStart()
        {
            if (IsStarted)
            {
                return true;
            }
            
            ConsoleUtils.ConsoleLineInput += ProcessConsoleInputAsync;
            ConsoleUtils.AsyncInputType = ConsoleInputType.Line;
            return true;
        }

        protected override Boolean OnStop()
        {
            ConsoleUtils.ConsoleLineInput -= ProcessConsoleInputAsync;
            return true;
        }

        protected override Boolean CheckMessage(IReaderMessage<String> message)
        {
            return !message.IsExternal;
        }

        public Task<Boolean> ForceProcessInputAsync(String message)
        {
            return ProcessMessageInputAsync(new ReaderStringMessage(message));
        }

        public Task<Boolean> ForceProcessInputAsync(IEnumerable<String> args)
        {
            return ProcessMessageInputAsync(new ReaderStringMessage(args));
        }

        private async void ProcessConsoleInputAsync(TypeHandledEventArgs<String> e)
        {
            if (!e.Handled && await ProcessInputAsync(new ReaderStringMessage(e.Value)).ConfigureAwait(false))
            {
                e.Handled = true;
            }
        }
    }
}