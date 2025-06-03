// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Utilities.Delegates;

namespace NetExtender.Utilities.Types
{
    public static class ValueDelegateUtilities
    {
        public static Info GetInfo<TDelegate>(this TDelegate @delegate) where TDelegate : struct, IValueDelegate<TDelegate>
        {
            return new Info(@delegate.Method, @delegate.GetArguments());
        }

        public readonly struct Info
        {
            public static implicit operator MethodInfo?(Info value)
            {
                return value.Method;
            }
            
            public static implicit operator Object?[]?(Info value)
            {
                return value.Arguments;
            }
            
            public MethodInfo? Method { get; }
            public Object?[]? Arguments { get; }
            
            public Info(MethodInfo? method, Object?[]? arguments)
            {
                Method = method;
                Arguments = arguments;
            }
        }
    }
}