// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Utils.Numerics;
using NetExtender.Messages.Interfaces;
using NetExtender.Messages.Rules;
using NetExtender.Messages.Rules.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.GUI.ConsoleGUI
{
    public class GUIConsolePage : NestedCommandRule<ConsoleKeyInfo>
    {
        public Boolean IsRecursive { get; set; } = true;
        
        private Int32 _selected = -1;
        private ICommandRule<ConsoleKeyInfo> _active;

        public GUIConsolePage(ConsoleKeyInfo id, IEqualityComparer<ConsoleKeyInfo> comparer = null)
            : base(id, null, comparer)
        {
            DefaultHandler = RunAsync;
        }

        protected sealed override async Task<Boolean> RunAsync(IConsoleRule<ConsoleKeyInfo> rule, IReaderMessage<ConsoleKeyInfo> message)
        {
            if (_active is not null)
            {
                if (message.Value.Key != ConsoleKey.Escape)
                {
                    return await _active.ExecuteAsync(message).ConfigureAwait(false);
                }

                _active = null;
                return true;
            }
            
            if (Rules.Count < 1)
            {
                return true;
            }
            
            Action action = message.Value.Key switch
            {
                ConsoleKey.Enter => Enter,
                ConsoleKey.UpArrow => RuleUp,
                ConsoleKey.DownArrow => RuleDown,
                _ => null
            };

            if (action is null)
            {
                return true;
            }
            
            action.Invoke();
            await RefreshAsync().ConfigureAwait(false);
            return true;
        }

        protected virtual Task RefreshAsync()
        {
            ConsoleUtils.Clear();
            Int32 i = 0;
            foreach (ICommandRule<ConsoleKeyInfo> rule in Rules.Values)
            {
                String text = rule.Name;

                if (_selected == i++)
                {
                    text = $"*{text}";
                }
                    
                text.ToConsole();
            }
            
            return Task.CompletedTask;
        }

        protected void Enter()
        {
            _active = Rules.GetValueByIndex(_selected);
        }

        protected void RuleUp()
        {
            _selected = (_selected - 1).ToRange(0, Rules.Count - 1, IsRecursive);
        }

        protected void RuleDown()
        {
            _selected = (_selected + 1).ToRange(0, Rules.Count - 1, IsRecursive);
        }
    }
}