using System;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class IgnoreAutoNameAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class IgnoreParentAutoNameAttribute : Attribute
    {
        public Byte Count { get; }
        public Type? Parent { get; }

        public IgnoreParentAutoNameAttribute()
            : this(1)
        {
        }

        public IgnoreParentAutoNameAttribute(Byte count)
        {
            Count = count > 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        public IgnoreParentAutoNameAttribute(Type type)
        {
            Parent = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}