// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Messages.Interfaces;

namespace NetExtender.Messages.Rules
{
    public class DefaultCommandRule<T> : NestedCommandRule<T>
    {
        public DefaultCommandRule(T id, ReaderHandler<T> handler = null, IEqualityComparer<T> comparer = null)
            : base(id, handler, comparer)
        {
        }

        protected override Boolean Check(IReaderMessage<T> message)
        {
            return true;
        }
    }
}