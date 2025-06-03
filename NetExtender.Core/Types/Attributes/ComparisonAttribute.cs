// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public abstract class ComparisonAttribute : DelegateAttribute
    {
        //TODO:
        private Type? _comparer;
        public Type? ComparerType
        {
            get
            {
                return _comparer;
            }
            init
            {
                _comparer = value;
            }
        }

        public StringComparison Comparison { get; init; } = StringComparison.Ordinal;

        protected ComparisonAttribute(Type? type, String? name)
            : base(type, name)
        {
        }

        protected ComparisonAttribute(Type? type, String? name, Int32 order)
            : base(type, name, order)
        {
        }

        protected virtual TComparer? Create<TComparer>() where TComparer : class
        {
            return ComparerType is not null ? (TComparer?) Activator.CreateInstance(ComparerType) : null;
        }
    }

    public abstract class ComparisonAttribute<T, TDelegate> : DelegateAttribute<T, TDelegate> where TDelegate : Delegate
    {
    }
}