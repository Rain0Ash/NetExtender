// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NetExtender.Utilities.Types
{
    public static class ExpressionUtilities
    {
        public static PropertyInfo? GetPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                return null;
            }

            if (member.Member is not PropertyInfo info)
            {
                return null;
            }

            Type? reflection = info.ReflectedType;
            
            if (reflection is null)
            {
                return null;
            }

            Type type = typeof(T);
            if (type != reflection && !type.IsSubclassOf(reflection))
            {
                return null;
            }

            return info;
        }
    }
}