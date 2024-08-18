using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NetExtender.WindowsPresentation.Types.Commands
{
    public abstract class MultiCommand<T> : Command<T>, IMultiCommand<T>
    {
        public virtual Boolean CanExecute(IEnumerable<T?>? parameter)
        {
            return parameter?.All(CanExecute) is not false;
        }

        public virtual Boolean CanExecute(IEnumerable? parameter)
        {
            return CanExecute(parameter?.OfType<T>());
        }

        public override Boolean CanExecute(Object? parameter)
        {
            return parameter switch
            {
                null => CanExecute(default(T)),
                T value => CanExecute(value),
                IEnumerable<T> value => CanExecute(value),
                IEnumerable value => CanExecute(value),
                _ => base.CanExecute(parameter)
            };
        }

        public virtual void Execute(IEnumerable<T?>? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (T? value in parameter)
            {
                Execute(value);
            }
        }

        public virtual void Execute(IEnumerable? parameter)
        {
            Execute(parameter?.OfType<T>());
        }

        public override void Execute(Object? parameter)
        {
            switch (parameter)
            {
                case null:
                    Execute(default(T));
                    return;
                case T value:
                    Execute(value);
                    return;
                case IEnumerable<T> value:
                    Execute(value);
                    return;
                case IEnumerable value:
                    Execute(value);
                    return;
                default:
                    base.Execute(parameter);
                    return;
            }
        }
    }
    
    public abstract class MultiCommand : Command, IMultiCommand
    {
        public virtual Boolean CanExecute(IEnumerable? parameter)
        {
            return parameter?.Cast<Object?>().All(CanExecute) is not false;
        }

        public virtual void Execute(IEnumerable? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            
            foreach (Object? value in parameter)
            {
                Execute(value);
            }
        }
    }
}