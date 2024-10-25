using System;
#pragma warning disable CA2200
// ReSharper disable PossibleIntendedRethrow

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
        
        protected sealed override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return CanExecuteHandler?.Invoke(parameter) is not false;
        }
        
        protected sealed override void ExecuteImplementation(Object? sender, T? parameter)
        {
            ExecuteHandler.Invoke(parameter);
        }
        
        protected sealed override Boolean CanRevertImplementation(Object? sender, T? parameter)
        {
            return CanRevertHandler?.Invoke(parameter) is not false;
        }
        
        protected sealed override void RevertImplementation(Object? sender, T? parameter)
        {
            RevertHandler.Invoke(parameter);
        }
        
        public override String? ToString()
        {
            return Name ?? (ExecuteHandler.Method is { DeclaringType: { } declaring } method ? $"{declaring}: {method.Name}" : ExecuteHandler.ToString());
        }
    }
    
    public class RelaySenderRevertCommand<T> : RevertCommand<T>
    {
        public SenderAction<T?> ExecuteHandler { get; }
        public SenderPredicate<T?>? CanExecuteHandler { get; init; }
        public SenderAction<T?> RevertHandler { get; }
        public SenderPredicate<T?>? CanRevertHandler { get; init; }

        public RelaySenderRevertCommand(SenderAction<T?> execute, SenderAction<T?> revert)
        {
            ExecuteHandler = execute ?? throw new ArgumentNullException(nameof(execute));
            RevertHandler = revert ?? throw new ArgumentNullException(nameof(revert));
        }
        
        protected sealed override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return CanExecuteHandler?.Invoke(sender, parameter) is not false;
        }

        protected sealed override void ExecuteImplementation(Object? sender, T? parameter)
        {
            ExecuteHandler.Invoke(sender, parameter);
        }
        
        protected sealed override Boolean CanRevertImplementation(Object? sender, T? parameter)
        {
            return CanRevertHandler?.Invoke(sender, parameter) is not false;
        }
        
        protected sealed override void RevertImplementation(Object? sender, T? parameter)
        {
            RevertHandler.Invoke(sender, parameter);
        }
        
        public override String? ToString()
        {
            return Name ?? (ExecuteHandler.Method is { DeclaringType: { } declaring } method ? $"{declaring}: {method.Name}" : ExecuteHandler.ToString());
        }
    }
}