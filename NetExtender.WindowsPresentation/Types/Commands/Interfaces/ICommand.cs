namespace System.Windows.Input
{
    public interface ICommand<in T> : ISenderCommand
    {
        public Boolean CanExecute(T? parameter);
        public Boolean CanExecute(Object? sender, T? parameter);
        public void Execute(T? parameter);
        public void Execute(Object? sender, T? parameter);
    }
    
    public interface ISenderCommand : ICommand
    {
        public Boolean CanExecute(Object? sender, Object? parameter);
        public void Execute(Object? sender, Object? parameter);
    }
}