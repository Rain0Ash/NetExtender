namespace System.Windows.Input
{
    public interface IRevertCommand<in T> : IRevertCommand, ICommand<T>
    {
        public new ICommand<T> Reverter { get; }
        
        public Boolean CanRevert(T? parameter);
        public void Revert(T? parameter);
    }
    
    public interface IRevertCommand : ICommand
    {
        public ICommand Reverter { get; }
        
        public Boolean CanRevert(Object? parameter);
        public void Revert(Object? parameter);
    }
}