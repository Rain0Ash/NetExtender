using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class RevertCommand<T> : Command<T>, IRevertCommand<T>
    {
        private protected ICommand<T>? _revertor;
        public virtual ICommand<T> Revertor
        {
            get
            {
                return _revertor ??= new RelayCommand<T>(Revert) { CanExecuteHandler = CanRevert };
            }
        }

        ICommand IRevertCommand.Revertor
        {
            get
            {
                return Revertor;
            }
        }

        public virtual Boolean CanRevert(T? parameter)
        {
            return true;
        }

        public virtual Boolean CanRevert(Object? parameter)
        {
            return parameter switch
            {
                null => CanRevert(default),
                T value => CanRevert(value),
                _ => false
            };
        }

        public abstract void Revert(T? parameter);

        public virtual void Revert(Object? parameter)
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

    public abstract class RevertCommand : Command, IRevertCommand
    {
        private protected ICommand? _revertor;
        public virtual ICommand Revertor
        {
            get
            {
                return _revertor ??= new RelayCommand<Object>(Revert) { CanExecuteHandler = CanRevert };
            }
        }
        
        public virtual Boolean CanRevert(Object? parameter)
        {
            return true;
        }

        public abstract void Revert(Object? parameter);
    }
}