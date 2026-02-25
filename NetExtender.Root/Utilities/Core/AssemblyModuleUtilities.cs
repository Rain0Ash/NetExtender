using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Core
{
    public static class AssemblyModuleUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Type> SafeResolveType(this Module module, Int32 token)
        {
            try
            {
                if (module.ResolveType(token) is { } result)
                {
                    return result;
                }

                return default;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }
    }
}