using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class RevertCommand<T> : Command<T>, IRevertCommand<T>
    {
        public new static Command Empty { get; } = new None();
        
        private protected ICommand<T>? _reverter;
        public virtual ICommand<T> Reverter
        {
            get
            {
                return _reverter ??= new RelayCommand<T>(Revert) { CanExecuteHandler = CanRevert };
            }
        }

        ICommand IRevertCommand.Reverter
        {
            get
            {
                return Reverter;
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
        
        private sealed class None : RevertCommand<T>
        {
            public override void Execute(T? parameter)
            {
            }
            
            public override void Revert(T? parameter)
            {
            }
        }
    }

    public abstract class RevertCommand : Command, IRevertCommand
    {
        public new static Command Empty { get; } = new None();
        
        private protected ICommand? _reverter;
        public virtual ICommand Reverter
        {
            get
            {
                return _reverter ??= new RelayCommand<Object>(Revert) { CanExecuteHandler = CanRevert };
            }
        }
        
        public virtual Boolean CanRevert(Object? parameter)
        {
            return true;
        }

        public abstract void Revert(Object? parameter);
        
        private sealed class None : RevertCommand
        {
            public override void Execute(Object? parameter)
            {
            }
            
            public override void Revert(Object? parameter)
            {
            }
        }
    }
}