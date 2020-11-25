// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Common;

namespace NetExtender.Messages.Rules.Interfaces
{
    public interface IConsoleRule<T>
    {
        public ReaderHandler<T> Handler { get; }
        public ConsoleRuleOptions Options { get; set; }
        public Task<Boolean> ExecuteAsync(IReaderMessage<T> message);
    }
}