using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IPropertyIntercept<TArgument> : IPropertyIntercept<Object?, TArgument>
    {
        public new event EventHandler<TArgument> PropertyIntercept
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercept += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercept -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertyIntercepting
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercepting += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercepting -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertyIntercepted
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercepted += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyIntercepted -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertyGetIntercept
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercept += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercept -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertyGetIntercepting
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercepting += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercepting -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertyGetIntercepted
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercepted += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertyGetIntercepted -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertySetIntercept
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercept += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercept -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertySetIntercepting
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercepting += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercepting -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> PropertySetIntercepted
        {
            add
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercepted += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IPropertyIntercept<Object?, TArgument>) this).PropertySetIntercepted -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
    }

    public interface IPropertyIntercept<out TSender, TArgument>
    {
        public event EventHandler<TSender, TArgument> PropertyIntercept
        {
            add
            {
                PropertyIntercepting += value;
                PropertyIntercepted += value;
            }
            remove
            {
                PropertyIntercepting -= value;
                PropertyIntercepted -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> PropertyIntercepting
        {
            add
            {
                PropertyGetIntercepting += value;
                PropertySetIntercepting += value;
            }
            remove
            {
                PropertyGetIntercepting -= value;
                PropertySetIntercepting -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> PropertyIntercepted
        {
            add
            {
                PropertyGetIntercepted += value;
                PropertySetIntercepted += value;
            }
            remove
            {
                PropertyGetIntercepted -= value;
                PropertySetIntercepted -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> PropertyGetIntercept
        {
            add
            {
                PropertyGetIntercepting += value;
                PropertyGetIntercepted += value;
            }
            remove
            {
                PropertyGetIntercepting -= value;
                PropertyGetIntercepted -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> PropertyGetIntercepting;
        public event EventHandler<TSender, TArgument> PropertyGetIntercepted;
        
        public event EventHandler<TSender, TArgument> PropertySetIntercept
        {
            add
            {
                PropertySetIntercepting += value;
                PropertySetIntercepted += value;
            }
            remove
            {
                PropertySetIntercepting -= value;
                PropertySetIntercepted -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> PropertySetIntercepting;
        public event EventHandler<TSender, TArgument> PropertySetIntercepted;
    }
}