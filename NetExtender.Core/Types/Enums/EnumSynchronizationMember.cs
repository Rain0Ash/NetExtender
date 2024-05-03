// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Enums.Attributes;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Enums
{
    public sealed record EnumSynchronizationMember<T, TEnum> : EnumSynchronizationMember, IReadOnlyList<TEnum> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public static implicit operator ImmutableArray<TEnum>(EnumSynchronizationMember<T, TEnum>? value)
        {
            return value?.Values ?? ImmutableArray<TEnum>.Empty;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator EnumSynchronizationMember<T>?(EnumSynchronizationMember<T, TEnum>? value)
        {
            return value is not null ? new EnumSynchronizationMember<T>(value.Attribute, value.Values.CastArray<Enum<T>>()) : null;
        }

        public override Type Type
        {
            get
            {
                return typeof(TEnum);
            }
        }

        public override Type Underlying
        {
            get
            {
                return typeof(T);
            }
        }

        public ImmutableArray<TEnum> Values { get; }

        public override ImmutableArray<IEnum> Array
        {
            get
            {
                return Values.CastArray<IEnum>();
            }
        }

        public override Int32 Count
        {
            get
            {
                return Values.Length;
            }
        }

        public EnumSynchronizationMember(IEnumerable values)
            : base(Initialize())
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.OfType<TEnum>().ToImmutableArray();
        }

        public EnumSynchronizationMember(params TEnum[] values)
            : this((IEnumerable<Enum<T>>) values)
        {
        }

        public EnumSynchronizationMember(IEnumerable<TEnum> values)
            : base(Initialize())
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.ToImmutableArray();
        }

        public EnumSynchronizationMember(ImmutableArray<TEnum> values)
            : base(Initialize())
        {
            Values = values;
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, IEnumerable values)
            : base(attribute)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.OfType<TEnum>().ToImmutableArray();
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, params TEnum[] values)
            : this(attribute, (IEnumerable<Enum<T>>) values)
        {
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, IEnumerable<TEnum> values)
            : base(attribute)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.ToImmutableArray();
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, ImmutableArray<TEnum> values)
            : base(attribute)
        {
            Values = values;
        }

        private static EnumSynchronizeAttribute Initialize()
        {
            return typeof(TEnum).GetCustomAttribute<EnumSynchronizeAttribute>() ?? throw new NotSupportedException($"Type '{typeof(TEnum).Name}' don't have '{nameof(EnumSynchronizeAttribute)}' attribute.");
        }

        public new ImmutableArray<TEnum>.Enumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator<TEnum> IEnumerable<TEnum>.GetEnumerator()
        {
            return ((IEnumerable<TEnum>) Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TEnum>) this).GetEnumerator();
        }

        public override TEnum this[Int32 index]
        {
            get
            {
                return Values[index];
            }
        }
    }
    
    public sealed record EnumSynchronizationMember<T> : EnumSynchronizationMember, IReadOnlyList<Enum<T>> where T : unmanaged, Enum
    {
        public static implicit operator ImmutableArray<Enum<T>>(EnumSynchronizationMember<T>? value)
        {
            return value?.Values ?? ImmutableArray<Enum<T>>.Empty;
        }

        public override Type Type
        {
            get
            {
                return typeof(Enum<T>);
            }
        }

        public override Type Underlying
        {
            get
            {
                return typeof(T);
            }
        }

        public ImmutableArray<Enum<T>> Values { get; }

        public override ImmutableArray<IEnum> Array
        {
            get
            {
                return Values.CastArray<IEnum>();
            }
        }

        public override Int32 Count
        {
            get
            {
                return Values.Length;
            }
        }

        public EnumSynchronizationMember(IEnumerable values)
            : base(Initialize())
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.OfType<Enum<T>>().ToImmutableArray();
        }

        public EnumSynchronizationMember(params Enum<T>[] values)
            : this((IEnumerable<Enum<T>>) values)
        {
        }

        public EnumSynchronizationMember(IEnumerable<Enum<T>> values)
            : base(Initialize())
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.ToImmutableArray();
        }

        public EnumSynchronizationMember(ImmutableArray<Enum<T>> values)
            : base(Initialize())
        {
            Values = values;
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, IEnumerable values)
            : base(attribute)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.OfType<Enum<T>>().ToImmutableArray();
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, params Enum<T>[] values)
            : this(attribute, (IEnumerable<Enum<T>>) values)
        {
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, IEnumerable<Enum<T>> values)
            : base(attribute)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Values = values.ToImmutableArray();
        }

        public EnumSynchronizationMember(EnumSynchronizeAttribute attribute, ImmutableArray<Enum<T>> values)
            : base(attribute)
        {
            Values = values;
        }

        private static EnumSynchronizeAttribute Initialize()
        {
            return typeof(T).GetCustomAttribute<EnumSynchronizeAttribute>() ?? throw new NotSupportedException($"Type '{typeof(T).Name}' don't have '{nameof(EnumSynchronizeAttribute)}' attribute.");
        }

        public new ImmutableArray<Enum<T>>.Enumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator<Enum<T>> IEnumerable<Enum<T>>.GetEnumerator()
        {
            return ((IEnumerable<Enum<T>>) Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Enum<T>>) this).GetEnumerator();
        }

        public override Enum<T> this[Int32 index]
        {
            get
            {
                return Values[index];
            }
        }
    }
    
    public abstract record EnumSynchronizationMember
    {
        public static implicit operator ImmutableArray<IEnum>(EnumSynchronizationMember? value)
        {
            return value?.Array ?? ImmutableArray<IEnum>.Empty;
        }
        
        public abstract Type Type { get; }
        public abstract Type Underlying { get; }
        
        public EnumSynchronizeAttribute Attribute { get; }
        public abstract ImmutableArray<IEnum> Array { get; }

        public abstract Int32 Count { get; }

        public virtual String Name
        {
            get
            {
                return Attribute.Name ?? Underlying.Name;
            }
        }

        public Int32 Order
        {
            get
            {
                return Attribute.Order;
            }
        }
        
        protected EnumSynchronizationMember(EnumSynchronizeAttribute attribute)
        {
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }
        
        public ImmutableArray<IEnum>.Enumerator GetEnumerator()
        {
            return Array.GetEnumerator();
        }

        public virtual IEnum this[Int32 index]
        {
            get
            {
                return Array[index];
            }
        }
    }
}