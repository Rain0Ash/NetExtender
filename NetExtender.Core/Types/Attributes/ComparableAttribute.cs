// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ComparableAttribute : ComparisonAttribute
    {
        public Boolean Inverse { get; init; }
        
        public ComparableAttribute()
            : this(null, null)
        {
        }
        
        public ComparableAttribute(Int32 order)
            : this(null, null, order)
        {
        }

        public ComparableAttribute(String? name)
            : this(null, name)
        {
        }

        public ComparableAttribute(String? name, Int32 order)
            : this(null, name, order)
        {
        }

        public ComparableAttribute(Type? type)
            : this(type, null)
        {
        }

        public ComparableAttribute(Type? type, Int32 order)
            : this(type, null, order)
        {
        }

        public ComparableAttribute(Type? type, String? name)
            : base(type, name)
        {
        }

        public ComparableAttribute(Type? type, String? name, Int32 order)
            : base(type, name, order)
        {
        }

        public virtual Int32 Compare<T>(T? x, T? y)
        {
            Int32 result = typeof(T) == typeof(String) ? Compare(Unsafe.As<T?, String?>(ref x), Unsafe.As<T?, String?>(ref y), false) : Comparer<T>.Default.Compare(x, y);
            return Inverse ? -result : result;
        }
        
        public Int32 Compare(String? x, String? y)
        {
            return Compare(x, y, Comparison);
        }

        protected Int32 Compare(String? x, String? y, Boolean inverse)
        {
            return Compare(x, y, Comparison, inverse);
        }

        public Int32 Compare(String? x, String? y, StringComparison comparison)
        {
            return Compare(x, y, comparison, Inverse);
        }

        protected virtual Int32 Compare(String? x, String? y, StringComparison comparison, Boolean inverse)
        {
            Int32 result = String.Compare(x, y, comparison);
            return inverse ? -result : result;
        }
    }
    
    public class ComparableAttribute<T> : ComparisonAttribute<T, ComparableAttribute<T>.CompareDelegate>
    {
        public delegate Int32 CompareDelegate(T? x, T? y);
        private static ComparableAttribute<T> Instance { get; } = new ComparableAttribute<T>();

        protected ComparableAttribute()
        {
        }

        public override Expression<CompareDelegate> Build(String? name)
        {
            return Build<ComparableAttribute>(name);
        }
        
        //TODO:
        protected virtual Expression<CompareDelegate> Build<TAttribute>(String? name) where TAttribute : ComparableAttribute
        {
            ParameterExpression x = Expression.Parameter(typeof(T), nameof(x));
            ParameterExpression y = Expression.Parameter(typeof(T), nameof(y));
            ImmutableArray<Member<TAttribute>> members = Members<TAttribute>(name);
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            
            var methods = new
            {
                String = typeof(ComparableAttribute).GetMethod(nameof(ComparableAttribute.Compare), binding, new[] { typeof(String), typeof(String) }),
                Generic = typeof(ComparableAttribute).GetMethod(nameof(ComparableAttribute.Compare), 1, binding, new[] { Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(0) }),
            };

            if (methods.String is null || methods.Generic is null)
            {
                throw new InvalidOperationException($"Can't find method {nameof(ComparableAttribute.Compare)} for {typeof(TAttribute).Name}.");
            }

            Expression body = Expression.Constant(0, typeof(Int32));
            LabelTarget @return = Expression.Label(typeof(Int32));
            ParameterExpression result = Expression.Variable(typeof(Int32), nameof(result));

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
                
                Expression assign = Expression.Assign(result, call);

                Expression @if = Expression.IfThen(Expression.NotEqual(assign, Expression.Constant(0, typeof(Int32))), Expression.Return(@return, result));
                body = Expression.Block(@if, body);
            }
            
            body = Expression.Block
            (
                Expression.IfThen(Expression.ReferenceEqual(x, y), Expression.Return(@return, Expression.Constant(0, typeof(Int32)))),
                Expression.IfThen(Expression.ReferenceEqual(y, Expression.Constant(null)), Expression.Return(@return, Expression.Constant(1, typeof(Int32)))),
                Expression.IfThen(Expression.ReferenceEqual(x, Expression.Constant(null)), Expression.Return(@return, Expression.Constant(-1, typeof(Int32)))),
                Expression.Block(new [] { result }, body),
                Expression.Label(@return, Expression.Constant(0))
            );

            return Expression.Lambda<CompareDelegate>(body, x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Int32 Invoke(T? x, T? y)
        {
            return Get().Invoke(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Int32 Invoke(String? name, T? x, T? y)
        {
            return Get(name).Invoke(x, y);
        }

        public static Int32 Compare(T? x, T? y)
        {
            return Instance.Invoke(x, y);
        }

        public static Int32 Compare(String? name, T? x, T? y)
        {
            return Instance.Invoke(name, x, y);
        }
    }
}