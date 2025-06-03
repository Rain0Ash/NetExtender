// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        private static class ExpressionStorage<TSource>
        {
            public static Func<TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T>
        {
            public static Func<T, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2>
        {
            public static Func<T1, T2, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3>
        {
            public static Func<T1, T2, T3, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4>
        {
            public static Func<T1, T2, T3, T4, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5>
        {
            public static Func<T1, T2, T3, T4, T5, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6>
        {
            public static Func<T1, T2, T3, T4, T5, T6, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8>().Compile();
        }
        
        private static class ExpressionStorage<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>
        {
            public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TSource> New { get; } = ExpressionUtilities.CreateNewExpression<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>().Compile();
        }
    }
}