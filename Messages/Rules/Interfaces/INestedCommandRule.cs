// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Messages.Rules.Interfaces
{
    public interface INestedCommandRule<T> : ICommandRule<T>
    {
        public IReadOnlyIndexDictionary<T, ICommandRule<T>> Rules { get; }
        
        public ReaderHandler<T> DefaultHandler { get; set; }

        public Boolean AddRule(ICommandRule<T> rule);

        public Boolean RemoveRule(ICommandRule<T> rule);
    }
}