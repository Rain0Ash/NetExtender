// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Messages.Rules
{
    public class NestedCommandRule<T> : CommandRule<T>, INestedCommandRule<T>
    {
        private readonly IndexDictionary<T, ICommandRule<T>> _rules;

        public IReadOnlyIndexDictionary<T, ICommandRule<T>> Rules
        {
            get
            {
                return _rules;
            }
        }

        private ReaderHandler<T> _handler;
        public virtual ReaderHandler<T> DefaultHandler
        {
            get
            {
                return _handler;
            }
            set
            {
                foreach (ICommandRule<T> rule in Rules.Values)
                {
                    if (rule is not INestedCommandRule<T> nested)
                    {
                        continue;
                    }

                    if (nested.DefaultHandler == _handler)
                    {
                        nested.DefaultHandler = value;
                    }
                }
                
                _handler = value;
            }
        }
        
        public NestedCommandRule(T id, ReaderHandler<T> handler = null, IEqualityComparer<T> comparer = null)
            : base(id)
        {
            _rules = new IndexDictionary<T, ICommandRule<T>>(comparer);
            DefaultHandler = handler;
        }

        protected override async Task<Boolean> RunAsync(IConsoleRule<T> rule, IReaderMessage<T> message)
        {
            if (message.Count > 0 && Rules.TryGetValue(message[0], out ICommandRule<T> nrule))
            {
                return await nrule.ExecuteAsync(message).ConfigureAwait(false);
            }

            if (DefaultHandler is null)
            {
                return false;
            }
                
            return await DefaultHandler.Invoke(this, message).ConfigureAwait(false);
        }
        
        public Boolean AddRule(ICommandRule<T> rule)
        {
            return rule is null || _rules.TryAdd(rule.Id, rule);
        }

        public Boolean RemoveRule(ICommandRule<T> rule)
        {
            return _rules.Remove(rule.Id);
        }
    }
}