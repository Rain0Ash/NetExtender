// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using System.Reflection;

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

        public static T Or<T>(T value1, T value2)
        {
            return MathGeneric<T>.Or(value1, value2);
        }

        public static T And<T>(T value1, T value2)
        {
            return MathGeneric<T>.And(value1, value2);
        }

        public static T Xor<T>(T value1, T value2)
        {
            return MathGeneric<T>.Xor(value1, value2);
        }

        public static T2 Convert<T1, T2>(T1 value)
        {
            return MathGeneric<T1, T2>.Convert(value);
        }

        public static T Add<T>(T value1, T value2)
        {
            return MathGeneric<T>.Add(value1, value2);
        }

        public static T1 AltAdd<T1, T2>(T1 value1, T2 value2)
        {
            return MathGeneric<T2, T1>.Add(value1, value2);
        }

        public static T Subtract<T>(T value1, T value2)
        {
            return MathGeneric<T>.Subtract(value1, value2);
        }

        public static T1 AltSubtract<T1, T2>(T1 value1, T2 value2)
        {
            return MathGeneric<T2, T1>.Subtract(value1, value2);
        }

        public static T Multiply<T>(T value1, T value2)
        {
            return MathGeneric<T>.Multiply(value1, value2);
        }

        public static T1 AltMultiply<T1, T2>(T1 value1, T2 value2)
        {
            return MathGeneric<T2, T1>.Multiply(value1, value2);
        }

        public static T Divide<T>(T value1, T value2)
        {
            return MathGeneric<T>.Divide(value1, value2);
        }

        public static T1 AltDivide<T1, T2>(T1 value1, T2 value2)
        {
            return MathGeneric<T2, T1>.Divide(value1, value2);
        }

        public static T Modulo<T>(T value1, T value2)
        {
            return MathGeneric<T>.Modulo(value1, value2);
        }

        public static T1 AltModulo<T1, T2>(T1 value1, T2 value2)
        {
            return MathGeneric<T2, T1>.Modulo(value1, value2);
        }

        public static Boolean Equal<T>(T value1, T value2)
        {
            return MathGeneric<T>.Equal(value1, value2);
        }

        public static Boolean NotEqual<T>(T value1, T value2)
        {
            return MathGeneric<T>.NotEqual(value1, value2);
        }

        public static Boolean Greater<T>(T value1, T value2)
        {
            return MathGeneric<T>.Greater(value1, value2);
        }

        public static Boolean GreaterEqual<T>(T value1, T value2)
        {
            return MathGeneric<T>.GreaterEqual(value1, value2);
        }

        public static Boolean LessThan<T>(T value1, T value2)
        {
            return MathGeneric<T>.Less(value1, value2);
        }

        public static Boolean LessEqual<T>(T value1, T value2)
        {
            return MathGeneric<T>.LessEqual(value1, value2);
        }

        public static T DivideInt32<T>(T value, Int32 divisor)
        {
            return MathGeneric<Int32, T>.Divide(value, divisor);
        }
    }

    public static class MathGeneric<TValue, TResult>
    {
        private static readonly Lazy<Func<TValue, TResult>> ConvertFunc;

        public static Func<TValue, TResult> Convert
        {
            get
            {
                return ConvertFunc.Value;
            }
        }

        static MathGeneric()
        {
            ConvertFunc = new Lazy<Func<TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TValue, TResult>(body => Expression.Convert(body, typeof(TResult))), true);
            AddFunc = new Lazy<Func<TResult, TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, TValue, TResult>(Expression.Add, true), true);
            SubtractFunc = new Lazy<Func<TResult, TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, TValue, TResult>(Expression.Subtract, true), true);
            MultiplyFunc = new Lazy<Func<TResult, TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, TValue, TResult>(Expression.Multiply, true), true);
            DivideFunc = new Lazy<Func<TResult, TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, TValue, TResult>(Expression.Divide, true), true);
            ModuloFunc = new Lazy<Func<TResult, TValue, TResult>>(() => ExpressionUtilities.CreateExpression<TResult, TValue, TResult>(Expression.Modulo, true), true);
        }

        private static readonly Lazy<Func<TResult, TValue, TResult>> AddFunc, SubtractFunc, MultiplyFunc, DivideFunc, ModuloFunc;

        public static Func<TResult, TValue, TResult> Add
        {
            get
            {
                return AddFunc.Value;
            }
        }

        public static Func<TResult, TValue, TResult> Subtract
        {
            get
            {
                return SubtractFunc.Value;
            }
        }

        public static Func<TResult, TValue, TResult> Multiply
        {
            get
            {
                return MultiplyFunc.Value;
            }
        }

        public static Func<TResult, TValue, TResult> Divide
        {
            get
            {
                return DivideFunc.Value;
            }
        }

        public static Func<TResult, TValue, TResult> Modulo
        {
            get
            {
                return ModuloFunc.Value;
            }
        }
    }

    public static class MathGeneric<T>
    {
        static MathGeneric()
        {
            Type type = typeof(T);

            if (type.GetTypeInfo().IsValueType && type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                throw new InvalidOperationException($"Generic math between {nameof(Nullable)} types is NotFunc implemented. Type: {typeof(T).FullName} is nullable.");
            }

            AddFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Add), true);
            SubtractFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Subtract), true);
            MultiplyFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Multiply), true);
            DivideFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Divide), true);
            ModuloFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Modulo), true);

            EqualFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.Equal), true);
            NotEqualFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.NotEqual), true);
            GreaterFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThan), true);
            GreaterEqualFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.GreaterThanOrEqual), true);
            LessFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThan), true);
            LessEqualFunc = new Lazy<Func<T, T, Boolean>>(() => ExpressionUtilities.CreateExpression<T, T, Boolean>(Expression.LessThanOrEqual), true);

            NegateFunc = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Negate), true);
            AndFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.And), true);
            OrFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.Or), true);
            NotFunc = new Lazy<Func<T, T>>(() => ExpressionUtilities.CreateExpression<T, T>(Expression.Not), true);
            XorFunc = new Lazy<Func<T, T, T>>(() => ExpressionUtilities.CreateExpression<T, T, T>(Expression.ExclusiveOr), true);
        }

        public static T? Zero
        {
            get
            {
                return default;
            }
        }

        private static readonly Lazy<Func<T, T>> NegateFunc, NotFunc;
        private static readonly Lazy<Func<T, T, T>> OrFunc, AndFunc, XorFunc;

        public static Func<T, T> Negate
        {
            get
            {
                return NegateFunc.Value;
            }
        }

        public static Func<T, T> Not
        {
            get
            {
                return NotFunc.Value;
            }
        }

        public static Func<T, T, T> Or
        {
            get
            {
                return OrFunc.Value;
            }
        }

        public static Func<T, T, T> And
        {
            get
            {
                return AndFunc.Value;
            }
        }

        public static Func<T, T, T> Xor
        {
            get
            {
                return XorFunc.Value;
            }
        }

        private static readonly Lazy<Func<T, T, T>> AddFunc, SubtractFunc, MultiplyFunc, DivideFunc, ModuloFunc;

        public static Func<T, T, T> Add
        {
            get
            {
                return AddFunc.Value;
            }
        }

        public static Func<T, T, T> Subtract
        {
            get
            {
                return SubtractFunc.Value;
            }
        }

        public static Func<T, T, T> Multiply
        {
            get
            {
                return MultiplyFunc.Value;
            }
        }

        public static Func<T, T, T> Divide
        {
            get
            {
                return DivideFunc.Value;
            }
        }

        public static Func<T, T, T> Modulo
        {
            get
            {
                return ModuloFunc.Value;
            }
        }

        private static readonly Lazy<Func<T, T, Boolean>> EqualFunc, NotEqualFunc, GreaterFunc, LessFunc, GreaterEqualFunc, LessEqualFunc;

        public static Func<T, T, Boolean> Equal
        {
            get
            {
                return EqualFunc.Value;
            }
        }

        public static Func<T, T, Boolean> NotEqual
        {
            get
            {
                return NotEqualFunc.Value;
            }
        }

        public static Func<T, T, Boolean> Greater
        {
            get
            {
                return GreaterFunc.Value;
            }
        }

        public static Func<T, T, Boolean> GreaterEqual
        {
            get
            {
                return GreaterEqualFunc.Value;
            }
        }

        public static Func<T, T, Boolean> Less
        {
            get
            {
                return LessFunc.Value;
            }
        }

        public static Func<T, T, Boolean> LessEqual
        {
            get
            {
                return LessEqualFunc.Value;
            }
        }
    }
}