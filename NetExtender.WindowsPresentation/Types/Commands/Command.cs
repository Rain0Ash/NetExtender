using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class Command<T> : Command, ICommand<T>
    {
        public virtual Boolean CanExecute(T? parameter)
        {
            return true;
        }

        public override Boolean CanExecute(Object? parameter)
        {
            return parameter switch
            {
                null => CanExecute(default),
                T value => CanExecute(value),
                _ => false
            };
        }

        public abstract void Execute(T? parameter);

        public override void Execute(Object? parameter)
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
    }

    public abstract class Command : ICommand
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