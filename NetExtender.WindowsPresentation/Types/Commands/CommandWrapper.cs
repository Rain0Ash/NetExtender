using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public sealed class CommandWrapper<T> : Command<T>
    {
        private ICommand<T> Command { get; }
        public override event EventHandler? CanExecuteChanged;
        
        public CommandWrapper(ICommand<T> command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void OnCanExecuteChanged(Object? sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, args);
        }

        public override Boolean CanExecute(T? parameter)
        {
            return Command.CanExecute(parameter);
        }

        public override Boolean CanExecute(Object? parameter)
        {
            return Command.CanExecute(parameter);
        }

        public override void Execute(T? parameter)
        {
            Command.Execute(parameter);
        }

        public override void Execute(Object? parameter)
        {
            Command.Execute(parameter);
        }

        public override Int32 GetHashCode()
        {
            return Command.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || Command.Equals(other);
        }

        public override String? ToString()
        {
            return Command.ToString();
        }
    }
    
    public sealed class CommandWrapper : Command
    {
        private ICommand Command { get; }
        public override event EventHandler? CanExecuteChanged;

        public CommandWrapper(ICommand command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void OnCanExecuteChanged(Object? sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, args);
        }

        public override Boolean CanExecute(Object? parameter)
        {
            return Command.CanExecute(parameter);
        }

        public override void Execute(Object? parameter)
        {
            Command.Execute(parameter);
        }

        public override Int32 GetHashCode()
        {
            return Command.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || Command.Equals(other);
        }

        public override String? ToString()
        {
            return Command.ToString();
        }
    }
}