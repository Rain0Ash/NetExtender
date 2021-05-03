// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace NetExtender.Comparers.Enum
{
    public struct EnumEqualityComparer<TEnum> : IEqualityComparer<TEnum> where TEnum : System.Enum
    {
        private static class BoxAvoidance
        {
            private static readonly Func<TEnum, Int32> Wrapper;

            static BoxAvoidance()
            {
                ParameterExpression p = Expression.Parameter(typeof(TEnum), null);
                UnaryExpression c = Expression.ConvertChecked(p, typeof(Int32));

                Wrapper = Expression.Lambda<Func<TEnum, Int32>>(c, p).Compile();
            }

            public static Int32 ToInt(TEnum @enum)
            {
                return Wrapper(@enum);
            }
        }

        public Boolean Equals(TEnum firstEnum, TEnum secondEnum)
        {
            return BoxAvoidance.ToInt(firstEnum) == BoxAvoidance.ToInt(secondEnum);
        }

        public Int32 GetHashCode(TEnum firstEnum)
        {
            return BoxAvoidance.ToInt(firstEnum);
        }
    }

    public struct EnumEqualityComparer<TEnum, TInt> : IEqualityComparer<TEnum> where TEnum : System.Enum
        where TInt : unmanaged, IConvertible, IEquatable<TInt>
    {
        private static class BoxAvoidance
        {
            private static readonly Func<TEnum, TInt> Wrapper;

            static BoxAvoidance()
            {
                ParameterExpression p = Expression.Parameter(typeof(TEnum), null);
                UnaryExpression c = Expression.ConvertChecked(p, typeof(TInt));

                Wrapper = Expression.Lambda<Func<TEnum, TInt>>(c, p).Compile();
            }

            public static TInt ToInt(TEnum @enum)
            {
                return Wrapper(@enum);
            }
        }

        public Boolean Equals(TEnum first, TEnum second)
        {
            return BoxAvoidance.ToInt(first).Equals(BoxAvoidance.ToInt(second));
        }

        public Int32 GetHashCode(TEnum value)
        {
            return BoxAvoidance.ToInt(value).ToInt32(CultureInfo.InvariantCulture);
        }
    }
}