using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    internal static class YieldEquatableAttribute
    {
        internal static ConstructorInfo Constructor { get; }
        
        static YieldEquatableAttribute()
        {
            Constructor = typeof(KeyValuePair<String, Type>).GetConstructor(new[] { typeof(String), typeof(Type) }) ?? throw new NeverOperationException();
        }
    }

    public class YieldEquatableAttribute<T> : ComparisonAttribute<T, YieldEquatableAttribute<T>.EqualsDelegate>
    {
        public delegate KeyValuePair<String, Type>[] EqualsDelegate(T? x, T? y);
        private static YieldEquatableAttribute<T> Instance { get; } = new YieldEquatableAttribute<T>();
        
        protected YieldEquatableAttribute()
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
            
            List<Expression> equality = new List<Expression>();

            foreach (Member<TAttribute> member in members)
            {
                if (member.MemberInfo.MemberType is not (MemberTypes.Field or MemberTypes.Property) || member.Type is not { } type || member.Attribute is not { } attribute)
                {
                    continue;
                }
                
                Expression accessX = Expression.MakeMemberAccess(x, member.MemberInfo);
                Expression accessY = Expression.MakeMemberAccess(y, member.MemberInfo);
                
                MethodInfo? method = type == typeof(String) ? methods.String : methods.Generic.MakeGenericMethod(type);
                Expression call = Expression.Call(Expression.Constant(attribute), method, accessX, accessY);
                
                NewExpression @new = Expression.New(YieldEquatableAttribute.Constructor, Expression.Constant(member.MemberInfo.Name), Expression.Constant(type));
                equality.Add(Expression.Condition(Expression.Not(call), @new, Expression.Constant(default(KeyValuePair<String, Type>))));
            }
            
            MethodInfo notnull = typeof(ArrayUtilities).GetMethod(nameof(ArrayUtilities.NotDefault), BindingFlags.Public | BindingFlags.Static, new[] { Type.MakeGenericMethodParameter(0).MakeArrayType() })?.MakeGenericMethod(typeof(KeyValuePair<String, Type>)) ?? throw new MissingMethodException(nameof(ArrayUtilities), nameof(ArrayUtilities.NotDefault));
            
            ConditionalExpression result = Expression.Condition(
                Expression.AndAlso(Expression.ReferenceEqual(x, Expression.Constant(null)), Expression.ReferenceEqual(y, Expression.Constant(null))),
                Expression.Constant(Array.Empty<KeyValuePair<String, Type>>()),
                Expression.Condition(
                    Expression.OrElse(Expression.ReferenceEqual(x, Expression.Constant(null)), Expression.ReferenceEqual(y, Expression.Constant(null))),
                    Expression.NewArrayInit(typeof(KeyValuePair<String, Type>), members.Select(static member => Expression.New(YieldEquatableAttribute.Constructor, Expression.Constant(member.MemberInfo.Name), Expression.Constant(member.Type)))),
                    Expression.Call(null, notnull, Expression.NewArrayInit(typeof(KeyValuePair<String, Type>), equality))
                )
            );

            return Expression.Lambda<EqualsDelegate>(result, x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private KeyValuePair<String, Type>[] Invoke(T? x, T? y)
        {
            return Get().Invoke(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private KeyValuePair<String, Type>[] Invoke(String? name, T? x, T? y)
        {
            return Get(name).Invoke(x, y);
        }

        public static KeyValuePair<String, Type>[] Equals(T? x, T? y)
        {
            return Instance.Invoke(x, y);
        }

        public static KeyValuePair<String, Type>[] Equals(String? name, T? x, T? y)
        {
            return Instance.Invoke(name, x, y);
        }
    }
}