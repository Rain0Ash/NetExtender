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