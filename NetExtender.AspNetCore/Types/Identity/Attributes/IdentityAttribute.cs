using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Enums.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore.Identity.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class NoIdentityAttribute : IdentityAttribute
    {
        public override Type? Type
        {
            get
            {
                return base.Type;
            }
            init
            {
                throw new NotSupportedException();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class AuthorizeIdentityAttribute : IdentityAttribute
    {
        public override Type? Type
        {
            get
            {
                return base.Type;
            }
            init
            {
                throw new NotSupportedException();
            }
        }

        public AuthorizeIdentityAttribute()
            : base(IdentityType.Any)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class AnyIdentityAttribute : IdentityAttribute
    {
        public override Type? Type
        {
            get
            {
                return base.Type;
            }
            init
            {
                throw new NotSupportedException();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class DenyIdentityAttribute : IdentityAttribute
    {
        public override Boolean IsStrict
        {
            get
            {
                return true;
            }
        }

        protected DenyIdentityAttribute(IdentityType? identity)
            : base(identity)
        {
        }
        
        protected DenyIdentityAttribute(UInt64 identity)
            : base(identity)
        {
        }

        public DenyIdentityAttribute(Enum? identity)
            : base(identity)
        {
        }

        public DenyIdentityAttribute(IEnum? identity)
            : base(identity)
        {
        }

        protected override Boolean IsAllow(IdentityType? identity)
        {
            return !base.IsAllow(identity);
        }

        protected override Boolean IsDeny(IdentityType? identity)
        {
            return !base.IsDeny(identity);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class IdentityAttribute : Attribute
    {
        [Flags]
        protected enum IdentityType : UInt64
        {
            Any = 0
        }

        private readonly Type? _type;
        public virtual Type? Type
        {
            get
            {
                return _type;
            }
            init
            {
                if (_type is { } type && type != value)
                {
                    throw new InvalidOperationException($"Can't set attribute type from '{type}' to '{value}'.");
                }
                
                _type = value;
            }
        }

        protected IdentityType? Identity { get; }

        public Boolean IsNoIdentity
        {
            get
            {
                return Identity is null;
            }
        }

        public Boolean IsAuthorize
        {
            get
            {
                return Identity is IdentityType.Any;
            }
        }

        public virtual Boolean IsStrict
        {
            get
            {
                return false;
            }
        }

        protected IdentityAttribute()
        {
        }

        protected IdentityAttribute(IdentityType? identity)
        {
            Identity = identity;
        }

        protected IdentityAttribute(UInt64 identity)
        {
            Identity = (IdentityType) identity;
        }

        public IdentityAttribute(Enum? identity)
        {
            _type = identity?.GetType();
            Identity = Convert(identity);
        }

        public IdentityAttribute(IEnum? identity)
        {
            _type = identity?.Id.GetType();
            Identity = Convert(identity);
        }

        protected static IdentityType? Convert(Enum? identity)
        {
            return identity?.GetTypeCode() switch
            {
                null => null,
                TypeCode.SByte => unchecked((IdentityType) identity.ToSByte()),
                TypeCode.Byte => (IdentityType) identity.ToByte(),
                TypeCode.Int16 => unchecked((IdentityType) identity.ToInt16()),
                TypeCode.UInt16 => (IdentityType) identity.ToUInt16(),
                TypeCode.Int32 => unchecked((IdentityType) identity.ToInt32()),
                TypeCode.UInt32 => (IdentityType) identity.ToUInt32(),
                TypeCode.Int64 => unchecked((IdentityType) identity.ToInt64()),
                TypeCode.UInt64 => (IdentityType) identity.ToUInt64(),
                { } type => throw new EnumUndefinedOrNotSupportedException<TypeCode>(type, nameof(identity), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IdentityType? Convert(IEnum? identity)
        {
            return Convert(identity?.Id);
        }

        protected IdentityType? Convert<T>(T? identity) where T : unmanaged, Enum
        {
            if (!IsAllow<T>())
            {
                throw new ArgumentException($"Argument '{identity}' must be equals '{Type}' for attribute '{GetType()}' with type '{typeof(T)}'.", nameof(identity));
            }
            
            return identity?.GetTypeCode() switch
            {
                null => null,
                TypeCode.SByte => unchecked((IdentityType) identity.ToSByte()),
                TypeCode.Byte => (IdentityType) identity.ToByte(),
                TypeCode.Int16 => unchecked((IdentityType) identity.ToInt16()),
                TypeCode.UInt16 => (IdentityType) identity.ToUInt16(),
                TypeCode.Int32 => unchecked((IdentityType) identity.ToInt32()),
                TypeCode.UInt32 => (IdentityType) identity.ToUInt32(),
                TypeCode.Int64 => unchecked((IdentityType) identity.ToInt64()),
                TypeCode.UInt64 => (IdentityType) identity.ToUInt64(),
                { } type => throw new EnumUndefinedOrNotSupportedException<TypeCode>(type, nameof(identity), null)
            };
        }

        protected Boolean TryConvert<T>(T? identity, out IdentityType? result) where T : unmanaged, Enum
        {
            if (!IsAllow<T>())
            {
                result = null;
                return false;
            }

            try
            {
                result = (IdentityType?) identity?.ToUInt64();
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public Boolean IsAllow<T>() where T : unmanaged, Enum
        {
            return Type is null || Type == typeof(T);
        }

        protected virtual Boolean IsAllow(IdentityType? identity)
        {
            return identity is { } type ? Identity?.HasFlag(type) is true : Identity is null;
        }

        public Boolean IsAllow<T>(T? identity) where T : unmanaged, Enum
        {
            return IsAllow(Convert(identity));
        }

        protected virtual Boolean IsDeny(IdentityType? identity)
        {
            return !IsAllow(identity);
        }

        public Boolean IsDeny<T>(T? identity) where T : unmanaged, Enum
        {
            return IsDeny(Convert(identity));
        }
    }
}