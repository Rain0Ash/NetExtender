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

        protected ComparisonAttribute(String? name, Type? type)
            : base(name, type)
        {
        }

        protected ComparisonAttribute(String? name, Type? type, Int32 order)
            : base(name, type, order)
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