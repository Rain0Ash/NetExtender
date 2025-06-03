// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            : this(null, name)
        {
        }

        public EquatableComparisonAttribute(String? name, Int32 order)
            : this(null, name, order)
        {
        }

        public EquatableComparisonAttribute(Type? type)
            : this(type, null)
        {
        }

        public EquatableComparisonAttribute(Type? type, Int32 order)
            : this(type, null, order)
        {
        }

        public EquatableComparisonAttribute(Type? type, String? name)
            : base(type, name)
        {
        }

        public EquatableComparisonAttribute(Type? type, String? name, Int32 order)
            : base(type, name, order)
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