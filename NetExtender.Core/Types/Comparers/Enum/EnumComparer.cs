// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace NetExtender.Types.Comparers
{
    public readonly struct EnumEqualityComparer<TEnum> : IEqualityComparer<TEnum> where TEnum : Enum
    {
        private static class BoxAvoidance
        {
            private static readonly Func<TEnum, Int32> Wrapper;

            static BoxAvoidance()
            {
                ParameterExpression parameter = Expression.Parameter(typeof(TEnum), null);
                UnaryExpression unary = Expression.ConvertChecked(parameter, typeof(Int32));

                Wrapper = Expression.Lambda<Func<TEnum, Int32>>(unary, parameter).Compile();
            }

            public static Int32 ToInt(TEnum value)
            {
                return Wrapper(value);
            }
        }

        public Boolean Equals(TEnum? first, TEnum? second)
        {
            if (first is null)
            {
                return second is null;
            }

            return second is not null && BoxAvoidance.ToInt(first) == BoxAvoidance.ToInt(second);
        }

        public Int32 GetHashCode(TEnum value)
        {
            return BoxAvoidance.ToInt(value);
        }
    }

    public readonly struct EnumEqualityComparer<TEnum, T> : IEqualityComparer<TEnum> where TEnum : Enum where T : unmanaged, IConvertible, IEquatable<T>
    {
        private static class BoxAvoidance
        {
            private static readonly Func<TEnum, T> Wrapper;

            static BoxAvoidance()
            {
                ParameterExpression parameter = Expression.Parameter(typeof(TEnum), null);
                UnaryExpression unary = Expression.ConvertChecked(parameter, typeof(T));

                Wrapper = Expression.Lambda<Func<TEnum, T>>(unary, parameter).Compile();
            }

            public static T ToInt(TEnum value)
            {
                return Wrapper(value);
            }
        }

        public Boolean Equals(TEnum? first, TEnum? second)
        {
            if (first is null)
            {
                return second is null;
            }

            return second is not null && BoxAvoidance.ToInt(first).Equals(BoxAvoidance.ToInt(second));
        }

        public Int32 GetHashCode(TEnum value)
        {
            return BoxAvoidance.ToInt(value).ToInt32(CultureInfo.InvariantCulture);
        }
    }
}