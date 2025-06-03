// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.WindowsPresentation.Types.Exceptions;
using NetExtender.WindowsPresentation.Utilities.Types;
#pragma warning disable CA2200
// ReSharper disable PossibleIntendedRethrow

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class Command<T> : Command, ICommand<T>
    {
        public new static Command<T> Empty { get; } = new None();
        
        public Boolean CanExecute(T? parameter)
        {
            return CanExecute(null, parameter);
        }
        
        public Boolean CanExecute(Object? sender, T? parameter)
        {
            try
            {
                return CanExecuteImplementation(sender, parameter);
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
        
        protected virtual Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return true;
        }
        
        protected override Boolean CanExecuteImplementation(Object? sender, Object? parameter)
        {
            return parameter switch
            {
                null => CanExecuteImplementation(sender, default),
                T value => CanExecuteImplementation(sender, value),
                _ => false
            };
        }
        
        public void Execute(T? parameter)
        {
            Execute(null, parameter);
        }

        public void Execute(Object? sender, T? parameter)
        {
            try
            {
                ExecuteImplementation(sender, parameter);
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
        
        protected abstract void ExecuteImplementation(Object? sender, T? parameter);
        
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
                default:
                    throw new CommandParameterTypeException($"Argument is not of type '{typeof(T).Name}' for {GetType().Name}.", nameof(parameter));
            }
        }
        
        private sealed class None : Command<T>
        {
            protected override void ExecuteImplementation(Object? sender, T? parameter)
            {
            }
        }
    }

    public abstract class Command : ISenderCommand
    {
        public static Command Empty { get; } = new None();
        
        public String? Name { get; init; }
        
        public virtual event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        
        public Boolean CanExecute(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            return CanExecute(sender, parameter);
        }

        public Boolean CanExecute(Object? sender, Object? parameter)
        {
            try
            {
                return CanExecuteImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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

        protected virtual Boolean CanExecuteImplementation(Object? sender, Object? parameter)
        {
            return true;
        }
        
        public void Execute(Object? parameter)
        {
            Sender(out Object? sender, ref parameter);
            Execute(sender, parameter);
        }
        
        public void Execute(Object? sender, Object? parameter)
        {
            try
            {
                ExecuteImplementation(sender, parameter);
            }
            catch (CommandParameterTypeException)
            {
                throw;
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
        
        protected abstract void ExecuteImplementation(Object? sender, Object? parameter);
        
        public static void Sender(out Object? sender, ref Object? parameter)
        {
            sender = null;
            if (parameter is CommandSenderArgs args)
            {
                (sender, parameter) = args;
            }
        }
        
        protected virtual ExceptionHandlerAction Handle<T>(Object? sender, T? parameter, Exception? exception)
        {
            return WindowsPresentationCommandUtilities.Exception(this, sender, parameter, exception);
        }
        
        protected virtual Boolean? HandleCanExecute(ExceptionHandlerAction action, Exception? exception)
        {
            switch (action)
            {
                case ExceptionHandlerAction.Successful:
                    return true;
                case ExceptionHandlerAction.Ignore:
                    return false;
                case ExceptionHandlerAction.Default:
                    goto case ExceptionHandlerAction.Throw;
                case ExceptionHandlerAction.Throw:
                    return null;
                case ExceptionHandlerAction.Rethrow:
                {
                    if (exception is null)
                    {
                        goto case ExceptionHandlerAction.Ignore;
                    }

                    throw exception;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ExceptionHandlerAction>(action, nameof(action), null);
            }
        }
        
        protected virtual Boolean HandleExecute(ExceptionHandlerAction action, Exception? exception)
        {
            switch (action)
            {
                case ExceptionHandlerAction.Successful:
                case ExceptionHandlerAction.Ignore:
                    return false;
                case ExceptionHandlerAction.Default:
                    goto case ExceptionHandlerAction.Throw;
                case ExceptionHandlerAction.Throw:
                    return true;
                case ExceptionHandlerAction.Rethrow:
                {
                    if (exception is null)
                    {
                        goto case ExceptionHandlerAction.Ignore;
                    }
                    
                    throw exception;
                }
                default:
                    throw new EnumUndefinedOrNotSupportedException<ExceptionHandlerAction>(action, nameof(action), null);
            }
        }
        
        public override String? ToString()
        {
            return Name ?? GetType().Name;
        }
        
        private sealed class None : Command
        {
            protected override void ExecuteImplementation(Object? sender, Object? parameter)
            {
            }
        }
    }
}