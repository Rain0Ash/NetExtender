using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class MultiRelayCommand<T> : RelayCommand<T>, ICommand<IEnumerable>, ICommand<IEnumerable<T?>>
    {
        protected Action<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        protected Func<IEnumerable<T?>?, Boolean> CanExecuteMultiHandler { get; }

        public MultiRelayCommand(Action<T?> execute)
            : this(execute, null)
        {
        }

        public MultiRelayCommand(Action<T?> execute, Func<T?, Boolean>? validator)
            : base(execute, validator)
        {
            ExecuteMultiHandler = DefaultExecute;
            CanExecuteMultiHandler = DefaultCanExecute;
        }

        public MultiRelayCommand(Action<IEnumerable<T?>?> execute)
            : base(execute is not null ? ToExecute(execute) : throw new ArgumentNullException(nameof(execute)))
        {
            ExecuteMultiHandler = execute;
            CanExecuteMultiHandler = DefaultCanExecute;
        }

        public MultiRelayCommand(Action<IEnumerable<T?>?> execute, Func<IEnumerable<T?>?, Boolean>? validator)
            : base(execute is not null ? ToExecute(execute) : throw new ArgumentNullException(nameof(execute)), ToCanExecute(validator))
        {
            ExecuteMultiHandler = execute;
            CanExecuteMultiHandler = validator ?? DefaultCanExecute;
        }

        public MultiRelayCommand(Action<T?> execute, Action<IEnumerable<T?>?>? multiexecute)
            : base(execute)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
            CanExecuteMultiHandler = DefaultCanExecute;
        }

        public MultiRelayCommand(Action<T?> execute, Func<T?, Boolean>? validator, Action<IEnumerable<T?>?>? multiexecute)
            : base(execute, validator)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
            CanExecuteMultiHandler = DefaultCanExecute;
        }

        public MultiRelayCommand(Action<T?> execute, Func<T?, Boolean>? validator, Action<IEnumerable<T?>?>? multiexecute, Func<IEnumerable<T?>?, Boolean>? multivalidator)
            : base(execute, validator)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
            CanExecuteMultiHandler = multivalidator ?? DefaultCanExecute;
        }

        [return: NotNullIfNotNull("execute")]
        protected static Action<T?>? ToExecute(Action<IEnumerable<T?>?>? execute)
        {
            return execute is not null ? parameter => execute(EnumerableUtilities.Factory(parameter)) : null;
        }

        [return: NotNullIfNotNull("validator")]
        protected static Func<T?, Boolean>? ToCanExecute(Func<IEnumerable<T?>?, Boolean>? validator)
        {
            return validator is not null ? parameter => validator(EnumerableUtilities.Factory(parameter)) : null;
        }

        protected Boolean DefaultCanExecute(T? parameter)
        {
            return base.CanExecute(parameter);
        }
        
        protected Boolean DefaultCanExecute(IEnumerable<T?>? parameter)
        {
            return parameter?.All(CanExecute) is not false;
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
        
        public override Boolean CanExecute(Object? parameter)
        {
            return parameter switch
            {
                null => base.CanExecute(default),
                T value => CanExecute(value),
                IEnumerable<T> value => CanExecute(value),
                IEnumerable value => CanExecute(value),
                _ => base.CanExecute(parameter)
            };
        }
        
        public Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return CanExecuteMultiHandler.Invoke(parameter);
        }
        
        public Boolean CanExecute(IEnumerable? parameter)
        {
            return CanExecute(parameter?.OfType<T>());
        }
        
        public override void Execute(Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    base.Execute(default);
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
        
        public void Execute(IEnumerable<T?>? parameter)
        {
            ExecuteMultiHandler.Invoke(parameter);
        }
        
        public void Execute(IEnumerable? parameter)
        {
            Execute(parameter?.OfType<T>());
        }
    }
}