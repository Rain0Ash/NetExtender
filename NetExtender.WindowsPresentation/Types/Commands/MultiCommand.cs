// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class MultiCommand<T> : Command<T>, IMultiCommand<T>
    {
        public new static Command Empty { get; } = new None();
        
        public Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return CanExecute(null, parameter);
        }
        
        public Boolean CanExecute(Object? sender, IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return true;
            }
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T? item in parameter)
            {
                if (!CanExecute(sender, item))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public Boolean CanExecute(IEnumerable? parameter)
        {
            return CanExecute(null, parameter);
        }
        
        public Boolean CanExecute(Object? sender, IEnumerable? parameter)
        {
            return CanExecute(sender, parameter?.OfType<T>());
        }
        
        protected override Boolean CanExecuteImplementation(Object? sender, Object? parameter)
        {
            return parameter switch
            {
                null => CanExecuteImplementation(sender, default),
                T value => CanExecuteImplementation(sender, value),
                IEnumerable<T> value => CanExecute(sender, value),
                IEnumerable value => CanExecute(sender, value),
                _ => base.CanExecuteImplementation(sender, parameter)
            };
        }
        
        public void Execute(IEnumerable<T?>? parameter)
        {
            Execute(null, parameter);
        }

        public virtual void Execute(Object? sender, IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (T? value in parameter)
            {
                Execute(sender, value);
            }
        }

        public void Execute(IEnumerable? parameter)
        {
            Execute(null, parameter);
        }

        public void Execute(Object? sender, IEnumerable? parameter)
        {
            Execute(sender, parameter?.OfType<T>());
        }

        protected override void ExecuteImplementation(Object? sender, Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    ExecuteImplementation(sender, default);
                    return;
                case T value:
                    ExecuteImplementation(sender, value);
                    return;
                case IEnumerable<T> value:
                    Execute(sender, value);
                    return;
                case IEnumerable value:
                    Execute(sender, value);
                    return;
                default:
                    base.ExecuteImplementation(sender, parameter);
                    return;
            }
        }
        
        private sealed class None : MultiCommand<T>
        {
            protected override void ExecuteImplementation(Object? sender, T? parameter)
            {
            }
            
            public override void Execute(Object? sender, IEnumerable<T?>? parameter)
            {
            }
        }
    }
    
    public abstract class MultiCommand : Command, IMultiCommand
    {
        public new static Command Empty { get; } = new None();
        
        public Boolean CanExecute(IEnumerable? parameter)
        {
            return CanExecute(null, parameter);
        }
        
        public virtual Boolean CanExecute(Object? sender, IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return true;
            }
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (Object? value in parameter)
            {
                if (!CanExecute(sender, value))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void Execute(IEnumerable? parameter)
        {
            Execute(null, parameter);
        }
        
        public virtual void Execute(Object? sender, IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (Object? value in parameter)
            {
                Execute(sender, value);
            }
        }
        
        private sealed class None : MultiCommand
        {
            protected override void ExecuteImplementation(Object? sender, Object? parameter)
            {
            }
            
            public override void Execute(Object? sender, IEnumerable? parameter)
            {
            }
        }
    }
}