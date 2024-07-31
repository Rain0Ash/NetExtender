using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class EquatableComparisonAttribute : ComparisonAttribute
    {
        public EquatableComparisonAttribute()
            : this(null, null)
        {
        }

        public EquatableComparisonAttribute(Int32 order)
            : this(null, null, order)
        {
        }

        public EquatableComparisonAttribute(String? name)
            : this(name, null)
        {
        }

        public EquatableComparisonAttribute(String? name, Int32 order)
            : this(name, null, order)
        {
        }

        public EquatableComparisonAttribute(Type? type)
            : this(null, type)
        {
        }

        public EquatableComparisonAttribute(Type? type, Int32 order)
            : this(null, type, order)
        {
        }

        public EquatableComparisonAttribute(String? name, Type? type)
            : base(name, type)
        {
        }

        public EquatableComparisonAttribute(String? name, Type? type, Int32 order)
            : base(name, type, order)
        {
        }

        public virtual Boolean Equals<T>(T? x, T? y)
        {
            return typeof(T) == typeof(String) ? Equals(Unsafe.As<T?, String?>(ref x), Unsafe.As<T?, String?>(ref y)) : EqualityComparer<T>.Default.Equals(x, y);
        }
        
        public Boolean Equals(String? x, String? y)
        {
            return Equals(x, y, Comparison);
        }
        
        public virtual Boolean Equals(String? x, String? y, StringComparison comparison)
        {
            return String.Equals(x, y, comparison);
        }
    }
}