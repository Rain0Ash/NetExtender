// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;

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
            return MathGeneric<T>.Equal(first, second);
        }

        public static Boolean NotEqual<T>(T first, T second)
        {
            return MathGeneric<T>.NotEqual(first, second);
        }

        public static Boolean Greater<T>(T first, T second)
        {
            return MathGeneric<T>.Greater(first, second);
        }

        public static Boolean GreaterEqual<T>(T first, T second)
        {
            return MathGeneric<T>.GreaterEqual(first, second);
        }

        public static Boolean LessThan<T>(T first, T second)
        {
            return MathGeneric<T>.Less(first, second);
        }

        public static Boolean LessEqual<T>(T first, T second)
        {
            return MathGeneric<T>.LessEqual(first, second);
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
            ConvertInternal = new Lazy<Func<T, TResult>>(() => ExpressionUtilities.CreateExpression<T, TResult>(body => Expression.Convert(body, typeof(TResult))), true);
            AddInternal = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Add, true), true);
            SubtractInternal = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Subtract, true), true);
            MultiplyInternal = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Multiply, true), true);
            DivideInternal = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Divide, true), true);
            ModuloInternal = new Lazy<Func<TResult, T, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, T, TResult>(Expression.Modulo, true), true);
        }

        private static Lazy<Func<T, TResult>> ConvertInternal { get; }

        public static Func<T, TResult> Convert
        {
            get
            {
                return ConvertInternal.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> AddInternal { get; }

        public static Func<TResult, T, TResult> Add
        {
            get
            {
                return AddInternal.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> SubtractInternal { get; }

        public static Func<TResult, T, TResult> Subtract
        {
            get
            {
                return SubtractInternal.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> MultiplyInternal { get; }

        public static Func<TResult, T, TResult> Multiply
        {
            get
            {
                return MultiplyInternal.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> DivideInternal { get; }

        public static Func<TResult, T, TResult> Divide
        {
            get
            {
                return DivideInternal.Value;
            }
        }

        private static Lazy<Func<TResult, T, TResult>> ModuloInternal { get; }

        public static Func<TResult, T, TResult> Modulo
        {
            get
            {
                return ModuloInternal.Value;
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

            AddInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Add), true);
            SubtractInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Subtract), true);
            MultiplyInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Multiply), true);
            DivideInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Divide), true);
            ModuloInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Modulo), true);

            EqualInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.Equal), true);
            NotEqualInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.NotEqual), true);
            GreaterInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThan), true);
            GreaterEqualInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThanOrEqual), true);
            LessInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThan), true);
            LessEqualInternal = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThanOrEqual), true);

            NegateInternal = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Negate), true);
            AndInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.And), true);
            OrInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Or), true);
            NotInternal = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Not), true);
            XorInternal = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.ExclusiveOr), true);
        }

        public static T? Zero
        {
            get
            {
                return default;
            }
        }

        private static Lazy<Func<T, T>> NegateInternal { get; }

        public static Func<T, T> Negate
        {
            get
            {
                return NegateInternal.Value;
            }
        }

        private static Lazy<Func<T, T>> NotInternal { get; }

        public static Func<T, T> Not
        {
            get
            {
                return NotInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> OrInternal { get; }

        public static Func<T, T, T> Or
        {
            get
            {
                return OrInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> AndInternal { get; }

        public static Func<T, T, T> And
        {
            get
            {
                return AndInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> XorInternal { get; }

        public static Func<T, T, T> Xor
        {
            get
            {
                return XorInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> AddInternal { get; }

        public static Func<T, T, T> Add
        {
            get
            {
                return AddInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> SubtractInternal { get; }

        public static Func<T, T, T> Subtract
        {
            get
            {
                return SubtractInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> MultiplyInternal { get; }

        public static Func<T, T, T> Multiply
        {
            get
            {
                return MultiplyInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> DivideInternal { get; }

        public static Func<T, T, T> Divide
        {
            get
            {
                return DivideInternal.Value;
            }
        }

        private static Lazy<Func<T, T, T>> ModuloInternal { get; }

        public static Func<T, T, T> Modulo
        {
            get
            {
                return ModuloInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> EqualInternal { get; }

        public static Func<T, T, Boolean> Equal
        {
            get
            {
                return EqualInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> NotEqualInternal { get; }

        public static Func<T, T, Boolean> NotEqual
        {
            get
            {
                return NotEqualInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> GreaterInternal { get; }

        public static Func<T, T, Boolean> Greater
        {
            get
            {
                return GreaterInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> LessInternal { get; }

        public static Func<T, T, Boolean> GreaterEqual
        {
            get
            {
                return GreaterEqualInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> GreaterEqualInternal { get; }

        public static Func<T, T, Boolean> Less
        {
            get
            {
                return LessInternal.Value;
            }
        }

        private static Lazy<Func<T, T, Boolean>> LessEqualInternal { get; }

        public static Func<T, T, Boolean> LessEqual
        {
            get
            {
                return LessEqualInternal.Value;
            }
        }
    }
}