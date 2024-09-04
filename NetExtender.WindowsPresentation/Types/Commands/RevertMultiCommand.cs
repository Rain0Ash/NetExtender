using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class RevertMultiCommand<T> : RevertCommand<T>, IRevertMultiCommand<T>
    {
        public new static Command Empty { get; } = new None();

        public override IMultiCommand<T> Reverter
        {
            get
            {
                return _reverter as IMultiCommand<T> ?? (IMultiCommand<T>) (_reverter = new RelayMultiCommand<T>(Revert, Revert)
                {
                    CanExecuteHandler = CanRevert,
                    CanExecuteMultiHandler = CanRevert
                });
            }
        }

        ICommand<IEnumerable<T?>> IRevertCommand<IEnumerable<T?>>.Reverter
        {
            get
            {
                return Reverter;
            }
        }

        public virtual Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return parameter?.All(CanExecute) is not false;
        }

        public virtual Boolean CanExecute(IEnumerable? parameter)
        {
            return CanExecute(parameter?.OfType<T>());
        }

        public override Boolean CanExecute(Object? parameter)
        {
            return parameter switch
            {
                null => CanExecute(default(T)),
                T value => CanExecute(value),
                IEnumerable<T> value => CanExecute(value),
                IEnumerable value => CanExecute(value),
                _ => base.CanExecute(parameter)
            };
        }

        public virtual void Execute(IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (T? value in parameter)
            {
                Execute(value);
            }
        }

        public virtual void Execute(IEnumerable? parameter)
        {
            Execute(parameter?.OfType<T>());
        }

        public override void Execute(Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    Execute(default(T));
                    return;
                case T value:
                    Execute(value);
                    return;
                case IEnumerable<T> value:
                    Execute(value);
                    return;
                case IEnumerable value:
                    Execute(value);
                    return;
                default:
                    base.Execute(parameter);
                    return;
            }
        }

        public virtual Boolean CanRevert(IEnumerable<T?>? parameter)
        {
            return parameter?.All(CanRevert) is not false;
        }

        public virtual Boolean CanRevert(IEnumerable? parameter)
        {
            return CanRevert(parameter?.OfType<T>());
        }
        
        public override Boolean CanRevert(Object? parameter)
        {
            return parameter switch
            {
                null => CanRevert(default(T)),
                T value => CanRevert(value),
                IEnumerable<T> value => CanRevert(value),
                IEnumerable value => CanRevert(value),
                _ => base.CanRevert(parameter)
            };
        }

        public virtual void Revert(IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (T? value in parameter)
            {
                Revert(value);
            }
        }

        public virtual void Revert(IEnumerable? parameter)
        {
            Revert(parameter?.OfType<T>());
        }
        
        public override void Revert(Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    Revert(default(T));
                    return;
                case T value:
                    Revert(value);
                    return;
                case IEnumerable<T> value:
                    Revert(value);
                    return;
                case IEnumerable value:
                    Revert(value);
                    return;
                default:
                    base.Revert(parameter);
                    return;
            }
        }
        
        private sealed class None : RevertMultiCommand<T>
        {
            public override void Execute(T? parameter)
            {
            }
            
            public override void Execute(IEnumerable<T?>? parameter)
            {
            }
            
            public override void Revert(T? parameter)
            {
            }
            
            public override void Revert(IEnumerable<T?>? parameter)
            {
            }
        }
    }

    public abstract class RevertMultiCommand : RevertCommand, IRevertMultiCommand
    {
        public new static Command Empty { get; } = new None();

        public override IMultiCommand Reverter
        {
            get
            {
                return _reverter as IMultiCommand ?? (IMultiCommand) (_reverter = new RelayMultiCommand<Object>(Revert)
                {
                    CanExecuteHandler = CanRevert
                });
            }
        }

        public virtual Boolean CanExecute(IEnumerable? parameter)
        {
            return parameter?.Cast<Object?>().All(CanExecute) is not false;
        }

        public virtual void Execute(IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (Object? value in parameter)
            {
                Execute(value);
            }
        }
        
        public virtual Boolean CanRevert(IEnumerable? parameter)
        {
            return parameter?.Cast<Object?>().All(CanRevert) is not false;
        }

        public virtual void Revert(IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (Object? value in parameter)
            {
                Revert(value);
            }
        }
        
        private sealed class None : RevertMultiCommand
        {
            public override void Execute(Object? parameter)
            {
            }
            
            public override void Execute(IEnumerable? parameter)
            {
            }
            
            public override void Revert(Object? parameter)
            {
            }
            
            public override void Revert(IEnumerable? parameter)
            {
            }
        }
    }
}