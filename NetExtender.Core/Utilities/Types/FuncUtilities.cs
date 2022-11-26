// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Application;

namespace NetExtender.Utilities.Types
{
    public static class FuncUtilities
    {
        public static T? Default<T>()
        {
            return default;
        }

        public static Func<T> WithFailFast<T>(this Func<T> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            T Func()
            {
                try
                {
                    return function();
                }
                catch (Exception exception)
                {
                    if (!Debugger.IsAttached)
                    {
                        throw EnvironmentUtilities.FailFast(exception);
                    }

                    Debugger.Break();
                    throw new NeverOperationException(exception);
                }
            }

            return Func;
        }
    }
}