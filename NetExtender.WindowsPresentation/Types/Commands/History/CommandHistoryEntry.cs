using System;
using System.Collections.Generic;
using System.Windows.Input;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Commands.History.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Commands.History
{
    public class CommandHistoryEntry<T>: CommandHistoryEntry, IEquatable<T>, IEquatable<CommandHistoryEntry<T>>
    {
        private protected readonly ICommand<T> _command;
        public override ICommand<T> Command
        {
            get
            {
                return _command;
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

        public CommandHistoryEntry(ICommand<T> command, T parameter, CommandHistoryEntryOptions options)
            : base(options)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _parameter = parameter;
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
                CommandHistoryEntry<T> entry => Equals(entry),
                ICommandHistoryEntry entry => Equals(entry),
                ICommand command => Equals(command),
                _ => false
            };
        }
        
        public virtual Boolean Equals(T? other)
        {
            return EqualityComparer<T>.Default.Equals(Parameter, other);
        }
        
        public override Boolean Equals(ICommand? other)
        {
            return Command.Equals(other);
        }

        public override Boolean Equals(CommandHistoryEntry? other)
        {
            return Equals(other as CommandHistoryEntry<T>);
        }
        
        public virtual Boolean Equals(CommandHistoryEntry<T>? other)
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

    public abstract class CommandHistoryEntry : ICommandHistoryEntry, IEquatable<ICommand>, IEquatable<CommandHistoryEntry>
    {
        public abstract ICommand Command { get; }
        
        ICommand ICommandHistoryEntry.Command
        {
            get
            {
                return Command;
            }
        }

        public CommandHistoryEntryState State { get; protected set; }
        public CommandHistoryEntryOptions Options { get; }
        public abstract Boolean CanExecute { get; }
        public abstract Boolean CanRevert { get; }
        
        protected CommandHistoryEntry(CommandHistoryEntryOptions options)
        {
            Options = options;
        }
        
        public abstract Boolean Execute();
        public abstract Boolean Revert();
        public abstract override Int32 GetHashCode();
        public abstract override Boolean Equals(Object? obj);
        public abstract Boolean Equals(ICommand? other);
        public abstract Boolean Equals(CommandHistoryEntry? other);
        public abstract Boolean Equals(ICommandHistoryEntry? other);
        public abstract override String ToString();
    }
}