using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class RelayMultiCommand<T> : RelayCommand<T>, IMultiCommand<T>
    {
        public Action<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        public Predicate<IEnumerable<T?>?>? CanExecuteMultiHandler { get; init; }

        public RelayMultiCommand(Action<T?> execute)
            : this(execute, null)
        {
        }

        public RelayMultiCommand(Action<IEnumerable<T?>?> execute)
            : base(execute is not null ? ToSingle(execute) : throw new ArgumentNullException(nameof(execute)))
        {
            ExecuteMultiHandler = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelayMultiCommand(Action<T?> execute, Action<IEnumerable<T?>?>? multiexecute)
            : base(execute)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
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
    }
}