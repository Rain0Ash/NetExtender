// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public delegate void ExecuteDelegate(Object? parameter);
    public delegate void ExecuteSenderDelegate(Object? sender, Object? parameter);
    public delegate void ExecuteDelegate<in T>(T? parameter);
    public delegate void ExecuteSenderDelegate<in T>(Object? sender, T? parameter);
    public delegate Boolean CanExecuteDelegate(Object? parameter);
    public delegate Boolean CanExecuteSenderDelegate(Object? sender, Object? parameter);
    public delegate Boolean CanExecuteDelegate<in T>(T? parameter);
    public delegate Boolean CanExecuteSenderDelegate<in T>(Object? sender, T? parameter);
    
    public class EventRelayCommand<T> : Command<T>
    {
        public event ExecuteDelegate<T>? Executed;
        public event CanExecuteDelegate<T>? CanExecuted;
        
        protected override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            if (CanExecuted is null)
            {
                return true;
            }
            
            foreach (Delegate @delegate in CanExecuted.GetInvocationList())
            {
                if (@delegate is CanExecuteDelegate<T> validator && !validator.Invoke(parameter))
                {
                    return false;
                }
            }

            return true;
        }
        
        protected override void ExecuteImplementation(Object? sender, T? parameter)
        {
            Executed?.Invoke(parameter);
        }
    }
    
    public class EventRelaySenderCommand<T> : Command<T>
    {
        public event ExecuteSenderDelegate<T>? Executed;
        public event CanExecuteSenderDelegate<T>? CanExecuted;
        
        protected override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            if (CanExecuted is null)
            {
                return true;
            }
            
            foreach (Delegate @delegate in CanExecuted.GetInvocationList())
            {
                if (@delegate is CanExecuteSenderDelegate<T> validator && !validator.Invoke(sender, parameter))
                {
                    return false;
                }
            }

            return true;
        }
        
        protected override void ExecuteImplementation(Object? sender, T? parameter)
        {
            Executed?.Invoke(sender, parameter);
        }
    }
}