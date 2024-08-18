using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Input;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryMultiEntry<T> : ICommandHistoryEntry, IEquatable<ICommand>, IEquatable<T>, IEquatable<CommandHistoryMultiEntry<T>>
    {
        private protected readonly IMultiCommand<T> _command;
        public virtual IMultiCommand<T> Command
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

        private protected readonly ImmutableList<T>? _parameter;
        protected virtual ImmutableList<T>? Parameter
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

        public CommandHistoryEntry(IMultiCommand<T> command, IEnumerable<T>? parameter)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _parameter = parameter?.ToImmutableList();
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