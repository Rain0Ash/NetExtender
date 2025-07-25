// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IIntercept<TArgument> : IIntercept<Object?, TArgument>
    {
        public new event EventHandler<TArgument> Intercept
        {
            add
            {
                ((IIntercept<Object?, TArgument>) this).Intercept += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IIntercept<Object?, TArgument>) this).Intercept -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }

        public new event EventHandler<TArgument> Intercepting
        {
            add
            {
                ((IIntercept<Object?, TArgument>) this).Intercepting += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IIntercept<Object?, TArgument>) this).Intercepting -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
        public new event EventHandler<TArgument> Intercepted
        {
            add
            {
                ((IIntercept<Object?, TArgument>) this).Intercepted += value.Unsafe<EventHandler<Object?, TArgument>>();
            }
            remove
            {
                ((IIntercept<Object?, TArgument>) this).Intercepted -= value.Unsafe<EventHandler<Object?, TArgument>>();
            }
        }
    }

    public interface IIntercept<out TSender, TArgument>
    {
        public event EventHandler<TSender, TArgument> Intercept
        {
            add
            {
                Intercepting += value;
                Intercepted += value;
            }
            remove
            {
                Intercepting -= value;
                Intercepted -= value;
            }
        }

        public event EventHandler<TSender, TArgument> Intercepting;
        public event EventHandler<TSender, TArgument> Intercepted;
    }
}