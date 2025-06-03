// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Numerics
{
    public static class MathGeneric
    {
        public static T Negate<T>(T value)
        {
            return MathGeneric<T>.Negate(value);
        }

        public static T Not<T>(T value)
        {
            return MathGeneric<T>.Not(value);
        }

        public static T Or<T>(T first, T second)
        {
            return MathGeneric<T>.Or(first, second);
        }

        public static T And<T>(T first, T second)
        {
            return MathGeneric<T>.And(first, second);
        }

        public static T Xor<T>(T first, T second)
        {
            return MathGeneric<T>.Xor(first, second);
        }

        public static T2 Convert<T1, T2>(T1 value)
        {
            return MathGeneric<T1, T2>.Convert(value);
        }

        public static T Add<T>(T first, T second)
        {
            return MathGeneric<T>.Add(first, second);
        }

        public static T1 AltAdd<T1, T2>(T1 first, T2 second)
        {
            return MathGeneric<T2, T1>.Add(first, second);
        }

        public static T Subtract<T>(T first, T second)
        {
            return MathGeneric<T>.Subtract(first, second);
        }

        public static T1 AltSubtract<T1, T2>(T1 first, T2 second)
        {
            return MathGeneric<T2, T1>.Subtract(first, second);
        }

        public static T Multiply<T>(T first, T second)
        {
            return MathGeneric<T>.Multiply(first, second);
        }

        public static T1 AltMultiply<T1, T2>(T1 first, T2 second)
        {
            return MathGeneric<T2, T1>.Multiply(first, second);
        }

        public static T Divide<T>(T first, T second)
        {
            return MathGeneric<T>.Divide(first, second);
        }

        public static T1 AltDivide<T1, T2>(T1 first, T2 second)
        {
            return MathGeneric<T2, T1>.Divide(first, second);
        }

        public static T Modulo<T>(T first, T second)
        {
            return MathGeneric<T>.Modulo(first, second);
        }

        public static T1 AltModulo<T1, T2>(T1 first, T2 second)
        {
            return MathGeneric<T2, T1>.Modulo(first, second);
        }

        public static Boolean Equal<T>(T first, T second)
        {
            return MathGeneric<T>.Equals(first, second);
        }

        public static Boolean NotEqual<T>(T first, T second)
        {
            return MathGeneric<T>.NotEquals(first, second);
        }

        public static Boolean Greater<T>(T first, T second)
        {
            return MathGeneric<T>.Greater(first, second);
        }

        public static Boolean GreaterEqual<T>(T first, T second)
        {
            return MathGeneric<T>.GreaterOrEquals(first, second);
        }

        public static Boolean LessThan<T>(T first, T second)
        {
            return MathGeneric<T>.Less(first, second);
        }

        public static Boolean LessEqual<T>(T first, T second)
        {
            return MathGeneric<T>.LessOrEquals(first, second);
        }

        public static T DivideInt32<T>(T value, Int32 divisor)
        {
            return MathGeneric<Int32, T>.Divide(value, divisor);
        }

        public static T DivideInt64<T>(T value, Int64 divisor)
        {
            return MathGeneric<Int64, T>.Divide(value, divisor);
        }
    }

    public static class MathGeneric<T, TResult>
    {
        static MathGeneric()
        {
            ConvertLazy = new Lazy<Func<T, TResult>>(() => ExpressionUtilities.CreateExpression<T, TResult>(body => Expression.Convert(body, typeof(TResult))), true);
            AddLazy = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Add, true), true);
            SubtractLazy = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Subtract, true), true);
            MultiplyLazy = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Multiply, true), true);
            DivideLazy = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Divide, true), true);
            ModuloLazy = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Modulo, true), true);
        }

        private static Lazy<Func<T, TResult>> ConvertLazy { get; }

        public static Func<T, TResult> Convert
        {
            get
            {
                return ConvertLazy.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> AddLazy { get; }

        public static Func<TResult, T, TResult> Add
        {
            get
            {
                return AddLazy.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> SubtractLazy { get; }

        public static Func<TResult, T, TResult> Subtract
        {
            get
            {
                return SubtractLazy.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> MultiplyLazy { get; }

        public static Func<TResult, T, TResult> Multiply
        {
            get
            {
                return MultiplyLazy.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> DivideLazy { get; }

        public static Func<TResult, T, TResult> Divide
        {
            get
            {
                return DivideLazy.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> ModuloLazy { get; }

        public static Func<TResult, T, TResult> Modulo
        {
            get
            {
                return ModuloLazy.Value;
            }
        }
    }

    public static class MathGeneric<T>
    {
        static MathGeneric()
        {
            Type type = typeof(T);

            if (type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                throw new InvalidOperationException($"Generic math between {nameof(Nullable)} types is NotFunc implemented. Type: {typeof(T).FullName} is nullable.");
            }

            AddLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Add), true);
            SubtractLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Subtract), true);
            MultiplyLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Multiply), true);
            DivideLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Divide), true);
            ModuloLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Modulo), true);

            EqualsLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.Equal), true);
            NotEqualsLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.NotEqual), true);
            GreaterLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThan), true);
            GreaterOrEqualsLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThanOrEqual), true);
            LessLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThan), true);
            LessOrEqualsLazy = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThanOrEqual), true);

            NegateLazy = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Negate), true);
            AndLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.And), true);
            OrLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Or), true);
            NotLazy = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Not), true);
            XorLazy = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.ExclusiveOr), true);
        }

        public static T? Zero
        {
            get
            {
                return default;
            }
        }

        private static Lazy<Func<T, T>> NegateLazy { get; }

        public static Func<T, T> Negate
        {
            get
            {
                return NegateLazy.Value;
            }
        }

        private static Lazy<Func<T, T>> NotLazy { get; }

        public static Func<T, T> Not
        {
            get
            {
                return NotLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> OrLazy { get; }

        public static Func<T, T, T> Or
        {
            get
            {
                return OrLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> AndLazy { get; }

        public static Func<T, T, T> And
        {
            get
            {
                return AndLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> XorLazy { get; }

        public static Func<T, T, T> Xor
        {
            get
            {
                return XorLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> AddLazy { get; }

        public static Func<T, T, T> Add
        {
            get
            {
                return AddLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> SubtractLazy { get; }

        public static Func<T, T, T> Subtract
        {
            get
            {
                return SubtractLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> MultiplyLazy { get; }

        public static Func<T, T, T> Multiply
        {
            get
            {
                return MultiplyLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> DivideLazy { get; }

        public static Func<T, T, T> Divide
        {
            get
            {
                return DivideLazy.Value;
            }
        }

        private static Lazy<Func<T, T, T>> ModuloLazy { get; }

        public static Func<T, T, T> Modulo
        {
            get
            {
                return ModuloLazy.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> EqualsLazy { get; }

        public new static Func<T, T, Boolean> Equals
        {
            get
            {
                return EqualsLazy.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> NotEqualsLazy { get; }

        public static Func<T, T, Boolean> NotEquals
        {
            get
            {
                return NotEqualsLazy.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> LessLazy { get; }

        public static Func<T, T, Boolean> Less
        {
            get
            {
                return LessLazy.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> LessOrEqualsLazy { get; }

        public static Func<T, T, Boolean> LessOrEquals
        {
            get
            {
                return LessOrEqualsLazy.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> GreaterLazy { get; }

        public static Func<T, T, Boolean> Greater
        {
            get
            {
                return GreaterLazy.Value;
            }
        }
        
        private static Lazy<Func<T, T, Boolean>> GreaterOrEqualsLazy { get; }

        public static Func<T, T, Boolean> GreaterOrEquals
        {
            get
            {
                return GreaterOrEqualsLazy.Value;
            }
        }
    }
}