// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Utilities.Core
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ReadOnlyAttribute : Attribute, IEquatable<ReadOnlyAttribute>, IEquatable<System.ComponentModel.ReadOnlyAttribute>
    {
        public static implicit operator Boolean(ReadOnlyAttribute? value)
        {
            return value?.IsReadOnly ?? false;
        }
        
        public static implicit operator Boolean?(ReadOnlyAttribute? value)
        {
            return value?.State;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator System.ComponentModel.ReadOnlyAttribute?(ReadOnlyAttribute? value)
        {
            return value is not null ? value.State switch
            {
                null => System.ComponentModel.ReadOnlyAttribute.Default,
                true => System.ComponentModel.ReadOnlyAttribute.Yes,
                false => System.ComponentModel.ReadOnlyAttribute.No
            } : null;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator ReadOnlyAttribute?(System.ComponentModel.ReadOnlyAttribute? value)
        {
            return value?.IsReadOnly switch
            {
                null => null,
                true => Yes,
                false => No
            };
        }

        public static ReadOnlyAttribute Default { get; } = new ReadOnlyAttribute(null);
        public static ReadOnlyAttribute Yes { get; } = new ReadOnlyAttribute(true);
        public static ReadOnlyAttribute No { get; } = new ReadOnlyAttribute(false);

        public Boolean? State { get; }

        public Boolean IsReadOnly
        {
            get
            {
                return State is not false;
            }
        }

        public ReadOnlyAttribute()
            : this(null)
        {
        }

        public ReadOnlyAttribute(Boolean @readonly)
        {
            State = @readonly;
        }

        public ReadOnlyAttribute(Boolean? @readonly)
        {
            State = @readonly;
        }

        public override Boolean IsDefaultAttribute()
        {
            return State == Default.State;
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                ReadOnlyAttribute attribute => Equals(attribute),
                System.ComponentModel.ReadOnlyAttribute attribute => Equals(attribute),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlyAttribute? other)
        {
            return other is not null && State == other.State;
        }

        public Boolean Equals(System.ComponentModel.ReadOnlyAttribute? other)
        {
            return other is not null && IsReadOnly == other.IsReadOnly;
        }
    }
}