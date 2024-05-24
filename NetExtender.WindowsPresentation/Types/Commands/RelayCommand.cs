using System;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class RelayCommand<T> : CommandAbstraction<T>
    {
        protected Action<T?> ExecuteHandler { get; }
        protected Func<T?, Boolean>? CanExecuteHandler { get; }

        public RelayCommand(Action<T?> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T?> execute, Func<T?, Boolean>? validator)
        {
            ExecuteHandler = execute ?? throw new ArgumentNullException(nameof(execute));
            CanExecuteHandler = validator;
        }

        public override Boolean CanExecute(T? parameter)
        {
            return CanExecuteHandler?.Invoke(parameter) is not false;
        }

        public override void Execute(T? parameter)
        {
            ExecuteHandler.Invoke(parameter);
        }
    }
}