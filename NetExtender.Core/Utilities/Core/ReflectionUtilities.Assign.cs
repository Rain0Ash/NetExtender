// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        private static class AssignStorage<TFrom, TTo>
        {
            public static Func<TFrom, TTo>? Simple { get; } = ExpressionUtilities.CreateAssignExpression<TFrom, TTo>(true)?.Compile();
            public static Func<TFrom, TTo>? Assign { get; } = ExpressionUtilities.CreateAssignExpression<TFrom, TTo>()?.Compile();
        }
    }
}