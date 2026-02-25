using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static class ParameterInfoUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("parameter")]
        public static String? FullName(this ParameterInfo? parameter)
        {
            return parameter is not null ? $"{parameter.ParameterType.Name} {parameter.Name}" : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("parameters")]
        public static String[]? Names(this ParameterInfo[]? parameters)
        {
            return parameters?.ConvertAll(static parameter => parameter.Name ?? String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("parameters")]
        public static Type[]? Types(this ParameterInfo[]? parameters)
        {
            return parameters?.ConvertAll(static parameter => parameter.ParameterType);
        }
    }
}