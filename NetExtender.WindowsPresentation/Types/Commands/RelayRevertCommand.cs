using System;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public class RelayRevertCommand<T> : RevertCommand<T>
    {
        public Action<T?> ExecuteHandler { get; }
        public Predicate<T?>? CanExecuteHandler { get; init; }
        public Action<T?> RevertHandler { get; }
        public Predicate<T?>? CanRevertHandler { get; init; }

        public RelayRevertCommand(Action<T?> execute, Action<T?> revert)
        {
            ExecuteHandler = execute ?? throw new ArgumentNullException(nameof(execute));
            RevertHandler = revert ?? throw new ArgumentNullException(nameof(revert));
        }

        public sealed override Boolean CanExecute(T? parameter)
        {
            return CanExecuteHandler?.Invoke(parameter) is not false;
        }

        public sealed override void Execute(T? parameter)
        {
            ExecuteHandler.Invoke(parameter);
        }

        public sealed override Boolean CanRevert(T? parameter)
        {
            return CanRevertHandler?.Invoke(parameter) is not false;
        }

        public sealed override void Revert(T? parameter)
        {
            RevertHandler.Invoke(parameter);
        }
    }
}