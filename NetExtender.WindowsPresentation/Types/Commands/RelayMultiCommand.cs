using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Utilities.Types;
#pragma warning disable CA2200
// ReSharper disable PossibleIntendedRethrow

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
            return CanExecute(null, parameter);
        }
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public Boolean CanExecute(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                return CanExecuteMultiHandler?.Invoke(parameter) is not false;
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleCanExecute(action, exception) is not { } result)
                {
                    throw;
                }
                
                return result;
            }
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Execute(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                ExecuteMultiHandler.Invoke(parameter);
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleExecute(action, exception))
                {
                    throw;
                }
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
    }
    
    public class RelaySenderMultiCommand<T> : RelaySenderCommand<T>, IMultiCommand<T>
    {
        public SenderAction<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        public SenderPredicate<IEnumerable<T?>?>? CanExecuteMultiHandler { get; init; }

        public RelaySenderMultiCommand(SenderAction<T?> execute)
            : this(execute, null)
        {
        }

        public RelaySenderMultiCommand(SenderAction<IEnumerable<T?>?> execute)
            : base(execute is not null ? ToSingle(execute) : throw new ArgumentNullException(nameof(execute)))
        {
            ExecuteMultiHandler = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public RelaySenderMultiCommand(SenderAction<T?> execute, SenderAction<IEnumerable<T?>?>? multiexecute)
            : base(execute)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
        }

        [return: NotNullIfNotNull("action")]
        protected static SenderAction<T?>? ToSingle(SenderAction<IEnumerable<T?>?>? action)
        {
            return action is not null ? (sender, parameter) => action(sender, EnumerableUtilities.Factory(parameter)) : null;
        }
        
        protected void DefaultExecute(Object? sender, T? parameter)
        {
            base.Execute(sender, parameter);
        }

        protected void DefaultExecute(Object? sender, IEnumerable<T?>? parameter)
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
        
        public Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return CanExecute(null, parameter);
        }
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public Boolean CanExecute(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                return CanExecuteMultiHandler?.Invoke(sender, parameter) is not false;
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleCanExecute(action, exception) is not { } result)
                {
                    throw;
                }
                
                return result;
            }
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Execute(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                ExecuteMultiHandler.Invoke(sender, parameter);
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleExecute(action, exception))
                {
                    throw;
                }
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
    }
}