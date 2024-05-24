using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
    public abstract class DelegateAttribute : Attribute
    {
        public Type? Type { get; }
        public String? Name { get; }
        public Int32 Order { get; init; } = Int32.MinValue;

        protected DelegateAttribute(String? name, Type? type)
        {
            Name = name;
            Type = type;
        }
    }
    
    public abstract class DelegateAttribute<T, TDelegate> where TDelegate : Delegate
    {
        protected static ConcurrentDictionary<String, TDelegate> Actions { get; } = new ConcurrentDictionary<String, TDelegate>();

        private TDelegate? _delegate;
        private TDelegate Delegate
        {
            get
            {
                return _delegate ??= Compile();
            }
        }

        protected TDelegate Get()
        {
            return Get(null);
        }

        protected TDelegate Get(String? name)
        {
            return name is not null ? Actions.GetOrAdd(name, Compile) : Delegate;
        }

        protected virtual Member<TAttribute> Convert<TAttribute>(MemberInfo member, Int32 order) where TAttribute : ComparisonAttribute
        {
            return new Member<TAttribute>(member) { Order = order };
        }

        protected virtual ImmutableArray<Member<TAttribute>> Members<TAttribute>(String? name) where TAttribute : ComparisonAttribute
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            Boolean Predicate(Member<TAttribute> member)
            {
                return !member.IsEmpty && String.Equals(member.Name, name, StringComparison.Ordinal);
            }

            return typeof(T).GetMembers(binding).Select(Convert<TAttribute>).Where(Predicate).OrderBy().ToImmutableArray();
        }

        public TDelegate Compile()
        {
            return Build().Compile();
        }

        public TDelegate Compile(String? name)
        {
            return Build(name).Compile();
        }

        public virtual Expression<TDelegate> Build()
        {
            return Build(null);
        }
        
        public abstract Expression<TDelegate> Build(String? name);

        protected readonly struct Member<TAttribute> : IEquatable<Member<TAttribute>>, IComparable<Member<TAttribute>> where TAttribute : ComparisonAttribute
        {
            public MemberInfo MemberInfo { get; }
            public TAttribute? Attribute { get; }

            public Type? Type
            {
                get
                {
                    return MemberInfo switch
                    {
                        FieldInfo info => info.FieldType,
                        PropertyInfo info => info.PropertyType,
                        _ => null
                    };
                }
            }

            public String? Name
            {
                get
                {
                    return Attribute?.Name;
                }
            }

            private readonly Int32? _order;

            public Int32 Order
            {
                get
                {
                    return Attribute?.Order switch
                    {
                        null => _order ?? Int32.MaxValue,
                        Int32.MinValue => _order ?? Int32.MaxValue,
                        _ => Attribute.Order
                    };
                }
                init
                {
                    if (Order == Int32.MaxValue)
                    {
                        _order = value;
                    }
                }
            }

            public Boolean IsEmpty
            {
                get
                {
                    return Attribute is null;
                }
            }

            public Member(MemberInfo info)
            {
                MemberInfo = info ?? throw new ArgumentNullException(nameof(info));
                Attribute = info.GetCustomAttribute<TAttribute>(true);
                _order = Attribute?.Order;
            }

            public Member(MemberInfo info, TAttribute? attribute)
            {
                MemberInfo = info ?? throw new ArgumentNullException(nameof(info));
                Attribute = attribute;
                _order = Attribute?.Order;
            }

            public Int32 CompareTo(Member<TAttribute> other)
            {
                Int32 current = Attribute is not null && Attribute.Order != Int32.MinValue ? Attribute.Order : Int32.MaxValue;
                Int32 another = other.Attribute is not null && other.Attribute.Order != Int32.MinValue ? other.Attribute.Order : Int32.MaxValue;

                if (current != another)
                {
                    return current.CompareTo(another);
                }

                current = _order ?? Int32.MaxValue;
                another = other._order ?? Int32.MaxValue;

                return current != another ? current.CompareTo(another) : String.Compare(MemberInfo.Name, other.MemberInfo.Name, StringComparison.Ordinal);
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(MemberInfo, Name, Order);
            }

            public override Boolean Equals(Object? other)
            {
                return other is Member<TAttribute> member && Equals(member);
            }

            public Boolean Equals(Member<TAttribute> other)
            {
                return MemberInfo == other.MemberInfo && Name == other.Name && Order == other.Order;
            }

            public override String ToString()
            {
                return $"Member<{typeof(TAttribute).Name}> {{ {nameof(MemberInfo)} = {MemberInfo.Name}, {nameof(Type)} = {Type?.Name}, {nameof(Name)} = {Name}, {nameof(Order)} = {Order} }}";
            }
        }
    }
}