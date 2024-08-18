using System;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class RelayCommand<T> : Command<T>
    {
        public Action<T?> ExecuteHandler { get; }
        public Predicate<T?>? CanExecuteHandler { get; init; }

        public RelayCommand(Action<T?> execute)
        {
            ExecuteHandler = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public sealed override Boolean CanExecute(T? parameter)
        {
            return CanExecuteHandler?.Invoke(parameter) is not false;
        }

        public sealed override void Execute(T? parameter)
        {
            ExecuteHandler.Invoke(parameter);
        }
    }
}