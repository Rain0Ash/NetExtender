// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.WindowsPresentation.Types.Exceptions;
#pragma warning disable CA2200
// ReSharper disable PossibleIntendedRethrow

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
                return _reverter ??= new RelaySenderCommand<T>(Revert) { CanExecuteHandler = CanRevert };
            }
        }

        ICommand IRevertCommand.Reverter
        {
            get
            {
                return Reverter;
            }
        }
        
        public Boolean CanRevert(T? parameter)
        {
            return CanRevert(null, parameter);
        }
        
        public Boolean CanRevert(Object? sender, T? parameter)
        {
            try
            {
                return CanRevertImplementation(sender, parameter);
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
        
        protected virtual Boolean CanRevertImplementation(Object? sender, T? parameter)
        {
            return true;
        }
        
        public Boolean CanRevert(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            return CanRevert(sender, parameter);
        }
        
        public Boolean CanRevert(Object? sender, Object? parameter)
        {
            try
            {
                return CanRevertImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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
        
        protected virtual Boolean CanRevertImplementation(Object? sender, Object? parameter)
        {
            return parameter switch
            {
                null => CanRevert(sender, default),
                T value => CanRevert(sender, value),
                _ => false
            };
        }
        
        public void Revert(T? parameter)
        {
            Revert(null, parameter);
        }
        
        public void Revert(Object? sender, T? parameter)
        {
            try
            {
                RevertImplementation(sender, parameter);
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
        
        protected abstract void RevertImplementation(Object? sender, T? parameter);
        
        public void Revert(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            Revert(sender, parameter);
        }
        
        public void Revert(Object? sender, Object? parameter)
        {
            try
            {
                RevertImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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
        
        protected virtual void RevertImplementation(Object? sender, Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    RevertImplementation(sender, default);
                    return;
                case T value:
                    RevertImplementation(sender, value);
                    return;
                default:
                    throw new CommandParameterTypeException($"Argument is not of type '{typeof(T).Name}' for {GetType().Name}.", nameof(parameter));
            }
        }
        
        protected virtual Boolean? HandleCanRevert(ExceptionHandlerAction action, Exception? exception)
        {
            return HandleCanExecute(action, exception);
        }
        
        protected virtual Boolean HandleRevert(ExceptionHandlerAction action, Exception? exception)
        {
            return HandleExecute(action, exception);
        }
        
        private sealed class None : RevertCommand<T>
        {
            protected override void ExecuteImplementation(Object? sender, T? parameter)
            {
            }
            
            protected override void RevertImplementation(Object? sender, T? parameter)
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
                return _reverter ??= new RelaySenderCommand<Object>(Revert) { CanExecuteHandler = CanRevert };
            }
        }
        
        public Boolean CanRevert(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            return CanRevert(sender, parameter);
        }
        
        public Boolean CanRevert(Object? sender, Object? parameter)
        {
            try
            {
                return CanRevertImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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
        
        protected virtual Boolean CanRevertImplementation(Object? sender, Object? parameter)
        {
            return true;
        }
        
        public void Revert(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            Revert(sender, parameter);
        }
        
        public void Revert(Object? sender, Object? parameter)
        {
            try
            {
                RevertImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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
        
        protected abstract void RevertImplementation(Object? sender, Object? parameter);
        
        protected virtual Boolean? HandleCanRevert(ExceptionHandlerAction action, Exception? exception)
        {
            return HandleCanExecute(action, exception);
        }
        
        protected virtual Boolean HandleRevert(ExceptionHandlerAction action, Exception? exception)
        {
            return HandleExecute(action, exception);
        }
        
        private sealed class None : RevertCommand
        {
            protected override void ExecuteImplementation(Object? sender, Object? parameter)
            {
            }
            
            protected override void RevertImplementation(Object? sender, Object? parameter)
            {
            }
        }
    }
}