// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Utils.Types;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Messages
{
    public abstract class Reader<T> : ReactiveObject, IReader<T>
    {
        protected INestedCommandRule<T> Rule { get; set; }

        public IReadOnlyIndexDictionary<T, ICommandRule<T>> Rules
        {
            get
            {
                return Rule.Rules;
            }
        }

        [Reactive]
        public Boolean IsStarted { get; private set; }
        
        [Reactive]
        public Boolean IsPaused { get; private set; }

        protected Reader(IEqualityComparer<T> comparer = null)
        {
            Rule = new DefaultCommandRule<T>(default, DefaultHandlerAsync, comparer);
        }
        
        public void Start()
        {
            if (OnStart())
            {
                IsStarted = true;
            }
        }

        protected virtual Boolean OnStart()
        {
            return true;
        }

        public void Pause()
        {
            if (OnPause())
            {
                IsPaused = true;
            }
        }

        protected virtual Boolean OnPause()
        {
            return true;
        }

        public void Resume()
        {
            if (OnResume())
            {
                IsPaused = false;
            }
        }

        protected virtual Boolean OnResume()
        {
            return true;
        }
        
        public void Stop()
        {
            if (OnStop())
            {
                IsStarted = false;
            }
        }

        protected virtual Boolean OnStop()
        {
            return true;
        }

        public Boolean AddRule(ICommandRule<T> rule)
        {
            return Rule.AddRule(rule);
        }
        
        public Boolean RemoveRule(ICommandRule<T> rule)
        {
            return Rule.RemoveRule(rule);
        }

        protected Boolean Check(IReaderMessage<T> message)
        {
            return IsStarted && !IsPaused && CheckMessage(message);
        }
        
        protected virtual Boolean CheckMessage(IReaderMessage<T> message)
        {
            return true;
        }

        public async Task<Boolean> ProcessInputAsync(IReaderMessage<T> message)
        {
            if (Check(message))
            {
                return await ProcessMessageInputAsync(message).ConfigureAwait(false);
            }

            return false;
        }

        protected virtual Task<Boolean> DefaultHandlerAsync(IConsoleRule<T> rule, IReaderMessage<T> message)
        {
            return TaskUtils.False;
        }
        
        protected Task<Boolean> ProcessInputAsync(IConsoleRule<T> rule, IReaderMessage<T> message)
        {
            return ProcessInputAsync(message);
        }

        public virtual Task<Boolean> ProcessMessageInputAsync(IReaderMessage<T> message)
        {
            return Rule.ExecuteAsync(message);
        }

        protected async Task<Boolean> InvokeHandlerAsync(ReaderHandler<T> handler, IReaderMessage<T> message)
        {
            if (handler is null)
            {
                return false;
            }
            
            return await handler.Invoke(null, message).ConfigureAwait(false);
        }
        
        public void Dispose()
        {
            Stop();
            Dispose(true);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }
    }
}