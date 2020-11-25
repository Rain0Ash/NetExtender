// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Comparers.KeyInfo;
using NetExtender.Events.Args;
using NetExtender.Messages;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules;
using NetExtender.Utils.IO;

namespace NetExtender.Consoles
{
    public class ConsoleKeyReader : Reader<ConsoleKeyInfo>
    {
        public ReaderHandler<ConsoleKeyInfo> DefaultHandler
        {
            get
            {
                return Rule.DefaultHandler;
            }
            set
            {
                Rule.DefaultHandler = value;
            }
        }

        public ConsoleKeyReader()
            : base(new KeyInfoComparer())
        {
        }

        protected override Boolean OnStart()
        {
            if (IsStarted)
            {
                return true;
            }
            
            ConsoleUtils.ConsoleKeyInfoInput += ProcessConsoleInputAsync;
            ConsoleUtils.AsyncInputType = ConsoleInputType.KeyInfoIntercept;
            return true;
        }
        
        protected override Boolean OnStop()
        {
            ConsoleUtils.ConsoleKeyInfoInput -= ProcessConsoleInputAsync;
            return true;
        }
        
        public override Task<Boolean> ProcessMessageInputAsync(IReaderMessage<ConsoleKeyInfo> key)
        {
            return InvokeHandlerAsync(DefaultHandler, key);
        }

        private async void ProcessConsoleInputAsync(TypeHandledEventArgs<ConsoleKeyInfo> e)
        {
            if (!e.Handled && await ProcessInputAsync(new ReaderMessage<ConsoleKeyInfo>(e.Value)).ConfigureAwait(false))
            {
                e.Handled = true;
            }
        }
    }
}