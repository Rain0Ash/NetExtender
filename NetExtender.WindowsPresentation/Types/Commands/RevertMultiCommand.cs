using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class RevertMultiCommand<T> : RevertCommand<T>, IRevertMultiCommand<T>
    {
        public override IMultiCommand<T> Revertor
        {
            get
            {
                return _revertor as IMultiCommand<T> ?? (IMultiCommand<T>) (_revertor = new RelayMultiCommand<T>(Revert, Revert)
                {
                    CanExecuteHandler = CanRevert,
                    CanExecuteMultiHandler = CanRevert
                });
            }
        }

        ICommand<IEnumerable<T?>> IRevertCommand<IEnumerable<T?>>.Revertor
        {
            get
            {
                return Revertor;
            }
        }

        ICommand<IEnumerable> IRevertCommand<IEnumerable>.Revertor
        {
            get
            {
                return Revertor;
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

        public void Revert(IEnumerable? parameter)
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
    }

    public abstract class RevertMultiCommand : RevertCommand, IRevertMultiCommand
    {
        public override IMultiCommand Revertor
        {
            get
            {
                return _revertor as IMultiCommand ?? (IMultiCommand) (_revertor = new RelayMultiCommand<Object>(Revert)
                {
                    CanExecuteHandler = CanRevert
                });
            }
        }

        ICommand<IEnumerable> IRevertCommand<IEnumerable>.Revertor
        {
            get
            {
                return Revertor;
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
        
        public Boolean CanRevert(IEnumerable? parameter)
        {
            return parameter?.Cast<Object?>().All(CanRevert) is not false;
        }

        public void Revert(IEnumerable? parameter)
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
    }
}