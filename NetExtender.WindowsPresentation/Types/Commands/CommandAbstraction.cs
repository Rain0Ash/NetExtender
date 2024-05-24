using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class CommandAbstraction<T> : CommandAbstraction, ICommand<T>
    {
        public sealed override Boolean CanExecute(Object? parameter)
        {
            return parameter switch
            {
                null => CanExecute(default),
                T value => CanExecute(value),
                _ => false
            };
        }

        public virtual Boolean CanExecute(T? parameter)
        {
            return true;
        }

        public sealed override void Execute(Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    Execute(default);
                    return;
                case T value:
                    Execute(value);
                    return;
                default:
                    throw new ArgumentException($"Argument is not of type '{typeof(T).Name}' for {GetType().Name}.", nameof(parameter));
            }
        }

        public abstract void Execute(T? parameter);
    }

    public abstract class CommandAbstraction : ICommand
    {
        public virtual event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public virtual Boolean CanExecute(Object? parameter)
        {
            return true;
        }

        public abstract void Execute(Object? parameter);
    }
}