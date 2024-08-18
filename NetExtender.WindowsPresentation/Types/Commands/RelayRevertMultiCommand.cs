using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class RelayRevertMultiCommand<T> : RelayRevertCommand<T>, IRevertMultiCommand<T>
    {
        public Action<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        public Predicate<IEnumerable<T?>?>? CanExecuteMultiHandler { get; init; }
        public Action<IEnumerable<T?>?> RevertMultiHandler { get; }
        public Predicate<IEnumerable<T?>?>? CanRevertMultiHandler { get; init; }

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

        IMultiCommand<T> IRevertMultiCommand<T>.Revertor
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

        public RelayRevertMultiCommand(Action<T?> execute, Action<T?> revert)
            : this(execute, null, revert, null)
        {
        }

        public RelayRevertMultiCommand(Action<IEnumerable<T?>?>? execute, Action<IEnumerable<T?>?>? revert)
            : this(execute is not null ? ToSingle(execute) : throw new ArgumentNullException(nameof(execute)), execute, revert is not null ? ToSingle(revert) : throw new ArgumentNullException(nameof(revert)), revert)
        {
        }

        public RelayRevertMultiCommand(Action<T?> execute, Action<IEnumerable<T?>?>? multiexecute, Action<T?> revert, Action<IEnumerable<T?>?>? multirevert)
            : base(execute, revert)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
            RevertMultiHandler = multirevert ?? DefaultRevert;
        }
        
        [return: NotNullIfNotNull("action")]
        protected static Action<T?>? ToSingle(Action<IEnumerable<T?>?>? action)
        {
            return action is not null ? parameter => action(EnumerableUtilities.Factory(parameter)) : null;
        }
        
        protected void DefaultExecute(T? parameter)
        {
            base.Execute(parameter);
        }

        protected void DefaultExecute(IEnumerable<T?>? parameter)
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
        
        protected void DefaultRevert(T? parameter)
        {
            base.Revert(parameter);
        }

        protected void DefaultRevert(IEnumerable<T?>? parameter)
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
        
        public Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return CanExecuteMultiHandler?.Invoke(parameter) is not false;
        }

        public Boolean CanExecute(IEnumerable? parameter)
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

        public void Execute(IEnumerable<T?>? parameter)
        {
            ExecuteMultiHandler.Invoke(parameter);
        }

        public void Execute(IEnumerable? parameter)
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

        public Boolean CanRevert(IEnumerable<T?>? parameter)
        {
            return CanRevertMultiHandler?.Invoke(parameter) is not false;
        }

        public Boolean CanRevert(IEnumerable? parameter)
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

        public void Revert(IEnumerable<T?>? parameter)
        {
            RevertMultiHandler.Invoke(parameter);
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
}