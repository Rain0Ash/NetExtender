using System;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public sealed class CommandSenderArgs : EventArgs, IEquatable<CommandSenderArgs>
    {
        public Object? Sender { get; }
        public Object? Parameter { get; }

        public CommandSenderArgs(Object? sender, Object? parameter)
        {
            Sender = sender;
            Parameter = parameter;
        }
        
        public void Deconstruct(out Object? sender, out Object? parameter)
        {
            sender = Sender;
            parameter = Parameter;
        }
        
        public void CanExecute(ICommand command, Object? parameter)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
        }
        
        public void Execute()
        {
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Sender, Parameter);
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                CommandSenderArgs args => Equals(args),
                _ => Equals(other, Sender) || Equals(other, Parameter)
            };
        }
        
        public Boolean Equals(CommandSenderArgs? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            return Equals(Sender, other.Sender) && Equals(Parameter, other.Parameter);
        }
        
        public override String ToString()
        {
            return $"{{ {nameof(Sender)}: {Sender}, {nameof(Parameter)}: {Parameter} }}";
        }
    }
}