using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IMethodIntercept<TArgument> : IMethodIntercept<Object?, TArgument>
    {
        public new event EventHandler<TArgument> MethodIntercept
        {
            add
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercept += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercept -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> MethodIntercepting
        {
            add
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercepting += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercepting -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        
        public new event EventHandler<TArgument> MethodIntercepted
        {
            add
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercepted += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IMethodIntercept<Object?, TArgument>) this).MethodIntercepted -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
    }

    public interface IMethodIntercept<out TSender, TArgument>
    {
        public event EventHandler<TSender, TArgument> MethodIntercept
        {
            add
            {
                MethodIntercepting += value;
                MethodIntercepted += value;
            }
            remove
            {
                MethodIntercepting -= value;
                MethodIntercepted -= value;
            }
        }
        
        public event EventHandler<TSender, TArgument> MethodIntercepting;
        public event EventHandler<TSender, TArgument> MethodIntercepted;
    }
}