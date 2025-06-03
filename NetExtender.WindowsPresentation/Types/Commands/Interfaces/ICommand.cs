// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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