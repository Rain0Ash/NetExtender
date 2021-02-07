// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Types.Strings
{
    public abstract class StringBase : ReactiveObject, IString
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "SpecifyACultureInStringConversionExplicitly")]
        public static explicit operator String(StringBase value)
        {
            return value.ToString();
        }
        
        public virtual Boolean Immutable
        {
            get
            {
                return true;
            }
        }
        
        public virtual Boolean Constant
        {
            get
            {
                return true;
            }
        }

        public virtual Int32 Length
        {
            get
            {
                return Text?.Length ?? 0;
            }
        }

        [Reactive]
        public virtual String Text { get; protected set; }

        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) || ToString().Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return ToString().GetHashCode();
        }

        [JetBrains.Annotations.NotNull]
        public override String ToString()
        {
            return Text ?? String.Empty;
        }

        [JetBrains.Annotations.NotNull]
        public virtual String ToString(IFormatProvider? provider)
        {
            return ToString();
        }
    }
}