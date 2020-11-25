// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Interfaces;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Types.Dictionaries.Interfaces;
using ReactiveUI;

namespace NetExtender.Messages.Interfaces
{
    public interface IReader<T> : IReactiveObject, ITask, IDisposable
    {
        public IReadOnlyIndexDictionary<T, ICommandRule<T>> Rules { get; }
        public Boolean AddRule(ICommandRule<T> rule);
        public Boolean RemoveRule(ICommandRule<T> rule);
        
        public Task<Boolean> ProcessInputAsync(IReaderMessage<T> message);
        public Task<Boolean> ProcessMessageInputAsync(IReaderMessage<T> message);
    }
}