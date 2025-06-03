// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class EquatableAttribute : EquatableComparisonAttribute
    {
        public EquatableAttribute()
        {
        }
        
        public EquatableAttribute(Int32 order)
            : base(order)
        {
        }

        public EquatableAttribute(String? name)
            : base(name)
        {
        }

        public EquatableAttribute(String? name, Int32 order)
            : base(name, order)
        {
        }

        public EquatableAttribute(Type? type)
            : base(type)
        {
        }

        public EquatableAttribute(Type? type, Int32 order)
            : base(type, order)
        {
        }

        public EquatableAttribute(Type? type, String? name)
            : base(type, name)
        {
        }

        public EquatableAttribute(Type? type, String? name, Int32 order)
            : base(type, name, order)
        {
        }
    }

    public class EquatableAttribute<T> : ComparisonAttribute<T, EquatableAttribute<T>.EqualsDelegate>
    {
        public delegate Boolean EqualsDelegate(T? x, T? y);
        private static EquatableAttribute<T> Instance { get; } = new EquatableAttribute<T>();

        protected EquatableAttribute()
        {
        }

        public override Expression<EqualsDelegate> Build(String? name)
        {
            return Build<EquatableAttribute>(name);
        }

        protected virtual Expression<EqualsDelegate> Build<TAttribute>(String? name) where TAttribute : EquatableAttribute
        {
            ParameterExpression x = Expression.Parameter(typeof(T), nameof(x));
            ParameterExpression y = Expression.Parameter(typeof(T), nameof(y));
            ImmutableArray<Member<TAttribute>> members = Members<TAttribute>(name);
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            
            var methods = new
            {
                String = typeof(TAttribute).GetMethod(nameof(EquatableAttribute.Equals), binding, new[] { typeof(String), typeof(String) }),
                Generic = typeof(TAttribute).GetMethod(nameof(EquatableAttribute.Equals), 1, binding, new[] { Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(0) }),
            };

            if (methods.String is null || methods.Generic is null)
            {
                throw new InvalidOperationException($"Can't find method {nameof(EquatableAttribute.Equals)} for {typeof(TAttribute).Name}.");
            }

            Expression body = Expression.Constant(true);

            for (Int32 index = members.Length - 1; index >= 0; index--)
            {
                Member<TAttribute> member = members[index];
                if (member.MemberInfo.MemberType is not (MemberTypes.Field or MemberTypes.Property) || member.Type is not { } type || member.Attribute is not { } attribute)
                {
                    continue;
                }

                Expression accessX = Expression.MakeMemberAccess(x, member.MemberInfo);
                Expression accessY = Expression.MakeMemberAccess(y, member.MemberInfo);

                MethodInfo? method = type == typeof(String) ? methods.String : methods.Generic.MakeGenericMethod(type);
                Expression call = Expression.Call(Expression.Constant(attribute), method, accessX, accessY);
                body = Expression.AndAlso(body, call);
            }

            BinaryExpression @null = Expression.OrElse(Expression.ReferenceEqual(x, Expression.Constant(null)), Expression.ReferenceEqual(y, Expression.Constant(null)));
            body = Expression.Condition(@null, Expression.ReferenceEqual(x, y), body);

            return Expression.Lambda<EqualsDelegate>(body, x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Boolean Invoke(T? x, T? y)
        {
            return Get().Invoke(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Boolean Invoke(String? name, T? x, T? y)
        {
            return Get(name).Invoke(x, y);
        }

        public static Boolean Equals(T? x, T? y)
        {
            return Instance.Invoke(x, y);
        }

        public static Boolean Equals(String? name, T? x, T? y)
        {
            return Instance.Invoke(name, x, y);
        }
    }
}