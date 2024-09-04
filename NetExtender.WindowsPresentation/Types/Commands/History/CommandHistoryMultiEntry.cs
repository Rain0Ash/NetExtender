using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Input;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryMultiEntry<T> : CommandHistoryEntry, ICommandHistoryMultiEntry<T>, IEquatable<IEnumerable<T>>, IEquatable<CommandHistoryMultiEntry<T>>
    {
        private protected readonly IMultiCommand<T> _command;
        public override IMultiCommand<T> Command
        {
            get
            {
                return _command;
            }
        }
        
        IMultiCommand ICommandHistoryMultiEntry.Command
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
        
        public override Boolean CanExecute
        {
            get
            {
                return State != CommandHistoryEntryState.Executed && Command.CanExecute(Parameter);
            }
        }

        public override Boolean CanRevert
        {
            get
            {
                return State == CommandHistoryEntryState.Executed && Command is IRevertCommand<T> command && command.CanRevert(Parameter);
            }
        }

        public CommandHistoryMultiEntry(IMultiCommand<T> command, IEnumerable<T>? parameter, CommandHistoryEntryOptions options)
            : base(options)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _parameter = parameter?.AsImmutableList();
        }
        
        public override Boolean Execute()
        {
            if (!CanExecute)
            {
                return false;
            }

            Command.Execute(Parameter);
            State = CommandHistoryEntryState.Executed;
            return true;
        }

        public override Boolean Revert()
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
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                CommandHistoryMultiEntry<T> entry => Equals(entry),
                ICommandHistoryEntry entry => Equals(entry),
                ICommand command => Equals(command),
                _ => false
            };
        }
        
        public virtual Boolean Equals(IEnumerable<T>? other)
        {
            return Parameter is not null ? other is not null && Parameter.SequenceEqual(other) : other is null;
        }
        
        public override Boolean Equals(ICommand? other)
        {
            return Command.Equals(other);
        }
        
        public override Boolean Equals(CommandHistoryEntry? other)
        {
            return Equals(other as CommandHistoryMultiEntry<T>);
        }
        
        public virtual Boolean Equals(CommandHistoryMultiEntry<T>? other)
        {
            return other is not null && Command.Equals(other.Command) && Equals(other.Parameter);
        }
        
        public override Boolean Equals(ICommandHistoryEntry? other)
        {
            return other is CommandHistoryEntry<T> entry && Equals(entry) || Equals(other?.Command);
        }

        public override String ToString()
        {
            return $"Command: {Command}, Parameter: {Parameter.GetString()}";
        }
    }
}