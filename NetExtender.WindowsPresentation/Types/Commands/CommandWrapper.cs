// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        
        protected override Boolean CanExecuteImplementation(Object? sender, T? parameter)
        {
            return Command.CanExecute(sender, parameter);
        }
        
        protected override Boolean CanExecuteImplementation(Object? sender, Object? parameter)
        {
            return Command.CanExecute(sender, parameter);
        }

        protected override void ExecuteImplementation(Object? sender, T? parameter)
        {
            Command.Execute(sender, parameter);
        }
        
        protected override void ExecuteImplementation(Object? sender, Object? parameter)
        {
            Command.Execute(sender, parameter);
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
        private ISenderCommand Command { get; }
        public override event EventHandler? CanExecuteChanged;
        
        public CommandWrapper(ICommand command)
        {
            Command = command switch
            {
                null => throw new ArgumentNullException(nameof(command)),
                ISenderCommand sender => sender,
                _ => new Wrapper(command)
            };

            Command.CanExecuteChanged += OnCanExecuteChanged;
        }
        
        private void OnCanExecuteChanged(Object? sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, args);
        }
        
        protected override Boolean CanExecuteImplementation(Object? sender, Object? parameter)
        {
            return Command.CanExecute(sender, parameter);
        }
        
        protected override void ExecuteImplementation(Object? sender, Object? parameter)
        {
            Command.Execute(sender, parameter);
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

        private sealed class Wrapper : Command
        {
            private ICommand Command { get; }
            public override event EventHandler? CanExecuteChanged;
            
            public Wrapper(ICommand command)
            {
                Command = command ?? throw new ArgumentNullException(nameof(command));
                Command.CanExecuteChanged += OnCanExecuteChanged;
            }
            
            private void OnCanExecuteChanged(Object? sender, EventArgs args)
            {
                CanExecuteChanged?.Invoke(this, args);
            }
            
            protected override Boolean CanExecuteImplementation(Object? sender, Object? parameter)
            {
                return Command.CanExecute(parameter);
            }
            
            protected override void ExecuteImplementation(Object? sender, Object? parameter)
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
                return Name ?? Command.ToString();
            }
        }
    }
}