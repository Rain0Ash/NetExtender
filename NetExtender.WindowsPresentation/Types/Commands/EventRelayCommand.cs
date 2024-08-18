using System;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public delegate void ExecuteDelegate(Object? parameter);
    public delegate void ExecuteDelegate<in T>(T? parameter);
    public delegate Boolean CanExecuteDelegate(Object? parameter);
    public delegate Boolean CanExecuteDelegate<in T>(T? parameter);
    
    public class EventRelayCommand<T> : Command<T>
    {
        public event ExecuteDelegate<T>? Executed;
        public event CanExecuteDelegate<T>? CanExecuted;

        public override Boolean CanExecute(T? parameter)
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

        public override void Execute(T? parameter)
        {
            Executed?.Invoke(parameter);
        }
    }
}