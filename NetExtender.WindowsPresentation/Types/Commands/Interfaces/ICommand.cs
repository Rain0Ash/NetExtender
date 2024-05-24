namespace System.Windows.Input
{
    public interface ICommand<in T> : ICommand
    {
        public Boolean CanExecute(T? parameter);
        public void Execute(T? parameter);
    }
}