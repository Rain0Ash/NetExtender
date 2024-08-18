using System;
using System.Collections.Generic;
using System.Windows.Input;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryEntry<T>: ICommandHistoryEntry, IEquatable<ICommand>, IEquatable<T>, IEquatable<CommandHistoryEntry<T>>
    {
        private protected readonly ICommand<T> _command;
        public virtual ICommand<T> Command
        {
            get
            {
                return _command;
            }
        }

        ICommand ICommandHistoryEntry.Command
        {
            get
            {
                return Command;
            }
        }

        private protected readonly T _parameter;
        protected virtual T Parameter
        {
            get
            {
                return _parameter;
            }
        }

        public CommandHistoryEntryState State { get; protected set; }

        public virtual Boolean CanExecute
        {
            get
            {
                return State != CommandHistoryEntryState.Executed && Command.CanExecute(Parameter);
            }
        }

        public virtual Boolean CanRevert
        {
            get
            {
                return State == CommandHistoryEntryState.Executed && Command is IRevertCommand<T> command && command.CanRevert(Parameter);
            }
        }

        public CommandHistoryEntry(ICommand<T> command, T parameter)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _parameter = parameter;
        }
        
        public virtual Boolean Execute()
        {
            if (!CanExecute)
            {
                return false;
            }

            Command.Execute(Parameter);
            State = CommandHistoryEntryState.Executed;
            return true;
        }

        public virtual Boolean Revert()
        {
            if (!CanRevert || Command is not IRevertCommand<T> command)
            {
                return false;
            }

            command.Revert(Parameter);
            State = CommandHistoryEntryState.Reverted;
            return true;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Command, Parameter);
        }

        public virtual Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Parameter, other);
        }

        public Boolean Equals(ICommand? other)
        {
            return Command.Equals(other);
        }

        public virtual Boolean Equals(CommandHistoryEntry<T>? other)
        {
            return other is not null && Command.Equals(other.Command) && Equals(other.Parameter);
        }

        public virtual Boolean Equals(ICommandHistoryEntry? other)
        {
            return other is CommandHistoryEntry<T> entry && Equals(entry) || Equals(other?.Command);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                CommandHistoryEntry<T> entry => Equals(entry),
                ICommandHistoryEntry entry => Equals(entry),
                ICommand command => Equals(command),
                _ => false
            };
        }
    }
}