namespace System.Windows.Input
{
    public interface IRevertCommand<in T> : IRevertCommand, ICommand<T>
    {
        public new ICommand<T> Reverter { get; }
        
        public Boolean CanRevert(T? parameter);
        public Boolean CanRevert(Object? sender, T? parameter);
        public void Revert(T? parameter);
        public void Revert(Object? sender, T? parameter);
    }

    public interface IRevertCommand : ISenderCommand
    {
        public ICommand Reverter { get; }
        
        public Boolean CanRevert(Object? parameter);
        public Boolean CanRevert(Object? sender, Object? parameter);
        public void Revert(Object? parameter);
        public void Revert(Object? sender, Object? parameter);
    }
}