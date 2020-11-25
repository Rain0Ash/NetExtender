// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules;
using NetExtender.Messages.Rules.Interfaces;

namespace NetExtender.Messages
{
    public abstract class MessageReader<T> : Reader<T>
    {
        public ReaderHandler<T> DefaultHandler
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
        
        protected MessageReader()
        {
        }

        public override async Task<Boolean> ProcessMessageInputAsync(IReaderMessage<T> message)
        {
            if (message.Count > 0 && Rules.TryGetValue(message[0], out ICommandRule<T> rule))
            {
                return await rule.ExecuteAsync(message).ConfigureAwait(false);
            }

            return await Rule.ExecuteAsync(message).ConfigureAwait(false);
        }
    }
}