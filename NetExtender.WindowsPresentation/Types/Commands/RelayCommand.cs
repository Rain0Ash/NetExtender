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
        
        protected sealed override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return CanExecuteHandler?.Invoke(parameter) is not false;
        }
        
        protected sealed override void ExecuteImplementation(Object? sender, T? parameter)
        {
            ExecuteHandler.Invoke(parameter);
        }
        
        public override String? ToString()
        {
            return Name ?? (ExecuteHandler.Method is { DeclaringType: { } declaring } method ? $"{declaring}: {method.Name}" : ExecuteHandler.ToString());
        }
    }
    
    public class RelaySenderCommand<T> : Command<T>
    {
        public SenderAction<T?> ExecuteHandler { get; }
        public SenderPredicate<T?>? CanExecuteHandler { get; init; }
        
        public RelaySenderCommand(SenderAction<T?> execute)
        {
            ExecuteHandler = execute ?? throw new ArgumentNullException(nameof(execute));
        }
        
        protected sealed override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return CanExecuteHandler?.Invoke(sender, parameter) is not false;
        }
        
        protected sealed override void ExecuteImplementation(Object? sender, T? parameter)
        {
            ExecuteHandler.Invoke(sender, parameter);
        }
        
        public override String? ToString()
        {
            return Name ?? (ExecuteHandler.Method is { DeclaringType: { } declaring } method ? $"{declaring}: {method.Name}" : ExecuteHandler.ToString());
        }
    }
}