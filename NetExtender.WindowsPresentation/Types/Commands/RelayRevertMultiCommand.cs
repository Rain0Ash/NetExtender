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
    public class RelayRevertMultiCommand<T> : RelayRevertCommand<T>, IRevertMultiCommand<T>
    {
        public Action<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        public Predicate<IEnumerable<T?>?>? CanExecuteMultiHandler { get; init; }
        public Action<IEnumerable<T?>?> RevertMultiHandler { get; }
        public Predicate<IEnumerable<T?>?>? CanRevertMultiHandler { get; init; }

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

        IMultiCommand<T> IRevertMultiCommand<T>.Reverter
        {
            get
            {
                return Reverter;
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
                null => CanExecuteImplementation(sender, default(T)),
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
                    ExecuteImplementation(sender, default(T));
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public Boolean CanRevert(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                return CanRevertMultiHandler?.Invoke(parameter) is not false;
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleCanRevert(action, exception) is not { } result)
                {
                    throw;
                }
                
                return result;
            }
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
                null => CanRevertImplementation(sender, default(T)),
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Revert(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                RevertMultiHandler.Invoke(parameter);
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleRevert(action, exception))
                {
                    throw;
                }
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
                    RevertImplementation(sender, default(T));
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
    }
    
    public class RelaySenderRevertMultiCommand<T> : RelaySenderRevertCommand<T>, IRevertMultiCommand<T>
    {
        public SenderAction<IEnumerable<T?>?> ExecuteMultiHandler { get; }
        public SenderPredicate<IEnumerable<T?>?>? CanExecuteMultiHandler { get; init; }
        public SenderAction<IEnumerable<T?>?> RevertMultiHandler { get; }
        public SenderPredicate<IEnumerable<T?>?>? CanRevertMultiHandler { get; init; }

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

        IMultiCommand<T> IRevertMultiCommand<T>.Reverter
        {
            get
            {
                return Reverter;
            }
        }

        public RelaySenderRevertMultiCommand(SenderAction<T?> execute, SenderAction<T?> revert)
            : this(execute, null, revert, null)
        {
        }

        public RelaySenderRevertMultiCommand(SenderAction<IEnumerable<T?>?>? execute, SenderAction<IEnumerable<T?>?>? revert)
            : this(execute is not null ? ToSingle(execute) : throw new ArgumentNullException(nameof(execute)), execute, revert is not null ? ToSingle(revert) : throw new ArgumentNullException(nameof(revert)), revert)
        {
        }

        public RelaySenderRevertMultiCommand(SenderAction<T?> execute, SenderAction<IEnumerable<T?>?>? multiexecute, SenderAction<T?> revert, SenderAction<IEnumerable<T?>?>? multirevert)
            : base(execute, revert)
        {
            ExecuteMultiHandler = multiexecute ?? DefaultExecute;
            RevertMultiHandler = multirevert ?? DefaultRevert;
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
        
        protected void DefaultRevert(Object? sender, T? parameter)
        {
            base.Revert(sender, parameter);
        }

        protected void DefaultRevert(Object? sender, IEnumerable<T?>? parameter)
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
                null => CanExecuteImplementation(sender, default(T)),
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
                    ExecuteImplementation(sender, default(T));
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public Boolean CanRevert(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                return CanRevertMultiHandler?.Invoke(sender, parameter) is not false;
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleCanRevert(action, exception) is not { } result)
                {
                    throw;
                }
                
                return result;
            }
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
                null => CanRevertImplementation(sender, default(T)),
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
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Revert(Object? sender, IEnumerable<T?>? parameter)
        {
            try
            {
                RevertMultiHandler.Invoke(sender, parameter);
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(sender, parameter, exception);
                if (HandleRevert(action, exception))
                {
                    throw;
                }
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
                    RevertImplementation(sender, default(T));
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
    }
}