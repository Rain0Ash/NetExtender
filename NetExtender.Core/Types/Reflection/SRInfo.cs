using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public sealed class SRInfo : IEquatable<SRInfo>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(SRInfo? value)
        {
            return value?.Value;
        }
        
        public String Name { get; }
        public Type SR { get; }
        
        public Assembly Assembly
        {
            get
            {
                return SR.Assembly;
            }
        }
        
        private readonly Func<String>? _handler;
        private Func<String>? Handler
        {
            get
            {
                return _handler;
            }
        }
        
        public String Value
        {
            get
            {
                return Handler is not null ? Handler.Invoke() : Name;
            }
        }
        
        public SRInfo(Type type, [CallerMemberName][NotNull] String? name = null)
        {
            SR = type ?? throw new ArgumentNullException(nameof(type));
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            Name = name;
            SRUtilities.TryGet(type, name, out _handler);
        }
        
        public SRInfo(Assembly assembly, [CallerMemberName][NotNull] String? name = null)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            SR = SRUtilities.SRType(assembly);
            Name = name;
            SRUtilities.TryGet(SR, Name, out _handler);
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Name, SR);
        }
        
        public override Boolean Equals(Object? other)
        {
            return Equals(other as SRInfo);
        }
        
        public Boolean Equals(SRInfo? other)
        {
            return ReferenceEquals(this, other) || other is not null && Name == other.Name && SR == other.SR;
        }
        
        public override String ToString()
        {
            return $"{SR.FullName}.{Name}";
        }
    }
}