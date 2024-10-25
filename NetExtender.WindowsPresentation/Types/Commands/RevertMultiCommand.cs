using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
#pragma warning disable CA2200
// ReSharper disable PossibleIntendedRethrow

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class RevertMultiCommand<T> : RevertCommand<T>, IRevertMultiCommand<T>
    {
        public new static Command Empty { get; } = new None();

        public override IMultiCommand<T> Reverter
        {
            get
            {
                return _reverter as IMultiCommand<T> ?? (IMultiCommand<T>) (_reverter = new RelaySenderMultiCommand<T>(Revert, Revert)
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

        public Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return CanExecute(null, parameter);
        }

        public virtual Boolean CanExecute(Object? sender, IEnumerable<T?>? parameter)
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
        
        public Boolean CanRevert(IEnumerable<T?>? parameter)
        {
            return CanRevert(null, parameter);
        }
        
        public virtual Boolean CanRevert(Object? sender, IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return true;
            }
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T? item in parameter)
            {
                if (!CanRevert(sender, item))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public Boolean CanRevert(IEnumerable? parameter)
        {
            return CanRevert(null, parameter);
        }
        
        public Boolean CanRevert(Object? sender, IEnumerable? parameter)
        {
            return CanRevert(sender, parameter?.OfType<T>());
        }
        
        protected override Boolean CanRevertImplementation(Object? sender, Object? parameter)
        {
            return parameter switch
            {
                null => CanRevertImplementation(sender, default),
                T value => CanRevertImplementation(sender, value),
                IEnumerable<T> value => CanRevert(sender, value),
                IEnumerable value => CanRevert(sender, value),
                _ => base.CanRevertImplementation(sender, parameter)
            };
        }
        
        public void Revert(IEnumerable<T?>? parameter)
        {
            Revert(null, parameter);
        }
        
        public virtual void Revert(Object? sender, IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (T? value in parameter)
            {
                Revert(sender, value);
            }
        }
        
        public void Revert(IEnumerable? parameter)
        {
            Revert(null, parameter);
        }
        
        public void Revert(Object? sender, IEnumerable? parameter)
        {
            Revert(sender, parameter?.OfType<T>());
        }
        
        protected override void RevertImplementation(Object? sender, Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    RevertImplementation(sender, default);
                    return;
                case T value:
                    RevertImplementation(sender, value);
                    return;
                case IEnumerable<T> value:
                    Revert(sender, value);
                    return;
                case IEnumerable value:
                    Revert(sender, value);
                    return;
                default:
                    base.RevertImplementation(sender, parameter);
                    return;
            }
        }
        
        private sealed class None : RevertMultiCommand<T>
        {
            protected override void ExecuteImplementation(Object? sender, T? parameter)
            {
            }
            
            public override void Execute(Object? sender, IEnumerable<T?>? parameter)
            {
            }
            
            protected override void RevertImplementation(Object? sender, T? parameter)
            {
            }
            
            public override void Revert(Object? sender, IEnumerable<T?>? parameter)
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
                return _reverter as IMultiCommand ?? (IMultiCommand) (_reverter = new RelaySenderMultiCommand<Object>(Revert)
                {
                    CanExecuteHandler = CanRevert
                });
            }
        }
        
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
        
        public Boolean CanRevert(IEnumerable? parameter)
        {
            return CanRevert(null, parameter);
        }
        
        public virtual Boolean CanRevert(Object? sender, IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return true;
            }
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (Object? value in parameter)
            {
                if (!CanRevert(sender, value))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void Revert(IEnumerable? parameter)
        {
            Revert(null, parameter);
        }
        
        public virtual void Revert(Object? sender, IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (Object? value in parameter)
            {
                Revert(sender, value);
            }
        }
        
        private sealed class None : RevertMultiCommand
        {
            protected override void ExecuteImplementation(Object? sender, Object? parameter)
            {
            }
            
            public override void Execute(Object? sender, IEnumerable? parameter)
            {
            }
            
            protected override void RevertImplementation(Object? sender, Object? parameter)
            {
            }
            
            public override void Revert(Object? sender, IEnumerable? parameter)
            {
            }
        }
    }
}