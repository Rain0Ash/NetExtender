// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localizations;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Interfaces;

namespace NetExtender.Messages.Rules
{
    public class CommandRule<T> : ConsoleRule<T>, ICommandRule<T>
    {
        public T Id { get; }
        public LocaleMultiString Name { get; set; }
        public LocaleMultiString Annotation { get; set; }

        public CommandRule(T id, ReaderHandler<T> handler = null)
            : base(handler)
        {
            Id = id;
        }

        protected override Boolean Check(IReaderMessage<T> message)
        {
            return message.Count > 0 && Equals(message[0], Id);
        }
    }
}